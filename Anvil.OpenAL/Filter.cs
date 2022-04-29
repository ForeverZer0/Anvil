using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security;
using Anvil.Native;
using JetBrains.Annotations;
// ReSharper disable StringLiteralTypo

namespace Anvil.OpenAL;

/// <summary>
/// Strongly-typed constants for filter properties.
/// </summary>
[PublicAPI]
public enum FilterProperty
{
	/// <summary>
	/// Gets the index of the first parameter within the effect.
	/// </summary>
	FirstParameter                = 0x0000,
	
	/// <summary>
	/// Gets the index of the last parameter within the effect.
	/// </summary>
	LastParameter                 = 0x8000,
	
	/// <summary>
	/// Gets a constant describing the effect type.
	/// </summary>
	Type                           = 0x8001,
}

public enum FilterType
{
	None                           = 0x0000,
	Lowpass                        = 0x0001,
	Highpass                       = 0x0002,
	Bandpass                       = 0x0003,
}

public enum LowpassParam
{
	Gain = 0x0001,
	GainHF = 0x0002,
}

public enum HighpassParam
{
	Gain = 0x0001,
	GainLF = 0x0002,
}

public enum BandpassParam
{
	Gain = 0x0001,
	GainLF = 0x0002,
	GainHF = 0x0003,
}

[SuppressUnmanagedCodeSecurity]
public static unsafe partial class AL
{
	[NativeMethod("alGenFilters"), Pure]
	public static Filter GenFilter()
	{
		Filter filter = default;
		alGenFilters(1, &filter);
		CheckErrorState();
		return filter;
	}
	
	[NativeMethod("alGenFilters"), NativeMethod("alFilteri"), Pure]
	public static Filter GenFilter(FilterType type)
	{
		Filter filter = default;
		alGenFilters(1, &filter);
		alFilteri(filter, FilterProperty.Type, Unsafe.As<FilterType, int>(ref type));
		CheckErrorState();
		return filter;
	}

	public static void DeleteFilter(Filter filter)
	{
		alDeleteFilters(1, &filter);
		CheckErrorState();
	}

	[NativeMethod("alGenFilters"), CLSCompliant(false)]
	public static void GenFilters(int n, Filter *filters)
	{
		alGenFilters(n, filters);
		CheckErrorState();
	}

