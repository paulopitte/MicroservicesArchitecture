﻿using Catalog.Api.Mappings;
using Catalog.Api.Repository;
using Core.Common.Extensions;
using Core.Common.Models;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;
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

            //// DISPONIBILIZA O "APPSETTINGS" (LIDO DO ARQUIVO DE CONFIGURAÇÕES)
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection)
                .AddScoped(cfg => cfg.GetService<IOptionsSnapshot<AppSettings>>().Value);



            //Ops: Necessario para leitura das configurações
            services.AddOptions();
            services.AddControllers();
            services.AddEndpointsApiExplorer();

          
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
