using System.Collections.Concurrent;

namespace Anvil.Logging;

public delegate void LogHandler(ILogger logger, LogLevel level, string entry, Exception? exception = null);

public static class LogManager
{
    private static readonly ConcurrentDictionary<Type, ILogger> loggers;
    private static readonly object addLock;

    static LogManager()
    {
        loggers = new ConcurrentDictionary<Type, ILogger>();
        addLock = new object();
    }

    public static ILogger GetLogger(Type type)
    {
        if (loggers.TryGetValue(type, out var logger))
            return logger;

        logger = new Logger(AddEntry); // TODO: Type
        if (!loggers.TryAdd(type, logger))
            Console.Error.Write($"Failed to register logger for type {type}.");

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