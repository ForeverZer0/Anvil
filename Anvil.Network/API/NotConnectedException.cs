namespace Anvil.Network.API;

/// <summary>
/// Exception class used when an attempt is made to transmit data without a network connection present.
/// </summary>
public class NotConnectedException : Exception
{
    /// <summary>
    /// Creates a new instance of the <see cref="NotConnectedException"/> class.
    /// </summary>
    public NotConnectedException()
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="NotConnectedException"/> class.
    /// </summary>
    /// <param name="message">The message to supply with the exception.</param>
    public NotConnectedException(string? message) : base(message) 
    {
    }
}