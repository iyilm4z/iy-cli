using System;
using System.Collections.Generic;
using System.Linq;

namespace Iy.Cli.Args
{
    public class CommandLineOptions : Dictionary<string, string>
    {
        public string GetOrNull( string name, params string[] alternativeNames)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"{nameof(name)} can not be null, empty or white space!", nameof(name));
            }

            var value = TryGetValue(name, out var obj) ? obj : default;
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            if (alternativeNames == null || !alternativeNames.Any())
            {
                return null;
            }

            foreach (var alternativeName in alternativeNames)
            {
                value = TryGetValue(alternativeName, out var altObj) ? altObj : default;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }

            return null;
        }
    }
}