using AspnetCoreIdentity.Domain.Shared;

namespace AspnetCoreIdentity.Domain.Interfaces.Services.Shared
{
    public interface IServiceBase<TEntity> : IDisposable where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task<object> AddAsync(TEntity objeto);
        Task UpdateAsync(TEntity objeto);
        Task DeleteAsync(TEntity objeto);
        Task DeleteByIdAsync(int id);
    }
}
