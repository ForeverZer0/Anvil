using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security;
using Anvil.Native;
using JetBrains.Annotations;
// ReSharper disable StringLiteralTypo

namespace Anvil.OpenAL;


[PublicAPI]
public enum EffectProperty
{
	FirstParameter                = 0x0000,
	LastParameter                 = 0x8000,
	Type                           = 0x8001,
}

[PublicAPI]
public enum ParameterType
{
	Float,
	Integer,
	Enumeration,
	Boolean,
	Vector,
	Preset
}





[SuppressUnmanagedCodeSecurity]
public static unsafe partial class AL
{
	
	[NativeMethod("alGenEffects"), Pure]
	public static Effect GenEffect()
	{
		Effect effect = default;
		alGenEffects(1, &effect);
		CheckErrorState();
		return effect;
	}
	
	[NativeMethod("alGenEffects"), NativeMethod("alEffecti"), Pure]
	public static Effect GenEffect(EffectType type)
	{
		Effect effect = default;
		alGenEffects(1, &effect);
		alEffecti(effect, EffectProperty.Type, Unsafe.As<EffectType, int>(ref type));
		CheckErrorState();
		return effect;
	}

	public static void DeleteEffect(Effect effect)
	{
		alDeleteEffects(1, &effect);
		CheckErrorState();
	}

	[NativeMethod("alGenEffects"), CLSCompliant(false)]
	public static void GenEffects(int n, Effect *effects)
	{
		alGenEffects(n, effects);
		CheckErrorState();
	}

