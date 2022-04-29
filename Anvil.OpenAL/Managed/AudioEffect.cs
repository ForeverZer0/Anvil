using System.Numerics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <summary>
/// Abstract base class for object-oriented wrappers encapsulating an OpenAL <see cref="Effect"/> object.
/// </summary>
[PublicAPI]
public abstract class AudioEffect : AudioHandle<Effect>
{
    /// <summary>
    /// Cache to store dynamically created and compiled activator delegates.
    /// </summary>
    private static readonly Dictionary<Type, Func<AudioEffect>> activatorCache;
    
    /// <summary>
    /// Static constructor.
    /// </summary>
    static AudioEffect()
    {
        activatorCache = new Dictionary<Type, Func<AudioEffect>>();
    }

    /// <summary>
    /// Factory method to create instances of audio effects based on the specified type.
    /// </summary>
    /// <typeparam name="TEffect">A type derived from <see cref="AudioEffect"/> with a parameterless constructor.</typeparam>
    /// <returns>A new instance of an <see cref="AudioEffect"/> with a compatible derived type.</returns>
    public static TEffect Factory<TEffect>() where TEffect : AudioEffect, new()
    {
        var type = typeof(TEffect);
        if (!activatorCache.TryGetValue(type, out var activator))
        {
            activator = Anvil.Factory.CreateActivator<TEffect>();
            activatorCache.Add(type, activator);
        }
        return (TEffect) activator.Invoke();
    }

