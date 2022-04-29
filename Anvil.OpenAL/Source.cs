using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security;
using Anvil.Native;
using JetBrains.Annotations;

// ReSharper disable StringLiteralTypo

namespace Anvil.OpenAL;


/// <summary>
/// Parameters to query/change the properties of <see cref="Source"/> objects.
/// </summary>
[PublicAPI]
public enum SourceProperty
{

	/// <summary>
	/// Specifies if the Source has relative coordinates.
	/// </summary>
	/// <remarks>
	/// Type: Boolean
	/// <para>Default: false</para>
	/// </remarks>
	SourceRelative = 0x202,

	/// <summary>
	/// The angle covered by the inner cone, where the source will not attenuate.
	/// </summary>
	/// <remarks>
	///	Type: int, float
	/// <para>Default: 360.0f</para>
	/// Range: 0 - 360
	/// </remarks>
	ConeInnerAngle = 0x1001,

	/// <summary>
	/// The angle covered by the outer cone, where the source will be fully attenuated.
	/// </summary>
	/// <remarks>
	///	Type: int, float
	/// <para>Default: 360.0f</para>
	/// Range: 0 - 360
	/// </remarks>
	ConeOuterAngle = 0x1002,

	/// <summary>
	/// A multiplier for the frequency (sample rate) of the source's buffer.
	/// </summary>
	/// <remarks>
	///	Type: float
	/// <para>Default: 1.0f</para>
	/// Range: 0.5f - 2.0f
	/// </remarks>
	Pitch = 0x1003,

	/// <summary>
	/// The source location in three dimensional space.
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
	/// Specifies the current direction in local space.
	/// <para>A zero-length vector specifies an omni-directional source (cone is ignored).</para>
	/// </summary>
	/// <remarks>
	/// Type: Vector3 (int/float)
	/// <para>Default: (0,0,0)</para>
	/// </remarks>
	Direction = 0x1005,

	/// <summary>
	/// Specifies the current velocity in local space.
	/// </summary>
	/// <remarks>
	/// Type: Vector3 (int/float)
	/// <para>Default: (0,0,0)</para>
	/// </remarks>
	Velocity = 0x1006,

	/// <summary>
	/// Specifies whether source is looping.
	/// </summary>
	/// <remarks>
	///	Type: Boolean
	/// Default: false
	/// </remarks>
	Looping = 0x1007,

	/// <summary>
	/// Specifies the buffer to provide sound samples.
	/// </summary>
	/// <remarks>
	/// Type: Buffer, int
	/// </remarks>
	Buffer = 0x1009,

	/// <summary>
	/// Source gain.
	/// <para/>
	/// A value of 1.0 means unattenuated. Each division by 2 equals an attenuation of about -6dB. Each multiplication by 2 equals an amplification of about +6dB.
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
	/// Minimum source gain.
	/// <para/>
	/// The minimum gain allowed for a source, after distance and cone attenuation is applied (if applicable).
	/// </summary>
	/// <remarks>
	/// Type: float
	/// <para>Range: 0.0f - 1.0f</para>
	/// </remarks>
	MinGain = 0x100D,

	/// <summary>
	/// Maximum source gain.
	/// <para/>
	/// The maximum gain allowed for a source, after distance and cone attenuation is applied (if applicable).
	/// </summary>
	/// <remarks>
	/// Type: float
	/// <para>Range: 0.0f - 1.0f</para>
	/// </remarks>
	MaxGain = 0x100E,

	/// <summary>
	/// Source state (query only).
	/// </summary>
	/// <remarks>
	/// Type: int, <see cref="Anvil.OpenAL.SourceState"/>
	/// </remarks>
	SourceState = 0x1010,

	/// <summary>
	/// Source Buffer Queue size (query only).
	/// <para/>
	/// The number of buffers queued using alSourceQueueBuffers, minus the buffers removed with
	/// <see cref="AL.SourceUnqueueBuffers(Source,OpenAL.Buffer[])"/>
	/// </summary>
	/// <remarks>Type: int</remarks>
	BuffersQueued = 0x1015,

	/// <summary>
	///  Buffer Queue processed count (query only).
	/// <para/>
	/// The number of queued buffers that have been fully processed, and can be removed with
	/// <see cref="AL.SourceUnqueueBuffers(Source,OpenAL.Buffer[])"/>.
	/// <para/>
	/// Looping sources will never fully process buffers because they will be set to play again for when the source loops.
	/// </summary>
	/// <remarks>Type: int</remarks>
	BuffersProcessed = 0x1016,

