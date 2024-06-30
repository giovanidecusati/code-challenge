using HttpLogParser.ConsoleApp.Engine.Abstractions;
using System.Text;

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
        var items = new List<LogItemModel>();

        // replaced to support large files
        //var lines = File.ReadAllLines(path);
        //foreach (var line in lines)
        //{
        //    var item = _logrowparser.ParseFromString(line);
        //    items.Add(item);
        //}

        // Enable support to large files
        const int BufferSize = 128;
        using (var fileStream = File.OpenRead(path))
        using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
        {
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                var item = _logrowparser.ParseFromString(line);
                items.Add(item);
            }
        }

        return items.AsReadOnly();
    }

}
