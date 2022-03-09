using Core.Common.App;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Common.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        private const string AppEventsRegistrationFailed =
                "Failed to register application events: unable to resolve the IApplicationEvents interface. Ensure that you have added the ApplicationEvents service on your services configuration. (are you missing a call to 'services.AddApplicationEvents();' ?)";

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
                  options.InjectStylesheet("/swagger-ui/site.css");
            });
            return app;
        }





        /// <summary>
        /// </summary>
        /// <param name="appLifetime"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="loggerFactory"></param>
        public static void RegisterApplicationEvents(this IHostApplicationLifetime appLifetime, IServiceProvider serviceProvider, ILoggerFactory loggerFactory = null)
        {
            if (appLifetime == null)
                throw new ArgumentNullException();
            if (serviceProvider == null)
                throw new ArgumentNullException();

            var appEvents = serviceProvider.GetService<IApplicationEvents>();
            if (appEvents == null)
            {
                if (loggerFactory == null)
                    throw new InvalidOperationException(AppEventsRegistrationFailed);

                loggerFactory.CreateLogger<ApplicationEvents>().LogWarning(AppEventsRegistrationFailed);
                return;
            }

            appLifetime.ApplicationStarted.Register(appEvents.OnStarted);
            appLifetime.ApplicationStopping.Register(appEvents.OnStopping);
            appLifetime.ApplicationStopped.Register(appEvents.OnStopped);
        }

    }
}
