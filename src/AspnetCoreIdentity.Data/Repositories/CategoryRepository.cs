using AspnetCoreIdentity.Data.Context;
using AspnetCoreIdentity.Data.Repositories.Shared;
using AspnetCoreIdentity.Domain.Entities;
using AspnetCoreIdentity.Domain.Interfaces.Repositories;

namespace AspnetCoreIdentity.Data.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context) { }
    }
}
