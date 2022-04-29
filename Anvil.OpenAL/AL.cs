using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Anvil.Native;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

[assembly: CLSCompliant(true)]

namespace Anvil.OpenAL;

// TODO

public enum GetPName
{
    /// <summary>
    /// Doppler scale for velocities.
    /// </summary>
    /// <remarks>
    /// Type: float
    /// <para>Range: 0.0f - infinity</para>
    /// Default: 1.0f
    /// </remarks>
    DopplerFactor = 0xC000
}

[SuppressUnmanagedCodeSecurity]
public static unsafe partial class AL
{
    /// <summary>
    /// Asserts that the underlying enum type is a 32-bits wide.
    /// </summary>
    /// <typeparam name="TEnum">An enum type.</typeparam>
    /// <remarks>This method is conditional, and only present when a DEBUG compiler flag is present.</remarks>
    [Conditional("DEBUG")]
    internal static void DebugAssertInt32<TEnum>() where TEnum : Enum
    {
        Debug.Assert(Marshal.SizeOf(Enum.GetUnderlyingType(typeof(TEnum))) == sizeof(int));
    }

    [NativeMethod("alDopplerFactor")]
    public static void DopplerFactor(float value)
    {
        alDopplerFactor(value);
        CheckErrorState();
    }

    [NativeMethod("alDopplerVelocity")]
    public static void DopplerVelocity(float value)
    {
        alDopplerVelocity(value);
        CheckErrorState();
    }

    [NativeMethod("alSpeedOfSound")]
    public static void SpeedOfSound(float value)
    {
        alSpeedOfSound(value);
        CheckErrorState();
    }

    [NativeMethod("alDistanceModel")]
    public static void DistanceModel(DistanceModel distanceModel)
    {
        alDistanceModel(distanceModel);
        CheckErrorState();
    }

    [NativeMethod("alEnable")]
    public static void Enable(int capability)
    {
        alEnable(capability);
        CheckErrorState();
    }

    [NativeMethod("alDisable")]
    public static void Disable(int capability)
    {
        alDisable(capability);
        CheckErrorState();
    }

    [NativeMethod("alIsEnabled")]
    public static bool IsEnabled(int capability)
    {
        return alIsEnabled(capability);
    }

    [NativeMethod("alGetString")]
    public static string? GetString(int parameter)
    {
        var ptr = alGetString(parameter);
        CheckErrorState();
        var length = 0;
        while (ptr[length] != 0)
        {
            length++;
        }
        return length == 0 ? null : Encoding.UTF8.GetString(ptr, length);
    }

    [NativeMethod("alGetStringiSOFT")]
    public static string? GetString(int parameter, int index)
    {
        var ptr = alGetStringiSOFT(parameter, index);
        CheckErrorState();
        var length = 0;
        while (ptr[0] != 0)
        {
            length++;
        }

        return length == 0 ? null : Encoding.UTF8.GetString(ptr, length);
    }
    
    [NativeMethod("alGetStringiSOFT")]
    public static string? GetString<TEnum>(TEnum parameter, int index) where TEnum : Enum
    {
        DebugAssertInt32<TEnum>();
        var ptr = alGetStringiSOFT(Unsafe.As<TEnum, int>(ref parameter), index);
        CheckErrorState();
        var length = 0;
        while (ptr[0] != 0)
        {
            length++;
        }

        return length == 0 ? null : Encoding.UTF8.GetString(ptr, length);
    }
    
    [NativeMethod("alGetBoolean")]
    public static bool GetBoolean(int parameter)
    {
        var value = alGetBoolean(parameter);
        CheckErrorState();
        return value;
    }

    [NativeMethod("alGetInteger")]
    public static int GetInteger(int parameter)
    {
        var value = alGetInteger(parameter);
        CheckErrorState();
        return value;
    }

    [NativeMethod("alGetInteger")]
    public static TResult GetEnum<TResult>(int parameter) where TResult : Enum
    {
        DebugAssertInt32<TResult>();
        var result = alGetInteger(parameter);
        CheckErrorState();
        return Unsafe.As<int, TResult>(ref result);
    }

    [NativeMethod("alGetFloat")]
    public static float GetFloat(int parameter)
    {
        var value = alGetFloat(parameter);
        CheckErrorState();
        return value;
    }

    [NativeMethod("alGetDouble")]
    public static double GetDouble(int parameter)
    {
        var value = alGetDouble(parameter);
        CheckErrorState();
        return value;
    }

    [NativeMethod("alGetBoolean")]
    public static bool GetBoolean<TEnum>(TEnum parameter) where TEnum : Enum
    {
        DebugAssertInt32<TEnum>();
        var value = alGetBoolean(Unsafe.As<TEnum, int>(ref parameter));
        CheckErrorState();
        return value;
    }

    [NativeMethod("alGetInteger")]
    public static int GetInteger<TEnum>(TEnum parameter) where TEnum : Enum
    {
        DebugAssertInt32<TEnum>();
        var  value = alGetInteger(Unsafe.As<TEnum, int>(ref parameter));
        CheckErrorState();
        return value;
    }
    
