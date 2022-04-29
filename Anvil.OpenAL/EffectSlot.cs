using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security;
using Anvil.Native;
using JetBrains.Annotations;
// ReSharper disable StringLiteralTypo

namespace Anvil.OpenAL;


[PublicAPI]
public enum EffectSlotProperty
{
	Effect = 0x0001,
	Gain = 0x0002,
	AuxiliarySendAuto = 0x0003,
}

[SuppressUnmanagedCodeSecurity]
public static unsafe partial class AL
{
	/// <summary>
	/// Creates a new <see cref="EffectSlot"/> instance.
	/// </summary>
	/// <returns>The newly created <see cref="EffectSlot"/> instance.</returns>
	[NativeMethod("alGenAuxiliaryEffectSlots"), Pure]
	public static EffectSlot GenAuxiliaryEffectSlot()
	{
		EffectSlot effectSlot = default;
		alGenAuxiliaryEffectSlots(1, &effectSlot);
		CheckErrorState();
		return effectSlot;
	}
	
	/// <summary>
	/// Creates a new <see cref="EffectSlot"/> instance and attaches the specified <paramref name="effect"/>.
	/// </summary>
	/// <param name="effect">The <see cref="Effect"/> to attach after creation.</param>
	/// <returns>The newly created <see cref="EffectSlot"/> instance with the <paramref name="effect"/> attached.</returns>
	[NativeMethod("alGenAuxiliaryEffectSlots"), NativeMethod("alAuxiliaryEffectSloti"), Pure]
	public static EffectSlot GenAuxiliaryEffectSlot(Effect effect)
	{
		EffectSlot effectSlot = default;
		alGenAuxiliaryEffectSlots(1, &effectSlot);
		alAuxiliaryEffectSloti(effectSlot, EffectSlotProperty.Effect, effect.Value);
		CheckErrorState();
		return effectSlot;
	}

	public static void DeleteAuxiliaryEffectSlot(EffectSlot effectSlot)
	{
		alDeleteAuxiliaryEffectSlots(1, &effectSlot);
		CheckErrorState();
	}

	[NativeMethod("alGenAuxiliaryEffectSlots"), CLSCompliant(false)]
	public static void GenAuxiliaryEffectSlots(int n, EffectSlot *effectSlots)
	{
		alGenAuxiliaryEffectSlots(n, effectSlots);
		CheckErrorState();
	}

