using Microsoft.EntityFrameworkCore;

namespace BLH.ApproveIQ.Domain.Repositories;

public interface IUnitOfWork
{
    DbContext DbContext { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}