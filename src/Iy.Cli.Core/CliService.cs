using System;
using System.Threading.Tasks;
using Iy.Cli.Args;
using Iy.Cli.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Iy.Cli
{
    public class CliService
    {
        private readonly ILogger<CliService> _logger;
        private readonly ICommandLineArgumentParser _commandLineArgumentParser;
        private readonly ICommandSelector _commandSelector;

        public CliService(ILogger<CliService> logger,
            ICommandLineArgumentParser commandLineArgumentParser,
            ICommandSelector commandSelector)
        {
            _logger = logger;
            _commandLineArgumentParser = commandLineArgumentParser;
            _commandSelector = commandSelector;
        }

        public async Task RunAsync(string[] args)
        {
            _logger.LogInformation("Iy CLI (https://www.iy-cli.io)");

            var commandLineArgs = _commandLineArgumentParser.Parse(args);

            var commandType = _commandSelector.Select(commandLineArgs);

            using (var scope = IocManager.Instance.ServiceProvider.CreateScope())
            {
                try
                {
                    var command = (IConsoleCommand)scope.ServiceProvider.GetRequiredService(commandType);
                    await command.ExecuteAsync(commandLineArgs);
                }
                catch (CliUsageException usageException)
                {
                    _logger.LogWarning(usageException.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError("An error occurred!", ex);
                }
            }
        }
    }
}
