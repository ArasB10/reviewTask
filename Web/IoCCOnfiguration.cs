using Domain;
using Microsoft.Extensions.DependencyInjection;
using Web.Initializers;

namespace Web
{
    public static class IocConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IReporistory, Repository>();

            services.AddTransient<DummyDataInitializer>();

            return services;
        }
    }
}
