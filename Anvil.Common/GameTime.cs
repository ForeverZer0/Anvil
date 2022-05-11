using System.Diagnostics;
using JetBrains.Annotations;

namespace Anvil;

/// <summary>
/// Method signature for objects that handle tick events.
/// </summary>
/// <param name="time">A <see cref="GameTime"/> instance that is providing the tick.</param>
public delegate void TickHandler(GameTime time);

/// <summary>
/// Provides methods for maintaining a high-resolution timer and measuring frame/tick times.
/// </summary>
[PublicAPI, DebuggerDisplay("Elapsed = {Elapsed}")]
public sealed class GameTime : IEquatable<GameTime>, IComparable<GameTime>, IComparable
{
    /// <summary>
    /// Represents one second of time.
    /// </summary>
    private static readonly TimeSpan OneSecond = TimeSpan.FromSeconds(1);

    /// <summary>
    /// A <see cref="TimeSpan"/> value indicating that the <see cref="GameTime"/> should tick as fast as possible.
    /// </summary>
    public static readonly TimeSpan Unlimited = TimeSpan.FromSeconds(-1);

    /// <summary>
    /// Occurs when the timer has elapsed enough to time to "tick", or when called manually with <see cref="DoTick"/>.
    /// </summary>
    public event TickHandler? Tick;

    /// <summary>
    /// Gets the delta value (the elapsed time between ticks).
    /// </summary>
    public TimeSpan Delta => delta;

    /// <summary>
    /// Gets the <see cref="Delta"/> value as a <see cref="float"/>.
    /// </summary>
    public float DeltaF => (float) delta.TotalSeconds;

    /// <summary>
    /// Gets the alpha value, a linear value between <c>0.0</c> and <c>1.0</c> indicating the ratio between the elapsed
    /// time in the current tick to the target delta.
    /// </summary>
    /// <remarks>
    /// This is most often used for computing logic "between frames", such as physics, movement prediction, etc.
    /// </remarks>
    public double Alpha => stopwatch.Elapsed / targetDelta;

    /// <summary>
    /// Gets a value indicating if the <see cref="GameTime"/> is currently elapsing time.
    /// </summary>
    public bool IsRunning => stopwatch.IsRunning;

    /// <summary>
    /// Gets the total elapsed time the <see cref="GameTime"/> has been running.
    /// </summary>
    public TimeSpan TotalElapsed => totalElapsed;
    
    /// <summary>
    /// Gets the elapsed time since the last tick event.
    /// </summary>
    public TimeSpan Elapsed => stopwatch.Elapsed;

    /// <summary>
    /// Gets the target number of ticks per second.
    /// </summary>
    public double TickRate { get; private set; }

    /// <summary>
    /// Creates a new instance of the <see cref="GameTime"/> class that ticks as fast as possible.
    /// </summary>
    /// <param name="startNow">Flag indicating if time should start being elapsed immediately.</param>
    public GameTime(bool startNow = false) : this(Unlimited, startNow)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the <see cref="GameTime"/> class.
    /// </summary>
    /// <param name="tps">The desired number of "ticks per second", or <c>-1.0</c> to tick as fast as possible.</param>
    /// <param name="startNow">Flag indicating if time should start being elapsed immediately.</param>
    /// <exception cref="DivideByZeroException">When <paramref name="tps"/> is <c>0.0</c>.</exception>
    public GameTime(double tps, bool startNow = false) : this(TimeSpan.FromSeconds(1.0 / tps), startNow)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="GameTime"/> class.
    /// </summary>
    /// <param name="delta">The desired time between ticks.</param>
    /// <param name="startNow">Flag indicating if time should start being elapsed immediately.</param>
    /// <exception cref="DivideByZeroException">When <paramref name="delta"/> is <see cref="TimeSpan.Zero"/>.</exception>
    public GameTime(TimeSpan delta, bool startNow = false)
    {
        if (delta == TimeSpan.Zero)
            throw new DivideByZeroException("Delta cannot be zero.");
        
        stopwatch = new Stopwatch();
        targetDelta = delta < -OneSecond ? -OneSecond : delta;
        if (startNow)
            stopwatch.Start();
    }

