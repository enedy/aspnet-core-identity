using AspnetCoreIdentity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspnetCoreIdentity.Data.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.Property(p => p.Code)
                .HasMaxLength(6)
                .IsUnicode(false);

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(p => p.Price)
                .HasPrecision(18, 2);

            builder.HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
