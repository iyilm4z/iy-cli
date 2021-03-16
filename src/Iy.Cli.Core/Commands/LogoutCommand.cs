using System.Text;
using System.Threading.Tasks;
using Iy.Cli.Args;
using Iy.Cli.Auth;

namespace Iy.Cli.Commands
{
    public class LogoutCommand : IConsoleCommand
    {
        public const string Name = "logout";

        protected AuthService AuthService { get; }

        public LogoutCommand(AuthService authService)
        {
            AuthService = authService;
        }

        public Task ExecuteAsync(CommandLineArgs commandLineArgs)
        {
            return AuthService.LogoutAsync();
        }

        public string GetUsageInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine("");
            sb.AppendLine("Usage:");
            sb.AppendLine("  iy logout");
            sb.AppendLine("");
            sb.AppendLine("See the documentation for more info: https://docs.iy-cli.io/cli");

            return sb.ToString();
        }

        public string GetShortDescription()
        {
            return "Sign out from " + CliUrls.AccountIyCom + ".";
        }
    }
}