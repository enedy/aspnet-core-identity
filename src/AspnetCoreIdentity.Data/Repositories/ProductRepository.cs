using AspnetCoreIdentity.Data.Context;
using AspnetCoreIdentity.Data.Repositories.Shared;
using AspnetCoreIdentity.Domain.Entities;
using AspnetCoreIdentity.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetCoreIdentity.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context) { }

        public async override Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Product
                .Include(p => p.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async override Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id.Equals(id));
        }
    }
}
