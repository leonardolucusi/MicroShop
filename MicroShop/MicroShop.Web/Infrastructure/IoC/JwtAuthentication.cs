using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MicroShop.Web.Infrastructure.IoC
{
    public static class JwtAuthentication
    {
        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.Cookies.Delete("Jwt");
                        if (!context.Request.Path.Equals("/Home/Index", StringComparison.OrdinalIgnoreCase) &&
                            !context.Request.Path.Equals("/Users/LoginPage", StringComparison.OrdinalIgnoreCase) &&
                            !context.Request.Path.Equals("/Users/RegisterPage", StringComparison.OrdinalIgnoreCase))
                        {
                            context.Response.Redirect("/Home/Index");
                        }
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        context.Response.Cookies.Delete("Jwt");
                        if (!context.Response.HasStarted &&
                            !context.Request.Path.Equals("/Home/Index", StringComparison.OrdinalIgnoreCase) &&
                            !context.Request.Path.Equals("/Users/LoginPage", StringComparison.OrdinalIgnoreCase) &&
                            !context.Request.Path.Equals("/Users/RegisterPage", StringComparison.OrdinalIgnoreCase))
                        {
                            context.HandleResponse();
                            context.Response.Redirect("/Home/Index");
                        }
                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        context.Response.Cookies.Delete("Jwt");
                        if (!context.Response.HasStarted &&
                            !context.Request.Path.Equals("/Home/Index", StringComparison.OrdinalIgnoreCase) &&
                            !context.Request.Path.Equals("/Users/LoginPage", StringComparison.OrdinalIgnoreCase) &&
                            !context.Request.Path.Equals("/Users/RegisterPage", StringComparison.OrdinalIgnoreCase))
                        {
                            context.Response.Redirect("/Home/Index");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
