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
        var min = _logitems
            .GroupBy(i => i.RemoteHostAddress)
            .Select(i => new { RemoteHostAddress = i.Key, Count = i.Count() })
            .OrderByDescending(i => i.Count)
            .Take(3)
            .Min(i => i.Count);

        return _logitems
            .GroupBy(i => i.RemoteHostAddress)
            .Select(i => new GetTop3MostActiveIpAddressesModel(i.Key, i.Count()))
            .OrderByDescending(i => i.Count)
            .TakeWhile(i => i.Count >= min)
            .ToList()
            .AsReadOnly();
    }

    public IReadOnlyCollection<GetTop3MostVisitedUrlsModel> GetTop3MostVisitedUrls()
    {
        var sanitizedItems = _logitems
            .Select(i => new { RequestAndProtocol = ProtocolSanitizer(i.RequestAndProtocol) });

        var min = sanitizedItems
            .GroupBy(i => i.RequestAndProtocol)
            .Select(i => new { RequestAndProtocol = i.Key, Count = i.Count() })
            .OrderByDescending(i => i.Count)
            .Take(3)
            .Min(i => i.Count);


        return sanitizedItems
            .GroupBy(i => i.RequestAndProtocol)
            .Select(i => new GetTop3MostVisitedUrlsModel(i.Key, i.Count()))
            .OrderByDescending(i => i.Count)
            .TakeWhile(i => i.Count >= min)
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
