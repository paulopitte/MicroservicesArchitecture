using Basket.Api.Repositories;
using Core.Common.Extensions;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace Basket.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {



        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();

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




            services.AddApplicationEvents();
            services.AddApiVersioningConfig();
            services.AddJwtconfig(configuration, null);
            services.AddSwaggerConfig();
            //  services.AddCaching(configuration);


            services.AddStackExchangeRedisCache(op =>
            {
                op.Configuration = configuration.GetValue<string>("DistributedCache:ConnectionString");
            });

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
                            "http://localhost:5063",
                            "https://localhost:5063",
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
