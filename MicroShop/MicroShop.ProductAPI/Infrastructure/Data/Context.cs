using MicroShop.ProductAPI.Domain.Entities;
using MicroShop.ProductAPI.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
namespace MicroShop.ProductAPI.Infrastructure.Data
{
    public class Context : DbContext
    {
        public Context() { }
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductMap());
        }
    }
}
