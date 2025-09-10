using Microsoft.EntityFrameworkCore;
using BLH.ApproveIQ.Domain.Primitives;
using BLH.ApproveIQ.Domain.Repositories;

namespace BLH.ApproveIQ.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbContext.Set<T>().AsQueryable<T>();
        }

        public async Task<IEnumerable<T>> AllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().ToListAsync(cancellationToken);
        }

        public IQueryable<T> AllQueryable()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task AddAsync(T obj, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<T>().AddAsync(obj, cancellationToken);
        }

        public void Remove(T obj)
        {
            _dbContext.Set<T>().Remove(obj);
        }
    }
}
