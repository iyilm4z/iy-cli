using System;
using System.Linq;

namespace Iy.Cli.Args
{
    public class CommandLineArgumentParser : ICommandLineArgumentParser
    {
        public CommandLineArgs Parse(string[] args)
        {
            if (args == null || !args.Any())
            {
                return CommandLineArgs.Empty();
            }

            var argumentList = args.ToList();

            var command = argumentList[0];
            argumentList.RemoveAt(0);

            if (!argumentList.Any())
            {
                return new CommandLineArgs(command);
            }

            var target = argumentList[0];
            if (target.StartsWith("-"))
            {
                target = null;
            }
            else
            {
                argumentList.RemoveAt(0);
            }

            if (!argumentList.Any())
            {
                return new CommandLineArgs(command, target);
            }

            var commandLineArgs = new CommandLineArgs(command, target);

            while (argumentList.Any())
            {
                var optionName = ParseOptionName(argumentList[0]);
                argumentList.RemoveAt(0);

                if (!argumentList.Any())
                {
                    commandLineArgs.Options[optionName] = null;
                    break;
                }

                if (IsOptionName(argumentList[0]))
                {
                    commandLineArgs.Options[optionName] = null;
                    continue;
                }

                commandLineArgs.Options[optionName] = argumentList[0];
                argumentList.RemoveAt(0);
            }

            return commandLineArgs;
        }

        private static bool IsOptionName(string argument)
        {
            return argument.StartsWith("-") || argument.StartsWith("--");
        }

        private static string ParseOptionName(string argument)
        {
            if (argument.StartsWith("--"))
            {
                if (argument.Length <= 2)
                {
                    throw new ArgumentException("Should specify an option name after '--' prefix!");
                }

                return RemoveOptionNamePreFix(argument, "--");
            }

            if (argument.StartsWith("-"))
            {
                if (argument.Length <= 1)
                {
                    throw new ArgumentException("Should specify an option name after '--' prefix!");
                }

                return RemoveOptionNamePreFix(argument, "-");
            }

            throw new ArgumentException("Option names should start with '-' or '--'.");
        }

        public static string RemoveOptionNamePreFix(string str, params string[] preFixes)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            if (preFixes == null || !preFixes.Any())
            {
                return str;
            }

            foreach (var preFix in preFixes)
            {
                if (!str.StartsWith(preFix, StringComparison.Ordinal))
                {
                    continue;
                }

                var len = str.Length - preFix.Length;

                if (str.Length < len)
                {
                    throw new ArgumentException("len can not be bigger than length of str!");
                }

                return str.Substring(str.Length - len, len);
            }

            return str;
        }
    }
}