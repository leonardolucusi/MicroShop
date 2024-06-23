using MicroShop.ProductAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroShop.ProductAPI.Infrastructure.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id)
               .HasConversion(
                   v => v.ToString(),
                   v => Ulid.Parse(v))
               .ValueGeneratedOnAdd();

            builder.Property(p => p.Price)
           .HasColumnType("decimal(18,2)");

        }
    }
}
