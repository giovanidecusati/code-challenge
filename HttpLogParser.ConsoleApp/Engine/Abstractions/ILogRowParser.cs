namespace HttpLogParser.ConsoleApp.Engine.Abstractions;

public interface ILogRowParser
{
    LogItemModel ParseFromString(string log);
}