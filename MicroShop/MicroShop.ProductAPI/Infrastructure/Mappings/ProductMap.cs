using MicroShop.ProductAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace MicroShop.ProductAPI.Infrastructure.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id)
            .IsRequired()
            .HasConversion(
                ulid => ulid.ToString(),
                str => Ulid.Parse(str)
            )
            .ValueGeneratedOnAdd();

            builder.Property(p => p.Price)
           .HasColumnType("decimal(18,2)");

        }
    }
}
