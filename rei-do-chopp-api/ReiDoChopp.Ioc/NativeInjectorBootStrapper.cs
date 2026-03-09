using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReiDoChopp.Ioc.Configurations;

namespace ReiDoChopp.Ioc
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDependencyInjections();
            services.AddCorsConfiguration();
            services.AddEntityFramework(configuration);
            services.AddAuth(configuration);
            services.AddHttp();
        }
    }
}
