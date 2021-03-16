using System.IO;

namespace Iy.Cli
{
    public static class CliPaths
    {
        public static string AccessToken => Path.Combine(RootPath, "iy-access-token.txt");

        public static string Log => Path.Combine(RootPath, "iy-cli-logs.log");

        private static readonly string RootPath = Path.Combine(Directory.GetCurrentDirectory());
    }
}