	/// <summary>
	/// Source reference distance.
	/// <para/>
	/// The distance in units that no attenuation occurs.
	/// <para/>
	/// At 0.0, no distance attenuation ever occurs on non-linear attenuation models.
	/// </summary>
	/// <remarks>
	/// Type: float
	/// <para>Range: 0.0f - infinity</para>
	/// Default: 1.0f
	/// </remarks>
	ReferenceDistance = 0x1020,

	/// <summary>
	/// Source rolloff factor.
	/// <para/>
	/// Multiplier to exaggerate or diminish distance attenuation.
	/// <para/>
	/// At 0.0, no distance attenuation ever occurs.
	/// </summary>
	/// <remarks>
	/// Type: float
	/// <para>Range: 0.0f - infinity</para>
	/// Default: 1.0f
	/// </remarks>
	RolloffFactor = 0x1021,

	/// <summary>
	/// Outer cone gain.
	/// <para/>
	/// The gain attenuation applied when the listener is outside of the source's outer cone.
	/// </summary>
	/// <remarks>
	/// Type: float
	/// <para>Range: 0.0f - infinity</para>
	/// Default: 0.0f
	/// </remarks>
	ConeOuterGain = 0x1022,

	/// <summary>
	/// Source maximum distance.
	/// <para/>
	/// The distance above which the source is not attenuated any further with a clamped distance model, or where
	/// attenuation reaches 0.0 gain for linear distance models with a default rolloff factor.
	/// </summary>
	/// <remarks>
	/// Type: float
	/// <para>Range: 0.0f - infinity</para>
	/// Default: float.MaxValue
	/// </remarks>
	MaxDistance = 0x1023,

	/// <summary>
	/// Source type (query only).
	/// <para/>
	/// A Source is Static if a Buffer has been attached using <see cref="Buffer"/>.
	/// <para>A Source is Streaming if one or more Buffers have been attached using SourceQueueBuffers.</para>
	/// A Source is Undetermined when it has the Buffer.None buffer attached using <see cref="Buffer"/>.
	/// </summary>
	/// <remarks>
	/// Type: int, <see cref="Anvil.OpenAL.SourceType"/>
	/// </remarks>
	SourceType = 0x1027,
	
	/// <summary>
	/// Doppler scale for velocities.
	/// </summary>
	/// <remarks>
	/// Type: float
	/// <para>Range: 0.0f - infinity</para>
	/// Default: 1.0f
	/// </remarks>
	DopplerFactor = 0xC000,
	
	/// <summary>
	/// // TODO
	/// </summary>
	DirectFilter = 0x20005,
	
	/// <summary>
	/// // TODO
	/// </summary>
	AuxiliarySendFilter = 0x20006,
	
	/// <summary>
	/// A multiplier on the amount of air absorption applied to the Source. The factor is multiplied
	/// by an internal air absorption gain HF value of <c>0.994</c> (-0.05dB) per meter which represents normal
	/// atmospheric humidity and temperature.
	/// <para/>
	/// By default the value is set to 0 which means that Air Absorption effects are disabled. A value of <c>1.0</c>
	/// will tell the Effects Extension engine to apply high frequency attenuation on the direct path of the source at a
	/// rate of 0.05dB per meter. 
	/// </summary>
	/// <remarks>
	/// Type: float
	/// <para>Range: 0.0f - 10.0f</para>
	/// Default: 0.0f
	/// </remarks>
	AirAbsorptionFactor = 0x20007,
	
	/// <summary>
	/// Attenuate the reflected sound (early reflections and reverberation) according to source-listener distance. In
	/// this case, however, room rolloff applies only to this sound source, and therefore affects only the reflected
	/// sound generated by this source.
	/// </summary>
	/// <remarks>
	/// Type: float
	/// <para>Range: 0.0f - 10.0f</para>
	/// Default: 0.0f
	/// </remarks>
	RoomRolloffFactor = 0x20008,
	
	/// <summary>
	/// The directivity high-frequency attenuation for both the direct-path and the reflected sounds of the sound source.
	/// </summary>
	/// <remarks>
	/// Type: float
	/// <para>Range: 0.0f - 10.0f</para>
	/// Default: 0.0f
	/// </remarks>
	ConeOuterGainHF = 0x20009,
	