    [NativeMethod("alGetInteger")]
    public static TResult GetEnum<TResult, TEnum>(TEnum parameter) where TEnum : Enum where TResult : Enum
    {
        DebugAssertInt32<TEnum>();
        DebugAssertInt32<TResult>();
        var result = alGetInteger(Unsafe.As<TEnum, int>(ref parameter));
        CheckErrorState();
        return Unsafe.As<int, TResult>(ref result);
    }

    [NativeMethod("alGetFloat")]
    public static float GetFloat<TEnum>(TEnum parameter) where TEnum : Enum
    {
        DebugAssertInt32<TEnum>();
        var value = alGetFloat(Unsafe.As<TEnum, int>(ref parameter));
        CheckErrorState();
        return value;
    }

    [NativeMethod("alGetDouble")]
    public static double GetDouble<TEnum>(TEnum parameter) where TEnum : Enum
    {
        DebugAssertInt32<TEnum>();
        var value = alGetDouble(Unsafe.As<TEnum, int>(ref parameter));
        CheckErrorState();
        return value;
    }
    
    [NativeMethod("alGetBooleanv"), CLSCompliant(false)]
    public static void GetBoolean(int parameter, bool *values)
    {
        alGetBooleanv(parameter, values);
        CheckErrorState();
    }

    [NativeMethod("alGetIntegerv"), CLSCompliant(false)]
    public static void GetInteger(int parameter, int *values)
    {
        alGetIntegerv(parameter, values);
        CheckErrorState();
    }

    [NativeMethod("alGetFloatv"), CLSCompliant(false)]
    public static void GetFloat(int parameter, float *values)
    {
        alGetFloatv(parameter, values);
        CheckErrorState();
    }

    [NativeMethod("alGetDoublev"), CLSCompliant(false)]
    public static void GetDouble(int parameter, double *values)
    {
        alGetDoublev(parameter, values);
        CheckErrorState();
    }

    [NativeMethod("alGetBooleanv")]
    public static void GetBoolean(int parameter, Span<bool> values)
    {
        fixed (bool* ptr = &values[0])
        {
            alGetBooleanv(parameter, ptr);
        }
        CheckErrorState();
    }

    [NativeMethod("alGetIntegerv")]
    public static void GetInteger(int parameter, Span<int> values)
    {
        fixed (int* ptr = &values[0])
        {
            alGetIntegerv(parameter, ptr);
        }
        CheckErrorState();
    }

    [NativeMethod("alGetFloatv")]
    public static void GetFloat(int parameter, Span<float> values)
    {
        fixed (float* ptr = &values[0])
        {
            alGetFloatv(parameter, ptr);
        }
        CheckErrorState();
    }

    [NativeMethod("alGetDoublev")]
    public static void GetDouble(int parameter, Span<double> values)
    {
        fixed (double* ptr = &values[0])
        {
            alGetDoublev(parameter, ptr);
        }
        CheckErrorState();
    }
    
    [NativeMethod("alGetBooleanv")]
    public static void GetBoolean(int parameter, bool[] values)
    {
        fixed (bool* ptr = &values[0])
        {
            alGetBooleanv(parameter, ptr);
        }
        CheckErrorState();
    }

    [NativeMethod("alGetIntegerv")]
    public static void GetInteger(int parameter, int[] values)
    {
        fixed (int* ptr = &values[0])
        {
            alGetIntegerv(parameter, ptr);
        }
        CheckErrorState();
    }

    [NativeMethod("alGetFloatv")]
    public static void GetFloat(int parameter, float[] values)
    {
        fixed (float* ptr = &values[0])
        {
            alGetFloatv(parameter, ptr);
        }
        CheckErrorState();
    }

    [NativeMethod("alGetDoublev")]
    public static void GetDouble(int parameter, double[] values)
    {
        fixed (double* ptr = &values[0])
        {
            alGetDoublev(parameter, ptr);
        }
        CheckErrorState();
    }
    
    [NativeMethod("alGetBooleanv"), CLSCompliant(false)]
    public static void GetBoolean<TEnum>(TEnum parameter, bool* values) where TEnum : Enum
    {
        DebugAssertInt32<TEnum>();
        alGetBooleanv(Unsafe.As<TEnum, int>(ref parameter), values);
        CheckErrorState();
    }

    [NativeMethod("alGetIntegerv"), CLSCompliant(false)]
    public static void GetInteger<TEnum>(TEnum parameter, int* values) where TEnum : Enum
    {
        DebugAssertInt32<TEnum>();
        alGetIntegerv(Unsafe.As<TEnum, int>(ref parameter), values);
        CheckErrorState();
    }

    [NativeMethod("alGetFloatv"), CLSCompliant(false)]
    public static void GetFloat<TEnum>(TEnum parameter, float* values) where TEnum : Enum
    {
        DebugAssertInt32<TEnum>();
        alGetFloatv(Unsafe.As<TEnum, int>(ref parameter), values);
        CheckErrorState();
    }

