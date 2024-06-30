
using System.Globalization;
using System.Text.RegularExpressions;
using HttpLogParser.ConsoleApp.Engine.Abstractions;

namespace HttpLogParser.ConsoleApp.Engine;

public class LogItemParser : ILogRowParser
{
    private readonly Regex ipRegex = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
    private readonly Regex datetimeRegex = new Regex(@"\[(.*?)\]");
    private readonly Regex clientInfoRegex = new Regex("\"([^\\\"]+)\"");


    public LogItemModel ParseFromString(string log)
    {
        var remoteHostAddress = ParseRemoteHostAddress(log);
        var remoteLogName = ParseRemoteLogName(log);
        var username = ParseUsername(log);
        var dateTimeAsGmt = ParseDateTimeAsGmt(log);
        var requestAndProtocol = ParseRequestAndProtocol(log);
        var serviceStatuscode = ParseServiceStatuscode(log);
        var bytesSent = ParseBytesSent(log);
        var clientInformation = ParseClientInformation(log);

        return new LogItemModel(
            remoteHostAddress,
            remoteLogName,
            username,
            dateTimeAsGmt,
            requestAndProtocol,
            serviceStatuscode,
            bytesSent,
            clientInformation);
    }

    public string ParseClientInformation(string log)
    {
        var result = clientInfoRegex.Matches(log);
        if (result.Count != 3)
            throw new InvalidCastException("ClientInformation");

        return DoubleQuoteSanitizer(result[2].Value);
    }

    public double ParseBytesSent(string log)
    {
        var cols = log.Split(" ");
        if (cols.Length < 9)
            throw new InvalidCastException("BytesSent");

        return double.Parse(cols[9]);
    }

    public string ParseServiceStatuscode(string log)
    {
        var cols = log.Split(" ");
        if (cols.Length < 8)
            throw new InvalidCastException("ServiceStatuscode");

        return cols[8];
    }

    public string ParseRequestAndProtocol(string log)
    {
        var result = clientInfoRegex.Matches(log);
        if (result.Count != 3)
            throw new InvalidCastException("RequestAndProtocol");

        return DoubleQuoteSanitizer(result[0].Value);
    }

    public DateTimeOffset ParseDateTimeAsGmt(string log)
    {
        string input_format = "dd/MMM/yyyy:HH:mm:ss zzz";
        var result = datetimeRegex.Matches(log);
        if (result.Count == 0)
            throw new InvalidCastException("DateTimeAsGmt");

        var datetimeasString = result[0].Value.Replace("[", string.Empty).Replace("]", string.Empty);
        return DateTimeOffset.ParseExact(datetimeasString, input_format, CultureInfo.InvariantCulture).ToUniversalTime();
    }

    public string ParseUsername(string log)
    {
        var cols = log.Split(" ");
        if (cols.Length < 2)
            throw new InvalidCastException("RemoteLogName");

        return HifenSanitizer(cols[2]);
    }

    public string ParseRemoteLogName(string log)
    {
        var cols = log.Split(" ");
        if (cols.Length < 1)
            throw new InvalidCastException("RemoteLogName");

        return HifenSanitizer(cols[1]);
    }

    public string ParseRemoteHostAddress(string log)
    {
        var result = ipRegex.Matches(log);
        if (result.Count == 0)
            throw new InvalidCastException("RemoteHostAddress");

        return result[0].Value;
    }

    private string HifenSanitizer(string content)
    {
        return content.Replace("-", string.Empty);
    }

    private string DoubleQuoteSanitizer(string content)
    {
        return content.Replace("\"", string.Empty);
    }
}
