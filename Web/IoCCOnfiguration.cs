using DataAccess;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using Service;
using Web.Initializers;

namespace Web
{
    public static class IocConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IReporistory, Repository>();

            services.AddSingleton<IFloorRepository, FloorRepository>();

            services.AddTransient<IFloorService, FloorService>();

            services.AddTransient<DummyDataInitializer>();

            return services;
        }
    }
}
