using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iy.Cli.Args;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Iy.Cli.Commands
{
    public class HelpCommand : IConsoleCommand
    {
        public const string Name = "help";

        private readonly ILogger<HelpCommand> _logger;
        private readonly CliOptions _cliOptions;

        public HelpCommand(ILogger<HelpCommand> logger,
            CliOptions cliOptions)
        {
            _logger = logger;
            _cliOptions = cliOptions;
        }

        public Task ExecuteAsync(CommandLineArgs commandLineArgs)
        {
            if (string.IsNullOrWhiteSpace(commandLineArgs.Target))
            {
                _logger.LogInformation(GetUsageInfo());

                return Task.CompletedTask;
            }

            if (!_cliOptions.Commands.ContainsKey(commandLineArgs.Target))
            {
                _logger.LogWarning($"There is no command named {commandLineArgs.Target}.");
                _logger.LogInformation(GetUsageInfo());

                return Task.CompletedTask;
            }

            var commandType = _cliOptions.Commands[commandLineArgs.Target];

            using (var scope = IocManager.Instance.ServiceProvider.CreateScope())
            {
                var command = (IConsoleCommand)scope.ServiceProvider.GetRequiredService(commandType);
                _logger.LogInformation(command.GetUsageInfo());
            }

            return Task.CompletedTask;
        }

        public string GetUsageInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine("");
            sb.AppendLine("Usage:");
            sb.AppendLine("");
            sb.AppendLine("    iy <command> <target> [options]");
            sb.AppendLine("");
            sb.AppendLine("Command List:");
            sb.AppendLine("");

            foreach (var command in _cliOptions.Commands.ToArray())
            {
                string shortDescription;

                using (var scope = IocManager.Instance.ServiceProvider.CreateScope())
                {
                    shortDescription = ((IConsoleCommand)scope.ServiceProvider.GetRequiredService(command.Value)).GetShortDescription();
                }

                sb.Append("    > ");
                sb.Append(command.Key);
                sb.Append(string.IsNullOrWhiteSpace(shortDescription) ? "" : ":");
                sb.Append(" ");
                sb.AppendLine(shortDescription);
            }

            sb.AppendLine("");
            sb.AppendLine("To get a detailed help for a command:");
            sb.AppendLine("");
            sb.AppendLine("    iy help <command>");
            sb.AppendLine("");
            sb.AppendLine("See the documentation for more info: https://docs.iy-cli.io/cli");

            return sb.ToString();
        }

        public string GetShortDescription()
        {
            return "Show command line help. Write ` iy help <command> `";
        }
    }
}
