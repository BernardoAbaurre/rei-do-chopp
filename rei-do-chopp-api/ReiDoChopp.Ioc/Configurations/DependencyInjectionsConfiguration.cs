using Microsoft.Extensions.DependencyInjection;
using ReiDoChopp.Application.Users.Services;
using ReiDoChopp.Domain.Users.Services;
using ReiDoChopp.Infra.Emails.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReiDoChopp.Ioc.Configurations
{
    internal static class DependencyInjectionsConfiguration
    {
        public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
        {
            services.Scan(scan => scan
            .FromAssemblyOf<UsersAppService>()
                .AddClasses(classes => classes.Where(type => !type.IsAssignableTo(typeof(Exception))))
                    .AsImplementedInterfaces()
                        .WithScopedLifetime());

            services.Scan(scan => scan
                .FromAssemblyOf<UsersService>()
                    .AddClasses(classes => classes.Where(type => !type.IsAssignableTo(typeof(Exception))))
                        .AsImplementedInterfaces()
                            .WithScopedLifetime());

            services.Scan(scan => scan
                .FromAssemblyOf<EmailsRepository>()
                    .AddClasses(classes => classes.Where(type => !type.IsAssignableTo(typeof(Exception))))
                        .AsImplementedInterfaces()
                            .WithScopedLifetime());

            return services;
        }
    }
}
