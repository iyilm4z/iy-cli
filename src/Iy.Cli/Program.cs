using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace Iy.Cli
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var services = new ServiceCollection();
            AddCliLogger(services);

            IocManager.Instance.RegisterServices(services);

            await IocManager.Instance.ServiceProvider
                .GetRequiredService<CliService>()
                .RunAsync(args);

            IocManager.Instance.DisposeServices();
        }

        public static void AddCliLogger(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
#if DEBUG
                .MinimumLevel.Override("Iy.Cli", LogEventLevel.Debug)
#else
                .MinimumLevel.Override("Iy.Cli", LogEventLevel.Information)
#endif
                .Enrich.FromLogContext()
                .WriteTo.File(CliPaths.Log)
                .WriteTo.Console()
                .CreateLogger();

            services.AddLogging(c => c.AddSerilog());
        }
    }
}