using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Globalization;
using System.Text;

namespace Core.Common.Extensions
{
    using Core.Common.App;
    using Models;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationEvents(this IServiceCollection services) =>
            services.AddSingleton<IApplicationEvents, ApplicationEvents>();

        public static IServiceCollection AddLogging(this IServiceCollection services, IConfiguration configuration = null)
        {
            return services;
        }

        //public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var redisOptions = new CacheOptions();
        //    configuration.GetSection("DistributedCache").Bind(redisOptions);
        //    services.AddRedisDistributedCache(ro =>
        //    {
        //        ro.ConnectionString = redisOptions.ConnectionString;
        //        ro.DatabaseId = redisOptions.DatabaseId;
        //        ro.DefaultCacheTime = redisOptions.DefaultCacheTime;
        //        ro.ShortCacheTime = redisOptions.ShortCacheTime;
        //        ro.IgnoreTimeoutException = redisOptions.IgnoreTimeoutException;
        //    });

        //    return services;
        //}

        public static IServiceCollection AddJwtconfig(this IServiceCollection services, IConfiguration configuration, string pJwtSettings = null)
        {


            // services.AddDataProtection();


            var jwtSettingsSection = configuration.GetSection("JwtSettings" ?? pJwtSettings);
            services.Configure<JwtSettings>(jwtSettingsSection);
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = true;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    ClockSkew = System.TimeSpan.Zero
                };
            });

            return services;
        }


        public static IServiceCollection AddApiVersioningConfig(this IServiceCollection services)
        {

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigurationSwaggerOptions>();

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;

            });
            return services;
        }



        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerDefaultValues>();
                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "Informe seu token usando: Bearer: {seu token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                };

                c.AddSecurityDefinition("Bearer", securityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }



        internal class SwaggerDefaultValues : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                var apiDescription = context.ApiDescription;
                operation.Deprecated = apiDescription.IsDeprecated();

                if (operation.Parameters == null) return;

                foreach (var parameter in operation.Parameters)
                {
                    var description = context.ApiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
                    var routeInfo = description.RouteInfo;

                    if (string.IsNullOrEmpty(parameter.Name))
                    {
                        parameter.Name = description.ModelMetadata?.Name;
                    }

                    if (parameter.Description == null)
                    {
                        parameter.Description = description.ModelMetadata?.Description;
                    }

                    if (routeInfo == null)
                    {
                        continue;
                    }

                    parameter.Required |= !routeInfo.IsOptional;
                }

                // Overwrite description for common response codes
                var statusBadRequest = StatusCodes.Status400BadRequest.ToString(CultureInfo.InvariantCulture);
                if (operation.Responses.ContainsKey(statusBadRequest))
                {
                    operation.Responses[statusBadRequest].Description =
                        "Invalid query parameter(s). Read the response description";
                }

                var statusUnauthorized = StatusCodes.Status401Unauthorized.ToString(CultureInfo.InvariantCulture);
                if (operation.Responses.ContainsKey(statusUnauthorized))
                {
                    operation.Responses[statusUnauthorized].Description =
                        "Authorization has been denied for this request";
                }
            }
        }


        public class ConfigurationSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
        {
            private readonly IApiVersionDescriptionProvider _provider;

            public ConfigurationSwaggerOptions(IApiVersionDescriptionProvider provider)
            {
                _provider = provider;
            }

            public void Configure(SwaggerGenOptions options)
            {
                foreach (var description in _provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                        description.GroupName,
                        CreateApiInfo(description));
                }
            }


            private static OpenApiInfo CreateApiInfo(ApiVersionDescription description)
            {
                return new OpenApiInfo
                {
                    Title = "Exemplo microservices - API",
                    License = new OpenApiLicense
                    {
                        Name = "Paulo Pitte",
                        Url = new Uri("https://paulopitte.io")
                    },
                    Version = description.ApiVersion.ToString(),
                    Description = "Sistema Integrado de Gestão Comercial",
                    Contact = new OpenApiContact
                    {
                        Name = "Brincadeiras com microservices - API",
                        Url = new Uri("https://paulopitte.io"),
                        Email = "paulopitte@gmail.com"
                    }
                };
            }
        }

    }
}
