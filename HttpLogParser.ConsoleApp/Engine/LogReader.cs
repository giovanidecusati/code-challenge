using HttpLogParser.ConsoleApp.Engine.Abstractions;

namespace HttpLogParser.ConsoleApp.Engine;
public class LogReader
{
    private readonly ILogRowParser _logrowparser;

    public LogReader(ILogRowParser logrowparser)
    {
        _logrowparser = logrowparser;
    }

    public IReadOnlyList<LogItemModel> ReadFromFile(string path)
    {
        var lines = File.ReadAllLines(path);
        var items = new List<LogItemModel>();
        foreach (var line in lines)
        {
            var item = _logrowparser.ParseFromString(line);
            items.Add(item);
        }

        return items.AsReadOnly();
    }

}
