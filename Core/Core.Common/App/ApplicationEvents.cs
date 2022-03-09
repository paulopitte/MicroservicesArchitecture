 using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Core.Common.App
{
    public class ApplicationEvents : IApplicationEvents
    {
        private static readonly Stopwatch WarmupTime = new();

        private static readonly Stopwatch StartupTime = new();

        private static readonly Stopwatch ShutdownTime = new();

        static ApplicationEvents()
        {
            StartupTime.Restart();
        }

        /// <summary>
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// </summary>
        protected ILogger<ApplicationEvents> Logger { get; }

        /// <summary>
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
         public ApplicationEvents(IServiceProvider serviceProvider, ILogger<ApplicationEvents> logger)
        {
            ServiceProvider = serviceProvider;
            Logger = logger;

            Initialize();
        }

        private void Initialize()
        {
            WarmupTime.Restart();

            AppDomain.CurrentDomain.DomainUnload += (sender, args) =>
            {
                OnStopping();
                OnStopped();
            };

            try
            {

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "FAILED TO WARMUP ONE OR MORE APPLICATION DEPENDENCIES.");
            }

            OnWarmup();
        }

        /// <summary>
        /// </summary>
        public virtual void OnWarmup()
        {
            WarmupTime.Stop();

            Logger.LogInformation("Application Warmup Completed in: {WarmupTime:0.00}ms.",
                Math.Round(WarmupTime.Elapsed.Duration().TotalMilliseconds, 2));
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public virtual void OnStarted()
        {
            StartupTime.Stop();

            Logger.LogInformation("Application Startup Completed in {StartupTime:0.00}ms.",
                Math.Round(StartupTime.Elapsed.Duration().TotalMilliseconds, 2));
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public virtual void OnStopping()
        {
            ShutdownTime.Restart();

            Logger.LogInformation("Application Stopping...");
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public virtual void OnStopped()
        {
            ShutdownTime.Stop();

            Logger.LogInformation("Application Shutdown Completed in {ShutdownTime:0.00}ms.",
                Math.Round(ShutdownTime.Elapsed.Duration().TotalMilliseconds, 2));

            //LogManager.GetInstance().CloseAndFlush();

            System.Threading.Thread.Sleep(1000);
        }

        /// <summary>
        /// </summary>
        public static void Starting() { }
    }
}
