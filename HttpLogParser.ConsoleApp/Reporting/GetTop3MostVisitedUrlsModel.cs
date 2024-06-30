namespace HttpLogParser.ConsoleApp.Reporting;
public record GetTop3MostVisitedUrlsModel(
    string RequestAndProtocol,
    double Count);