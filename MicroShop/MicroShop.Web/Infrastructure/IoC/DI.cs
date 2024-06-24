using MicroShop.Web.Application.Mapping;
using MicroShop.Web.Domain.Interfaces;
using MicroShop.Web.Infrastructure.Data;
using MicroShop.Web.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;

namespace MicroShop.Web.Infrastructure.IoC
{
    public static class DI
    {
        public static void AddDependecyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(UserProfile).Assembly);
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddDbContext<Context>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'Context' not found.")));

            services.AddHttpClient("ProductAPI", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7037");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
        }
    }
}
