
using CommandLine;
using HttpLogParser.ConsoleApp.Config;
using HttpLogParser.ConsoleApp.Engine;
using HttpLogParser.ConsoleApp.Reporting;

var cliOptions = Parser.Default.ParseArguments<CommandLineOptions>(args)
  .WithParsed(CommandLineOptions.RunOptions)
  .WithNotParsed(CommandLineOptions.HandleParseError);

var logRader = new LogReader(new LogItemParser());
var logitems = logRader.ReadFromFile(cliOptions.Value.File);

var logReporting = new LogReporting(logitems);

OutputNumberOfUniqueIpAddresses(logReporting.GetNumberOfUniqueIpAddresses());
Console.WriteLine("---------------------------------------------------");

OutputGetTop3MostVisitedUrls(logReporting.GetTop3MostVisitedUrls());
Console.WriteLine("---------------------------------------------------");

OutputGetTop3MostActiveIpAddresses(logReporting.GetTop3MostActiveIpAddresses());

Console.WriteLine(Environment.NewLine);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();

void OutputNumberOfUniqueIpAddresses(IReadOnlyCollection<string> readOnlyCollection)
{
    Console.WriteLine($"Number Of Unique Ip Addresses:{readOnlyCollection.Count}");
    foreach (var item in readOnlyCollection)
    {
        Console.WriteLine(item);
    }
}

void OutputGetTop3MostVisitedUrls(IReadOnlyCollection<GetTop3MostVisitedUrlsModel> getTop3MostVisitedUrlsModels)
{

    Console.WriteLine($"Top 3 most visited URLs: {getTop3MostVisitedUrlsModels.Count}");
    foreach (var item in getTop3MostVisitedUrlsModels)
    {
        Console.WriteLine($"{item.RequestAndProtocol}({item.Count})");
    }
}

void OutputGetTop3MostActiveIpAddresses(IReadOnlyCollection<GetTop3MostActiveIpAddressesModel> getTop3MostActiveIpAddressesModels)
{
    Console.WriteLine($"Top 3 most active IP addresses: {getTop3MostActiveIpAddressesModels.Count}");
    foreach (var item in getTop3MostActiveIpAddressesModels)
    {
        Console.WriteLine($"{item.RemoteHostAddress}({item.Count})");
    }
}