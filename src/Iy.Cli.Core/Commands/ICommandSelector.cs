using System;
using Iy.Cli.Args;

namespace Iy.Cli.Commands
{
    public interface ICommandSelector
    {
        Type Select(CommandLineArgs commandLineArgs);
    }
}