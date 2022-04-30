namespace Anvil.Logging;

/// <summary>
/// Concrete implementation of a generic <see cref="ILogger"/> class.
/// </summary>
public class Logger : ILogger
{
    private readonly LogHandler handler;
    
    /// <summary>
    /// Creates a new instance of the <see cref="Logger"/> class with the specified <paramref name="handler"/>.
    /// </summary>
    /// <param name="handler">A delegate that handles log messages.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="handler"/> is <c>null</c>.</exception>
    public Logger(LogHandler handler)
    {
        this.handler = handler ?? throw new ArgumentNullException(nameof(handler));
    }

    /// <inheritdoc />
    public void Fatal(string entry) => handler.Invoke(this, LogLevel.Fatal, entry);

    /// <inheritdoc />
    public void Fatal(Exception exception, string entry) => handler.Invoke(this, LogLevel.Fatal, entry, exception);

    /// <inheritdoc />
    public void Error(string entry) => handler.Invoke(this, LogLevel.Error, entry);

    /// <inheritdoc />
    public void Error(Exception exception, string entry) => handler.Invoke(this, LogLevel.Error, entry, exception);

    /// <inheritdoc />
    public void Warn(string entry) => handler.Invoke(this, LogLevel.Warn, entry);

    /// <inheritdoc />
    public void Info(string entry) => handler.Invoke(this, LogLevel.Info, entry);

    /// <inheritdoc />
    public void Debug(string entry) => handler.Invoke(this, LogLevel.Debug, entry);

    /// <inheritdoc />
    public void Trace(string entry) => handler.Invoke(this, LogLevel.Trace, entry);
}