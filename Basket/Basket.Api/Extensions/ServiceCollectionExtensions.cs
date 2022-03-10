 
using Core.Common.Extensions;
using Core.Common.Models;
using MediatR;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;
using System.IO.Compression;

namespace Basket.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
          
            return services;
        }

        public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));


            //Ops: Necessario para leitura das configurações
            services.AddOptions();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddHealthChecks();
            services.AddMvc(); // ==>  Necessario para uso da Interface IDistributedCache


            services.AddApiVersioningConfig();
            services.AddJwtconfig(configuration, null);
            services.AddSwaggerConfig();
            services.AddCaching(configuration);


            // HABILITA O MODULO DE COMPACTAÇÃO PARA RESPONSE HTTP
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });


            return services;
        }

        public static IServiceCollection AddCorsAPI(this IServiceCollection services, string corsPolicyName)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddCors(options =>
            {
                options.AddPolicy(corsPolicyName,
                    builder => builder
                        .WithOrigins(
                            "http://localhost:60414",
                            "https://localhost:60414",
                            "https://localhost:8080")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithExposedHeaders("x-custom-header")
                );
            });
            return services;
        }
    }
}
