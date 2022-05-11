using System.Collections.Concurrent;

namespace Anvil.Logging;

public delegate void LogHandler(ILogger logger, LogLevel level, string entry, Exception? exception = null);

public static class LogManager
{
    private static readonly ConcurrentDictionary<string, ILogger> loggers;
    private static readonly object addLock;

    static LogManager()
    {
        loggers = new ConcurrentDictionary<string, ILogger>();
        addLock = new object();
    }

    public static ILogger GetLogger(Type type) => GetLogger(type.FullName ?? type.Name);

    public static ILogger GetLogger(string name)
    {
        if (loggers.TryGetValue(name, out var logger))
            return logger;

        logger = new Logger(AddEntry);
        loggers.TryAdd(name, logger);

        return logger;
    }

    public static ILogger GetLogger<T>() => GetLogger(typeof(T));

    private static void AddEntry(ILogger logger, LogLevel level, string entry, Exception? exception = null)
    {
        lock (addLock)
        {
            // TODO:
            Console.WriteLine(entry);
        }
    }
}