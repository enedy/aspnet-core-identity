using AspnetCoreIdentity.Domain.Shared;

namespace AspnetCoreIdentity.Domain.Interfaces.Repositories.Shared
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<object> AddAsync(TEntity objeto);
        Task UpdateAsync(TEntity objeto);
        Task DeleteAsync(TEntity objeto);
        Task DeleteByIdAsync(Guid id);
    }
}
