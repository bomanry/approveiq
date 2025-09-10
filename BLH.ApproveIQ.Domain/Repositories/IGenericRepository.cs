using BLH.ApproveIQ.Domain.Primitives;

namespace BLH.ApproveIQ.Domain.Repositories;

public interface IGenericRepository<T> where T : Entity
{
    IQueryable<T> GetQueryable();
    Task<IEnumerable<T>> AllAsync(CancellationToken cancellationToken = default);
    IQueryable<T> AllQueryable();
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(T obj, CancellationToken cancellationToken = default);
    void Remove(T obj);
}
