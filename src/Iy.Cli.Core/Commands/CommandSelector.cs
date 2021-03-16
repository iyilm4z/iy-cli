using System;
using Iy.Cli.Args;

namespace Iy.Cli.Commands
{
    public class CommandSelector : ICommandSelector
    {
        protected CliOptions Options { get; }

        public CommandSelector(CliOptions options)
        {
            Options = options;
        }

        public Type Select(CommandLineArgs commandLineArgs)
        {
            if (string.IsNullOrWhiteSpace(commandLineArgs.Command))
            {
                return typeof(HelpCommand);
            }

            return (Options.Commands.TryGetValue(commandLineArgs.Command, out var obj) ? obj : default)
                   ?? typeof(HelpCommand);
        }
    }
}