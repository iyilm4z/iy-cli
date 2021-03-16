using System.Threading.Tasks;
using Iy.Cli.Args;

namespace Iy.Cli.Commands
{
    public interface IConsoleCommand
    {
        Task ExecuteAsync(CommandLineArgs commandLineArgs);

        string GetUsageInfo();

        string GetShortDescription();
    }
}