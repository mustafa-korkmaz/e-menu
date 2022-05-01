using Application.Services.Menu;
using Application.Services.Product;
using Application.Services.Tenant;
using Infrastructure.Configuration;

namespace Presentation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigSections(
           this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MongoDbConfig>(
                config.GetSection("MongoDbConfig"));

            return services;
        }

        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            //ViewModels to DTOs mappings
            services.AddAutoMapper(typeof(MappingProfile));

            //DTOs to Domain entities mappings
            services.AddAutoMapper(typeof(Application.MappingProfile));

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITenantContextService, TenantContextService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IMenuService, MenuService>();

            return services;
        }
    }
}


