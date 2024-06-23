using MicroShop.ProductAPI.Application.Interfaces;
using MicroShop.ProductAPI.Application.Mapping;
using MicroShop.ProductAPI.Application.Services;
using MicroShop.ProductAPI.Domain.Repositories;
using MicroShop.ProductAPI.Infrastructure.Data;
using MicroShop.ProductAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MicroShop.ProductAPI.Infrastructure.IoC
{
    public static class DI
    {
        public static void AddDependecyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ProductProfile).Assembly);

            services.AddDbContext<Context>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'Context' not found.")));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
