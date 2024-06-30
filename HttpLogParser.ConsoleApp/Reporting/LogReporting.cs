using HttpLogParser.ConsoleApp.Engine;

namespace HttpLogParser.ConsoleApp.Reporting;
public class LogReporting
{
    private IReadOnlyList<LogItemModel> _logitems;

    public LogReporting(IReadOnlyList<LogItemModel> logitems)
    {
        _logitems = logitems;
    }

    public IReadOnlyCollection<string> GetNumberOfUniqueIpAddresses()
    {
        return _logitems.DistinctBy(i => i.RemoteHostAddress)
            .Select(i => i.RemoteHostAddress)
            .ToList()
            .AsReadOnly();
    }

    public IReadOnlyCollection<GetTop3MostActiveIpAddressesModel> GetTop3MostActiveIpAddresses()
    {
        //TODO: TAKE 3 is not the best result
        return _logitems.GroupBy(i => i.RemoteHostAddress)
            .Select(i => new GetTop3MostActiveIpAddressesModel(i.Key, i.Count()))
            .OrderByDescending(i => i.Count)
            .Take(3)
            .ToList()
            .AsReadOnly();
    }

    public IReadOnlyCollection<GetTop3MostVisitedUrlsModel> GetTop3MostVisitedUrls()
    {
        //TODO: TAKE 3 is not the best result
        return _logitems
            .GroupBy(i => ProtocolSanitizer(i.RequestAndProtocol))
            .Select(i => new GetTop3MostVisitedUrlsModel(i.Key, i.Count()))
            .OrderByDescending(i => i.Count)
            .Take(3)
            .ToList()
            .AsReadOnly();
    }

    private string ProtocolSanitizer(string requestAndProtocol)
    {
        return requestAndProtocol
            .Replace("GET", string.Empty)
            .Replace("HEAD", string.Empty)
            .Replace("POST", string.Empty)
            .Replace("PUT", string.Empty)
            .Replace("DELETE", string.Empty)
            .Replace("CONNECT", string.Empty)
            .Replace("OPTIONS", string.Empty)
            .Replace("TRACE", string.Empty)
            .Replace("PATCH", string.Empty)
            .Trim();
    }
}
