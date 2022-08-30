using AspnetCoreIdentity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspnetCoreIdentity.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasData(new[]
            {
            new Category("Electronics"),
            new Category("Computing"),
            new Category("Clothing"),
            new Category("Books")
        });

            builder.ToTable("Category");
        }
    }
}
