using CommandLine;

namespace HttpLogParser.ConsoleApp.Config;
internal class CommandLineOptions
{

    [Option('f', "file", Required = false, HelpText = "The log file location.")]
    public string File { get; set; }

    [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
    public bool Verbose { get; set; }

    public static void RunOptions(CommandLineOptions opts)
    {
        //handle options
    }
    public static void HandleParseError(IEnumerable<Error> errs)
    {
        //handle errors
    }
}
