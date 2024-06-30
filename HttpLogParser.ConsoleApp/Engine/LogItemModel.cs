namespace HttpLogParser.ConsoleApp.Engine;

/// <summary>
/// <seealso cref="https://learn.microsoft.com/en-us/windows/win32/http/ncsa-logging"/>
/// </summary>
/// <param name="RemoteHostAddress">The IP address of the client that made the request.</param>
/// <param name="RemoteLogName">Not used. This value is always a hyphen.</param>
/// <param name="Username">The name of the authenticated user that accessed the server. Anonymous users are indicated by a hyphen. The best practice is for the application always to provide the user name.</param>
/// <param name="DateTimeAsGmt">The local date and time at which the activity occurred. The offset from Greenwich mean time is also indicated.</param>
/// <param name="RequestAndProtocol">The HTTP protocol version that the client used.</param>
/// <param name="ServiceStatuscode">The HTTP status code. (A value of 200 indicates that the request completed successfully.)</param>
/// <param name="BytesSent">The number of bytes sent by the server.</param>
/// <param name="ClientInformation">Client information.</param>
public record LogItemModel(
    string RemoteHostAddress,
    string RemoteLogName,
    string Username,
    DateTimeOffset DateTimeAsGmt,
    string RequestAndProtocol,
    string ServiceStatuscode,
    double BytesSent,
    string ClientInformation);
