using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <summary>
/// Represents an audio effect that is capable of providing reverberations to the output sound. 
/// </summary>
/// <seealso cref="Reverb"/>
/// <seealso cref="EaxReverb"/>
[PublicAPI]
public interface IReverb
{
	/// <inheritdoc cref="ReverbProperties.Density" />
	float Density { get; set; }

	/// <inheritdoc cref="ReverbProperties.Diffusion" />
	float Diffusion { get; set; }

	/// <inheritdoc cref="ReverbProperties.Gain" />
	float Gain { get; set; }

	/// <inheritdoc cref="ReverbProperties.GainHF" />
	float GainHF { get; set; }

	/// <inheritdoc cref="ReverbProperties.DecayTime" />
	float DecayTime { get; set; }

	/// <inheritdoc cref="ReverbProperties.DecayHFRatio" />
	float DecayHFRatio { get; set; }

	/// <inheritdoc cref="ReverbProperties.ReflectionsGain" />
	float ReflectionsGain { get; set; }

	/// <inheritdoc cref="ReverbProperties.ReflectionsDelay" />
	float ReflectionsDelay { get; set; }

	/// <inheritdoc cref="ReverbProperties.LateReverbGain" />
	float LateReverbGain { get; set; }

	/// <inheritdoc cref="ReverbProperties.LateReverbDelay" />
	float LateReverbDelay { get; set; }

	/// <inheritdoc cref="ReverbProperties.AirAbsorptionGainHF" />
	float AirAbsorptionGainHF { get; set; }

	/// <inheritdoc cref="ReverbProperties.RoomRolloffFactor" />
	float RoomRolloffFactor { get; set; }

	/// <inheritdoc cref="ReverbProperties.DecayHFLimit" />
	bool DecayHFLimit { get; set; }

	/// <summary>
	/// Gets or sets an object that describes the configuration of an <see cref="IReverb"/> instance, and can be used
	/// to restore it.
	/// </summary>
	ReverbProperties Preset { get; set; }
}