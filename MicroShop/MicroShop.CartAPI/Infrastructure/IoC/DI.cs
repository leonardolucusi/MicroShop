using MicroShop.CartAPI.Application.Interfaces;
using MicroShop.CartAPI.Application.Services;
using MicroShop.CartAPI.Domain.Repositories;
using MicroShop.CartAPI.Infrastructure.Data;
using MicroShop.CartAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MicroShop.CartAPI.Infrastructure.IoC
{
    public static class DI
    {
        public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartService, CartService>();

            services.AddDbContext<Context>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'Context' not found.")));
        }
    }
}
