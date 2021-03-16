namespace Iy.Cli.Args
{
    public interface ICommandLineArgumentParser
    {
        CommandLineArgs Parse(string[] args);
    }
}