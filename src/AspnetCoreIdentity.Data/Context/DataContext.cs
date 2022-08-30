using AspnetCoreIdentity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreIdentity.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
