using MicroShop.CartAPI.Domain.Entities;
using MicroShop.CartAPI.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace MicroShop.CartAPI.Infrastructure.Data
{
    public class Context : DbContext
    {
        public Context() { }
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CartMap());
        }
    }
}
