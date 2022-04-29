using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <summary>
/// Object-oriented wrapper encapsulating an OpenAL <see cref="EffectSlot"/> object.
/// </summary>
[PublicAPI]
public class AudioEffectSlot : AudioHandle<EffectSlot>
{
    /// <summary>
    /// Gets or sets the <see cref="AudioEffect"/> feeding this <see cref="AudioEffectSlot"/>.
    /// </summary>
    public AudioEffect? Effect
    {
        get
        {
            var id = AL.GetAuxiliaryEffectSlotI(Handle, EffectSlotProperty.Effect);
            return AudioEffect.Wrap(id);
        }
        set => AL.AuxiliaryEffectSlotI(Handle, EffectSlotProperty.Effect, value?.Handle ?? default);
    }

    public float Gain
    {
        get => AL.GetAuxiliaryEffectSlotF(Handle, EffectSlotProperty.Gain);
        set => AL.AuxiliaryEffectSlotF(Handle, EffectSlotProperty.Gain, Math.Clamp(value, 0.0f, 1.0f));
    }

    public bool SendAuto
    {
        get => AL.GetAuxiliaryEffectSlotI(Handle, EffectSlotProperty.AuxiliarySendAuto) != 0;
        set => AL.AuxiliaryEffectSlotI(Handle, EffectSlotProperty.AuxiliarySendAuto, value ? 1 : 0);
    }
	
    /// <summary>
    /// Creates a new <see cref="EffectSlot"/> and wraps it as a <see cref="AudioEffectSlot"/> instance.
    /// </summary>
    public AudioEffectSlot() : this(AL.GenAuxiliaryEffectSlot())
    {
    }
	
    /// <summary>
    /// Creates a new <see cref="EffectSlot"/> and wraps it as a <see cref="AudioEffectSlot"/> instance.
    /// </summary>
    /// <param name="effect">An <see cref="AudioEffect"/> to attach to this slot.</param>
    public AudioEffectSlot(AudioEffect? effect) : this(AL.GenAuxiliaryEffectSlot())
    {
        if (effect is not null)
            AL.AuxiliaryEffectSlotI(Handle, EffectSlotProperty.Effect, effect.Handle);
    }
    
    /// <inheritdoc />
    protected internal AudioEffectSlot(EffectSlot handle) : base(handle)
    {
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
            AL.DeleteAuxiliaryEffectSlot(Handle);
    }

    /// <inheritdoc />
    public override bool IsValid => AL.IsAuxiliaryEffectSlot(Handle);
}