using Mapster;
using WebArMa.ArMaMelk.API.Infrastructure.Mapster;

namespace WebArMa.ArMaMelk.API.EndPoint.Utils.Configurations
{
    public static class AddTypeAdapterConfigRegistor
    {
        public static IServiceCollection AddTypeAdapterConfig(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(PersonMappingConfigurations).Assembly);
            services.AddSingleton(config);

            return services;
        }
    }
}
