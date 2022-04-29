using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security;
using Anvil.Native;
using JetBrains.Annotations;
// ReSharper disable StringLiteralTypo

namespace Anvil.OpenAL;

[PublicAPI]
public enum ListenerProperty
{
    /// <summary>
    /// The listener location in three dimensional space.
    /// <para/>
    /// OpenAL, like OpenGL, uses a right handed coordinate system, where in a frontal default view X (thumb) points
    /// right, Y points up (index finger), and Z points towards the viewer/camera (middle finger).
    /// <para/>
    /// To switch from a left handed coordinate system, flip the sign on the Z coordinate.
    /// </summary>
    /// <remarks>
    /// Type: Vector3 (int/float)
    /// <para>Default: (0,0,0)</para>
    /// </remarks>
    Position = 0x1004,
	
    /// <summary>
    /// Listener gain.
    /// <para/>
    /// A value of 1.0 means unattenuated. Each division by 2 equals an attenuation of about -6dB. Each multiplication
    /// by 2 equals an amplification of about +6dB.
    /// <para/>
    /// A value of 0.0 is meaningless with respect to a logarithmic scale; it is silent.
    /// </summary>
    /// <remarks>
    /// Type: float
    /// <para>Default: 1.0f</para>
    /// Range: 0.0f - infinity
    /// </remarks>
    Gain = 0x100A,
	
    /// <summary>
    /// Listener orientation.
    /// <para/>
    /// Effectively two three dimensional vectors. The first vector is the front (or "at") and the second is the top (or "up").
    /// <para/>
    /// Both vectors are in local space.
    /// </summary>
    /// <remarks>
    /// Type: float[6]
    /// <para>Default: {0.0, 0.0, -1.0, 0.0, 1.0, 0.0}</para>
    /// </remarks>
    Orientation = 0x100F,
    
    /// <summary>
    /// Specifies the current velocity in local space.
    /// </summary>
    /// <remarks>
    /// Type: Vector3 (int/float)
    /// <para>Default: (0,0,0)</para>
    /// </remarks>
    Velocity = 0x1006,
    
    /// <summary>
    /// Specify how many meters are represented by the units in other properties.
    /// </summary>
    /// <remarks>
    /// Type: float
    /// <para>Default: 1.0f</para>
    /// </remarks>
    MetersPerUnit = 0x20004
}



    
    
[SuppressUnmanagedCodeSecurity]
public static unsafe partial class AL
{
    [NativeMethod("alListenerf")]
    public static void ListenerF(ListenerProperty parameter, float value)
    {
        alListenerf(parameter, value);
        CheckErrorState();
    }

    [NativeMethod("alListener3f")]
    public static void ListenerF(ListenerProperty parameter, float value1, float value2, float value3)
    {
        alListener3f(parameter, value1, value2, value3);
        CheckErrorState();
    }

    [NativeMethod("alListenerfv"), CLSCompliant(false)]
    public static void ListenerF(ListenerProperty parameter, float *values)
    {
        alListenerfv(parameter, values);
        CheckErrorState();
    }

    [NativeMethod("alListenerfv")]
    public static void ListenerF(ListenerProperty parameter, ReadOnlySpan<float> values)
    {
        fixed (float* ptr = &values[0])
        {
            alListenerfv(parameter, ptr);
        }
        CheckErrorState();
    }
    
    [NativeMethod("alListenerfv")]
    public static void ListenerF(ListenerProperty parameter, float[] values)
    {
        fixed (float* ptr = &values[0])
        {
            alListenerfv(parameter, ptr);
        }
        CheckErrorState();
    }
    
    [NativeMethod("alListenerfv")]
    public static void ListenerF(ListenerProperty parameter, Vector3 values)
    {
        alListenerfv(parameter, &values.X);
        CheckErrorState();
    }
    
    [NativeMethod("alListenerfv")]
    public static void ListenerOrientation((Vector3, Vector3) orientation)
    {
        alListenerfv(ListenerProperty.Orientation, &orientation.Item1.X);
        CheckErrorState();
    }

    [NativeMethod("alListenerfv"), CLSCompliant(false)]
    public static void ListenerF(ListenerProperty parameter, Vector3 *value)
    {
        alListenerfv(parameter, (float*) value);
        CheckErrorState();
    }

    [NativeMethod("alListeneri")]
    public static void ListenerI(ListenerProperty parameter, int value)
    {
        alListeneri(parameter, value);
        CheckErrorState();
    }
    
    [NativeMethod("alListeneri")]
    public static void ListenerI<TEnum>(ListenerProperty parameter, TEnum value) where TEnum : Enum
    {
        DebugAssertInt32<TEnum>();
        alListeneri(parameter, Unsafe.As<TEnum, int>(ref value));
        CheckErrorState();
    }

    [NativeMethod("alListener3i")]
    public static void ListenerI(ListenerProperty parameter, int value1, int value2, int value3)
    {
        alListener3i(parameter, value1, value2, value3);
        CheckErrorState();
    }

    [NativeMethod("alListeneriv"), CLSCompliant(false)]
    public static void ListenerI(ListenerProperty parameter, int *values)
    {
        alListeneriv(parameter, values);
        CheckErrorState();
    }

