using LetsEncryptRenewer.WebJob;
using LetsEncryptRenewer.WebJob.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;


namespace NotificationTestConsole
{
    internal static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureWebJobs((b) =>
                {
                    b.AddAzureStorageCoreServices()
                    .AddAzureStorage()
                    .AddTimers();
                })
                .ConfigureAppConfiguration(b =>
                {
                    b.AddCommandLine(args);
                    b.AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);
                    b.AddEnvironmentVariables();
                })
                .ConfigureLogging((context, b) =>
                {
                    Enum.TryParse(context.Configuration["LetsEncryptRenewerWebJobSettings:ApplicationInsights:LogLevel:Default"], out LogLevel logLevel);
                    b.SetMinimumLevel(logLevel);
                    b.AddConsole();

                    string appInsightsKey = context.Configuration["LetsEncryptRenewerWebJobSettings:ApplicationInsights:InstrumentationKey"];
                    if (!string.IsNullOrEmpty(appInsightsKey))
                    {
                        b.AddApplicationInsights(o => o.InstrumentationKey = appInsightsKey);
                    }
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<Functions, Functions>();

                    services.AddScoped(settings =>
                    {
                        return ConfigBuilder.BuildLetsEncryptConfig(context);
                    });

                    services.AddSingleton<ITelemetryLogger, TelemetryLogger>(
                     service =>
                     {
                         return new TelemetryLogger(context.Configuration, "LetsEncryptRenewerWebJobSettings:ApplicationInsights");
                     }
                 );
                })
                .UseConsoleLifetime();

            var host = builder.Build();
            using (host)
            {
                await host.RunAsync();
            }
        }
    }
}
