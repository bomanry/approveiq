using BLH.ApproveIQ.Application.Users.Queries;
using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BLH.ApproveIQ.Presentation.Controllers;

[Route("api/[controller]")]
public sealed class UsersController : ApiController
{
    public UsersController(ISender sender) : base(sender)
    {
    }

    [HttpPost("validate")]
    public async Task<IActionResult> ValidateUser([FromBody] ValidateUserRequest request, CancellationToken cancellationToken)
    {
        var query = new ValidateUserByEmailQuery(request.Email);
        var result = await Sender.Send(query, cancellationToken);
        
        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        var user = result.Value;
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        return Ok(new ValidateUserResponse
        {
            IsValid = true,
            User = new UserResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            }
        });
    }
}

public class ValidateUserRequest
{
    public string Email { get; set; } = string.Empty;
}

public class ValidateUserResponse
{
    public bool IsValid { get; set; }
    public UserResponse? User { get; set; }
}

public class UserResponse
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}