    [NativeMethod("alListeneriv")]
    public static void ListenerI(ListenerProperty parameter, ReadOnlySpan<int> values)
    {
        fixed (int* ptr = &values[0])
        {
            alListeneriv(parameter, ptr);
        }
        CheckErrorState();
    }
    
    [NativeMethod("alListeneriv")]
    public static void ListenerI(ListenerProperty parameter, int[] values)
    {
        fixed (int* ptr = &values[0])
        {
            alListeneriv(parameter, ptr);
        }
        CheckErrorState();
    }
    
    [NativeMethod("alGetListenerf"), Pure]
    public static float GetListenerF(ListenerProperty parameter)
    {
        float temp = default;
        alGetListenerf(parameter, &temp);
        CheckErrorState();
        return temp;
    }
    
    [NativeMethod("alGetListenerfv"), Pure]
    public static Vector3 GetListenerV(ListenerProperty parameter)
    {
        Vector3 temp = default;
        alGetListenerfv(parameter, &temp.X);
        CheckErrorState();
        return temp;
    }
    
    [NativeMethod("alGetListenerfv"), Pure]
    public static (Vector3, Vector3) GetListenerOrientation()
    {
        (Vector3, Vector3) temp = default;
        alGetListenerfv(ListenerProperty.Orientation, &temp.Item1.X);
        CheckErrorState();
        return temp;
    }

    [NativeMethod("alGetListener3f"), CLSCompliant(false)]
    public static void GetListenerF(ListenerProperty parameter, float *value1, float *value2, float *value3)
    {
        alGetListener3f(parameter, value1, value2, value3);
        CheckErrorState();
    }

    [NativeMethod("alGetListener3f")]
    public static void GetListenerF(ListenerProperty parameter, out float value1, out float value2, out float value3)
    {
        var v = stackalloc float[3];
        alGetListener3f(parameter, &v[0], &v[1], &v[2]);
        CheckErrorState();
        value1 = v[0];
        value2 = v[1];
        value3 = v[2];
    }

    [NativeMethod("alGetListenerfv"), CLSCompliant(false)]
    public static void GetListenerF(ListenerProperty parameter, float *values)
    {
        alGetListenerfv(parameter, values);
        CheckErrorState();
    }

    [NativeMethod("alGetListenerfv")]
    public static void GetListenerF(ListenerProperty parameter, Span<float> values)
    {
        fixed (float* ptr = &values[0])
        {
            alGetListenerfv(parameter, ptr);
        }
        CheckErrorState();
    }
    
    [NativeMethod("alGetListenerfv")]
    public static void GetListenerF(ListenerProperty parameter, float[] values)
    {
        fixed (float* ptr = &values[0])
        {
            alGetListenerfv(parameter, ptr);
        }
        CheckErrorState();
    }
    
    [NativeMethod("alGetListenerfv")]
    public static void GetListenerF(ListenerProperty parameter, ref Vector3 value)
    {
        fixed (float* ptr = &value.X)
        {
            alGetListenerfv(parameter, ptr);
        }
        CheckErrorState();
    }
    
    [NativeMethod("alGetListenerfv"), CLSCompliant(false)]
    public static void GetListenerF(ListenerProperty parameter, Vector3 *value)
    {
        alGetListenerfv(parameter, (float*) value);
        CheckErrorState();
    }
    
    [NativeMethod("alGetListeneri"), Pure]
    public static int GetListenerI(ListenerProperty parameter)
    {
        int temp = default;
        alGetListeneri(parameter, &temp);
        CheckErrorState();
        return temp;
    }
    
    [NativeMethod("alGetListeneri"), Pure]
    public static TEnum GetListenerI<TEnum>(ListenerProperty parameter) where TEnum : Enum
    {
        DebugAssertInt32<TEnum>();
        int temp = default;
        alGetListeneri(parameter, &temp);
        CheckErrorState();
        return Unsafe.As<int, TEnum>(ref temp);
    }

    [NativeMethod("alGetListener3i"), CLSCompliant(false)]
    public static void GetListenerI(ListenerProperty parameter, int *value1, int *value2, int *value3)
    {
        alGetListener3i(parameter, value1, value2, value3);
        CheckErrorState();
    }

    [NativeMethod("alGetListener3i")]
    public static void GetListenerI(ListenerProperty parameter, out int value1, out int value2, out int value3)
    {
        var v = stackalloc int[3];
        alGetListener3i(parameter, &v[0], &v[1], &v[2]);
        CheckErrorState();
        value1 = v[0];
        value2 = v[1];
        value3 = v[2];
    }

    [NativeMethod("alGetListeneriv"), CLSCompliant(false)]
    public static void GetListenerI(ListenerProperty parameter, int *values)
    {
        alGetListeneriv(parameter, values);
        CheckErrorState();
    }

    [NativeMethod("alGetListeneriv")]
    public static void GetListenerI(ListenerProperty parameter, Span<int> values)
    {
        fixed (int* ptr = &values[0])
        {
            alGetListeneriv(parameter, ptr);
        }
        CheckErrorState();
    }
    
    [NativeMethod("alGetListeneriv")]
    public static void GetListenerI(ListenerProperty parameter, int[] values)
    {
        fixed (int* ptr = &values[0])
        {
            alGetListeneriv(parameter, ptr);
        }
        CheckErrorState();
    }
}