	[NativeMethod("alGenAuxiliaryEffectSlots")]
	public static void GenAuxiliaryEffectSlots(Span<EffectSlot> effectSlots)
	{
		fixed (EffectSlot* ptr = &effectSlots[0])
		{
			alGenAuxiliaryEffectSlots(effectSlots.Length, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alGenAuxiliaryEffectSlots")]
	public static void GenAuxiliaryEffectSlots(EffectSlot[] effectSlots)
	{
		fixed (EffectSlot* ptr = &effectSlots[0])
		{
			alGenAuxiliaryEffectSlots(effectSlots.Length, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alDeleteAuxiliaryEffectSlots"), CLSCompliant(false)]
	public static void DeleteAuxiliaryEffectSlots(int n, EffectSlot  *effectSlots)
	{
		alDeleteAuxiliaryEffectSlots(n, effectSlots);
		CheckErrorState();
	}

	[NativeMethod("alDeleteAuxiliaryEffectSlots")]
	public static void DeleteAuxiliaryEffectSlots(Span<EffectSlot> effectSlots)
	{
		fixed (EffectSlot* ptr = &effectSlots[0])
		{
			alDeleteAuxiliaryEffectSlots(effectSlots.Length, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alDeleteAuxiliaryEffectSlots")]
	public static void DeleteAuxiliaryEffectSlots(EffectSlot[] effectSlots)
	{
		fixed (EffectSlot* ptr = &effectSlots[0])
		{
			alDeleteAuxiliaryEffectSlots(effectSlots.Length, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alIsAuxiliaryEffectSlot"), ContractAnnotation("effectSlot:null => false"), Pure]
	public static bool IsAuxiliaryEffectSlot(EffectSlot? effectSlot)
	{
		return effectSlot.HasValue && alIsAuxiliaryEffectSlot(effectSlot.Value);
	}

	[NativeMethod("alAuxiliaryEffectSlotf")]
	public static void AuxiliaryEffectSlotF(EffectSlot effectSlot, EffectSlotProperty parameter, float value)
	{
		alAuxiliaryEffectSlotf(effectSlot, parameter, value);
		CheckErrorState();
	}

	[NativeMethod("alAuxiliaryEffectSlotfv"), CLSCompliant(false)]
	public static void AuxiliaryEffectSlotF(EffectSlot effectSlot, EffectSlotProperty parameter, float* values)
	{
		alAuxiliaryEffectSlotfv(effectSlot, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alAuxiliaryEffectSlotfv")]
	public static void AuxiliaryEffectSlotF(EffectSlot effectSlot, EffectSlotProperty parameter, ReadOnlySpan<float> values)
	{
		fixed (float* ptr = &values[0])
		{
			alAuxiliaryEffectSlotfv(effectSlot, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alAuxiliaryEffectSlotfv")]
	public static void AuxiliaryEffectSlotF(EffectSlot effectSlot, EffectSlotProperty parameter, float[] values)
	{
		fixed (float* ptr = &values[0])
		{
			alAuxiliaryEffectSlotfv(effectSlot, parameter, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alAuxiliaryEffectSlotfv")]
	public static void AuxiliaryEffectSlotF(EffectSlot effectSlot, EffectSlotProperty parameter, Vector3 value)
	{
		alAuxiliaryEffectSlotfv(effectSlot, parameter, &value.X);
		CheckErrorState();
	}
	
	[NativeMethod("alAuxiliaryEffectSlotfv"), CLSCompliant(false)]
	public static void AuxiliaryEffectSlotF(EffectSlot effectSlot, EffectSlotProperty parameter, Vector3 *value)
	{
		alAuxiliaryEffectSlotfv(effectSlot, parameter, (float*) value);
		CheckErrorState();
	}

	[NativeMethod("alAuxiliaryEffectSloti")]
	public static void AuxiliaryEffectSlotI(EffectSlot effectSlot, EffectSlotProperty parameter, int value)
	{
		alAuxiliaryEffectSloti(effectSlot, parameter, value);
		CheckErrorState();
	}
	
	[NativeMethod("alAuxiliaryEffectSloti")]
	public static void AuxiliaryEffectSlotI<T>(EffectSlot effectSlot, EffectSlotProperty parameter, T value) where T : unmanaged
	{
		if (Unsafe.SizeOf<T>() != sizeof(int))
			throw new ArgumentException("Generic type must be an unmanaged type 32-bits wide.", nameof(T));
		
		alAuxiliaryEffectSloti(effectSlot, parameter, Unsafe.As<T, int>(ref value));
		CheckErrorState();
	}

	[NativeMethod("alAuxiliaryEffectSlotiv"), CLSCompliant(false)]
	public static void AuxiliaryEffectSlotI(EffectSlot effectSlot, EffectSlotProperty parameter, int* values)
	{
		alAuxiliaryEffectSlotiv(effectSlot, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alAuxiliaryEffectSlotiv")]
	public static void AuxiliaryEffectSlotI(EffectSlot effectSlot, EffectSlotProperty parameter, ReadOnlySpan<int> values)
	{
		fixed (int* ptr = &values[0])
		{
			alAuxiliaryEffectSlotiv(effectSlot, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alAuxiliaryEffectSlotiv")]
	public static void AuxiliaryEffectSlotI(EffectSlot effectSlot, EffectSlotProperty parameter, int[] values)
	{
		fixed (int* ptr = &values[0])
		{
			alAuxiliaryEffectSlotiv(effectSlot, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alGetAuxiliaryEffectSlotf"), Pure]
	public static float GetAuxiliaryEffectSlotF(EffectSlot effectSlot, EffectSlotProperty parameter)
	{
		float temp = default;
		alGetAuxiliaryEffectSlotf(effectSlot, parameter, &temp);
		CheckErrorState();
		return temp;
	}

	[NativeMethod("alGetAuxiliaryEffectSlotfv"), CLSCompliant(false)]
	public static void GetAuxiliaryEffectSlotF(EffectSlot effectSlot, EffectSlotProperty parameter, float* values)
	{
		alGetAuxiliaryEffectSlotfv(effectSlot, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alGetAuxiliaryEffectSlotfv")]
	public static void GetAuxiliaryEffectSlotF(EffectSlot effectSlot, EffectSlotProperty parameter, Span<float> values)
	{
		fixed (float* ptr = &values[0])
		{
			alGetAuxiliaryEffectSlotfv(effectSlot, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alGetAuxiliaryEffectSlotfv")]
	public static void GetAuxiliaryEffectSlotF(EffectSlot effectSlot, EffectSlotProperty parameter, float[] values)
	{
		fixed (float* ptr = &values[0])
		{
			alGetAuxiliaryEffectSlotfv(effectSlot, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alGetAuxiliaryEffectSlotfv"), CLSCompliant(false)]
	public static void GetAuxiliaryEffectSlotF(EffectSlot effectSlot, EffectSlotProperty parameter, Vector3 *value)
	{
		alGetAuxiliaryEffectSlotfv(effectSlot, parameter, (float*) value);
		CheckErrorState();
	}

	[NativeMethod("alGetAuxiliaryEffectSlotfv"), Pure]
	public static Vector3 GetAuxiliaryEffectSlotV(EffectSlot effectSlot, EffectSlotProperty parameter)
	{
		Vector3 temp = default;
		alGetAuxiliaryEffectSlotfv(effectSlot, parameter, &temp.X);
		CheckErrorState();
		return temp;
	}

	[NativeMethod("alGetAuxiliaryEffectSloti"), Pure]
	public static int GetAuxiliaryEffectSlotI(EffectSlot effectSlot, EffectSlotProperty parameter)
	{
		int temp = default;
		alGetAuxiliaryEffectSloti(effectSlot, parameter, &temp);
		CheckErrorState();
		return temp;
	}
	
	[NativeMethod("alGetAuxiliaryEffectSloti"), Pure]
	public static T GetAuxiliaryEffectSlotI<T>(EffectSlot effectSlot, EffectSlotProperty parameter)
	{
		if (Unsafe.SizeOf<T>() != sizeof(int))
			throw new ArgumentException("Generic type must be an unmanaged type 32-bits wide.", nameof(T));

		int temp = default;
		alGetAuxiliaryEffectSloti(effectSlot, parameter, &temp);
		CheckErrorState();
		return Unsafe.As<int, T>(ref temp);
	}

	[NativeMethod("alGetAuxiliaryEffectSlotiv"), CLSCompliant(false)]
	public static void GetAuxiliaryEffectSlotI(EffectSlot effectSlot, EffectSlotProperty parameter, int* values)
	{
		alGetAuxiliaryEffectSlotiv(effectSlot, parameter, values);
		CheckErrorState();
	}
	
	[NativeMethod("alGetAuxiliaryEffectSlotiv"), CLSCompliant(false)]
	public static void GetAuxiliaryEffectSlotI<T>(EffectSlot effectSlot, EffectSlotProperty parameter, T* values) where T : unmanaged
	{
		if (Unsafe.SizeOf<T>() != sizeof(int))
			throw new ArgumentException("Generic type must be an unmanaged type 32-bits wide.", nameof(T));
	
		alGetAuxiliaryEffectSlotiv(effectSlot, parameter, (int*) values);
		CheckErrorState();
	}

	[NativeMethod("alGetAuxiliaryEffectSlotiv")]
	public static void GetAuxiliaryEffectSlotI(EffectSlot effectSlot, EffectSlotProperty parameter, Span<int> values)
	{
		fixed (int* ptr = &values[0])
		{
			alGetAuxiliaryEffectSlotiv(effectSlot, parameter, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alGetAuxiliaryEffectSlotiv")]
	public static void GetAuxiliaryEffectSlotI(EffectSlot effectSlot, EffectSlotProperty parameter, int[] values)
	{
		fixed (int* ptr = &values[0])
		{
			alGetAuxiliaryEffectSlotiv(effectSlot, parameter, ptr);
		}
		CheckErrorState();
	}
}