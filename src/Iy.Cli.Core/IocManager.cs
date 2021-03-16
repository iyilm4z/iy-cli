using System;
using Iy.Cli.Args;
using Iy.Cli.Auth;
using Iy.Cli.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Iy.Cli
{
    public class IocManager
    {
        protected internal IocManager() { }

        public static IocManager Instance { get; protected set; } = new IocManager();

        public IServiceProvider ServiceProvider { get; private set; }

        public void RegisterServices(IServiceCollection services = null)
        {
            services ??= new ServiceCollection();

            services.AddTransient<CliService>();
            services.AddTransient<ICommandSelector, CommandSelector>();
            services.AddTransient<ICommandLineArgumentParser, CommandLineArgumentParser>();
            services.AddTransient<HelpCommand>();
            services.AddTransient<LoginCommand>();
            services.AddTransient<LogoutCommand>();
            services.AddTransient<AuthService>();

            var cliOptions = new CliOptions();
            cliOptions.Commands[HelpCommand.Name] = typeof(HelpCommand);
            cliOptions.Commands[LoginCommand.Name] = typeof(LoginCommand);
            cliOptions.Commands[LogoutCommand.Name] = typeof(LogoutCommand);

            services.AddSingleton(cliOptions);

            ServiceProvider = services.BuildServiceProvider();
        }

        public void DisposeServices()
        {
            if (ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}