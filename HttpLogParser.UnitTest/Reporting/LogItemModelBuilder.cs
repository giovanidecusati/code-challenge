using HttpLogParser.ConsoleApp.Engine;

namespace HttpLogParser.UnitTest.Reporting;
internal class LogItemModelBuilder
{
    private List<LogItemModel> _list;

    public LogItemModelBuilder()
    {
        _list = new List<LogItemModel>();
    }

    public LogItemModelBuilder With(string RemoteHostAddress, string RequestAndProtocol)
    {
        _list.Add(new LogItemModel(
            RemoteHostAddress,
            string.Empty,
            string.Empty,
            DateTimeOffset.MinValue,
            RequestAndProtocol,
            string.Empty,
            double.MinValue,
            string.Empty));

        return this;
    }

    public IReadOnlyList<LogItemModel> Build()
    {
        return _list;
    }
}
