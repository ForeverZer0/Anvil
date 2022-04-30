using JetBrains.Annotations;

namespace Anvil.Logging;

/// <summary>
/// Represents am object capable of handling log messages.
/// </summary>
[PublicAPI]
public interface ILogger
{
    /// <summary>
    /// Logs an error that is incapable of being handled and is causing an application crash.
    /// </summary>
    /// <param name="entry">The log entry to add.</param>
    void Fatal(string entry);
    
    /// <summary>
    /// Logs an error that is incapable of being handled and is causing an application crash.
    /// </summary>
    /// <param name="exception">An exception that describes the error.</param>
    /// <param name="entry">The log entry to add.</param>
    void Fatal(Exception exception, string entry);

    /// <summary>
    /// Logs an error that is capable of being handled to prevent a crash.
    /// </summary>
    /// <param name="entry">The log entry to add.</param>
    void Error(string entry);

    /// <summary>
    /// Logs an error that is capable of being handled to prevent a crash.
    /// </summary>
    /// <param name="exception">An exception that describes the error.</param>
    /// <param name="entry">The log entry to add.</param>
    void Error(Exception exception, string entry);

    /// <summary>
    /// Logs a possible issue that may need corrected or lead to possible errors.
    /// </summary>
    /// <param name="entry">The log entry to add.</param>
    void Warn(string entry);

    /// <summary>
    /// Logs a general informational message.
    /// </summary>
    /// <param name="entry">The log entry to add.</param>
    void Info(string entry);

    /// <summary>
    /// Logs a message regarding internal state of the application for debugging purposed.
    /// </summary>
    /// <param name="entry">The log entry to add.</param>
    void Debug(string entry);

    /// <summary>
    /// Logs verbose messages regarding internal state for profiling purposes.
    /// </summary>
    /// <param name="entry">The log entry to add.</param>
    void Trace(string entry);
}