using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <summary>
/// Abstract base class for object-oriented wrappers encapsulating an OpenAL <see cref="Filter"/> object.
/// </summary>
[PublicAPI]
public abstract class AudioFilter : AudioHandle<Filter>
{
    /// <summary>
    /// Cache to store dynamically created and compiled activator delegates.
    /// </summary>
    private static readonly Dictionary<Type, Func<AudioFilter>> activatorCache;

    /// <summary>
    /// Static constructor.
    /// </summary>
    static AudioFilter()
    {
        activatorCache = new Dictionary<Type, Func<AudioFilter>>();
    }

    /// <summary>
    /// Creates a new <see cref="Filter"/> and wraps it as a <see cref="AudioFilter"/> instance.
    /// </summary>
    protected AudioFilter() : this(AL.GenFilter())
    {
    }
    
    /// <inheritdoc />
    protected AudioFilter(Filter handle) : base(handle)
    {
    }

    /// <summary>
    /// Factory method to create instances of audio filters based on the specified <paramref name="type"/>.
    /// </summary>
    /// <param name="type">A constant describing the type of <see cref="AudioFilter"/> to create.</param>
    /// <returns>A new instance of an <see cref="AudioFilter"/> with a compatible derived type.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// When <paramref name="type"/> is <see cref="FilterType.None"/> or an unnamed value.
    /// </exception>
    public static AudioFilter Factory(FilterType type)
    {
        return type switch
        {
            FilterType.Lowpass => Factory<LowpassFilter>(),
            FilterType.Highpass => Factory<HighpassFilter>(),
            FilterType.Bandpass => Factory<BandpassFilter>(),
            FilterType.None => throw new ArgumentOutOfRangeException(nameof(type), "None is not a valid filter type."),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    /// <summary>
    /// Factory method to create instances of audio filters based on the specified type.
    /// </summary>
    /// <typeparam name="TFilter">A type derived from <see cref="AudioFilter"/> with a parameterless constructor.</typeparam>
    /// <returns>A new instance of an <see cref="AudioFilter"/> with a compatible derived type.</returns>
    public static TFilter Factory<TFilter>() where TFilter : AudioFilter, new()
    {
        var type = typeof(TFilter);
        if (!activatorCache.TryGetValue(type, out var activator))
        {
            activator = Emit.Ctor<Func<AudioFilter>>(type, Emit.PublicAndPrivate);
            activatorCache.Add(typeof(TFilter), activator);
        }
        return (TFilter) activator.Invoke();
    }

    internal static AudioFilter? Wrap(int id)
    {
        if (id == 0)
            return null;
		
        var filter = Unsafe.As<int, Filter>(ref id);
        var type = AL.GetFilterI<FilterType>(filter, FilterProperty.Type);

        return type switch
        {
            FilterType.None => null,
            FilterType.Lowpass => new LowpassFilter(filter),
            FilterType.Highpass => new HighpassFilter(filter),
            FilterType.Bandpass => new BandpassFilter(filter),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    /// <summary>
    /// TODO
    /// </summary>
    public abstract float Gain { get; set; }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
            AL.DeleteFilter(Handle);	
    }

    /// <inheritdoc />
    public override bool IsValid => AL.IsFilter(Handle);
}

/// <summary>
/// Represents an object that can filter high-frequency content from a signal. 
/// </summary>
public interface ILowpassFilter
{
    /// <summary>
    /// TODO
    /// </summary>
    float GainHF { get; set; }
}

/// <summary>
/// Represents an object that can filter low-frequency content from a signal. 
/// </summary>
public interface IHighpassFilter
{
    /// <summary>
    /// TODO
    /// </summary>
    float GainLF { get; set; }
}

/// <summary>
/// A low-pass filter used to remove high-frequency content from a signal. 
/// </summary>
public class LowpassFilter : AudioFilter, ILowpassFilter
{
    /// <summary>
    /// Creates a new <see cref="Filter"/> and wraps it as a <see cref="LowpassFilter"/> instance.
    /// </summary>
    public LowpassFilter() : base(AL.GenFilter(FilterType.Lowpass))
    {
    }
    
    /// <inheritdoc />
    protected internal LowpassFilter(Filter handle) : base(handle)
    {
    }

    /// <inheritdoc />
    public override float Gain
    {
        get => AL.GetFilterF(Handle, LowpassParam.Gain);
        set => AL.FilterF(Handle, LowpassParam.Gain, Math.Clamp(value, 0.0f, 1.0f));
    }

    /// <inheritdoc />
    public float GainHF
    {
        get => AL.GetFilterF(Handle, LowpassParam.GainHF);
        set => AL.FilterF(Handle, LowpassParam.GainHF, Math.Clamp(value, 0.0f, 1.0f));
    }
}

/// <summary>
/// A high-pass filter used to remove low-frequency content from a signal. 
/// </summary>
public class HighpassFilter : AudioFilter, IHighpassFilter
{
    /// <summary>
    /// Creates a new <see cref="Filter"/> and wraps it as a <see cref="HighpassFilter"/> instance.
    /// </summary>
    public HighpassFilter() : base(AL.GenFilter(FilterType.Highpass))
    {
    }
    
    /// <inheritdoc />
    protected internal HighpassFilter(Filter handle) : base(handle)
    {
    }
    
    /// <inheritdoc />
    public override float Gain
    {
        get => AL.GetFilterF(Handle, HighpassParam.Gain);
        set => AL.FilterF(Handle, HighpassParam.Gain, Math.Clamp(value, 0.0f, 1.0f));
    }
    
    /// <inheritdoc />
    public float GainLF
    {
        get => AL.GetFilterF(Handle, HighpassParam.GainLF);
        set => AL.FilterF(Handle, HighpassParam.GainLF, Math.Clamp(value, 0.0f, 1.0f));
    }
}

/// <summary>
/// A band-pass filter used to remove high and low frequency content from a signal. 
/// </summary>
public class BandpassFilter : AudioFilter, ILowpassFilter, IHighpassFilter
{
    /// <summary>
    /// Creates a new <see cref="Filter"/> and wraps it as a <see cref="BandpassFilter"/> instance.
    /// </summary>
    public BandpassFilter() : this(AL.GenFilter(FilterType.Bandpass))
    {
    }
    
    /// <inheritdoc />
    protected internal BandpassFilter(Filter handle) : base(handle)
    {
    }
    
    /// <inheritdoc />
    public override float Gain
    {
        get => AL.GetFilterF(Handle, BandpassParam.Gain);
        set => AL.FilterF(Handle, BandpassParam.Gain, Math.Clamp(value, 0.0f, 1.0f));
    }
    
    /// <inheritdoc />
    public float GainHF
    {
        get => AL.GetFilterF(Handle, BandpassParam.GainHF);
        set => AL.FilterF(Handle, BandpassParam.GainHF, Math.Clamp(value, 0.0f, 1.0f));
    }
    
    /// <inheritdoc />
    public float GainLF
    {
        get => AL.GetFilterF(Handle, BandpassParam.GainLF);
        set => AL.FilterF(Handle, BandpassParam.GainLF, Math.Clamp(value, 0.0f, 1.0f));
    }
}