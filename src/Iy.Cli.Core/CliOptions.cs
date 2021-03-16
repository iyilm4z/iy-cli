using System;
using System.Collections.Generic;

namespace Iy.Cli
{
    public class CliOptions
    {
        public Dictionary<string, Type> Commands { get; }

        public CliOptions()
        {
            Commands = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
        }
    }
}