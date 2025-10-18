using BLH.ApproveIQ.Application.Abstractions.Messaging;
using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Domain.Repositories;
using BLH.ApproveIQ.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace BLH.ApproveIQ.Application.Users.Queries;

public sealed record GetAllUsersQuery() : IQuery<List<User>>;

internal sealed class GetAllUsersQueryHandler(IGenericRepository<User> userRepository) : IQueryHandler<GetAllUsersQuery, List<User>>
{
    public async Task<Result<List<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetQueryable()
            .OrderBy(u => u.FirstName)
            .ThenBy(u => u.LastName)
            .ToListAsync(cancellationToken);
            
        return Result.Success(users);
    }
}