    /// <summary>
    /// Begins measuring elapsed time.
    /// </summary>
    public void Start() => stopwatch.Start();

    /// <summary>
    /// Stops measuring elapsed time.
    /// </summary>
    public void Stop() => stopwatch.Stop();
    
    /// <summary>
    /// Restarts the timings, changing the target rate to a new target rate.
    /// </summary>
    /// <param name="newTps">The desired number of "ticks per second", or <c>-1.0</c> to tick as fast as possible.</param>
    /// <exception cref="DivideByZeroException">When <paramref name="newTps"/> is <c>0.0</c>.</exception>
    public void Restart(double newTps) => Restart(TimeSpan.FromSeconds(1.0 / newTps));

    /// <summary>
    /// Restarts the timings, changing the target rate to a new delta value.
    /// </summary>
    /// <param name="newDelta">The desired time between ticks.</param>
    /// <exception cref="DivideByZeroException">When <paramref name="newDelta"/> is <see cref="TimeSpan.Zero"/>.</exception>
    public void Restart(TimeSpan newDelta)
    {
        if (newDelta == TimeSpan.Zero)
            throw new DivideByZeroException("Delta cannot be zero.");
        
        targetDelta = newDelta < Unlimited ? Unlimited : newDelta;
        rateCounter = TimeSpan.Zero;
        tickCounter = 0;
        
        stopwatch.Restart();
    }

    /// <summary>
    /// Restarts the timings, keeping the same target rate.
    /// </summary>
    public void Restart() => Restart(targetDelta);
    
    /// <summary>
    /// Gets a flag indicating if the <see cref="GameTime"/> has elapsed enough time to tick.
    /// </summary>
    public bool ShouldTick => stopwatch.Elapsed >= targetDelta;

    /// <summary>
    /// Performs a "tick" operation, resetting the measurements for the next iteration and updating the TPS values (if
    /// applicable).
    /// </summary>
    public void DoTick()
    {
        delta = stopwatch.Elapsed;
        totalElapsed += delta;
        
        tickCounter++;
        rateCounter += delta;
        
        if (rateCounter >= OneSecond)
        {
            TickRate = tickCounter;
            rateCounter = TimeSpan.Zero;
            tickCounter = 0;
        }
        
        stopwatch.Restart();
        Tick?.Invoke(this);
    }

    /// <summary>
    /// Checks if enough time has elapsed to perform a tick, and performs one if so.
    /// </summary>
    /// <returns><c>true</c> if a tick was performed, otherwise <c>false</c>.</returns>
    public bool TryDoTick()
    {
        if (!ShouldTick)
            return false;
        DoTick();
        return true;
    }

    /// <inheritdoc />
    public override string ToString() => $"{TotalElapsed} (TPS:{TickRate})";

    /// <inheritdoc />
    public bool Equals(GameTime? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || stopwatch.Equals(other.stopwatch);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is GameTime other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => stopwatch.GetHashCode();

    /// <inheritdoc />
    public int CompareTo(GameTime? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        return ReferenceEquals(null, other) ? 1 : totalElapsed.CompareTo(other.totalElapsed);
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj)) return 1;
        if (ReferenceEquals(this, obj)) return 0;
        return obj is GameTime other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(GameTime)}");
    }
    
    public static bool operator ==(GameTime? left, GameTime? right) => Equals(left, right);

    public static bool operator !=(GameTime? left, GameTime? right) => !Equals(left, right);

    public static bool operator <(GameTime? left, GameTime? right)
    {
        return Comparer<GameTime>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(GameTime? left, GameTime? right)
    {
        return Comparer<GameTime>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(GameTime? left, GameTime? right)
    {
        return Comparer<GameTime>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(GameTime? left, GameTime? right)
    {
        return Comparer<GameTime>.Default.Compare(left, right) >= 0;
    }

    private readonly Stopwatch stopwatch;
    private int tickCounter;
    private TimeSpan delta;
    private TimeSpan targetDelta;
    private TimeSpan rateCounter;
    private TimeSpan totalElapsed;
}