	/// <summary>
	/// Determines if the direct-path is automatically filtered according to the orientation of the source relative to
	/// the listener.
	/// </summary>
	/// <remarks>
	/// Type: bool
	/// Default: true
	/// </remarks>
	DirectFilterGainHFAuto = 0x2000A,
	
	/// <summary>
	/// Determines if the intensity of the source’s reflected sound is automatically attenuated according to
	/// source-listener distance and source directivity (as determined by the cone parameters). If it is <c>false</c>,
	/// the reflected sound is not attenuated according to distance and directivity.
	/// </summary>
	/// <remarks>
	/// Type: bool
	/// Default: true
	/// </remarks>
	AuxiliarySendFilterGainAuto = 0x2000B,
	
	/// <summary>
	/// Determines if the intensity of a source’s reflected sound at high frequencies will be automatically attenuated
	/// according to the high-frequency source directivity as set by the <see cref="ConeOuterGainHF"/> property. If
	/// <see cref="ConeOuterGainHF"/> is set to <c>1.0</c>, the source is not more directive at high frequencies and
	/// this property has no effect. Otherwise, making the Source more directive at high frequencies will have the
	/// natural effect of reducing the amount of high frequencies in the reflected sound.
	/// <para/>
	/// If this property is <c>false</c>, the source’s reflected sound is not filtered at all according to the source
	/// directivity. 
	/// </summary>
	/// <remarks>
	/// Type: bool
	/// Default: true
	/// </remarks>
	AuxiliarySendFilterGainHFAuto = 0x2000C,
	
	/// <summary>
	/// Apply spatialization (i.e. panning, distance attenuation, etc) to sounds.
	/// </summary>
	Spatialize = 0x1214, // TODO
	
	/// <summary>
	/// TODO
	/// </summary>
	[RequiredExtension("AL_EXT_source_distance_model")]
	DistanceModel = 0x200
}

/// <summary>
/// Describes the behavior of OpenAL regarding how it applies spatialization to sounds.
/// </summary>
[PublicAPI]
public enum SourceSpatialization
{
	/// <summary>
	/// Disable spatialization of all sources.
	/// </summary>
	None = 0x0000,
	
	/// <summary>
	/// Attempt to apply spatialization to all sources.
	/// </summary>
	All = 0x0001,
	
	/// <summary>
	/// Enable for single-channel buffers, disable for multi-channel.
	/// </summary>
	Auto = 0x0002
}

/// <summary>
/// Describes the source type.
/// </summary>
[PublicAPI]
public enum SourceType
{
	/// <summary>
	/// Static source using a single buffer.
	/// </summary>
	Static = 0x1028,
	
	/// <summary>
	/// Streaming with multiple queued buffers.
	/// </summary>
	Streaming = 0x1029,
	
	/// <summary>
	/// Initial state before buffers have been added to the source, and cannot yet be determined.
	/// </summary>
	Undetermined = 0x1030
}

[PublicAPI]
public enum OffsetType
{
	/// <summary>
	/// Source buffer position, in seconds.
	/// </summary>
	Seconds = 0x1024,
	
	/// <summary>
	/// Source buffer position, in sample frames.
	/// </summary>
	Samples = 0x1025,
	
	/// <summary>
	/// Source buffer position, in bytes.
	/// </summary>
	Bytes = 0x1026,
}

[SuppressUnmanagedCodeSecurity]
public static unsafe partial class AL
{
	[NativeMethod("alGenSources")]
	public static Source GenSource()
	{
		Source source = default;
		alGenSources(1, &source);
		CheckErrorState();
		return source;
	}

	public static void DeleteSource(Source source)
	{
		alDeleteSources(1, &source);
		CheckErrorState();
	}

	[NativeMethod("alGenSources"), CLSCompliant(false)]
	public static void GenSources(int n, Source *sources)
	{
		alGenSources(n, sources);
		CheckErrorState();
	}

