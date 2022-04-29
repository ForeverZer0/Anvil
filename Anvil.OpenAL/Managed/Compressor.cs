using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <inheritdoc cref="EffectType.Compressor"/>
[PublicAPI]
public class Compressor : AudioEffect
{
    /// <summary>
    /// Creates a new instance of the <see cref="Compressor"/> class.
    /// </summary>
    /// <remarks>Automatically creates and wraps the underlying <see cref="Effect"/> object.</remarks>
    protected Compressor() : base(AL.GenEffect(EffectType.Compressor))
    {
    }
    
    /// <summary>
    /// Creates a new <see cref="Compressor"/> instance.
    /// </summary>
    /// <param name="handle">The effect to wrap.</param>
    protected internal Compressor(Effect handle) : base(handle)
    {
    }

    public bool Enabled
    {
        get => AL.GetEffectB(Handle, CompressorParam.Enabled);
        set => SetParam(CompressorParam.Enabled, value);
    }

    /// <inheritdoc />
    public override void Restore()
    {
        SetParam(CompressorParam.Enabled, true);
    }
}