using AspnetCoreIdentity.Domain.Entities;
using AspnetCoreIdentity.Domain.Interfaces.Repositories;
using AspnetCoreIdentity.Domain.Interfaces.Services;
using AspnetCoreIdentity.Domain.Services.Shared;

namespace AspnetCoreIdentity.Domain.Services
{
    public class ProductService : ServiceBase<Product>, IProductService
    {
        public ProductService(IProductRepository productRepository) : base(productRepository) { }
    }
}