	[NativeMethod("alGenSources")]
	public static void GenSources(Span<Source> sources)
	{
		fixed (Source* ptr = &sources[0])
		{
			alGenSources(sources.Length, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alGenSources")]
	public static void GenSources(Source[] sources)
	{
		fixed (Source* ptr = &sources[0])
		{
			alGenSources(sources.Length, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alDeleteSources"), CLSCompliant(false)]
	public static void DeleteSources(int n, Source  *sources)
	{
		alDeleteSources(n, sources);
		CheckErrorState();
	}

	[NativeMethod("alDeleteSources")]
	public static void DeleteSources(Span<Source> sources)
	{
		fixed (Source* ptr = &sources[0])
		{
			alDeleteSources(sources.Length, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alDeleteSources")]
	public static void DeleteSources(Source[] sources)
	{
		fixed (Source* ptr = &sources[0])
		{
			alDeleteSources(sources.Length, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alIsSource"), ContractAnnotation("source:null => false")]
	public static bool IsSource(Source? source)
	{
		return source.HasValue && alIsSource(source.Value);
	}

	[NativeMethod("alSourcePlayv"), CLSCompliant(false)]
	public static void SourcePlay(int n, Source *sources)
	{
		alSourcePlayv(n, sources);
		CheckErrorState();
	}

	[NativeMethod("alSourceStopv"), CLSCompliant(false)]
	public static void SourceStop(int n, Source *sources)
	{
		alSourceStopv(n, sources);
		CheckErrorState();
	}

	[NativeMethod("alSourceRewindv"), CLSCompliant(false)]
	public static void SourceRewind(int n, Source *sources)
	{
		alSourceRewindv(n, sources);
		CheckErrorState();
	}

	[NativeMethod("alSourcePausev"), CLSCompliant(false)]
	public static void SourcePause(int n, Source *sources)
	{
		alSourcePausev(n, sources);
		CheckErrorState();
	}

	[NativeMethod("alSourcePlayv")]
	public static void SourcePlay(ReadOnlySpan<Source> sources)
	{
		fixed (Source* ptr = &sources[0])
		{
			alSourcePlayv(sources.Length, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alSourceStopv")]
	public static void SourceStop(ReadOnlySpan<Source> sources)
	{
		fixed (Source* ptr = &sources[0])
		{
			alSourceStopv(sources.Length, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alSourceRewindv")]
	public static void SourceRewind(ReadOnlySpan<Source> sources)
	{
		fixed (Source* ptr = &sources[0])
		{
			alSourceRewindv(sources.Length, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alSourcePausev")]
	public static void SourcePause(ReadOnlySpan<Source> sources)
	{
		fixed (Source* ptr = &sources[0])
		{
			alSourcePausev(sources.Length, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alSourcePlayv")]
	public static void SourcePlay(Source[] sources)
	{
		fixed (Source* ptr = &sources[0])
		{
			alSourcePlayv(sources.Length, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alSourceStopv")]
	public static void SourceStop(Source[] sources)
	{
		fixed (Source* ptr = &sources[0])
		{
			alSourceStopv(sources.Length, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alSourceRewindv")]
	public static void SourceRewind(Source[] sources)
	{
		fixed (Source* ptr = &sources[0])
		{
			alSourceRewindv(sources.Length, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alSourcePausev")]
	public static void SourcePause(Source[] sources)
	{
		fixed (Source* ptr = &sources[0])
		{
			alSourcePausev(sources.Length, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alSourcePlay")]
	public static void SourcePlay(Source source)
	{
		alSourcePlay(source);
		CheckErrorState();
	}

	[NativeMethod("alSourceStop")]
	public static void SourceStop(Source source)
	{
		alSourceStop(source);
		CheckErrorState();
	}

	[NativeMethod("alSourceRewind")]
	public static void SourceRewind(Source source)
	{
		alSourceRewind(source);
		CheckErrorState();
	}

	[NativeMethod("alSourcePause")]
	public static void SourcePause(Source source)
	{
		alSourcePause(source);
		CheckErrorState();
	}
	
	[NativeMethod("alSourceQueueBuffers"), CLSCompliant(false)]
	public static void SourceQueueBuffer(Source source, Buffer buffer)
	{
		alSourceQueueBuffers(source, 1, &buffer);
		CheckErrorState();
	}

	[NativeMethod("alSourceQueueBuffers"), CLSCompliant(false)]
	public static void SourceQueueBuffers(Source source, int nb, Buffer *buffers)
	{
		alSourceQueueBuffers(source, nb, buffers);
		CheckErrorState();
	}

	[NativeMethod("alSourceUnqueueBuffers"), CLSCompliant(false)]
	public static void SourceUnqueueBuffers(Source source, int nb, Buffer *buffers)
	{
		alSourceUnqueueBuffers(source, nb, buffers);
		CheckErrorState();
	}

	[NativeMethod("alSourceQueueBuffers")]
	public static void SourceQueueBuffers(Source source, ReadOnlySpan<Buffer> buffers)
	{
		fixed (Buffer* ptr = &buffers[0])
		{
			alSourceQueueBuffers(source, buffers.Length, ptr);   
		}
		CheckErrorState();
	}
	
	[NativeMethod("alSourceUnqueueBuffers")]
	public static Buffer SourceUnqueueBuffer(Source source)
	{
		Buffer buffer;
		alSourceUnqueueBuffers(source, 1, &buffer); 
		CheckErrorState();
		return buffer;
	}

	[NativeMethod("alSourceUnqueueBuffers")]
	public static void SourceUnqueueBuffers(Source source, ReadOnlySpan<Buffer> buffers)
	{
		fixed (Buffer* ptr = &buffers[0])
		{
			alSourceUnqueueBuffers(source, buffers.Length, ptr);   
		}
		CheckErrorState();
	}
	
	[NativeMethod("alSourceQueueBuffers")]
	public static void SourceQueueBuffers(Source source, Buffer[] buffers)
	{
		fixed (Buffer* ptr = &buffers[0])
		{
			alSourceQueueBuffers(source, buffers.Length, ptr);   
		}
		CheckErrorState();
	}

	[NativeMethod("alSourceUnqueueBuffers")]
	public static void SourceUnqueueBuffers(Source source, Buffer[] buffers)
	{
		fixed (Buffer* ptr = &buffers[0])
		{
			alSourceUnqueueBuffers(source, buffers.Length, ptr);   
		}
		CheckErrorState();
	}
	
	[NativeMethod("alSourcef")]
	public static void SourceF(Source source, SourceProperty parameter, float value)
	{
		alSourcef(source, parameter, value);
		CheckErrorState();
	}

	[NativeMethod("alSource3f")]
	public static void SourceF(Source source, SourceProperty parameter, float value1, float value2, float value3)
	{
		alSource3f(source, parameter, value1, value2, value3);
		CheckErrorState();
	}

	[NativeMethod("alSourcefv"), CLSCompliant(false)]
	public static void SourceF(Source source, SourceProperty parameter, float* values)
	{
		alSourcefv(source, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alSourcefv")]
	public static void SourceF(Source source, SourceProperty parameter, ReadOnlySpan<float> values)
	{
		fixed (float* ptr = &values[0])
		{
			alSourcefv(source, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alSourcefv")]
	public static void SourceF(Source source, SourceProperty parameter, float[] values)
	{
		fixed (float* ptr = &values[0])
		{
			alSourcefv(source, parameter, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alSourcefv")]
	public static void SourceF(Source source, SourceProperty parameter, Vector3 value)
	{
		alSourcefv(source, parameter, &value.X);
		CheckErrorState();
	}
	
	[NativeMethod("alSourcefv"), CLSCompliant(false)]
	public static void SourceF(Source source, SourceProperty parameter, Vector3 *value)
	{
		alSourcefv(source, parameter, (float*) value);
		CheckErrorState();
	}
	
	[NativeMethod("alSourcei")]
	public static void SourceB(Source source, SourceProperty parameter, bool value)
	{
		alSourcei(source, parameter, value ? TRUE : FALSE);
		CheckErrorState();
	}

	[NativeMethod("alSourcei")]
	public static void SourceI(Source source, SourceProperty parameter, int value)
	{
		alSourcei(source, parameter, value);
		CheckErrorState();
	}

	[NativeMethod("alSourcei")]
	public static void SourceI<T>(Source source, SourceProperty parameter, T value) where T : unmanaged
	{
		if (Unsafe.SizeOf<T>() != sizeof(int))
			throw new ArgumentException("Generic type must be an unmanaged type 32-bits wide.", nameof(T));
		
		alSourcei(source, parameter, Unsafe.As<T, int>(ref value));
		CheckErrorState();
	}

	[NativeMethod("alSource3i")]
	public static void SourceI(Source source, SourceProperty parameter, int value1, int value2, int value3)
	{
		alSource3i(source, parameter, value1, value2, value3);
		CheckErrorState();
	}
	
	[NativeMethod("alSource3i")]
	public static void SourceI(Source source, SourceProperty parameter, EffectSlot? slot, int send, Filter? filter)
	{
		alSource3i(source, parameter, (slot ?? default).Value, send, (filter ?? default).Value);
		CheckErrorState();
	}

	[NativeMethod("alSourceiv"), CLSCompliant(false)]
	public static void SourceI(Source source, SourceProperty parameter, int* values)
	{
		alSourceiv(source, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alSourceiv")]
	public static void SourceI(Source source, SourceProperty parameter, ReadOnlySpan<int> values)
	{
		fixed (int* ptr = &values[0])
		{
			alSourceiv(source, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alSourceiv")]
	public static void SourceI(Source source, SourceProperty parameter, int[] values)
	{
		fixed (int* ptr = &values[0])
		{
			alSourceiv(source, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alGetSourcef")]
	public static float GetSourceF(Source source, SourceProperty parameter)
	{
		float temp = default;
		alGetSourcef(source, parameter, &temp);
		CheckErrorState();
		return temp;
	}

	[NativeMethod("alGetSourcef")]
	public static void GetSourceF(Source source, SourceProperty parameter, out float value)
	{
		float temp = default;
		alGetSourcef(source, parameter, &temp);
		CheckErrorState();
		value = temp;
	}

	[NativeMethod("alGetSource3f"), CLSCompliant(false)]
	public static void GetSourceF(Source source, SourceProperty parameter, float* value1, float* value2, float* value3)
	{
		alGetSource3f(source, parameter, value1, value2, value3);
		CheckErrorState();
	}

	[NativeMethod("alGetSource3f")]
	public static void GetSourceF(Source source, SourceProperty parameter, out float value1, out float value2, out float value3)
	{
		var v = stackalloc float[3];
		alGetSource3f(source, parameter, &v[0], &v[1], &v[2]);
		CheckErrorState();
		value1 = v[0];
		value2 = v[1];
		value3 = v[2];
	}

	[NativeMethod("alGetSourcefv"), CLSCompliant(false)]
	public static void GetSourceF(Source source, SourceProperty parameter, float* values)
	{
		alGetSourcefv(source, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alGetSourcefv")]
	public static void GetSourceF(Source source, SourceProperty parameter, Span<float> values)
	{
		fixed (float* ptr = &values[0])
		{
			alGetSourcefv(source, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alGetSourcefv")]
	public static void GetSourceF(Source source, SourceProperty parameter, float[] values)
	{
		fixed (float* ptr = &values[0])
		{
			alGetSourcefv(source, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alGetSourcefv"), CLSCompliant(false)]
	public static void GetSourceF(Source source, SourceProperty parameter, Vector3 *value)
	{
		alGetSourcefv(source, parameter, (float*) value);
		CheckErrorState();
	}

	[NativeMethod("alGetSourcefv")]
	public static void GetSourceF(Source source, SourceProperty parameter, out Vector3 value)
	{
		Vector3 temp = default;
		alGetSourcefv(source, parameter, &temp.X);
		CheckErrorState();
		value = temp;
	}
	
	[NativeMethod("alGetSourcei")]
	public static int GetSourceI(Source source, SourceProperty parameter)
	{
		int temp = default;
		alGetSourcei(source, parameter, &temp);
		CheckErrorState();
		return temp;
	}
	
	[NativeMethod("alGetSourcei")]
	public static bool GetSourceB(Source source, SourceProperty parameter)
	{
		int temp = default;
		alGetSourcei(source, parameter, &temp);
		CheckErrorState();
		return temp != FALSE;
	}
	
	[NativeMethod("alGetSourcei")]
	public static T GetSourceI<T>(Source source, SourceProperty parameter) where T : unmanaged
	{
		if (Unsafe.SizeOf<T>() != sizeof(int))
			throw new ArgumentException("Generic type must be an unmanaged type 32-bits wide.", nameof(T));
		
		int temp = default;
		alGetSourcei(source, parameter, &temp);
		CheckErrorState();
		return Unsafe.As<int, T>(ref temp);
	}
	
	[NativeMethod("alGetSourcei")]
	public static void GetSourceI(Source source, SourceProperty parameter, out int value)
	{
		int temp = default;
		alGetSourcei(source, parameter, &temp);
		CheckErrorState();
		value = temp;
	}
	
	[NativeMethod("alGetSourcei")]
	public static void GetSourceI(Source source, SourceProperty parameter, out Buffer value)
	{
		int temp = default;
		alGetSourcei(source, parameter, &temp);
		CheckErrorState();
		value = Unsafe.As<int, Buffer>(ref temp);
	}
	
	[NativeMethod("alGetSourcei")]
	public static void GetSourceI(Source source, SourceProperty parameter, out Filter value)
	{
		int temp = default;
		alGetSourcei(source, parameter, &temp);
		CheckErrorState();
		value = Unsafe.As<int, Filter>(ref temp);
	}
	
	[NativeMethod("alGetSourcei")]
	public static void GetSource<TEnum>(Source source, SourceProperty parameter, out TEnum value) where TEnum : Enum
	{
		DebugAssertInt32<TEnum>();
		int temp = default;
		alGetSourcei(source, parameter, &temp);
		CheckErrorState();
		value = Unsafe.As<int, TEnum>(ref temp);
	}

	[NativeMethod("alGetSource3i"), CLSCompliant(false)]
	public static void GetSourceI(Source source, SourceProperty parameter, int* value1, int* value2, int* value3)
	{
		alGetSource3i(source, parameter, value1, value2, value3);
		CheckErrorState();
	}

	[NativeMethod("alGetSource3i")]
	public static void GetSourceI(Source source, SourceProperty parameter, out int value1, out int value2, out int value3)
	{
		var v = stackalloc int[3];
		alGetSource3i(source, parameter, &v[0], &v[1], &v[2]);
		CheckErrorState();
		value1 = v[0];
		value2 = v[1];
		value3 = v[2];
	}

	[NativeMethod("alGetSourceiv"), CLSCompliant(false)]
	public static void GetSourceI(Source source, SourceProperty parameter, int* values)
	{
		alGetSourceiv(source, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alGetSourceiv")]
	public static void GetSourceI(Source source, SourceProperty parameter, Span<int> values)
	{
		fixed (int* ptr = &values[0])
		{
			alGetSourceiv(source, parameter, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alGetSourceiv")]
	public static void GetSourceI(Source source, SourceProperty parameter, int[] values)
	{
		fixed (int* ptr = &values[0])
		{
			alGetSourceiv(source, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alGetSourceiv"), CLSCompliant(false)]
	public static void GetSourceI(Source source, SourceProperty parameter, Buffer* values)
	{
		alGetSourceiv(source, parameter, (int*) values);
		CheckErrorState();
	}

	[NativeMethod("alGetSourceiv")]
	public static void GetSourceI(Source source, SourceProperty parameter, Span<Buffer> values)
	{
		fixed (Buffer* ptr = &values[0])
		{
			alGetSourceiv(source, parameter, (int*) ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alGetSourceiv")]
	public static void GetSourceI(Source source, SourceProperty parameter, Buffer[] values)
	{
		fixed (Buffer* ptr = &values[0])
		{
			alGetSourceiv(source, parameter, (int*) ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alSourcei64SOFT")]
	public static void SourceL(Source source, SourceProperty parameter, long value)
	{
		alSourcei64SOFT(source, parameter, value);
		CheckErrorState();
	}

	[NativeMethod("alSource3i64SOFT")]
	public static void SourceL(Source source, SourceProperty parameter, long value1, long value2, long value3)
	{
		alSource3i64SOFT(source, parameter, value1, value2, value3);
		CheckErrorState();
	}

	[NativeMethod("alSourcei64vSOFT"), CLSCompliant(false)]
	public static void SourceL(Source source, SourceProperty parameter, long* values)
	{
		alSourcei64vSOFT(source, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alSourcei64vSOFT")]
	public static void SourceL(Source source, SourceProperty parameter, ReadOnlySpan<long> values)
	{
		fixed (long* ptr = &values[0])
		{
			alSourcei64vSOFT(source, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alSourcei64vSOFT")]
	public static void SourceL(Source source, SourceProperty parameter, long[] values)
	{
		fixed (long* ptr = &values[0])
		{
			alSourcei64vSOFT(source, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alGetSourcei64SOFT")]
	public static long GetSourceL(Source source, SourceProperty parameter)
	{
		long temp = default;
		alGetSourcei64SOFT(source, parameter, &temp);
		CheckErrorState();
		return temp;
	}

	[NativeMethod("alGetSourcei64SOFT")]
	public static void GetSourceL(Source source, SourceProperty parameter, out long value)
	{
		long temp = default;
		alGetSourcei64SOFT(source, parameter, &temp);
		CheckErrorState();
		value = temp;
	}

	[NativeMethod("alGetSource3i64SOFT"), CLSCompliant(false)]
	public static void GetSourceL(Source source, SourceProperty parameter, long* value1, long* value2, long* value3)
	{
		alGetSource3i64SOFT(source, parameter, value1, value2, value3);
		CheckErrorState();
	}

	[NativeMethod("alGetSource3i64SOFT")]
	public static void GetSourceL(Source source, SourceProperty parameter, out long value1, out long value2, out long value3)
	{
		var v = stackalloc long[3];
		alGetSource3i64SOFT(source, parameter, &v[0], &v[1], &v[2]);
		CheckErrorState();
		value1 = v[0];
		value2 = v[1];
		value3 = v[2];
	}

	[NativeMethod("alGetSourcei64vSOFT"), CLSCompliant(false)]
	public static void GetSourceL(Source source, SourceProperty parameter, long* values)
	{
		alGetSourcei64vSOFT(source, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alGetSourcei64vSOFT")]
	public static void GetSourceL(Source source, SourceProperty parameter, Span<long> values)
	{
		fixed (long* ptr = &values[0])
		{
			alGetSourcei64vSOFT(source, parameter, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alGetSourcei64vSOFT")]
	public static void GetSourceL(Source source, SourceProperty parameter, long[] values)
	{
		fixed (long* ptr = &values[0])
		{
			alGetSourcei64SOFT(source, parameter, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alSourcedSOFT")]
	public static void SourceD(Source source, SourceProperty parameter, double value)
	{
		alSourcedSOFT(source, parameter, value);
		CheckErrorState();
	}

	[NativeMethod("alSource3dSOFT")]
	public static void SourceD(Source source, SourceProperty parameter, double value1, double value2, double value3)
	{
		alSource3dSOFT(source, parameter, value1, value2, value3);
		CheckErrorState();
	}

	[NativeMethod("alSourcedvSOFT"), CLSCompliant(false)]
	public static void SourceD(Source source, SourceProperty parameter, double* values)
	{
		alSourcedvSOFT(source, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alSourcedvSOFT")]
	public static void SourceD(Source source, SourceProperty parameter, ReadOnlySpan<double> values)
	{
		fixed (double* ptr = &values[0])
		{
			alSourcedvSOFT(source, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alSourcedvSOFT")]
	public static void SourceD(Source source, SourceProperty parameter, double[] values)
	{
		fixed (double* ptr = &values[0])
		{
			alSourcedvSOFT(source, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alGetSourcedSOFT")]
	public static double GetSourceD(Source source, SourceProperty parameter)
	{
		double temp = default;
		alGetSourcedSOFT(source, parameter, &temp);
		CheckErrorState();
		return temp;
	}
	
	[NativeMethod("alGetSourcedSOFT")]
	public static void GetSourceD(Source source, SourceProperty parameter, out double value)
	{
		double temp = default;
		alGetSourcedSOFT(source, parameter, &temp);
		CheckErrorState();
		value = temp;
	}

	[NativeMethod("alGetSource3dSOFT"), CLSCompliant(false)]
	public static void GetSourceD(Source source, SourceProperty parameter, double* value1, double* value2, double* value3)
	{
		alGetSource3dSOFT(source, parameter, value1, value2, value3);
		CheckErrorState();
	}

	[NativeMethod("alGetSource3dSOFT")]
	public static void GetSourceD(Source source, SourceProperty parameter, out double value1, out double value2, out double value3)
	{
		var v = stackalloc double[3];
		alGetSource3dSOFT(source, parameter, &v[0], &v[1], &v[2]);
		CheckErrorState();
		value1 = v[0];
		value2 = v[1];
		value3 = v[2];
	}

	[NativeMethod("alGetSourcedvSOFT"), CLSCompliant(false)]
	public static void GetSourceD(Source source, SourceProperty parameter, double* values)
	{
		alGetSourcedvSOFT(source, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alGetSourcedvSOFT")]
	public static void GetSourceD(Source source, SourceProperty parameter, Span<double> values)
	{
		fixed (double* ptr = &values[0])
		{
			alGetSourcedvSOFT(source, parameter, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alGetSourcedvSOFT")]
	public static void GetSourceD(Source source, SourceProperty parameter, double[] values)
	{
		fixed (double* ptr = &values[0])
		{
			alGetSourcedSOFT(source, parameter, ptr);
		}
		CheckErrorState();
	}
}