    [NativeMethod("alGetDoublev"), CLSCompliant(false)]
    public static void GetDouble<TEnum>(TEnum parameter, double* values) where TEnum : Enum
    {
        DebugAssertInt32<TEnum>();
        alGetDoublev(Unsafe.As<TEnum, int>(ref parameter), values);
        CheckErrorState();
    }
    
    [NativeMethod("alGetBooleanv")]
    public static void GetBoolean<TEnum>(TEnum parameter, Span<bool> values) where TEnum : Enum
    {
        DebugAssertInt32<TEnum>();
        fixed (bool* ptr = &values[0])
        {
            alGetBooleanv(Unsafe.As<TEnum, int>(ref parameter), ptr);
        }
        CheckErrorState();
    }

    [NativeMethod("alGetIntegerv")]
    public static void GetInteger<TEnum>(TEnum parameter, Span<int> values) where TEnum : Enum
    {
        DebugAssertInt32<TEnum>();
        fixed (int* ptr = &values[0])
        {
            alGetIntegerv(Unsafe.As<TEnum, int>(ref parameter), ptr);
        }
        CheckErrorState();
    }

    [NativeMethod("alGetFloatv")]
    public static void GetFloat<TEnum>(TEnum parameter, Span<float> values) where TEnum : Enum
    {
        DebugAssertInt32<TEnum>();
        fixed (float* ptr = &values[0])
        {
            alGetFloatv(Unsafe.As<TEnum, int>(ref parameter), ptr);
        }
        CheckErrorState();
    }

    [NativeMethod("alGetDoublev")]
    public static void GetDouble<TEnum>(TEnum parameter, Span<double> values) where TEnum : Enum
    {
        DebugAssertInt32<TEnum>();
        fixed (double* ptr = &values[0])
        {
            alGetDoublev(Unsafe.As<TEnum, int>(ref parameter), ptr);
        }
        CheckErrorState();
    }
    
    [NativeMethod("alGetBooleanv")]
    public static void GetBoolean<TEnum>(TEnum parameter, bool[] values) where TEnum : Enum
    {
        DebugAssertInt32<TEnum>();
        fixed (bool* ptr = &values[0])
        {
            alGetBooleanv(Unsafe.As<TEnum, int>(ref parameter), ptr);
        }
        CheckErrorState();
    }

    [NativeMethod("alGetIntegerv")]
    public static void GetInteger<TEnum>(TEnum parameter, int[] values) where TEnum : Enum
    {
        DebugAssertInt32<TEnum>();
        fixed (int* ptr = &values[0])
        {
            alGetIntegerv(Unsafe.As<TEnum, int>(ref parameter), ptr);
        }
        CheckErrorState();
    }

    [NativeMethod("alGetFloatv")]
    public static void GetFloat<TEnum>(TEnum parameter, float[] values) where TEnum : Enum
    {
        DebugAssertInt32<TEnum>();
        fixed (float* ptr = &values[0])
        {
            alGetFloatv(Unsafe.As<TEnum, int>(ref parameter), ptr);
        }
        CheckErrorState();
    }

    [NativeMethod("alGetDoublev")]
    public static void GetDouble<TEnum>(TEnum parameter, double[] values) where TEnum : Enum
    {
        DebugAssertInt32<TEnum>();
        fixed (double* ptr = &values[0])
        {
            alGetDoublev(Unsafe.As<TEnum, int>(ref parameter), ptr);
        }
        CheckErrorState();
    }
    
    [NativeMethod("alGetError")]
    public static Error GetError()
    {
        return alGetError();
    }

    [NativeMethod("alIsExtensionPresent"), ContractAnnotation("extensionName:null => false")]
    public static bool IsExtensionPresent(string? extensionName)
    {
        if (extensionName is null)
            return false;

        bool value;
        fixed (byte* ptr = &UTF8String.Pin(extensionName))
        {
            value = alIsExtensionPresent(ptr);
        }
        CheckErrorState();
        return value;
    }

    [NativeMethod("alGetProcAddress")]
    public static IntPtr GetProcAddress(string? name)
    {
        if (name is null)
            return IntPtr.Zero;

        IntPtr value;
        fixed (byte* ptr = &UTF8String.Pin(name))
        {
            value = new IntPtr(alGetProcAddress(ptr));
        }
        CheckErrorState();
        return value;
    }
    
    [NativeMethod("alGetProcAddress")]
    public static TDelegate GetProcAddress<TDelegate>(string name) where TDelegate : Delegate
    {
        TDelegate value;
        fixed (byte* ptr = &UTF8String.Pin(name))
        {
            var address = new IntPtr(alGetProcAddress(ptr));
            value = Marshal.GetDelegateForFunctionPointer<TDelegate>(address);
        }
        CheckErrorState();
        return value;
    }

    [NativeMethod("alGetEnumValue")]
    public static int GetEnumValue(string name)
    {
        int value;
        fixed (byte* ptr = &UTF8String.Pin(name))
        {
            value = alGetEnumValue(ptr);
        }
        CheckErrorState();
        return value;
    }
}
