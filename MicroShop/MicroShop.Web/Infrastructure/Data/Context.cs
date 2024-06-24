using MicroShop.Web.Domain.Entities;
using MicroShop.Web.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace MicroShop.Web.Infrastructure.Data
{
    public class Context : DbContext
    {
        public Context() { }
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<User> Users{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserMap());
        }
    }
}
