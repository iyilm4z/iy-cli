using System;
using System.Text;
using System.Threading.Tasks;
using Iy.Cli.Args;
using Iy.Cli.Auth;
using Iy.Cli.Utils;
using Microsoft.Extensions.Logging;

namespace Iy.Cli.Commands
{
    public class LoginCommand : IConsoleCommand
    {
        public const string Name = "login";

        private readonly ILogger<LoginCommand> _logger;

        private readonly AuthService _authService;

        public LoginCommand(ILogger<LoginCommand> logger,
            AuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
        {
            if (string.IsNullOrEmpty(commandLineArgs.Target))
            {
                throw new CliUsageException(
                    "Username name is missing!" +
                    Environment.NewLine + Environment.NewLine +
                    GetUsageInfo()
                );
            }

            var password = commandLineArgs.Options.GetOrNull(Options.Password.Short, Options.Password.Long);
            if (password == null)
            {
                Console.Write("Password: ");
                password = ConsoleHelper.ReadSecret();
                if (string.IsNullOrWhiteSpace(password))
                {
                    throw new CliUsageException(
                        "Password is missing!" +
                        Environment.NewLine + Environment.NewLine +
                        GetUsageInfo()
                    );
                }
            }

            await _authService.LoginAsync(
                commandLineArgs.Target,
                password
            );

            _logger.LogInformation($"Successfully logged in as '{commandLineArgs.Target}'");
        }

        public string GetUsageInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine("");
            sb.AppendLine("Usage:");
            sb.AppendLine("  iy login <username>");
            sb.AppendLine("  iy login <username> -p <password>");
            sb.AppendLine("");
            sb.AppendLine("Example:");
            sb.AppendLine("");
            sb.AppendLine("  iy login john");
            sb.AppendLine("  iy login john -p 1234");
            sb.AppendLine("");
            sb.AppendLine("See the documentation for more info: https://docs.iy-cli.io/cli");

            return sb.ToString();
        }

        public string GetShortDescription()
        {
            return "Sign in to " + CliUrls.AccountIyCom + ".";
        }

        public static class Options
        {
            public static class Password
            {
                public const string Short = "p";
                public const string Long = "password";
            }
        }
    }
}