	[NativeMethod("alGenEffects")]
	public static void GenEffects(Span<Effect> effects)
	{
		fixed (Effect* ptr = &effects[0])
		{
			alGenEffects(effects.Length, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alGenEffects")]
	public static void GenEffects(Effect[] effects)
	{
		fixed (Effect* ptr = &effects[0])
		{
			alGenEffects(effects.Length, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alDeleteEffects"), CLSCompliant(false)]
	public static void DeleteEffects(int n, Effect  *effects)
	{
		alDeleteEffects(n, effects);
		CheckErrorState();
	}

	[NativeMethod("alDeleteEffects")]
	public static void DeleteEffects(Span<Effect> effects)
	{
		fixed (Effect* ptr = &effects[0])
		{
			alDeleteEffects(effects.Length, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alDeleteEffects")]
	public static void DeleteEffects(Effect[] effects)
	{
		fixed (Effect* ptr = &effects[0])
		{
			alDeleteEffects(effects.Length, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alIsEffect"), ContractAnnotation("effect:null => false"), Pure]
	public static bool IsEffect(Effect? effect)
	{
		return effect.HasValue && alIsEffect(effect.Value);
	}

	[NativeMethod("alEffectf")]
	public static void EffectF(Effect effect, EffectProperty parameter, float value)
	{
		alEffectf(effect, parameter, value);
		CheckErrorState();
	}

	[NativeMethod("alEffectfv"), CLSCompliant(false)]
	public static void EffectF(Effect effect, EffectProperty parameter, float* values)
	{
		alEffectfv(effect, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alEffectfv")]
	public static void EffectF(Effect effect, EffectProperty parameter, ReadOnlySpan<float> values)
	{
		fixed (float* ptr = &values[0])
		{
			alEffectfv(effect, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alEffectfv")]
	public static void EffectF(Effect effect, EffectProperty parameter, float[] values)
	{
		fixed (float* ptr = &values[0])
		{
			alEffectfv(effect, parameter, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alEffectfv")]
	public static void EffectF(Effect effect, EffectProperty parameter, Vector3 value)
	{
		alEffectfv(effect, parameter, &value.X);
		CheckErrorState();
	}
	
	[NativeMethod("alEffectfv"), CLSCompliant(false)]
	public static void EffectF(Effect effect, EffectProperty parameter, Vector3 *value)
	{
		alEffectfv(effect, parameter, (float*) value);
		CheckErrorState();
	}

	[NativeMethod("alEffecti")]
	public static void EffectI(Effect effect, EffectProperty parameter, int value)
	{
		alEffecti(effect, parameter, value);
		CheckErrorState();
	}
	
	[NativeMethod("alEffecti")]
	public static void EffectI(Effect effect, EffectProperty parameter, EffectType value)
	{
		alEffecti(effect, parameter, Unsafe.As<EffectType, int>(ref value));
		CheckErrorState();
	}
	
	[NativeMethod("alEffecti")]
	public static void EffectI<T>(Effect effect, EffectProperty parameter, T value) where T : unmanaged
	{
		if (Unsafe.SizeOf<T>() != sizeof(int))
			throw new ArgumentException("Generic type must be an unmanaged type 32-bits wide.", nameof(T));
		
		alEffecti(effect, parameter, Unsafe.As<T, int>(ref value));
		CheckErrorState();
	}

	[NativeMethod("alEffectiv"), CLSCompliant(false)]
	public static void EffectI(Effect effect, EffectProperty parameter, int* values)
	{
		alEffectiv(effect, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alEffectiv")]
	public static void EffectI(Effect effect, EffectProperty parameter, ReadOnlySpan<int> values)
	{
		fixed (int* ptr = &values[0])
		{
			alEffectiv(effect, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alEffectiv")]
	public static void EffectI(Effect effect, EffectProperty parameter, int[] values)
	{
		fixed (int* ptr = &values[0])
		{
			alEffectiv(effect, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alGetEffectf"), Pure]
	public static float GetEffectF(Effect effect, EffectProperty parameter)
	{
		float temp = default;
		alGetEffectf(effect, parameter, &temp);
		CheckErrorState();
		return temp;
	}

	[NativeMethod("alGetEffectfv"), CLSCompliant(false)]
	public static void GetEffectF(Effect effect, EffectProperty parameter, float* values)
	{
		alGetEffectfv(effect, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alGetEffectfv")]
	public static void GetEffectF(Effect effect, EffectProperty parameter, Span<float> values)
	{
		fixed (float* ptr = &values[0])
		{
			alGetEffectfv(effect, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alGetEffectfv")]
	public static void GetEffectF(Effect effect, EffectProperty parameter, float[] values)
	{
		fixed (float* ptr = &values[0])
		{
			alGetEffectfv(effect, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alGetEffectfv"), CLSCompliant(false)]
	public static void GetEffectF(Effect effect, EffectProperty parameter, Vector3 *value)
	{
		alGetEffectfv(effect, parameter, (float*) value);
		CheckErrorState();
	}

	[NativeMethod("alGetEffectfv")]
	public static void GetEffectF(Effect effect, EffectProperty parameter, out Vector3 value)
	{
		Vector3 temp = default;
		alGetEffectfv(effect, parameter, &temp.X);
		CheckErrorState();
		value = temp;
	}

	[NativeMethod("alGetEffecti"), Pure]
	public static int GetEffectI(Effect effect, EffectProperty parameter)
	{
		int temp = default;
		alGetEffecti(effect, parameter, &temp);
		CheckErrorState();
		return temp;
	}
	
	[NativeMethod("alGetEffecti"), Pure]
	public static T GetEffectI<T>(Effect effect, EffectProperty parameter)
	{
		if (Unsafe.SizeOf<T>() != sizeof(int))
			throw new ArgumentException("Generic type must be an unmanaged type 32-bits wide.", nameof(T));

		int temp = default;
		alGetEffecti(effect, parameter, &temp);
		CheckErrorState();
		return Unsafe.As<int, T>(ref temp);
	}
	
	[NativeMethod("alGetEffecti")]
	public static void GetEffectI<T>(Effect effect, EffectProperty parameter, out T value) where T : unmanaged
	{
		if (Unsafe.SizeOf<T>() != sizeof(int))
			throw new ArgumentException("Generic type must be an unmanaged type 32-bits wide.", nameof(T));
		
		int temp = default;
		alGetEffecti(effect, parameter, &temp);
		CheckErrorState();
		value = Unsafe.As<int, T>(ref temp);
	}

	[NativeMethod("alGetEffectiv"), CLSCompliant(false)]
	public static void GetEffectI(Effect effect, EffectProperty parameter, int* values)
	{
		alGetEffectiv(effect, parameter, values);
		CheckErrorState();
	}
	
	[NativeMethod("alGetEffectiv"), CLSCompliant(false)]
	public static void GetEffectI<T>(Effect effect, EffectProperty parameter, T* values) where T : unmanaged
	{
		if (Unsafe.SizeOf<T>() != sizeof(int))
			throw new ArgumentException("Generic type must be an unmanaged type 32-bits wide.", nameof(T));
	
		alGetEffectiv(effect, parameter, (int*) values);
		CheckErrorState();
	}

	[NativeMethod("alGetEffectiv")]
	public static void GetEffectI(Effect effect, EffectProperty parameter, Span<int> values)
	{
		fixed (int* ptr = &values[0])
		{
			alGetEffectiv(effect, parameter, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alGetEffectiv")]
	public static void GetEffectI(Effect effect, EffectProperty parameter, int[] values)
	{
		fixed (int* ptr = &values[0])
		{
			alGetEffectiv(effect, parameter, ptr);
		}
		CheckErrorState();
	}
	
	#region Reverb

	[NativeMethod("alGetEffecti"), Pure]
	public static bool GetEffectB(Effect effect, ReverbParam parameter)
	{
		int temp = default;
		alGetEffecti(effect, Unsafe.As<ReverbParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp != FALSE;
	}

	[NativeMethod("alGetEffectf"), Pure]
	public static float GetEffectF(Effect effect, ReverbParam parameter)
	{
		float temp = default;
		alGetEffectf(effect, Unsafe.As<ReverbParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp;
	}

	[NativeMethod("alEffecti")]
	public static void EffectB(Effect effect, ReverbParam parameter, bool value)
	{
		alEffectiReverb(effect, parameter, value ? TRUE : FALSE);
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<ReverbParam, int>(ref parameter), ParameterType.Boolean, value);
	}

	[NativeMethod("alEffectf")]
	public static void EffectF(Effect effect, ReverbParam parameter, float value)
	{
		alEffectfReverb(effect, parameter, value);
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<ReverbParam, int>(ref parameter), ParameterType.Float, value);
	}

	[NativeMethod("alEffecti"), NativeMethod("alEffectf"), NativeMethod("alEffectfv"), NativeMethod("alGetEffecti")]
	public static void EffectReverb(Effect effect, ReverbProperties properties)
	{
		const int REVERB_TYPE = 0x0001;
		const int EAX_REVERB_TYPE = 0x8000;
		
		int type;
		alGetEffecti(effect, EffectProperty.Type, &type);

		switch (type)
		{
			case REVERB_TYPE:
			{
				alEffectfReverb(effect, ReverbParam.Density, properties.Density);
				alEffectfReverb(effect, ReverbParam.Diffusion, properties.Diffusion);
				alEffectfReverb(effect, ReverbParam.Gain, properties.Gain);
				alEffectfReverb(effect, ReverbParam.GainHF, properties.GainHF);
				alEffectfReverb(effect, ReverbParam.DecayTime, properties.DecayTime);
				alEffectfReverb(effect, ReverbParam.DecayHFRatio, properties.DecayHFRatio);
				alEffectfReverb(effect, ReverbParam.ReflectionsGain, properties.ReflectionsGain);
				alEffectfReverb(effect, ReverbParam.ReflectionsDelay, properties.ReflectionsDelay);
				alEffectfReverb(effect, ReverbParam.LateReverbGain, properties.LateReverbGain);
				alEffectfReverb(effect, ReverbParam.LateReverbDelay, properties.LateReverbDelay);
				alEffectfReverb(effect, ReverbParam.AirAbsorptionGainHF, properties.AirAbsorptionGainHF);
				alEffectfReverb(effect, ReverbParam.RoomRolloffFactor, properties.RoomRolloffFactor);
				alEffectiReverb(effect, ReverbParam.DecayHFLimit, properties.DecayHFLimit ? TRUE : FALSE);
				break;
			}
			case EAX_REVERB_TYPE:
			{
				var lateReverb = properties.LateReverbPan;
				var reflections = properties.ReflectionsPan;
			
				alEffectfEaxReverb(effect, EaxReverbParam.Density, properties.Density);
				alEffectfEaxReverb(effect, EaxReverbParam.Diffusion, properties.Diffusion);
				alEffectfEaxReverb(effect, EaxReverbParam.Gain, properties.Gain);
				alEffectfEaxReverb(effect, EaxReverbParam.GainHF, properties.GainHF);
				alEffectfEaxReverb(effect, EaxReverbParam.GainLF, properties.GainLF);
				alEffectfEaxReverb(effect, EaxReverbParam.DecayTime, properties.DecayTime);
				alEffectfEaxReverb(effect, EaxReverbParam.DecayHFRatio, properties.DecayHFRatio);
				alEffectfEaxReverb(effect, EaxReverbParam.DecayLFRatio, properties.DecayLFRatio);
				alEffectfEaxReverb(effect, EaxReverbParam.ReflectionsGain, properties.ReflectionsGain);
				alEffectfEaxReverb(effect, EaxReverbParam.ReflectionsDelay, properties.ReflectionsDelay);
				alEffectfvEaxReverb(effect, EaxReverbParam.ReflectionsPan, &reflections.X);
				alEffectfEaxReverb(effect, EaxReverbParam.LateReverbGain, properties.LateReverbGain);
				alEffectfEaxReverb(effect, EaxReverbParam.LateReverbDelay, properties.LateReverbDelay);
				alEffectfvEaxReverb(effect, EaxReverbParam.LateReverbPan, &lateReverb.X);
				alEffectfEaxReverb(effect, EaxReverbParam.EchoTime, properties.EchoTime);
				alEffectfEaxReverb(effect, EaxReverbParam.EchoDepth, properties.EchoDepth);
				alEffectfEaxReverb(effect, EaxReverbParam.ModulationTime, properties.ModulationTime);
				alEffectfEaxReverb(effect, EaxReverbParam.ModulationDepth, properties.ModulationDepth);
				alEffectfEaxReverb(effect, EaxReverbParam.AirAbsorptionGainHF, properties.AirAbsorptionGainHF);
				alEffectfEaxReverb(effect, EaxReverbParam.HFReference, properties.HFReference);
				alEffectfEaxReverb(effect, EaxReverbParam.LFReference, properties.LFReference);
				alEffectfEaxReverb(effect, EaxReverbParam.RoomRolloffFactor, properties.RoomRolloffFactor);
				alEffectiEaxReverb(effect, EaxReverbParam.DecayHFLimit, properties.DecayHFLimit ? TRUE : FALSE);
				break;
			}
			default: return;
		}
		EffectParamChanged?.Invoke(effect, -1, ParameterType.Preset, properties);
	}
	
	[NativeMethod("alEffecti"), NativeMethod("alEffectf"), NativeMethod("alEffectfv"), NativeMethod("alGetEffecti")]
	public static ReverbProperties GetEffectReverb(Effect effect)
	{
		const int REVERB_TYPE = 0x0001;
		const int EAX_REVERB_TYPE = 0x8000;
		
		int type;
		alGetEffecti(effect, EffectProperty.Type, &type);
		
		switch (type)
		{
			case REVERB_TYPE:
			{
				return new ReverbProperties
				{
					Density = GetEffectF(effect, ReverbParam.Density),
					Diffusion = GetEffectF(effect, ReverbParam.Diffusion),
					Gain = GetEffectF(effect, ReverbParam.Gain),
					GainHF = GetEffectF(effect, ReverbParam.GainHF),
					DecayTime = GetEffectF(effect, ReverbParam.DecayTime),
					DecayHFRatio = GetEffectF(effect, ReverbParam.DecayHFRatio),
					ReflectionsGain = GetEffectF(effect, ReverbParam.ReflectionsGain),
					ReflectionsDelay = GetEffectF(effect, ReverbParam.ReflectionsDelay),
					LateReverbGain = GetEffectF(effect, ReverbParam.LateReverbGain),
					LateReverbDelay = GetEffectF(effect, ReverbParam.LateReverbDelay),
					AirAbsorptionGainHF = GetEffectF(effect, ReverbParam.AirAbsorptionGainHF),
					RoomRolloffFactor = GetEffectF(effect, ReverbParam.RoomRolloffFactor),
					DecayHFLimit = GetEffectB(effect, ReverbParam.DecayHFLimit)
				};
			}
			case EAX_REVERB_TYPE:
			{
				return new ReverbProperties
				{
					Density = GetEffectF(effect, EaxReverbParam.Density),
					Diffusion = GetEffectF(effect, EaxReverbParam.Diffusion),
					Gain = GetEffectF(effect, EaxReverbParam.Gain),
					GainHF = GetEffectF(effect, EaxReverbParam.GainHF),
					GainLF = GetEffectF(effect, EaxReverbParam.GainLF),
					DecayTime = GetEffectF(effect, EaxReverbParam.DecayTime),
					DecayHFRatio = GetEffectF(effect, EaxReverbParam.DecayHFRatio),
					DecayLFRatio = GetEffectF(effect, EaxReverbParam.DecayLFRatio),
					ReflectionsGain = GetEffectF(effect, EaxReverbParam.ReflectionsGain),
					ReflectionsDelay = GetEffectF(effect, EaxReverbParam.ReflectionsDelay),
					ReflectionsPan = GetEffectV(effect, EaxReverbParam.ReflectionsPan),
					LateReverbGain = GetEffectF(effect, EaxReverbParam.LateReverbGain),
					LateReverbDelay = GetEffectF(effect, EaxReverbParam.LateReverbDelay),
					LateReverbPan = GetEffectV(effect, EaxReverbParam.LateReverbPan),
					EchoTime = GetEffectF(effect, EaxReverbParam.EchoTime),
					EchoDepth = GetEffectF(effect, EaxReverbParam.EchoDepth),
					ModulationTime = GetEffectF(effect, EaxReverbParam.ModulationTime),
					ModulationDepth = GetEffectF(effect, EaxReverbParam.ModulationDepth),
					AirAbsorptionGainHF = GetEffectF(effect, EaxReverbParam.AirAbsorptionGainHF),
					HFReference = GetEffectF(effect, EaxReverbParam.HFReference),
					LFReference = GetEffectF(effect, EaxReverbParam.LFReference),
					RoomRolloffFactor = GetEffectF(effect, EaxReverbParam.RoomRolloffFactor),
					DecayHFLimit = GetEffectB(effect, EaxReverbParam.DecayHFLimit),
				};
			}
			default: return new ReverbProperties();
		}
	}
	
	#endregion
	
	#region EaxReverb

	[NativeMethod("alGetEffecti"), Pure]
	public static bool GetEffectB(Effect effect, EaxReverbParam parameter)
	{
		int temp = default;
		alGetEffecti(effect, Unsafe.As<EaxReverbParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp != FALSE;
	}

	[NativeMethod("alGetEffectf"),Pure]
	public static float GetEffectF(Effect effect, EaxReverbParam parameter)
	{
		float temp = default;
		alGetEffectf(effect, Unsafe.As<EaxReverbParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp;
	}
	
	[NativeMethod("alGetEffectfv"), Pure]
	public static Vector3 GetEffectV(Effect effect, EaxReverbParam parameter)
	{
		Vector3 temp = default;
		alGetEffectfv(effect, Unsafe.As<EaxReverbParam, EffectProperty>(ref parameter), &temp.X);
		CheckErrorState();
		return temp;
	}

	[NativeMethod("alEffecti")]
	public static void EffectB(Effect effect, EaxReverbParam parameter, bool value)
	{
		alEffectiEaxReverb(effect, parameter, value ? TRUE : FALSE);
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<EaxReverbParam, int>(ref parameter), ParameterType.Boolean, value);
	}

	[NativeMethod("alEffectf")]
	public static void EffectF(Effect effect, EaxReverbParam parameter, float value)
	{
		alEffectfEaxReverb(effect, parameter, value);
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<EaxReverbParam, int>(ref parameter), ParameterType.Float, value);
	}
	
	[NativeMethod("alEffectfv")]
	public static void EffectV(Effect effect, EaxReverbParam parameter, Vector3 value)
	{
		alEffectfvEaxReverb(effect, parameter, &value.X);
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<EaxReverbParam, int>(ref parameter), ParameterType.Vector, value);
	}

	#endregion

	#region Chorus

	[NativeMethod("alGetEffecti"), Pure]
	public static int GetEffectI(Effect effect, ChorusParam parameter)
	{
		int temp = default;
		alGetEffecti(effect, Unsafe.As<ChorusParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp;
	}

	[NativeMethod("alGetEffecti"), Pure]
	public static TEnum GetEffectI<TEnum>(Effect effect, ChorusParam parameter) where TEnum : Enum
	{
		DebugAssertInt32<TEnum>();
		int temp = default;
		alGetEffecti(effect, Unsafe.As<ChorusParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return Unsafe.As<int, TEnum>(ref temp);
	}
	
	[NativeMethod("alGetEffectf"), Pure]
	public static float GetEffectF(Effect effect, ChorusParam parameter)
	{
		float temp = default;
		alGetEffectf(effect, Unsafe.As<ChorusParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp;
	}

	[NativeMethod("alEffecti")]
	public static void EffectI(Effect effect, ChorusParam parameter, int value)
	{
		alEffecti(effect, Unsafe.As<ChorusParam, EffectProperty>(ref parameter), value);
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<ChorusParam, int>(ref parameter), ParameterType.Integer, value);
	}

	[NativeMethod("alEffecti")]
	public static void EffectI(Effect effect, ChorusParam parameter, ChorusWaveform value)
	{
		alEffecti(effect, Unsafe.As<ChorusParam, EffectProperty>(ref parameter), Unsafe.As<ChorusWaveform, int>(ref value));
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<ChorusParam, int>(ref parameter), ParameterType.Enumeration, value);
	}
	
	[NativeMethod("alEffectf")]
	public static void EffectF(Effect effect, ChorusParam parameter, float value)
	{
		alEffectf(effect, Unsafe.As<ChorusParam, EffectProperty>(ref parameter), value);
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<ChorusParam, int>(ref parameter), ParameterType.Float, value);
	}

	#endregion
	
	#region Distortion

	[NativeMethod("alGetEffectf"), Pure]
	public static float GetEffectF(Effect effect, DistortionParam parameter)
	{
		float temp = default;
		alGetEffectf(effect, Unsafe.As<DistortionParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp;
	}
	
	[NativeMethod("alEffectf")]
	public static void EffectF(Effect effect, DistortionParam parameter, float value)
	{
		alEffectf(effect, Unsafe.As<DistortionParam, EffectProperty>(ref parameter), value);
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<DistortionParam, int>(ref parameter), ParameterType.Float, value);
	}

	#endregion
	
	#region Echo

	[NativeMethod("alGetEffectf"), Pure]
	public static float GetEffectF(Effect effect, EchoParam parameter)
	{
		float temp = default;
		alGetEffectf(effect, Unsafe.As<EchoParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp;
	}

	[NativeMethod("alEffectf")]
	public static void EffectF(Effect effect, EchoParam parameter, float value)
	{
		alEffectf(effect, Unsafe.As<EchoParam, EffectProperty>(ref parameter), value);
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<EchoParam, int>(ref parameter), ParameterType.Float, value);
	}

	#endregion
	
	#region Flanger

	[NativeMethod("alGetEffecti"), Pure]
	public static int GetEffectI(Effect effect, FlangerParam parameter)
	{
		int temp = default;
		alGetEffecti(effect, Unsafe.As<FlangerParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp;
	}
	
	[NativeMethod("alGetEffecti"), Pure]
	public static TEnum GetEffectI<TEnum>(Effect effect, FlangerParam parameter) where TEnum : Enum
	{
		DebugAssertInt32<TEnum>();
		int temp = default;
		alGetEffecti(effect, Unsafe.As<FlangerParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return Unsafe.As<int, TEnum>(ref temp);
	}
	
	[NativeMethod("alGetEffectf"), Pure]
	public static float GetEffectF(Effect effect, FlangerParam parameter)
	{
		float temp = default;
		alGetEffectf(effect, Unsafe.As<FlangerParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp;
	}
	
	[NativeMethod("alEffecti")]
	public static void EffectI(Effect effect, FlangerParam parameter, int value)
	{
		alEffecti(effect, Unsafe.As<FlangerParam, EffectProperty>(ref parameter), value);
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<FlangerParam, int>(ref parameter), ParameterType.Integer, value);
	}

	[NativeMethod("alEffecti")]
	public static void EffectI(Effect effect, FlangerParam parameter, FlangerWaveform value)
	{
		alEffecti(effect, Unsafe.As<FlangerParam, EffectProperty>(ref parameter), Unsafe.As<FlangerWaveform, int>(ref value));
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<FlangerParam, int>(ref parameter), ParameterType.Enumeration, value);
	}
	
	[NativeMethod("alEffectf")]
	public static void EffectF(Effect effect, FlangerParam parameter, float value)
	{
		alEffectf(effect, Unsafe.As<FlangerParam, EffectProperty>(ref parameter), value);
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<FlangerParam, int>(ref parameter), ParameterType.Float, value);
	}

	#endregion
	
	#region FrequencyShifter

	[NativeMethod("alGetEffecti"), Pure]
	public static FrequencyShifterDirection GetEffectI(Effect effect, FrequencyShifterParam parameter)
	{
		int temp = default;
		alGetEffecti(effect, Unsafe.As<FrequencyShifterParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return Unsafe.As<int, FrequencyShifterDirection>(ref temp);
	}
	
	[NativeMethod("alGetEffectf"), Pure]
	public static float GetEffectF(Effect effect, FrequencyShifterParam parameter)
	{
		float temp = default;
		alGetEffectf(effect, Unsafe.As<FrequencyShifterParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp;
	}

	[NativeMethod("alEffecti")]
	public static void EffectI(Effect effect, FrequencyShifterParam parameter, FrequencyShifterDirection value)
	{
		alEffecti(effect, Unsafe.As<FrequencyShifterParam, EffectProperty>(ref parameter), Unsafe.As<FrequencyShifterDirection, int>(ref value));
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<FrequencyShifterParam, int>(ref parameter), ParameterType.Enumeration, value);
	}

	[NativeMethod("alEffectf")]
	public static void EffectF(Effect effect, FrequencyShifterParam parameter, float value)
	{
		alEffectf(effect, Unsafe.As<FrequencyShifterParam, EffectProperty>(ref parameter), value);
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<FrequencyShifterParam, int>(ref parameter), ParameterType.Float, value);
	}

	#endregion
	
	#region VocalMorpher

	[NativeMethod("alGetEffecti"), Pure]
	public static int GetEffectI(Effect effect, VocalMorpherParam parameter)
	{
		int temp = default;
		alGetEffecti(effect, Unsafe.As<VocalMorpherParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp;
	}

	[NativeMethod("alGetEffecti"), Pure]
	public static TEnum GetEffectI<TEnum>(Effect effect, VocalMorpherParam parameter) where TEnum : Enum
	{
		DebugAssertInt32<TEnum>();
		int temp = default;
		alGetEffecti(effect, Unsafe.As<VocalMorpherParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return Unsafe.As<int, TEnum>(ref temp);
	}
	
	[NativeMethod("alGetEffectf"), Pure]
	public static float GetEffectF(Effect effect, VocalMorpherParam parameter)
	{
		float temp = default;
		alGetEffectf(effect, Unsafe.As<VocalMorpherParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp;
	}

	[NativeMethod("alEffecti")]
	public static void EffectI(Effect effect, VocalMorpherParam parameter, int value)
	{
		alEffecti(effect, Unsafe.As<VocalMorpherParam, EffectProperty>(ref parameter), value);
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<VocalMorpherParam, int>(ref parameter), ParameterType.Integer, value);
	}
	
	[NativeMethod("alEffecti")]
	public static void EffectI(Effect effect, VocalMorpherParam parameter, VocalMorpherWaveform value)
	{
		alEffecti(effect, Unsafe.As<VocalMorpherParam, EffectProperty>(ref parameter), Unsafe.As<VocalMorpherWaveform, int>(ref value));
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<VocalMorpherParam, int>(ref parameter), ParameterType.Enumeration, value);
	}
	
	[NativeMethod("alEffecti")]
	public static void EffectI(Effect effect, VocalMorpherParam parameter, Phoneme value)
	{
		alEffecti(effect, Unsafe.As<VocalMorpherParam, EffectProperty>(ref parameter), Unsafe.As<Phoneme, int>(ref value));
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<VocalMorpherParam, int>(ref parameter), ParameterType.Enumeration, value);
	}

	[NativeMethod("alEffectf")]
	public static void EffectF(Effect effect, VocalMorpherParam parameter, float value)
	{
		alEffectf(effect, Unsafe.As<VocalMorpherParam, EffectProperty>(ref parameter), value);
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<VocalMorpherParam, int>(ref parameter), ParameterType.Float, value);
	}
	
	#endregion
	
	#region PitchShifter

	[NativeMethod("alGetEffecti"), Pure]
	public static int GetEffectI(Effect effect, PitchShifterParam parameter)
	{
		int temp = default;
		alGetEffecti(effect, Unsafe.As<PitchShifterParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp;
	}

	[NativeMethod("alEffecti")]
	public static void EffectI(Effect effect, PitchShifterParam parameter, int value)
	{
		alEffecti(effect, Unsafe.As<PitchShifterParam, EffectProperty>(ref parameter), value);
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<PitchShifterParam, int>(ref parameter), ParameterType.Float, value);
	}

	#endregion
	
	#region RingModulator

	[NativeMethod("alGetEffecti"), Pure]
	public static RingModulatorWaveform GetEffectI(Effect effect, RingModulatorParam parameter)
	{
		int temp = default;
		alGetEffecti(effect, Unsafe.As<RingModulatorParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return Unsafe.As<int, RingModulatorWaveform>(ref temp);
	}

	[NativeMethod("alGetEffectf"), Pure]
	public static float GetEffectF(Effect effect, RingModulatorParam parameter)
	{
		float temp = default;
		alGetEffectf(effect, Unsafe.As<RingModulatorParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp;
	}

	[NativeMethod("alEffecti")]
	public static void EffectI(Effect effect, RingModulatorParam parameter, RingModulatorWaveform value)
	{
		alEffecti(effect, Unsafe.As<RingModulatorParam, EffectProperty>(ref parameter), Unsafe.As<RingModulatorWaveform, int>(ref value));
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<RingModulatorParam, int>(ref parameter), ParameterType.Enumeration, value);
	}
	
	[NativeMethod("alEffectf")]
	public static void EffectF(Effect effect, RingModulatorParam parameter, float value)
	{
		alEffectf(effect, Unsafe.As<RingModulatorParam, EffectProperty>(ref parameter), value);
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<RingModulatorParam, int>(ref parameter), ParameterType.Float, value);
	}
	
	#endregion
	
	#region Autowah

	[NativeMethod("alGetEffectf"), Pure]
	public static float GetEffectF(Effect effect, AutowahParam parameter)
	{
		float temp = default;
		alGetEffectf(effect, Unsafe.As<AutowahParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp;
	}

	[NativeMethod("alEffectf")]
	public static void EffectF(Effect effect, AutowahParam parameter, float value)
	{
		alEffectf(effect, Unsafe.As<AutowahParam, EffectProperty>(ref parameter), value);
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<AutowahParam, int>(ref parameter), ParameterType.Float, value);
	}

	#endregion
	
	#region Compressor
	
	[NativeMethod("alGetEffecti"), Pure]
	public static bool GetEffectB(Effect effect, CompressorParam parameter)
	{
		int temp = default;
		alGetEffecti(effect, Unsafe.As<CompressorParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp != FALSE;
	}

	[NativeMethod("alEffecti")]
	public static void EffectB(Effect effect, CompressorParam parameter, bool value)
	{
		alEffecti(effect, Unsafe.As<CompressorParam, EffectProperty>(ref parameter), value ? TRUE : FALSE);
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<CompressorParam, int>(ref parameter), ParameterType.Boolean, value);
	}
	
	#endregion
	
	#region Equalizer
	
	[NativeMethod("alGetEffectf"), Pure]
	public static float GetEffectF(Effect effect, EqualizerParam parameter)
	{
		float temp = default;
		alGetEffectf(effect, Unsafe.As<EqualizerParam, EffectProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp;
	}
	
	[NativeMethod("alEffectf")]
	public static void EffectF(Effect effect, EqualizerParam parameter, float value)
	{
		alEffectf(effect, Unsafe.As<EqualizerParam, EffectProperty>(ref parameter), value);
		CheckErrorState();
		EffectParamChanged?.Invoke(effect, Unsafe.As<EqualizerParam, int>(ref parameter), ParameterType.Float, value);
	}

	#endregion
}