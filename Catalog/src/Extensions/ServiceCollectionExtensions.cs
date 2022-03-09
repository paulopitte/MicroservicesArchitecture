using Catalog.Api.Mappings;
using Catalog.Api.Repository;
using Core.Common.Extensions;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace Catalog.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }

        public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));


            //Ops: Necessario para leitura das configurações
            services.AddOptions();




          
            services.AddAutoMapper(typeof(DomainToResponseMappingProfile), typeof(RequestToCommandMappingProfile));
            services.AddApiVersioningConfig();
            services.AddJwtconfig(configuration, null);
            services.AddSwaggerConfig();
            services.AddHealthChecks();


            // HABILITA O MODULO DE COMPACTAÇÃO PARA RESPONSE HTTP
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });


            services.AddScoped<ICatalogContext, CatalogContext>();
            return services;
        }
    }
}
