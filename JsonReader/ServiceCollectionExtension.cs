using Microsoft.Extensions.DependencyInjection;
using static PersonRecord.JsonReader.IJsonService;

namespace PersonRecord.JsonReader
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMapperService(this IServiceCollection services)
        {
            services.AddSingleton<IMapperService, JsonService>();
            return services;
        }
    }
}