    /// <summary>
    /// Factory method to create instances of audio effects based on the specified <paramref name="type"/>.
    /// </summary>
    /// <param name="type">A constant describing the type of <see cref="AudioEffect"/> to create.</param>
    /// <returns>A new instance of an <see cref="AudioEffect"/> with a compatible derived type.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// When <paramref name="type"/> is <see cref="EffectType.None"/> or an unnamed value.
    /// </exception>
    public static AudioEffect Factory(EffectType type)
    {
        return type switch
        {
            EffectType.Reverb => Factory<Reverb>(),
            EffectType.Chorus => Factory<Chorus>(),
            EffectType.Distortion => Factory<Distortion>(),
            EffectType.Echo => Factory<Echo>(),
            EffectType.Flanger => Factory<Flanger>(),
            EffectType.FrequencyShifter => Factory<FrequencyShifter>(),
            EffectType.VocalMorpher => Factory<VocalMorpher>(),
            EffectType.PitchShifter => Factory<PitchShifter>(),
            EffectType.RingModulator => Factory<RingModulator>(),
            EffectType.Autowah => Factory<Autowah>(),
            EffectType.Compressor => Factory<Compressor>(),
            EffectType.Equalizer => Factory<Equalizer>(),
            EffectType.EaxReverb => Factory<EaxReverb>(),
            EffectType.None => throw new ArgumentOutOfRangeException(nameof(type), "None is not a valid effect type."),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    internal static AudioEffect? Wrap(int id)
    {
        if (id == 0)
            return null;

        var effect = Unsafe.As<int, Effect>(ref id);
        var type = AL.GetEffectI<EffectType>(effect, EffectProperty.Type);
        return type switch
        {
            EffectType.Reverb => new Reverb(effect),
            EffectType.Chorus => new Chorus(effect),
            EffectType.Distortion => new Distortion(effect),
            EffectType.Echo => new Echo(effect),
            EffectType.Flanger => new Flanger(effect),
            EffectType.FrequencyShifter => new FrequencyShifter(effect),
            EffectType.VocalMorpher => new VocalMorpher(effect),
            EffectType.PitchShifter => new PitchShifter(effect),
            EffectType.RingModulator => new RingModulator(effect),
            EffectType.Autowah => new Autowah(effect),
            EffectType.Compressor => new Compressor(effect),
            EffectType.Equalizer => new Equalizer(effect),
            EffectType.EaxReverb => new EaxReverb(effect),
            EffectType.None => null,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Occurs when a parameter that effects the output sound is changed.
    /// </summary>
    public event EffectParamHandler? ParameterChanged;
    
    /// <inheritdoc />
    protected AudioEffect(Effect handle) : base(handle)
    {
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
            AL.DeleteEffect(Handle);
    }

    /// <inheritdoc />
    public override bool IsValid => AL.IsEffect(Handle);
    
    /// <summary>
    /// Restores the <see cref="AudioEffect"/> to its default configuration of parameters.
    /// </summary>
    /// <remarks>This invokes the <see cref="ParameterChanged"/> event only once, not for each parameter.</remarks>
    public abstract void Restore();
    
    /// <summary>
    /// Sets a <see cref="float"/> parameter.
    /// </summary>
    /// <param name="param">The parameter index to change.</param>
    /// <param name="value">The value to set.</param>
    /// <param name="min">The minimum valid value.</param>
    /// <param name="max">The maximum valid value.</param>
    /// <typeparam name="TEnum">An enum value whose type has a 32-bit backing store.</typeparam>
    /// <remarks>Automatically clamps values and calls <see cref="OnParameterChanged{TEnum}"/>.</remarks>
    protected void SetParam<TEnum>(TEnum param, float value, float min, float max) where TEnum : struct, Enum
    {
        AL.EffectF(Handle, Unsafe.As<TEnum, EffectProperty>(ref param), Math.Clamp(value, min, max));
        OnParameterChanged(param);
    }
	
    /// <summary>
    /// Sets a <see cref="int"/> parameter.
    /// </summary>
    /// <param name="param">The parameter index to change.</param>
    /// <param name="value">The value to set.</param>
    /// <param name="min">The minimum valid value.</param>
    /// <param name="max">The maximum valid value.</param>
    /// <typeparam name="TEnum">An enum value whose type has a 32-bit backing store.</typeparam>
    /// <remarks>Automatically clamps values and calls <see cref="OnParameterChanged{TEnum}"/>.</remarks>
    protected void SetParam<TEnum>(TEnum param, int value, int min, int max) where TEnum : struct, Enum
    {
        AL.EffectI(Handle, Unsafe.As<TEnum, EffectProperty>(ref param), Math.Clamp(value, min, max));
        OnParameterChanged(param);
    }
	
    /// <summary>
    /// Sets a <see cref="bool"/> parameter.
    /// </summary>
    /// <param name="param">The parameter index to change.</param>
    /// <param name="value">The value to set.</param>
    /// <typeparam name="TEnum">An enum value whose type has a 32-bit backing store.</typeparam>
    /// <remarks>Automatically clamps values and calls <see cref="OnParameterChanged{TEnum}"/>.</remarks>
    protected void SetParam<TEnum>(TEnum param, bool value) where TEnum : struct, Enum
    {
        AL.EffectI(Handle, Unsafe.As<TEnum, EffectProperty>(ref param), value ? AL.TRUE : AL.FALSE);
        OnParameterChanged(param);
    }
	
    /// <summary>
    /// Sets a <see cref="int"/> parameter that is represented by an enumeration value.
    /// </summary>
    /// <param name="param">The parameter index to change.</param>
    /// <param name="value">The value to set.</param>
    /// <param name="min">The minimum valid value.</param>
    /// <param name="max">The maximum valid value.</param>
    /// <typeparam name="TEnum">An enum value whose type has a 32-bit backing store.</typeparam>
    /// <typeparam name="T">An enum value whose type has a 32-bit backing store.</typeparam>
    /// <remarks>Automatically clamps values and calls <see cref="OnParameterChanged{TEnum}"/>.</remarks>
    protected void SetParam<TEnum, T>(TEnum param, T value, int min, int max) where TEnum : struct, Enum where T : struct, Enum
    {
        var i = Unsafe.As<T, int>(ref value);
        AL.EffectI(Handle, Unsafe.As<TEnum, EffectProperty>(ref param), Math.Clamp(i, min, max));
        OnParameterChanged(param);
    }

    /// <summary>
    /// Sets a <see cref="Vector3"/> parameter.
    /// </summary>
    /// <param name="param">The parameter index to change.</param>
    /// <param name="value">The value to set.</param>
    /// <typeparam name="TEnum">An enum value whose type has a 32-bit backing store.</typeparam>
    /// <remarks>Automatically clamps values and calls <see cref="OnParameterChanged{TEnum}"/>.</remarks>
    protected void SetParam<TEnum>(TEnum param, Vector3 value) where TEnum : struct, Enum
    {
        AL.EffectF(Handle, Unsafe.As<TEnum, EffectProperty>(ref param), value);
        OnParameterChanged(param);
    }

    /// <summary>
    /// Invokes the <see cref="ParameterChanged"/> event with the specified parameter index.
    /// </summary>
    /// <param name="param">An enumeration describing the parameter that was changed.</param>
    /// <typeparam name="TEnum">An enum value whose type has a 32-bit backing store.</typeparam>
    protected void OnParameterChanged<TEnum>(TEnum param) where TEnum : struct, Enum
    {
        ParameterChanged?.Invoke(this, Unsafe.As<TEnum, int>(ref param));
    }
	
    /// <summary>
    /// Invokes the <see cref="ParameterChanged"/> event with the specified parameter index.
    /// </summary>
    /// <param name="param">
    /// The index of the parameter that was changed, or <c>-1</c> to indicate multiple/all parameters were changed (i.e.
    /// a call to <see cref="Restore"/> or setting via a preset).
    /// </param>
    protected void OnParameterChanged(int param = -1)
    {
        ParameterChanged?.Invoke(this, param);
    }
}