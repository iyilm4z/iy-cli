namespace Iy.Cli
{
    public static class CliUrls
    {
#if DEBUG
        public const string AccountIyCom = AccountIyComDevelopment;
#else
        public const string AccountIyCom = AccountIyComProduction;
#endif

        public const string AccountIyComProduction = "https://account.iy-cli.io/";

        public const string AccountIyComDevelopment = "https://localhost:44333/";
    }
}
