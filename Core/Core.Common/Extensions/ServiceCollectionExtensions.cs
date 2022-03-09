using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Globalization;

namespace Core.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
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
                    options.SwaggerDoc(description.GroupName, CreateApiInfo(description));
                }
            }


            private static OpenApiInfo CreateApiInfo(ApiVersionDescription description)
            {
                return new OpenApiInfo
                {
                    Title = "Sistema Integrado de Gestão Comercial - API",
                    Version = description.ApiVersion.ToString(),
                    Description = "Sistema Integrado de Gestão Comercial",
                    Contact = new OpenApiContact
                    {
                        Name = "Sistema Integrado de Gestão Comercial",
                        Url = new Uri("https://paulopitte.io")
                    }
                };
            }
        }

    }
}
