using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Core.Common.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers.Clear();
                });
            });

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
                //  options.InjectStylesheet("/swagger-ui/site.css");
            });
            return app;
        }
    }
}
