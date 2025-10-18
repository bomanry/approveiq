using BLH.ApproveIQ.Application.Abstractions.Messaging;
using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Domain.Repositories;
using BLH.ApproveIQ.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLH.ApproveIQ.Application.Auth.Commands;

public sealed record LoginCommand(string Email) : ICommand<LoginResponse>;

public sealed record LoginResponse(string Token, string Email, string FirstName, string LastName, Guid UserId);

internal sealed class LoginCommandHandler(
    IGenericRepository<User> userRepository,
    IConfiguration configuration) 
    : ICommandHandler<LoginCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
        {
            return Result.Failure<LoginResponse>(new Error("Auth.InvalidEmail", "Email is required"));
        }

        var user = await userRepository.GetQueryable()
            .FirstOrDefaultAsync(u => u.Email == request.Email && u.AccountEnabled == true, cancellationToken);

        if (user is null)
        {
            return Result.Failure<LoginResponse>(new Error("Auth.UserNotFound", "User not found or account disabled"));
        }

        var token = GenerateJwtToken(user);
        
        return Result.Success(new LoginResponse(token, user.Email, user.FirstName, user.LastName, user.Id));
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSecret = configuration["Jwt:Secret"] ?? "your-very-secure-secret-key-that-should-be-at-least-32-characters";
        var jwtIssuer = configuration["Jwt:Issuer"] ?? "ApproveIQ";
        var jwtAudience = configuration["Jwt:Audience"] ?? "ApproveIQ";
        
        var key = Encoding.ASCII.GetBytes(jwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("Role", user.Role ?? "User")
            }),
            Expires = DateTime.UtcNow.AddDays(7), // Token expires in 7 days
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = jwtIssuer,
            Audience = jwtAudience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}