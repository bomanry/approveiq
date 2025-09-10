using BLH.ApproveIQ.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BLH.ApproveIQ.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public DbContext DbContext => _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _dbContext.SaveChangesAsync(cancellationToken);
}