	[NativeMethod("alGenFilters")]
	public static void GenFilters(Span<Filter> filters)
	{
		fixed (Filter* ptr = &filters[0])
		{
			alGenFilters(filters.Length, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alGenFilters")]
	public static void GenFilters(Filter[] filters)
	{
		fixed (Filter* ptr = &filters[0])
		{
			alGenFilters(filters.Length, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alDeleteFilters"), CLSCompliant(false)]
	public static void DeleteFilters(int n, Filter  *filters)
	{
		alDeleteFilters(n, filters);
		CheckErrorState();
	}

	[NativeMethod("alDeleteFilters")]
	public static void DeleteFilters(Span<Filter> filters)
	{
		fixed (Filter* ptr = &filters[0])
		{
			alDeleteFilters(filters.Length, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alDeleteFilters")]
	public static void DeleteFilters(Filter[] filters)
	{
		fixed (Filter* ptr = &filters[0])
		{
			alDeleteFilters(filters.Length, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alIsFilter"), ContractAnnotation("filter:null => false"), Pure]
	public static bool IsFilter(Filter? filter)
	{
		return filter.HasValue && alIsFilter(filter.Value);
	}
	
	[NativeMethod("alFilterf")]
	public static void FilterF(Filter filter, LowpassParam parameter, float value)
	{
		alFilterf(filter, Unsafe.As<LowpassParam, FilterProperty>(ref parameter), value);
		CheckErrorState();
	}
	
	[NativeMethod("alFilterf")]
	public static void FilterF(Filter filter, HighpassParam parameter, float value)
	{
		alFilterf(filter, Unsafe.As<HighpassParam, FilterProperty>(ref parameter), value);
		CheckErrorState();
	}
	
	[NativeMethod("alFilterf")]
	public static void FilterF(Filter filter, BandpassParam parameter, float value)
	{
		alFilterf(filter, Unsafe.As<BandpassParam, FilterProperty>(ref parameter), value);
		CheckErrorState();
	}

	[NativeMethod("alFilterf")]
	public static void FilterF(Filter filter, FilterProperty parameter, float value)
	{
		alFilterf(filter, parameter, value);
		CheckErrorState();
	}

	[NativeMethod("alFilterfv"), CLSCompliant(false)]
	public static void FilterF(Filter filter, FilterProperty parameter, float* values)
	{
		alFilterfv(filter, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alFilterfv")]
	public static void FilterF(Filter filter, FilterProperty parameter, ReadOnlySpan<float> values)
	{
		fixed (float* ptr = &values[0])
		{
			alFilterfv(filter, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alFilterfv")]
	public static void FilterF(Filter filter, FilterProperty parameter, float[] values)
	{
		fixed (float* ptr = &values[0])
		{
			alFilterfv(filter, parameter, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alFilterfv")]
	public static void FilterF(Filter filter, FilterProperty parameter, Vector3 value)
	{
		alFilterfv(filter, parameter, &value.X);
		CheckErrorState();
	}
	
	[NativeMethod("alFilterfv"), CLSCompliant(false)]
	public static void FilterF(Filter filter, FilterProperty parameter, Vector3 *value)
	{
		alFilterfv(filter, parameter, (float*) value);
		CheckErrorState();
	}

	[NativeMethod("alFilteri")]
	public static void FilterI(Filter filter, FilterProperty parameter, int value)
	{
		alFilteri(filter, parameter, value);
		CheckErrorState();
	}
	
	[NativeMethod("alFilteri")]
	public static void FilterI<T>(Filter filter, FilterProperty parameter, T value) where T : unmanaged
	{
		if (Unsafe.SizeOf<T>() != sizeof(int))
			throw new ArgumentException("Generic type must be an unmanaged type 32-bits wide.", nameof(T));
		
		alFilteri(filter, parameter, Unsafe.As<T, int>(ref value));
		CheckErrorState();
	}

	[NativeMethod("alFilteriv"), CLSCompliant(false)]
	public static void FilterI(Filter filter, FilterProperty parameter, int* values)
	{
		alFilteriv(filter, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alFilteriv")]
	public static void FilterI(Filter filter, FilterProperty parameter, ReadOnlySpan<int> values)
	{
		fixed (int* ptr = &values[0])
		{
			alFilteriv(filter, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alFilteriv")]
	public static void FilterI(Filter filter, FilterProperty parameter, int[] values)
	{
		fixed (int* ptr = &values[0])
		{
			alFilteriv(filter, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alGetFilterf"), Pure]
	public static float GetFilterF(Filter filter, FilterProperty parameter)
	{
		float temp = default;
		alGetFilterf(filter, parameter, &temp);
		CheckErrorState();
		return temp;
	}
	
	[NativeMethod("alGetFilterf"), Pure]
	public static float GetFilterF(Filter filter, LowpassParam parameter)
	{
		float temp = default;
		alGetFilterf(filter, Unsafe.As<LowpassParam, FilterProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp;
	}
	
	[NativeMethod("alGetFilterf"), Pure]
	public static float GetFilterF(Filter filter, HighpassParam parameter)
	{
		float temp = default;
		alGetFilterf(filter, Unsafe.As<HighpassParam, FilterProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp;
	}
	
	[NativeMethod("alGetFilterf"), Pure]
	public static float GetFilterF(Filter filter, BandpassParam parameter)
	{
		float temp = default;
		alGetFilterf(filter, Unsafe.As<BandpassParam, FilterProperty>(ref parameter), &temp);
		CheckErrorState();
		return temp;
	}

	[NativeMethod("alGetFilterfv"), CLSCompliant(false)]
	public static void GetFilterF(Filter filter, FilterProperty parameter, float* values)
	{
		alGetFilterfv(filter, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alGetFilterfv")]
	public static void GetFilterF(Filter filter, FilterProperty parameter, Span<float> values)
	{
		fixed (float* ptr = &values[0])
		{
			alGetFilterfv(filter, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alGetFilterfv")]
	public static void GetFilterF(Filter filter, FilterProperty parameter, float[] values)
	{
		fixed (float* ptr = &values[0])
		{
			alGetFilterfv(filter, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alGetFilterfv"), CLSCompliant(false)]
	public static void GetFilterF(Filter filter, FilterProperty parameter, Vector3 *value)
	{
		alGetFilterfv(filter, parameter, (float*) value);
		CheckErrorState();
	}

	[NativeMethod("alGetFilterfv")]
	public static void GetFilterF(Filter filter, FilterProperty parameter, out Vector3 value)
	{
		Vector3 temp = default;
		alGetFilterfv(filter, parameter, &temp.X);
		CheckErrorState();
		value = temp;
	}

	[NativeMethod("alGetFilteri"), Pure]
	public static int GetFilterI(Filter filter, FilterProperty parameter)
	{
		int temp = default;
		alGetFilteri(filter, parameter, &temp);
		CheckErrorState();
		return temp;
	}
	
	[NativeMethod("alGetFilteri"), Pure]
	public static T GetFilterI<T>(Filter filter, FilterProperty parameter)
	{
		if (Unsafe.SizeOf<T>() != sizeof(int))
			throw new ArgumentException("Generic type must be an unmanaged type 32-bits wide.", nameof(T));

		int temp = default;
		alGetFilteri(filter, parameter, &temp);
		CheckErrorState();
		return Unsafe.As<int, T>(ref temp);
	}

	[NativeMethod("alGetFilteriv"), CLSCompliant(false)]
	public static void GetFilterI(Filter filter, FilterProperty parameter, int* values)
	{
		alGetFilteriv(filter, parameter, values);
		CheckErrorState();
	}
	
	[NativeMethod("alGetFilteriv"), CLSCompliant(false)]
	public static void GetFilterI<T>(Filter filter, FilterProperty parameter, T* values) where T : unmanaged
	{
		if (Unsafe.SizeOf<T>() != sizeof(int))
			throw new ArgumentException("Generic type must be an unmanaged type 32-bits wide.", nameof(T));
	
		alGetFilteriv(filter, parameter, (int*) values);
		CheckErrorState();
	}

	[NativeMethod("alGetFilteriv")]
	public static void GetFilterI(Filter filter, FilterProperty parameter, Span<int> values)
	{
		fixed (int* ptr = &values[0])
		{
			alGetFilteriv(filter, parameter, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alGetFilteriv")]
	public static void GetFilterI(Filter filter, FilterProperty parameter, int[] values)
	{
		fixed (int* ptr = &values[0])
		{
			alGetFilteriv(filter, parameter, ptr);
		}
		CheckErrorState();
	}
}