using Core.Framework.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Infrastructure.Logging.Microsoft
{
    public static class LoggerConfiguration
    {
        public static IHostBuilder ConfigureApplicationLogging(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddLogging(logging =>
                {
                    logging.ClearProviders();

                    if (System.Console.IsOutputRedirected == false)
                    {
                        logging.AddJsonConsole(options =>
                        {
                            options.JsonWriterOptions = new System.Text.Json.JsonWriterOptions { Indented = true };
                        });
                    }

                    // Resolve ILogContext after the service provider is built
                    //services.AddSingleton<ILoggerProvider>(provider =>
                    //{
                    //    var logContext = provider.GetRequiredService<ILogContext>();

                    //    return new JsonRollingFileLoggerProvider("Logs", 10485760, 10, 5, logContext);
                    //});

                    // Resolve ILogContext after the service provider is built (Transient!)
                    //services.AddTransient<ILoggerProvider>(provider => // This is the crucial change!
                    //{
                    //    var logContext = provider.GetRequiredService<ILogContext>();
                    //    return new JsonRollingFileLoggerProvider("Logs", 10485760, 10, 5, logContext);
                    //});

                    services.AddTransient<ILoggerProvider>(provider =>
                    {
                        // Create a scope to resolve scoped services
                        using (var scope = provider.CreateScope())
                        {
                            var logContext = scope.ServiceProvider.GetRequiredService<ILogContext>();
                            return new JsonRollingFileLoggerProvider("Logs", 10485760, 10, 5, logContext);
                        }
                    });

                    logging.AddFilter("Microsoft", LogLevel.None);
                    logging.AddFilter("System", LogLevel.None);

                    logging.SetMinimumLevel(LogLevel.Information);
                });
            });
        }
    }
}
