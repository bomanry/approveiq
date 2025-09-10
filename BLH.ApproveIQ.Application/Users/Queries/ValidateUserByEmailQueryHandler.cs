using BLH.ApproveIQ.Application.Abstractions.Messaging;
using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Domain.Repositories;
using BLH.ApproveIQ.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace BLH.ApproveIQ.Application.Users.Queries;

public sealed record ValidateUserByEmailQuery(string Email) : IQuery<User?>;

internal sealed class ValidateUserByEmailQueryHandler(IGenericRepository<User> userRepository) : IQueryHandler<ValidateUserByEmailQuery, User?>
{
    public async Task<Result<User?>> Handle(ValidateUserByEmailQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
        {
            return Result.Success<User?>(null);
        }

        var user = await userRepository.GetQueryable()
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
        return Result.Success(user);
    }
}