using Mailjet.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ReiDoChopp.Domain.Helpers.Repositories;
using ReiDoChopp.Infra.Emails.Repositories;

namespace ReiDoChopp.Ioc.Configurations
{
    internal static class HttpConfiguration
    {
        public static IServiceCollection AddHttp(this IServiceCollection services)
        {
            services.AddHttpClient<EmailsRepository>();
            services.AddHttpClient<HelpersRepository>();

            return services;
        }
    }
}
