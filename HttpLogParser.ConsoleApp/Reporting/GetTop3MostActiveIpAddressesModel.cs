namespace HttpLogParser.ConsoleApp.Reporting;
public record GetTop3MostActiveIpAddressesModel(
    string RemoteHostAddress,
    double Count);