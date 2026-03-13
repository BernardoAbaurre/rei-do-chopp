using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReiDoChopp.Domain.Roles.Entities;
using ReiDoChopp.Domain.Users.Entities;
using ReiDoChopp.Infra.Data;
namespace ReiDoChopp.Ioc.Configurations
{
    internal static class EntityFrameworkConfiguration
    {
        public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ReiDoChoppDbContext>(options =>
            {
                options.UseLazyLoadingProxies()
                .UseNpgsql(configuration["ConnectionStrings:ReiDoChoppConnectionString"]!).EnableSensitiveDataLogging();
            });

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 0;
            })
            .AddEntityFrameworkStores<ReiDoChoppDbContext>()
            .AddDefaultTokenProviders();


            return services;
        }
    }
}
