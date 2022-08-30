using AspnetCoreIdentity.Data.Context;
using AspnetCoreIdentity.Domain.Interfaces.Repositories.Shared;
using AspnetCoreIdentity.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreIdentity.Data.Repositories.Shared
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : Entity
    {
        protected readonly DataContext _context;

        public RepositoryBase(DataContext dataContext) =>
            _context = dataContext;

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await _context.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();

        public virtual async Task<TEntity?> GetByIdAsync(Guid id) =>
            await _context.Set<TEntity>().FindAsync(id);

        public virtual async Task<object> AddAsync(TEntity obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
            return obj.Id;
        }

        public virtual async Task UpdateAsync(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteByIdAsync(Guid id)
        {
            var obj = await GetByIdAsync(id);
            if (obj == null)
                throw new Exception("Not Found On Database.");

            await DeleteAsync(obj);
        }

        public void Dispose() => _context.Dispose();
    }
}
