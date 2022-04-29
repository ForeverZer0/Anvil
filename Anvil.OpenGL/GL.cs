using System.Collections.Specialized;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Anvil.Native;
using JetBrains.Annotations;

[assembly: CLSCompliant(true)]

// ReSharper disable StringLiteralTypo

namespace Anvil.OpenGL;

[PublicAPI]
// ReSharper disable once InconsistentNaming
public static unsafe partial class GL
{
	/// <summary>
	/// Symbolic constant value for a boolean <c>false</c> as an <see cref="int"/>.
	/// </summary>
	public const int FALSE = 0;
	
	/// <summary>
	/// Symbolic constant to represent <c>0</c> or "none".
	/// </summary>
	public const int ZERO = 0;
	
	/// <summary>
	/// Symbolic constant to represent <c>0</c> or "none".
	/// </summary>
	public const int NONE = 0;
	
	/// <summary>
	/// Symbolic constant value for a boolean <c>true</c> as an <see cref="int"/>.
	/// </summary>
	public const int TRUE = 1;
	
	/// <summary>
	/// Symbolic constant to represent <c>1</c>.
	/// </summary>
	public const int ONE = 1;
	
	/// <summary>
	/// Symbolic constant to represent an invalid indexer.
	/// </summary>
	public const int INVALID_INDEX = -1;
	
	/// <summary>
	/// Symbolic constant to represent indicate a timeout should be ignored.
	/// </summary>
	public const long TIMEOUT_IGNORED = -1;
		
	/// <summary>
	/// Maximum amount of memory that can be allocated on the stack.
	/// </summary>
	private const int MAX_STACK_ALLOC = 1024;
		
	/// <summary>
	/// Select active texture unit.
	/// </summary>
	[NativeMethod("glActiveTexture")]
	public static void ActiveTexture(TextureUnit texture) => glActiveTexture(texture);

	/// <summary>
	/// Attaches a shader object to a program object.
	/// </summary>
	[NativeMethod("glAttachShader")]
	public static void AttachShader(Program program, Shader shader) => glAttachShader(program, shader);

	/// <summary>
	/// Start conditional rendering.
	/// </summary>
	[NativeMethod("glBeginConditionalRender")]
	public static void BeginConditionalRender(int id, ConditionalRenderMode mode) => glBeginConditionalRender(id, mode);

	/// <summary>
	/// Delimit the boundaries of a query object.
	/// </summary>
	[NativeMethod("glBeginQuery")]
	public static void BeginQuery(QueryTarget target, Query id) => glBeginQuery(target, id);

	/// <summary>
	/// Start transform feedback operation.
	/// </summary>
	[NativeMethod("glBeginTransformFeedback")]
	public static void BeginTransformFeedback(PrimitiveType primitiveMode) => glBeginTransformFeedback(primitiveMode);

	/// <summary>
	/// Associates a generic vertex attribute index with a named attribute variable.
	/// </summary>
	[NativeMethod("glBindAttribLocation")]
	public static void BindAttribLocation(Program program, int index, string name)
	{
		fixed (byte* ptr = &UTF8String.Pin(name))
		{
			glBindAttribLocation(program, index, ptr);
		}
	}

	/// <summary>
	/// Bind a named buffer object.
	/// </summary>
	[NativeMethod("glBindBuffer")]
	public static void BindBuffer(BufferTarget target, Buffer? buffer) => glBindBuffer(target, buffer ?? default);

	/// <summary>
	/// Bind a buffer object to an indexed buffer target.
	/// </summary>
	[NativeMethod("glBindBufferBase")]
	public static void BindBufferBase(BufferTarget target, int index, Buffer buffer) => glBindBufferBase(target, index, buffer);

	/// <summary>
	/// Bind a range within a buffer object to an indexed buffer target.
	/// </summary>
	[NativeMethod("glBindBufferRange")]
	public static void BindBufferRange(BufferTarget target, int index, Buffer buffer, nint offset, nint size) => glBindBufferRange(target, index, buffer, offset, size);

	/// <summary>
	/// Bind a user-defined varying out variable to a fragment shader color number.
	/// </summary>
	[NativeMethod("glBindFragDataLocation")]
	public static void BindFragDataLocation(Program program, int color, string? name)
	{
		fixed (byte* ptr = &UTF8String.Pin(name))
		{
			glBindFragDataLocation(program, color, ptr);
		}
	}

	/// <summary>
	/// Bind a user-defined varying out variable to a fragment shader color number and index.
	/// </summary>
	[NativeMethod("glBindFragDataLocationIndexed")]
	public static void BindFragDataLocationIndexed(Program program, int colorNumber, int index, string name)
	{
		fixed (byte* ptr = &UTF8String.Pin(name))
		{
			glBindFragDataLocationIndexed(program, colorNumber, index, ptr);
		}
	}

	/// <summary>
	/// Bind a framebuffer to a framebuffer target.
	/// </summary>
	[NativeMethod("glBindFramebuffer")]
	public static void BindFramebuffer(FramebufferTarget target, Framebuffer? framebuffer) => glBindFramebuffer(target, framebuffer ?? default);

	/// <summary>
	/// Bind a renderbuffer to a renderbuffer target.
	/// </summary>
	[NativeMethod("glBindRenderbuffer")]
	public static void BindRenderbuffer(RenderbufferTarget target, Renderbuffer? renderbuffer) => glBindRenderbuffer(target, renderbuffer ?? default);

	/// <summary>
	/// Bind a named sampler to a texturing target.
	/// </summary>
	[NativeMethod("glBindSampler")]
	public static void BindSampler(int unit, Sampler? sampler) => glBindSampler(unit, sampler ?? default);

	/// <summary>
	/// Bind a named texture to a texturing target.
	/// </summary>
	[NativeMethod("glBindTexture")]
	public static void BindTexture(TextureTarget target, Texture? texture) => glBindTexture(target, texture ?? default);

	/// <summary>
	/// Bind a vertex array object.
	/// </summary>
	[NativeMethod("glBindVertexArray")]
	public static void BindVertexArray(VertexArray? array) => glBindVertexArray(array ?? default);

	/// <summary>
	/// Set the blend color.
	/// </summary>
	[NativeMethod("glBlendColor")]
	public static void BlendColor(float red, float green, float blue, float alpha) => glBlendColor(red, green, blue, alpha);

	/// <summary>
	/// Specify the equation used for both the rgb blend equation and the alpha blend equation.
	/// </summary>
	[NativeMethod("glBlendEquation")]
	public static void BlendEquation(BlendEquationMode mode) => glBlendEquation(mode);

	/// <summary>
	/// Set the rgb blend equation and the alpha blend equation separately.
	/// </summary>
	[NativeMethod("glBlendEquationSeparate")]
	public static void BlendEquationSeparate(BlendEquationMode modeRgb, BlendEquationMode modeAlpha) => glBlendEquationSeparate(modeRgb, modeAlpha);

	/// <summary>
	/// Specify pixel arithmetic.
	/// </summary>
	[NativeMethod("glBlendFunc")]
	public static void BlendFunc(BlendingFactor srcFactor, BlendingFactor dstFactor) => glBlendFunc(srcFactor, dstFactor);

	/// <summary>
	/// Specify pixel arithmetic for rgb and alpha components separately.
	/// </summary>
	[NativeMethod("glBlendFuncSeparate")]
	public static void BlendFuncSeparate(BlendingFactor srcFactorRgb, BlendingFactor dstFactorRgb, BlendingFactor srcFactorAlpha, BlendingFactor dstFactorAlpha) => glBlendFuncSeparate(srcFactorRgb, dstFactorRgb, srcFactorAlpha, dstFactorAlpha);

	/// <summary>
	/// Copy a block of pixels from the read framebuffer to the draw framebuffer.
	/// </summary>
	[NativeMethod("glBlitFramebuffer")]
	public static void BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, ClearBufferMask mask, BlitFramebufferFilter filter)
	{
		glBlitFramebuffer(srcX0, srcY0, srcX1, srcY1, dstX0, dstY0, dstX1, dstY1, mask, filter);
	}
		
	/// <summary>
	/// Copy a block of pixels from the read framebuffer to the draw framebuffer.
	/// </summary>
	[NativeMethod("glBlitFramebuffer")]
	public static void BlitFramebuffer(Rectangle src, Rectangle dst, ClearBufferMask mask, BlitFramebufferFilter filter)
	{
		glBlitFramebuffer(src.Left, src.Top, src.Right, src.Bottom, dst.Left, dst.Top, dst.Right, dst.Bottom, mask, filter);
	}

	/// <summary>
	/// Creates and initializes a buffer object's data store.
	/// </summary>
	[NativeMethod("glBufferData"), CLSCompliant(false)]
	public static void BufferData(BufferTarget target, nint size, void* data, BufferUsage usage) => glBufferData(target, size, data, usage);

	/// <summary>
	/// Updates a subset of a buffer object's data store.
	/// </summary>
	[NativeMethod("glBufferSubData"), CLSCompliant(false)]
	public static void BufferSubData(BufferTarget target, nint offset, nint size, void* data) => glBufferSubData(target, offset, size, data);

	/// <summary>
	/// Creates and initializes a buffer object's data store.
	/// </summary>
	[NativeMethod("glBufferData")]
	public static void BufferData(BufferTarget target, nint size, IntPtr data, BufferUsage usage) => glBufferData(target, size, data.ToPointer(), usage);

	/// <summary>
	/// Updates a subset of a buffer object's data store.
	/// </summary>
	[NativeMethod("glBufferSubData")]
	public static void BufferSubData(BufferTarget target, nint offset, nint size, IntPtr data) => glBufferSubData(target, offset, size, data.ToPointer());

	/// <summary>
	/// Creates and initializes a buffer object's data store.
	/// </summary>
	[NativeMethod("glBufferData")]
	public static void BufferData<T>(BufferTarget target, ReadOnlySpan<T> data, BufferUsage usage) where T : unmanaged
	{
		nint size = Unsafe.SizeOf<T>() * data.Length;
		fixed (T* ptr = &data.GetPinnableReference())
		{
			glBufferData(target, size, ptr, usage);
		}
	}

	/// <summary>
	/// Updates a subset of a buffer object's data store.
	/// </summary>
	[NativeMethod("glBufferSubData")]
	public static void BufferSubData<T>(BufferTarget target, nint offset, ReadOnlySpan<T> data) where T : unmanaged
	{
		nint size = Unsafe.SizeOf<T>() * data.Length;
		fixed (T* ptr = &data.GetPinnableReference())
		{
			glBufferSubData(target, offset, size, ptr);
		}
	}
		
	/// <summary>
	/// Check the completeness status of a framebuffer.
	/// </summary>
	[NativeMethod("glCheckFramebufferStatus")]
	public static FramebufferStatus CheckFramebufferStatus(FramebufferTarget target) => glCheckFramebufferStatus(target);

	/// <summary>
	/// Specify whether data read via  should be clamped.
	/// </summary>
	[NativeMethod("glClampColor")]
	public static void ClampColor(ClampColorTarget target, ClampColorMode clamp) => glClampColor(target, clamp);

	/// <summary>
	/// Clear buffers to preset values.
	/// </summary>
	[NativeMethod("glClear")]
	public static void Clear(ClearBufferMask mask) => glClear(mask);

	/// <summary>
	/// Clear individual buffers of the currently bound draw framebuffer.
	/// </summary>
	[NativeMethod("glClearBufferfi")]
	public static void ClearBuffer(ClearBufferTarget buffer, int drawBuffer, float depth, int stencil) => glClearBufferfi(buffer, drawBuffer, depth, stencil);

	/// <summary>
	/// Clear individual buffers of the currently bound draw framebuffer.
	/// </summary>
	[NativeMethod("glClearBufferfv"), CLSCompliant(false)]
	public static void ClearBuffer(ClearBufferTarget buffer, int drawBuffer, float* value) => glClearBufferfv(buffer, drawBuffer, value);

	/// <summary>
	/// Clear individual buffers of the currently bound draw framebuffer.
	/// </summary>
	[NativeMethod("glClearBufferiv"), CLSCompliant(false)]
	public static void ClearBuffer(ClearBufferTarget buffer, int drawBuffer, int* value) => glClearBufferiv(buffer, drawBuffer, value);

	/// <summary>
	/// Clear individual buffers of the currently bound draw framebuffer.
	/// </summary>
	[NativeMethod("glClearBufferuiv"), CLSCompliant(false)]
	public static void ClearBuffer(ClearBufferTarget buffer, int drawBuffer, uint* value) => glClearBufferuiv(buffer, drawBuffer, value);

	/// <summary>
	/// Clear individual buffers of the currently bound draw framebuffer.
	/// </summary>
	[NativeMethod("glClearBufferfv")]
	public static void ClearBuffer(ClearBufferTarget buffer, int drawBuffer, ReadOnlySpan<float> value)
	{
		fixed (float* ptr = &value.GetPinnableReference())
		{
			glClearBufferfv(buffer, drawBuffer, ptr);
		}
	}

	/// <summary>
	/// Clear individual buffers of the currently bound draw framebuffer.
	/// </summary>
	[NativeMethod("glClearBufferiv")]
	public static void ClearBuffer(ClearBufferTarget buffer, int drawBuffer, ReadOnlySpan<int> value)
	{
		fixed (int* ptr = &value.GetPinnableReference())
		{
			glClearBufferiv(buffer, drawBuffer, ptr);
		}
	}

	/// <summary>
	/// Clear individual buffers of the currently bound draw framebuffer.
	/// </summary>
	[NativeMethod("glClearBufferuiv"), CLSCompliant(false)]
	public static void ClearBuffer(ClearBufferTarget buffer, int drawBuffer, ReadOnlySpan<uint> value)
	{
		fixed (uint* ptr = &value.GetPinnableReference())
		{
			glClearBufferuiv(buffer, drawBuffer, ptr);
		}
	}
		
	/// <summary>
	/// Specify clear values for the color buffers.
	/// </summary>
	[NativeMethod("glClearColor")]
	public static void ClearColor(float red, float green, float blue, float alpha) => glClearColor(red, green, blue, alpha);
		
	/// <summary>
	/// Specify clear values for the color buffers.
	/// </summary>
	[NativeMethod("glClearColor")]
	public static void ClearColor(ColorF color) => glClearColor(color.R, color.G, color.B, color.A);

	/// <summary>
	/// Specify the clear value for the depth buffer.
	/// </summary>
	[NativeMethod("glClearDepth")]
	public static void ClearDepth(double depth) => glClearDepth(depth);

	/// <summary>
	/// Specify the clear value for the stencil buffer.
	/// </summary>
	[NativeMethod("glClearStencil")]
	public static void ClearStencil(int s) => glClearStencil(s);

	/// <summary>
	/// Block and wait for a sync object to become signaled.
	/// </summary>
	[NativeMethod("glClientWaitSync"), CLSCompliant(false)]
	public static SyncStatus ClientWaitSync(Sync sync, SyncObjectMask flags, ulong timeout) => glClientWaitSync(sync, flags, timeout);
		
	/// <summary>
	/// Block and wait for a sync object to become signaled.
	/// </summary>
	[NativeMethod("glClientWaitSync")]
	public static SyncStatus ClientWaitSync(Sync sync, SyncObjectMask flags, long timeout) => glClientWaitSync(sync, flags, unchecked((ulong)timeout));

	/// <summary>
	/// Enable and disable writing of frame buffer color components.
	/// </summary>
	[NativeMethod("glColorMask")]
	public static void ColorMask(bool red, bool green, bool blue, bool alpha) => glColorMask(red, green, blue, alpha);

	/// <summary>
	/// Enable and disable writing of frame buffer color components.
	/// </summary>
	[NativeMethod("glColorMaski")]
	public static void ColorMask(int index, bool red, bool green, bool blue, bool alpha) => glColorMaski(index, red, green, blue, alpha);
		
	/// <summary>
	/// Set the current color.
	/// </summary>
	[NativeMethod("glColorP3ui")]
	public static void ColorP3(ColorPointerType type, BitVector32 color) => glColorP3ui(type, Unsafe.As<BitVector32, int>(ref color));

	/// <summary>
	/// Set the current color.
	/// </summary>
	[NativeMethod("glColorP3uiv"), CLSCompliant(false)]
	public static void ColorP3(ColorPointerType type, BitVector32* color) => glColorP3uiv(type, (int*) color);

	/// <summary>
	/// Set the current color.
	/// </summary>
	[NativeMethod("glColorP3uiv")]
	public static void ColorP3(ColorPointerType type, ReadOnlySpan<BitVector32> color)
	{
		fixed (BitVector32* ptr = &color.GetPinnableReference())
		{
			glColorP3uiv(type, (int*) ptr);	
		}
	}

	/// <summary>
	/// Set the current color.
	/// </summary>
	[NativeMethod("glColorP4ui")]
	public static void ColorP4(ColorPointerType type, BitVector32 color) => glColorP4ui(type, Unsafe.As<BitVector32, int>(ref color));

	/// <summary>
	/// Set the current color.
	/// </summary>
	[NativeMethod("glColorP4uiv"), CLSCompliant(false)]
	public static void ColorP4(ColorPointerType type, BitVector32* color) => glColorP4uiv(type, (int*) color);
		
	/// <summary>
	/// Set the current color.
	/// </summary>
	[NativeMethod("glColorP4uiv")]
	public static void ColorP4(ColorPointerType type, ReadOnlySpan<BitVector32> color)
	{
		fixed (BitVector32* ptr = &color.GetPinnableReference())
		{
			glColorP4uiv(type, (int*) ptr);	
		}
	}

	/// <summary>
	/// Set the current color.
	/// </summary>
	[NativeMethod("glColorP3ui"), CLSCompliant(false)]
	public static void ColorP3(ColorPointerType type, uint color) => glColorP3ui(type, Unsafe.As<uint, int>(ref color));

	/// <summary>
	/// Set the current color.
	/// </summary>
	[NativeMethod("glColorP3uiv"), CLSCompliant(false)]
	public static void ColorP3(ColorPointerType type, uint* color) => glColorP3uiv(type, (int*) color);

	/// <summary>
	/// Set the current color.
	/// </summary>
	[NativeMethod("glColorP3uiv"), CLSCompliant(false)]
	public static void ColorP3(ColorPointerType type, ReadOnlySpan<uint> color)
	{
		fixed (uint* ptr = &color.GetPinnableReference())
		{
			glColorP3uiv(type, (int*) ptr);	
		}
	}

	/// <summary>
	/// Set the current color.
	/// </summary>
	[NativeMethod("glColorP4ui"), CLSCompliant(false)]
	public static void ColorP4(ColorPointerType type, uint color) => glColorP4ui(type, Unsafe.As<uint, int>(ref color));

	/// <summary>
	/// Set the current color.
	/// </summary>
	[NativeMethod("glColorP4uiv"), CLSCompliant(false)]
	public static void ColorP4(ColorPointerType type, uint* color) => glColorP4uiv(type, (int*) color);
		
	/// <summary>
	/// Set the current color.
	/// </summary>
	[NativeMethod("glColorP4uiv"), CLSCompliant(false)]
	public static void ColorP4(ColorPointerType type, ReadOnlySpan<uint> color)
	{
		fixed (uint* ptr = &color.GetPinnableReference())
		{
			glColorP4uiv(type, (int*) ptr);	
		}
	}
		
	/// <summary>
	/// Set the current color.
	/// </summary>
	[NativeMethod("glColorP3ui")]
	public static void ColorP3(ColorPointerType type, int color) => glColorP3ui(type, color);

	/// <summary>
	/// Set the current color.
	/// </summary>
	[NativeMethod("glColorP3uiv"), CLSCompliant(false)]
	public static void ColorP3(ColorPointerType type, int* color) => glColorP3uiv(type, color);

	/// <summary>
	/// Set the current color.
	/// </summary>
	[NativeMethod("glColorP3uiv")]
	public static void ColorP3(ColorPointerType type, ReadOnlySpan<int> color)
	{
		fixed (int* ptr = &color.GetPinnableReference())
		{
			glColorP3uiv(type, ptr);	
		}
	}

	/// <summary>
	/// Set the current color.
	/// </summary>
	[NativeMethod("glColorP4ui")]
	public static void ColorP4(ColorPointerType type, int color) => glColorP4ui(type, color);

	/// <summary>
	/// Set the current color.
	/// </summary>
	[NativeMethod("glColorP4uiv"), CLSCompliant(false)]
	public static void ColorP4(ColorPointerType type, int* color) => glColorP4uiv(type, color);
		
	/// <summary>
	/// Set the current color.
	/// </summary>
	[NativeMethod("glColorP4uiv")]
	public static void ColorP4(ColorPointerType type, ReadOnlySpan<int> color)
	{
		fixed (int* ptr = &color.GetPinnableReference())
		{
			glColorP4uiv(type, ptr);	
		}
	}

	/// <summary>
	/// Compiles a shader object.
	/// </summary>
	[NativeMethod("glCompileShader")]
	public static void CompileShader(Shader shader) => glCompileShader(shader);

	/// <summary>
	/// Specify a one-dimensional texture image in a compressed format.
	/// </summary>
	[NativeMethod("glCompressedTexImage1D"), CLSCompliant(false)]
	public static void CompressedTexImage1D(TextureTarget target, int level, InternalFormat internalFormat, int width, int border, int imageSize, void* data)
	{
		glCompressedTexImage1D(target, level, internalFormat, width, border, imageSize, data);
	}
		
	/// <summary>
	/// Specify a one-dimensional texture image in a compressed format.
	/// </summary>
	[NativeMethod("glCompressedTexImage1D")]
	public static void CompressedTexImage1D(TextureTarget target, int level, InternalFormat internalFormat, int width, int border, int imageSize, IntPtr data)
	{
		glCompressedTexImage1D(target, level, internalFormat, width, border, imageSize, data.ToPointer());
	}
		
	/// <summary>
	/// Specify a one-dimensional texture image in a compressed format.
	/// </summary>
	[NativeMethod("glCompressedTexImage1D")]
	public static void CompressedTexImage1D<T>(TextureTarget target, int level, InternalFormat internalFormat, int width, int border, int imageSize, ReadOnlySpan<T> data) where T : unmanaged
	{
		fixed (T* ptr = &data.GetPinnableReference())
		{
			glCompressedTexImage1D(target, level, internalFormat, width, border, imageSize, ptr);
		}
	}

	/// <summary>
	/// Specify a two-dimensional texture image in a compressed format.
	/// </summary>
	[NativeMethod("glCompressedTexImage2D"), CLSCompliant(false)]
	public static void CompressedTexImage2D(TextureTarget target, int level, InternalFormat internalFormat, int width, int height, int border, int imageSize, void* data)
	{
		glCompressedTexImage2D(target, level, internalFormat, width, height, border, imageSize, data);
	}
		
	/// <summary>
	/// Specify a two-dimensional texture image in a compressed format.
	/// </summary>
	[NativeMethod("glCompressedTexImage2D")]
	public static void CompressedTexImage2D(TextureTarget target, int level, InternalFormat internalFormat, int width, int height, int border, int imageSize, IntPtr data)
	{
		glCompressedTexImage2D(target, level, internalFormat, width, height, border, imageSize, data.ToPointer());
	}

	/// <summary>
	/// Specify a two-dimensional texture image in a compressed format.
	/// </summary>
	[NativeMethod("glCompressedTexImage2D")]
	public static void CompressedTexImage2D<T>(TextureTarget target, int level, InternalFormat internalFormat, int width, int height, int border, int imageSize, ReadOnlySpan<T> data) where T : unmanaged
	{
		fixed (T* ptr = &data.GetPinnableReference())
		{
			glCompressedTexImage2D(target, level, internalFormat, width, height, border, imageSize, ptr);	
		}
	}

	/// <summary>
	/// Specify a three-dimensional texture image in a compressed format.
	/// </summary>
	[NativeMethod("glCompressedTexImage3D"), CLSCompliant(false)]
	public static void CompressedTexImage3D(TextureTarget target, int level, InternalFormat internalFormat, int width, int height, int depth, int border, int imageSize, void* data)
	{
		glCompressedTexImage3D(target, level, internalFormat, width, height, depth, border, imageSize, data);
	}
		
	/// <summary>
	/// Specify a three-dimensional texture image in a compressed format.
	/// </summary>
	[NativeMethod("glCompressedTexImage3D")]
	public static void CompressedTexImage3D(TextureTarget target, int level, InternalFormat internalFormat, int width, int height, int depth, int border, int imageSize, IntPtr data)
	{
		glCompressedTexImage3D(target, level, internalFormat, width, height, depth, border, imageSize, data.ToPointer());
	}

	/// <summary>
	/// Specify a three-dimensional texture image in a compressed format.
	/// </summary>
	[NativeMethod("glCompressedTexImage3D")]
	public static void CompressedTexImage3D<T>(TextureTarget target, int level, InternalFormat internalFormat, int width, int height, int depth, int border, int imageSize, ReadOnlySpan<T> data) where T : unmanaged
	{
		fixed (T* ptr = &data.GetPinnableReference())
		{
			glCompressedTexImage3D(target, level, internalFormat, width, height, depth, border, imageSize, ptr);	
		}
	}

	/// <summary>
	/// Specify a one-dimensional texture subimage in a compressed format.
	/// </summary>
	[NativeMethod("glCompressedTexSubImage1D"), CLSCompliant(false)]
	public static void CompressedTexSubImage1D(TextureTarget target, int level, int xOffset, int width, InternalFormat format, int imageSize, void* data) => glCompressedTexSubImage1D(target, level, xOffset, width, format, imageSize, data);

	/// <summary>
	/// Specify a two-dimensional texture subimage in a compressed format.
	/// </summary>
	[NativeMethod("glCompressedTexSubImage2D"), CLSCompliant(false)]
	public static void CompressedTexSubImage2D(TextureTarget target, int level, int xOffset, int yOffset, int width, int height, InternalFormat format, int imageSize, void* data) => glCompressedTexSubImage2D(target, level, xOffset, yOffset, width, height, format, imageSize, data);

	/// <summary>
	/// Specify a three-dimensional texture subimage in a compressed format.
	/// </summary>
	[NativeMethod("glCompressedTexSubImage3D"), CLSCompliant(false)]
	public static void CompressedTexSubImage3D(TextureTarget target, int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, InternalFormat format, int imageSize, void* data) => glCompressedTexSubImage3D(target, level, xOffset, yOffset, zOffset, width, height, depth, format, imageSize, data);
		
	/// <summary>
	/// Specify a one-dimensional texture subimage in a compressed format.
	/// </summary>
	[NativeMethod("glCompressedTexSubImage1D")]
	public static void CompressedTexSubImage1D(TextureTarget target, int level, int xOffset, int width, InternalFormat format, int imageSize, IntPtr data) => glCompressedTexSubImage1D(target, level, xOffset, width, format, imageSize, data.ToPointer());

	/// <summary>
	/// Specify a two-dimensional texture subimage in a compressed format.
	/// </summary>
	[NativeMethod("glCompressedTexSubImage2D")]
	public static void CompressedTexSubImage2D(TextureTarget target, int level, int xOffset, int yOffset, int width, int height, InternalFormat format, int imageSize, IntPtr data) => glCompressedTexSubImage2D(target, level, xOffset, yOffset, width, height, format, imageSize, data.ToPointer());

	/// <summary>
	/// Specify a three-dimensional texture subimage in a compressed format.
	/// </summary>
	[NativeMethod("glCompressedTexSubImage3D")]
	public static void CompressedTexSubImage3D(TextureTarget target, int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, InternalFormat format, int imageSize, IntPtr data) => glCompressedTexSubImage3D(target, level, xOffset, yOffset, zOffset, width, height, depth, format, imageSize, data.ToPointer());



	/// <summary>
	/// Specify a one-dimensional texture subimage in a compressed format.
	/// </summary>
	[NativeMethod("glCompressedTexSubImage1D")]
	public static void CompressedTexSubImage1D<T>(TextureTarget target, int level, int xOffset, int width, InternalFormat format, int imageSize, ReadOnlySpan<T> data) where T : unmanaged
	{
		fixed (T* ptr = &data.GetPinnableReference())
		{
			glCompressedTexSubImage1D(target, level, xOffset, width, format, imageSize, ptr);	
		}
	}

	/// <summary>
	/// Specify a two-dimensional texture subimage in a compressed format.
	/// </summary>
	[NativeMethod("glCompressedTexSubImage2D")]
	public static void CompressedTexSubImage2D<T>(TextureTarget target, int level, int xOffset, int yOffset, int width, int height, InternalFormat format, int imageSize, ReadOnlySpan<T> data) where T : unmanaged
	{
		fixed (T* ptr = &data.GetPinnableReference())
		{
			glCompressedTexSubImage2D(target, level, xOffset, yOffset, width, height, format, imageSize, ptr);	
		}
	}

	/// <summary>
	/// Specify a three-dimensional texture subimage in a compressed format.
	/// </summary>
	[NativeMethod("glCompressedTexSubImage3D")]
	public static void CompressedTexSubImage3D<T>(TextureTarget target, int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, InternalFormat format, int imageSize, ReadOnlySpan<T> data) where T : unmanaged
	{
		fixed (T* ptr = &data.GetPinnableReference())
		{
			glCompressedTexSubImage3D(target, level, xOffset, yOffset, zOffset, width, height, depth, format, imageSize, ptr);	
		}
	}
		
	/// <summary>
	/// Copy part of the data store of a buffer object to the data store of another buffer object.
	/// </summary>
	[NativeMethod("glCopyBufferSubData")]
	public static void CopyBufferSubData(CopyBufferSubDataTarget readTarget, CopyBufferSubDataTarget writeTarget, nint readOffset, nint writeOffset, nint size) => glCopyBufferSubData(readTarget, writeTarget, readOffset, writeOffset, size);

	/// <summary>
	/// Copy pixels into a 1d texture image.
	/// </summary>
	[NativeMethod("glCopyTexImage1D")]
	public static void CopyTexImage1D(TextureTarget target, int level, InternalFormat internalFormat, int x, int y, int width, int border) => glCopyTexImage1D(target, level, internalFormat, x, y, width, border);

	/// <summary>
	/// Copy pixels into a 2d texture image.
	/// </summary>
	[NativeMethod("glCopyTexImage2D")]
	public static void CopyTexImage2D(TextureTarget target, int level, InternalFormat internalFormat, int x, int y, int width, int height, int border) => glCopyTexImage2D(target, level, internalFormat, x, y, width, height, border);

	/// <summary>
	/// Copy a one-dimensional texture subimage.
	/// </summary>
	[NativeMethod("glCopyTexSubImage1D")]
	public static void CopyTexSubImage1D(TextureTarget target, int level, int xOffset, int x, int y, int width) => glCopyTexSubImage1D(target, level, xOffset, x, y, width);

	/// <summary>
	/// Copy a two-dimensional texture subimage.
	/// </summary>
	[NativeMethod("glCopyTexSubImage2D")]
	public static void CopyTexSubImage2D(TextureTarget target, int level, int xOffset, int yOffset, int x, int y, int width, int height) => glCopyTexSubImage2D(target, level, xOffset, yOffset, x, y, width, height);

	/// <summary>
	/// Copy a three-dimensional texture subimage.
	/// </summary>
	[NativeMethod("glCopyTexSubImage3D")]
	public static void CopyTexSubImage3D(TextureTarget target, int level, int xOffset, int yOffset, int zOffset, int x, int y, int width, int height) => glCopyTexSubImage3D(target, level, xOffset, yOffset, zOffset, x, y, width, height);

	/// <summary>
	/// Specify whether front- or back-facing facets can be culled.
	/// </summary>
	[NativeMethod("glCullFace")]
	public static void CullFace(CullFaceMode mode) => glCullFace(mode);

	/// <summary>
	/// Specify the value used for depth buffer comparisons.
	/// </summary>
	[NativeMethod("glDepthFunc")]
	public static void DepthFunc(DepthFunction func) => glDepthFunc(func);

	/// <summary>
	/// Enable or disable writing into the depth buffer.
	/// </summary>
	[NativeMethod("glDepthMask")]
	public static void DepthMask(bool flag) => glDepthMask(flag);

	/// <summary>
	/// Specify mapping of depth values from normalized device coordinates to window coordinates.
	/// </summary>
	[NativeMethod("glDepthRange")]
	public static void DepthRange(double n, double f) => glDepthRange(n, f);

	/// <summary>
	/// Detaches a shader object from a program object to which it is attached.
	/// </summary>
	[NativeMethod("glDetachShader")]
	public static void DetachShader(Program program, Shader shader) => glDetachShader(program, shader);

	/// <summary>
	/// Disable a generic vertex attribute array.
	/// </summary>
	[NativeMethod("glDisableVertexAttribArray")]
	public static void DisableVertexAttribArray(int index) => glDisableVertexAttribArray(index);

	/// <summary>
	/// Disable server-side GL capabilities.
	/// </summary>
	[NativeMethod("glDisablei")]
	public static void Disable(EnableCap target, int index) => glDisablei(target, index);
		
	/// <summary>
	/// Disable server-side GL capabilities.
	/// </summary>
	[NativeMethod("glDisablei")]
	public static void Disable(int cap, int index) => glDisablei(Unsafe.As<int, EnableCap>(ref cap), index);

	/// <summary>
	/// Render primitives from array data.
	/// </summary>
	[NativeMethod("glDrawArrays")]
	public static void DrawArrays(PrimitiveType mode, int first, int count) => glDrawArrays(mode, first, count);

	/// <summary>
	/// Draw multiple instances of a range of elements.
	/// </summary>
	[NativeMethod("glDrawArraysInstanced")]
	public static void DrawArraysInstanced(PrimitiveType mode, int first, int count, int instanceCount) => glDrawArraysInstanced(mode, first, count, instanceCount);

	/// <summary>
	/// Specify which color buffers are to be drawn into.
	/// </summary>
	[NativeMethod("glDrawBuffer")]
	public static void DrawBuffer(DrawBufferMode buf) => glDrawBuffer(buf);

	/// <summary>
	/// Specifies a list of color buffers to be drawn into.
	/// </summary>
	[NativeMethod("glDrawBuffers"), CLSCompliant(false)]
	public static void DrawBuffers(int n, DrawBufferMode* buffers) => glDrawBuffers(n, buffers);

	/// <summary>
	/// Specifies a list of color buffers to be drawn into.
	/// </summary>
	[NativeMethod("glDrawBuffers")]
	public static void DrawBuffers(ReadOnlySpan<DrawBufferMode> buffers)
	{
		fixed (DrawBufferMode* ptr = &buffers.GetPinnableReference())
		{
			glDrawBuffers(buffers.Length, ptr);
		}
	}
		
	/// <summary>
	/// Render primitives from array data.
	/// </summary>
	[NativeMethod("glDrawElements"), CLSCompliant(false)]
	public static void DrawElements(PrimitiveType mode, int count, DrawElementsType type, void* indices) => glDrawElements(mode, count, type, indices);

	/// <summary>
	/// Render primitives from array data.
	/// </summary>
	[NativeMethod("glDrawElements")]
	public static void DrawElements(PrimitiveType mode, int count, DrawElementsType type, IntPtr indices) => glDrawElements(mode, count, type, indices.ToPointer());

	/// <summary>
	/// Render primitives from array data.
	/// </summary>
	[NativeMethod("glDrawElements")]
	public static void DrawElements<T>(PrimitiveType mode, int count, DrawElementsType type, ReadOnlySpan<T> indices) where T : unmanaged
	{
		fixed (T* ptr = &indices.GetPinnableReference())
		{
			glDrawElements(mode, count, type, ptr);
		}
	}

	/// <summary>
	/// Render primitives from array data.
	/// </summary>
	[NativeMethod("glDrawElements")]
	public static void DrawElements(PrimitiveType mode, int count, DrawElementsType type)
	{
		glDrawElements(mode, count, type, null);
	}
	
	/// <summary>
	/// Render primitives from array data.
	/// </summary>
	[NativeMethod("glDrawElements")]
	public static void DrawElements(PrimitiveType mode, ReadOnlySpan<byte> indices)
	{
		fixed (byte* ptr = &indices.GetPinnableReference())
		{
			glDrawElements(mode, indices.Length, DrawElementsType.UnsignedByte, ptr);
		}
	}

	/// <summary>
	/// Render primitives from array data.
	/// </summary>
	[NativeMethod("glDrawElements"), CLSCompliant(false)]
	public static void DrawElements(PrimitiveType mode, ReadOnlySpan<uint> indices)
	{
		fixed (uint* ptr = &indices.GetPinnableReference())
		{
			glDrawElements(mode, indices.Length, DrawElementsType.UnsignedInt, ptr);
		}
	}
		
	/// <summary>
	/// Render primitives from array data.
	/// </summary>
	[NativeMethod("glDrawElements")]
	public static void DrawElements(PrimitiveType mode, ReadOnlySpan<short> indices)
	{
		fixed (short* ptr = &indices.GetPinnableReference())
		{
			glDrawElements(mode, indices.Length, DrawElementsType.UnsignedShort, ptr);
		}
	}
		
	/// <summary>
	/// Render primitives from array data.
	/// </summary>
	[NativeMethod("glDrawElements")]
	public static void DrawElements(PrimitiveType mode, ReadOnlySpan<int> indices)
	{
		fixed (int* ptr = &indices.GetPinnableReference())
		{
			glDrawElements(mode, indices.Length, DrawElementsType.UnsignedInt, ptr);
		}
	}
		
	/// <summary>
	/// Render primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawElementsBaseVertex"), CLSCompliant(false)]
	public static void DrawElementsBaseVertex(PrimitiveType mode, int count, DrawElementsType type, void* indices, int baseVertex) => glDrawElementsBaseVertex(mode, count, type, indices, baseVertex);

	/// <summary>
	/// Render primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawElementsBaseVertex")]
	public static void DrawElementsBaseVertex(PrimitiveType mode, int count, DrawElementsType type, IntPtr indices, int baseVertex) => glDrawElementsBaseVertex(mode, count, type, indices.ToPointer(), baseVertex);


	/// <summary>
	/// Render primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawElementsBaseVertex")]
	public static void DrawElementsBaseVertex<T>(PrimitiveType mode, int count, DrawElementsType type, ReadOnlySpan<T> indices, int baseVertex) where T : unmanaged
	{
		fixed (T* ptr = &indices.GetPinnableReference())
		{
			glDrawElementsBaseVertex(mode, count, type, ptr, baseVertex);
		}
	}
		
	/// <summary>
	/// Render primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawElementsBaseVertex")]
	public static void DrawElementsBaseVertex(PrimitiveType mode, ReadOnlySpan<byte> indices, int baseVertex)
	{
		fixed (byte* ptr = &indices.GetPinnableReference())
		{
			glDrawElementsBaseVertex(mode, indices.Length, DrawElementsType.UnsignedByte, ptr, baseVertex);
		}
	}
		
	/// <summary>
	/// Render primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawElementsBaseVertex"), CLSCompliant(false)]
	public static void DrawElementsBaseVertex(PrimitiveType mode, ReadOnlySpan<ushort> indices, int baseVertex)
	{
		fixed (ushort* ptr = &indices.GetPinnableReference())
		{
			glDrawElementsBaseVertex(mode, indices.Length, DrawElementsType.UnsignedShort, ptr, baseVertex);
		}
	}
		
	/// <summary>
	/// Render primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawElementsBaseVertex"), CLSCompliant(false)]
	public static void DrawElementsBaseVertex(PrimitiveType mode, ReadOnlySpan<uint> indices, int baseVertex)
	{
		fixed (uint* ptr = &indices.GetPinnableReference())
		{
			glDrawElementsBaseVertex(mode, indices.Length, DrawElementsType.UnsignedInt, ptr, baseVertex);
		}
	}
		
	/// <summary>
	/// Render primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawElementsBaseVertex")]
	public static void DrawElementsBaseVertex(PrimitiveType mode, ReadOnlySpan<short> indices, int baseVertex)
	{
		fixed (short* ptr = &indices.GetPinnableReference())
		{
			glDrawElementsBaseVertex(mode, indices.Length, DrawElementsType.UnsignedShort, ptr, baseVertex);
		}
	}
		
	/// <summary>
	/// Render primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawElementsBaseVertex")]
	public static void DrawElementsBaseVertex(PrimitiveType mode, ReadOnlySpan<int> indices, int baseVertex)
	{
		fixed (int* ptr = &indices.GetPinnableReference())
		{
			glDrawElementsBaseVertex(mode, indices.Length, DrawElementsType.UnsignedInt, ptr, baseVertex);
		}
	}
		
	/// <summary>
	/// Draw multiple instances of a set of elements.
	/// </summary>
	[NativeMethod("glDrawElementsInstanced"), CLSCompliant(false)]
	public static void DrawElementsInstanced(PrimitiveType mode, int count, DrawElementsType type, void* indices, int instanceCount) => glDrawElementsInstanced(mode, count, type, indices, instanceCount);

	/// <summary>
	/// Draw multiple instances of a set of elements.
	/// </summary>
	[NativeMethod("glDrawElementsInstanced")]
	public static void DrawElementsInstanced(PrimitiveType mode, int count, DrawElementsType type, IntPtr indices, int instanceCount) => glDrawElementsInstanced(mode, count, type, indices.ToPointer(), instanceCount);

	/// <summary>
	/// Draw multiple instances of a set of elements.
	/// </summary>
	[NativeMethod("glDrawElementsInstanced")]
	public static void DrawElementsInstanced<T>(PrimitiveType mode, int count, DrawElementsType type, ReadOnlySpan<T> indices, int instanceCount) where T : unmanaged
	{
		fixed (T* ptr = &indices.GetPinnableReference())
		{
			glDrawElementsInstanced(mode, count, type, ptr, instanceCount);
		}
	}

	/// <summary>
	/// Draw multiple instances of a set of elements.
	/// </summary>
	[NativeMethod("glDrawElementsInstanced")]
	public static void DrawElementsInstanced(PrimitiveType mode, ReadOnlySpan<byte> indices, int instanceCount)
	{
		fixed (byte* ptr = &indices.GetPinnableReference())
		{
			glDrawElementsInstanced(mode, indices.Length, DrawElementsType.UnsignedByte, ptr, instanceCount);
		}
	}
		
	/// <summary>
	/// Draw multiple instances of a set of elements.
	/// </summary>
	[NativeMethod("glDrawElementsInstanced"), CLSCompliant(false)]
	public static void DrawElementsInstanced(PrimitiveType mode, ReadOnlySpan<ushort> indices, int instanceCount)
	{
		fixed (ushort* ptr = &indices.GetPinnableReference())
		{
			glDrawElementsInstanced(mode, indices.Length, DrawElementsType.UnsignedShort, ptr, instanceCount);
		}
	}
		
	/// <summary>
	/// Draw multiple instances of a set of elements.
	/// </summary>
	[NativeMethod("glDrawElementsInstanced"), CLSCompliant(false)]
	public static void DrawElementsInstanced(PrimitiveType mode, ReadOnlySpan<uint> indices, int instanceCount)
	{
		fixed (uint* ptr = &indices.GetPinnableReference())
		{
			glDrawElementsInstanced(mode, indices.Length, DrawElementsType.UnsignedInt, ptr, instanceCount);
		}
	}
		
	/// <summary>
	/// Draw multiple instances of a set of elements.
	/// </summary>
	[NativeMethod("glDrawElementsInstanced")]
	public static void DrawElementsInstanced(PrimitiveType mode, ReadOnlySpan<short> indices, int instanceCount)
	{
		fixed (short* ptr = &indices.GetPinnableReference())
		{
			glDrawElementsInstanced(mode, indices.Length, DrawElementsType.UnsignedShort, ptr, instanceCount);
		}
	}
		
	/// <summary>
	/// Draw multiple instances of a set of elements.
	/// </summary>
	[NativeMethod("glDrawElementsInstanced")]
	public static void DrawElementsInstanced(PrimitiveType mode, ReadOnlySpan<int> indices, int instanceCount)
	{
		fixed (int* ptr = &indices.GetPinnableReference())
		{
			glDrawElementsInstanced(mode, indices.Length, DrawElementsType.UnsignedInt, ptr, instanceCount);
		}
	}
		
	/// <summary>
	/// Render multiple instances of a set of primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawElementsInstancedBaseVertex"), CLSCompliant(false)]
	public static void DrawElementsInstancedBaseVertex(PrimitiveType mode, int count, DrawElementsType type, void* indices, int instanceCount, int baseVertex) => glDrawElementsInstancedBaseVertex(mode, count, type, indices, instanceCount, baseVertex);

	/// <summary>
	/// Render multiple instances of a set of primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawElementsInstancedBaseVertex")]
	public static void DrawElementsInstancedBaseVertex(PrimitiveType mode, int count, DrawElementsType type, IntPtr indices, int instanceCount, int baseVertex) => glDrawElementsInstancedBaseVertex(mode, count, type, indices.ToPointer(), instanceCount, baseVertex);

	/// <summary>
	/// Render multiple instances of a set of primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawElementsInstancedBaseVertex")]
	public static void DrawElementsInstancedBaseVertex<T>(PrimitiveType mode, int count, DrawElementsType type, ReadOnlySpan<T> indices, int instanceCount, int baseVertex) where T : unmanaged
	{
		fixed (T* ptr = &indices.GetPinnableReference())
		{
			glDrawElementsInstancedBaseVertex(mode, count, type, ptr, instanceCount, baseVertex);
		}
	}
		
	/// <summary>
	/// Render multiple instances of a set of primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawElementsInstancedBaseVertex")]
	public static void DrawElementsInstancedBaseVertex(PrimitiveType mode, ReadOnlySpan<byte> indices, int instanceCount, int baseVertex)
	{
		fixed (byte* ptr = &indices.GetPinnableReference())
		{
			glDrawElementsInstancedBaseVertex(mode, indices.Length, DrawElementsType.UnsignedByte, ptr, instanceCount, baseVertex);
		}
	}
		
	/// <summary>
	/// Render multiple instances of a set of primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawElementsInstancedBaseVertex"), CLSCompliant(false)]
	public static void DrawElementsInstancedBaseVertex(PrimitiveType mode, ReadOnlySpan<ushort> indices, int instanceCount, int baseVertex)
	{
		fixed (ushort* ptr = &indices.GetPinnableReference())
		{
			glDrawElementsInstancedBaseVertex(mode, indices.Length, DrawElementsType.UnsignedShort, ptr, instanceCount, baseVertex);
		}
	}
		
	/// <summary>
	/// Render multiple instances of a set of primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawElementsInstancedBaseVertex"), CLSCompliant(false)]
	public static void DrawElementsInstancedBaseVertex(PrimitiveType mode, ReadOnlySpan<uint> indices, int instanceCount, int baseVertex)
	{
		fixed (uint* ptr = &indices.GetPinnableReference())
		{
			glDrawElementsInstancedBaseVertex(mode, indices.Length, DrawElementsType.UnsignedInt, ptr, instanceCount, baseVertex);
		}
	}
		
	/// <summary>
	/// Render multiple instances of a set of primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawElementsInstancedBaseVertex")]
	public static void DrawElementsInstancedBaseVertex(PrimitiveType mode, ReadOnlySpan<short> indices, int instanceCount, int baseVertex)
	{
		fixed (short* ptr = &indices.GetPinnableReference())
		{
			glDrawElementsInstancedBaseVertex(mode, indices.Length, DrawElementsType.UnsignedShort, ptr, instanceCount, baseVertex);
		}
	}
		
	/// <summary>
	/// Render multiple instances of a set of primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawElementsInstancedBaseVertex")]
	public static void DrawElementsInstancedBaseVertex(PrimitiveType mode, ReadOnlySpan<int> indices, int instanceCount, int baseVertex)
	{
		fixed (int* ptr = &indices.GetPinnableReference())
		{
			glDrawElementsInstancedBaseVertex(mode, indices.Length, DrawElementsType.UnsignedInt, ptr, instanceCount, baseVertex);
		}
	}
		
	/// <summary>
	/// Render primitives from array data.
	/// </summary>
	[NativeMethod("glDrawRangeElements"), CLSCompliant(false)]
	public static void DrawRangeElements(PrimitiveType mode, int start, int end, int count, DrawElementsType type, void* indices) => glDrawRangeElements(mode, start, end, count, type, indices);

	/// <summary>
	/// Render primitives from array data.
	/// </summary>
	[NativeMethod("glDrawRangeElements")]
	public static void DrawRangeElements(PrimitiveType mode, int start, int end, int count, DrawElementsType type, IntPtr indices) => glDrawRangeElements(mode, start, end, count, type, indices.ToPointer());

	/// <summary>
	/// Render primitives from array data.
	/// </summary>
	[NativeMethod("glDrawRangeElements")]
	public static void DrawRangeElements<T>(PrimitiveType mode, int start, int end, int count, DrawElementsType type, ReadOnlySpan<T> indices) where T : unmanaged
	{
		fixed (T* ptr = &indices.GetPinnableReference())
		{
			glDrawRangeElements(mode, start, end, count, type, ptr);
		}
	}
		
	/// <summary>
	/// Render primitives from array data.
	/// </summary>
	[NativeMethod("glDrawRangeElements")]
	public static void DrawRangeElements(PrimitiveType mode, int start, int end, ReadOnlySpan<byte> indices)
	{
		fixed (byte* ptr = &indices.GetPinnableReference())
		{
			glDrawRangeElements(mode, start, end, indices.Length, DrawElementsType.UnsignedByte, ptr);
		}
	}
		
	/// <summary>
	/// Render primitives from array data.
	/// </summary>
	[NativeMethod("glDrawRangeElements"), CLSCompliant(false)]
	public static void DrawRangeElements(PrimitiveType mode, int start, int end, ReadOnlySpan<ushort> indices)
	{
		fixed (ushort* ptr = &indices.GetPinnableReference())
		{
			glDrawRangeElements(mode, start, end, indices.Length, DrawElementsType.UnsignedShort, ptr);
		}
	}
		
	/// <summary>
	/// Render primitives from array data.
	/// </summary>
	[NativeMethod("glDrawRangeElements"), CLSCompliant(false)]
	public static void DrawRangeElements(PrimitiveType mode, int start, int end, ReadOnlySpan<uint> indices)
	{
		fixed (uint* ptr = &indices.GetPinnableReference())
		{
			glDrawRangeElements(mode, start, end, indices.Length, DrawElementsType.UnsignedInt, ptr);
		}
	}
		
	/// <summary>
	/// Render primitives from array data.
	/// </summary>
	[NativeMethod("glDrawRangeElements")]
	public static void DrawRangeElements(PrimitiveType mode, int start, int end, ReadOnlySpan<short> indices)
	{
		fixed (short* ptr = &indices.GetPinnableReference())
		{
			glDrawRangeElements(mode, start, end, indices.Length, DrawElementsType.UnsignedShort, ptr);
		}
	}
		
	/// <summary>
	/// Render primitives from array data.
	/// </summary>
	[NativeMethod("glDrawRangeElements")]
	public static void DrawRangeElements(PrimitiveType mode, int start, int end, ReadOnlySpan<int> indices)
	{
		fixed (int* ptr = &indices.GetPinnableReference())
		{
			glDrawRangeElements(mode, start, end, indices.Length, DrawElementsType.UnsignedInt, ptr);
		}
	}
		
	/// <summary>
	/// Render primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawRangeElementsBaseVertex"), CLSCompliant(false)]
	public static void DrawRangeElementsBaseVertex(PrimitiveType mode, int start, int end, int count, DrawElementsType type, void* indices, int baseVertex) => glDrawRangeElementsBaseVertex(mode, start, end, count, type, indices, baseVertex);

	/// <summary>
	/// Render primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawRangeElementsBaseVertex")]
	public static void DrawRangeElementsBaseVertex(PrimitiveType mode, int start, int end, int count, DrawElementsType type, IntPtr indices, int baseVertex) => glDrawRangeElementsBaseVertex(mode, start, end, count, type, indices.ToPointer(), baseVertex);

	/// <summary>
	/// Render primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawRangeElementsBaseVertex")]
	public static void DrawRangeElementsBaseVertex<T>(PrimitiveType mode, int start, int end, int count, DrawElementsType type, ReadOnlySpan<T> indices, int baseVertex) where T : unmanaged
	{
		fixed (T* ptr = &indices.GetPinnableReference())
		{
			glDrawRangeElementsBaseVertex(mode, start, end, count, type, ptr, baseVertex);
		}
	}
		
	/// <summary>
	/// Render primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawRangeElementsBaseVertex")]
	public static void DrawRangeElementsBaseVertex(PrimitiveType mode, int start, int end, ReadOnlySpan<byte> indices, int baseVertex)
	{
		fixed (byte* ptr = &indices.GetPinnableReference())
		{
			glDrawRangeElementsBaseVertex(mode, start, end, indices.Length, DrawElementsType.UnsignedByte, ptr, baseVertex);
		}
	}
		
	/// <summary>
	/// Render primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawRangeElementsBaseVertex"), CLSCompliant(false)]
	public static void DrawRangeElementsBaseVertex(PrimitiveType mode, int start, int end, ReadOnlySpan<ushort> indices, int baseVertex)
	{
		fixed (ushort* ptr = &indices.GetPinnableReference())
		{
			glDrawRangeElementsBaseVertex(mode, start, end, indices.Length, DrawElementsType.UnsignedShort, ptr, baseVertex);
		}
	}
		
	/// <summary>
	/// Render primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawRangeElementsBaseVertex"), CLSCompliant(false)]
	public static void DrawRangeElementsBaseVertex(PrimitiveType mode, int start, int end, ReadOnlySpan<uint> indices, int baseVertex)
	{
		fixed (uint* ptr = &indices.GetPinnableReference())
		{
			glDrawRangeElementsBaseVertex(mode, start, end, indices.Length, DrawElementsType.UnsignedInt, ptr, baseVertex);
		}
	}
		
	/// <summary>
	/// Render primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawRangeElementsBaseVertex")]
	public static void DrawRangeElementsBaseVertex(PrimitiveType mode, int start, int end, ReadOnlySpan<short> indices, int baseVertex)
	{
		fixed (short* ptr = &indices.GetPinnableReference())
		{
			glDrawRangeElementsBaseVertex(mode, start, end, indices.Length, DrawElementsType.UnsignedShort, ptr, baseVertex);
		}
	}
		
	/// <summary>
	/// Render primitives from array data with a per-element offset.
	/// </summary>
	[NativeMethod("glDrawRangeElementsBaseVertex")]
	public static void DrawRangeElementsBaseVertex(PrimitiveType mode, int start, int end, ReadOnlySpan<int> indices, int baseVertex)
	{
		fixed (int* ptr = &indices.GetPinnableReference())
		{
			glDrawRangeElementsBaseVertex(mode, start, end, indices.Length, DrawElementsType.UnsignedInt, ptr, baseVertex);
		}
	}

	/// <summary>
	/// Enable a generic vertex attribute array.
	/// </summary>
	[NativeMethod("glEnableVertexAttribArray")]
	public static void EnableVertexAttribArray(int index) => glEnableVertexAttribArray(index);

	/// <summary>
	/// Enable server-side GL capabilities.
	/// </summary>
	[NativeMethod("glEnablei")]
	public static void Enable(EnableCap target, int index) => glEnablei(target, index);
		
	/// <summary>
	/// Enable server-side GL capabilities.
	/// </summary>
	[NativeMethod("glEnablei")]
	public static void Enable(int target, int index) => glEnablei(Unsafe.As<int, EnableCap>(ref target), index);

	/// <summary>
	/// Ends conditional rendering.
	/// </summary>
	/// <seealso cref="BeginConditionalRender"/>
	[NativeMethod("glEndConditionalRender")]
	public static void EndConditionalRender() => glEndConditionalRender();

	/// <summary>
	/// Assigns the samples-passed counter to the query object's result value.
	/// </summary>
	/// <seealso cref="BeginQuery"/>
	[NativeMethod("glEndQuery")]
	public static void EndQuery(QueryTarget target) => glEndQuery(target);

	/// <summary>
	/// Ends a transform feedback operation.
	/// </summary>
	/// <seealso cref="BeginTransformFeedback"/>
	[NativeMethod("glEndTransformFeedback")]
	public static void EndTransformFeedback() => glEndTransformFeedback();

	/// <summary>
	/// Block until all GL execution is complete.
	/// </summary>
	[NativeMethod("glFinish")]
	public static void Finish() => glFinish();

	/// <summary>
	/// Force execution of GL commands in finite time.
	/// </summary>
	[NativeMethod("glFlush")]
	public static void Flush() => glFlush();

	/// <summary>
	/// Indicate modifications to a range of a mapped buffer.
	/// </summary>
	[NativeMethod("glFlushMappedBufferRange")]
	public static void FlushMappedBufferRange(BufferTarget target, nint offset, nint length) => glFlushMappedBufferRange(target, offset, length);

	/// <summary>
	/// Attach a renderbuffer as a logical buffer to the currently bound framebuffer object.
	/// </summary>
	[NativeMethod("glFramebufferRenderbuffer")]
	public static void FramebufferRenderbuffer(FramebufferTarget target, FramebufferAttachment attachment, RenderbufferTarget renderbufferTarget, Renderbuffer renderbuffer) => glFramebufferRenderbuffer(target, attachment, renderbufferTarget, renderbuffer);

	/// <summary>
	/// Attach a level of a texture object as a logical buffer to the currently bound framebuffer object.
	/// </summary>
	[NativeMethod("glFramebufferTexture")]
	public static void FramebufferTexture(FramebufferTarget target, FramebufferAttachment attachment, Texture texture, int level) => glFramebufferTexture(target, attachment, texture, level);

	/// <summary>
	/// Attach a level of a texture object as a logical buffer to the currently bound framebuffer object.
	/// </summary>
	[NativeMethod("glFramebufferTexture1D")]
	public static void FramebufferTexture1D(FramebufferTarget target, FramebufferAttachment attachment, TextureTarget texTarget, Texture texture, int level) => glFramebufferTexture1D(target, attachment, texTarget, texture, level);

	/// <summary>
	/// Attach a level of a texture object as a logical buffer to the currently bound framebuffer object.
	/// </summary>
	[NativeMethod("glFramebufferTexture2D")]
	public static void FramebufferTexture2D(FramebufferTarget target, FramebufferAttachment attachment, TextureTarget texTarget, Texture texture, int level) => glFramebufferTexture2D(target, attachment, texTarget, texture, level);

	/// <summary>
	/// Attach a level of a texture object as a logical buffer to the currently bound framebuffer object.
	/// </summary>
	[NativeMethod("glFramebufferTexture3D")]
	public static void FramebufferTexture3D(FramebufferTarget target, FramebufferAttachment attachment, TextureTarget texTarget, Texture texture, int level, int zOffset) => glFramebufferTexture3D(target, attachment, texTarget, texture, level, zOffset);

	/// <summary>
	/// Attach a single layer of a texture to a framebuffer.
	/// </summary>
	[NativeMethod("glFramebufferTextureLayer")]
	public static void FramebufferTextureLayer(FramebufferTarget target, FramebufferAttachment attachment, Texture texture, int level, int layer) => glFramebufferTextureLayer(target, attachment, texture, level, layer);

	/// <summary>
	/// Define front- and back-facing polygons.
	/// </summary>
	[NativeMethod("glFrontFace")]
	public static void FrontFace(FrontFaceDirection mode) => glFrontFace(mode);

	/// <summary>
	/// Generate mipmaps for a specified texture target.
	/// </summary>
	[NativeMethod("glGenerateMipmap")]
	public static void GenerateMipmap(TextureTarget target) => glGenerateMipmap(target);

	/// <summary>
	/// Returns information about an active attribute variable for the specified program object.
	/// </summary>
	[NativeMethod("glGetActiveAttrib"), CLSCompliant(false)]
	public static void GetActiveAttrib(Program program, int index, int bufSize, int* length, int* size, AttributeType* type, byte* name) => glGetActiveAttrib(program, index, bufSize, length, size, type, name);

	/// <summary>
	/// Returns information about an active attribute variable for the specified program object.
	/// </summary>
	[NativeMethod("glGetActiveAttrib"), NativeMethod("glGetProgramiv")]
	public static void GetActiveAttrib(Program program, int index, out int size, out AttributeType type, out string? name)
	{
		int length;
		glGetProgramiv(program, ProgramProperty.ActiveAttributeMaxLength, &length);

		int attrSize;
		AttributeType attrType;
		var buffer = length > MAX_STACK_ALLOC ? new byte[length] : stackalloc byte[length] ;
		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			glGetActiveAttrib(program, index, length, &length, &attrSize, &attrType, ptr);
			name = Encoding.UTF8.GetString(ptr, length);
		}

		size = attrSize;
		type = attrType;
	}
		
	/// <summary>
	/// Returns information about an active attribute variable for the specified program object.
	/// </summary>
	[NativeMethod("glGetActiveAttrib")]
	public static void GetActiveAttrib(Program program, int index, out int size, out AttributeType type, out int length, Span<byte> buffer)
	{
		int attrSize, nameLength;
		AttributeType attrType;
		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			glGetActiveAttrib(program, index, buffer.Length, &nameLength, &attrSize, &attrType, ptr);
		}
		size = attrSize;
		type = attrType;
		length = nameLength;
	}
		
	/// <summary>
	/// Returns information about an active uniform variable for the specified program object.
	/// </summary>
	[NativeMethod("glGetActiveUniform"), CLSCompliant(false)]
	public static void GetActiveUniform(Program program, int index, int bufSize, int* length, int* size, UniformType* type, byte* name) => glGetActiveUniform(program, index, bufSize, length, size, type, name);
		
	/// <summary>
	/// Returns information about an active uniform variable for the specified program object.
	/// </summary>
	[NativeMethod("glGetActiveUniform"), NativeMethod("glGetProgramiv")]
	public static void GetActiveUniform(Program program, int index, int bufSize, out int size, out UniformType type, out string? name)
	{
		int uniSize, length;
		UniformType uniType;
		glGetProgramiv(program, ProgramProperty.ActiveUniformMaxLength, &length);
			
		var buffer = length > MAX_STACK_ALLOC ? new byte[length] : stackalloc byte[length] ;
		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			glGetActiveUniform(program, index, length, &length, &uniSize, &uniType, ptr);
			name = Encoding.UTF8.GetString(ptr, length);
		}
		size = uniSize;
		type = uniType;
	}

	/// <summary>
	/// Returns information about an active uniform variable for the specified program object.
	/// </summary>
	[NativeMethod("glGetActiveUniform")]
	public static void GetActiveUniform(Program program, int index, int bufSize, out int size, out UniformType type, out int length, Span<byte> buffer)
	{
		int uniSize, nameLength;
		UniformType uniType;
		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			glGetActiveUniform(program, index, buffer.Length, &nameLength, &uniSize, &uniType, ptr);
		}

		length = nameLength;
		size = uniSize;
		type = uniType;
	}
		
	/// <summary>
	/// Retrieve the name of an active uniform block.
	/// </summary>
	[NativeMethod("glGetActiveUniformBlockName"), CLSCompliant(false)]
	public static void GetActiveUniformBlockName(Program program, int uniformBlockIndex, int bufSize, int* length, byte* uniformBlockName) => glGetActiveUniformBlockName(program, uniformBlockIndex, bufSize, length, uniformBlockName);

	/// <summary>
	/// Retrieve the name of an active uniform block.
	/// </summary>
	[NativeMethod("glGetActiveUniformBlockName")]
	public static void GetActiveUniformBlockName(Program program, int uniformBlockIndex, out int length, Span<byte> buffer)
	{
		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			int nameLength;
			glGetActiveUniformBlockName(program, uniformBlockIndex, buffer.Length, &nameLength, ptr);
			length = nameLength;
		}
	}
		
	/// <summary>
	/// Retrieve the name of an active uniform block.
	/// </summary>
	[NativeMethod("glGetActiveUniformBlockName"), NativeMethod("glGetProgramiv")]
	public static string? GetActiveUniformBlockName(Program program, int uniformBlockIndex)
	{
		int length;
		glGetProgramiv(program, ProgramProperty.ActiveUniformBlockMaxNameLength, &length);
		if (length == 0)
			return null;
			
		var buffer = length > MAX_STACK_ALLOC ? new byte[length] : stackalloc byte[length];
		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			glGetActiveUniformBlockName(program, uniformBlockIndex, buffer.Length, &length, ptr);
			return Encoding.UTF8.GetString(ptr, length);
		}
	}

	/// <summary>
	/// Query information about an active uniform block.
	/// </summary>
	[NativeMethod("glGetActiveUniformBlockiv"), CLSCompliant(false)]
	public static void GetActiveUniformBlock(Program program, int uniformBlockIndex, UniformBlockPName paramName, int* parameters) => glGetActiveUniformBlockiv(program, uniformBlockIndex, paramName, parameters);

	/// <summary>
	/// Query information about an active uniform block.
	/// </summary>
	[NativeMethod("glGetActiveUniformBlockiv")]
	public static void GetActiveUniformBlock(Program program, int uniformBlockIndex, UniformBlockPName paramName, Span<int> parameters)
	{
		fixed (int* ptr = &parameters.GetPinnableReference())
		{
			glGetActiveUniformBlockiv(program, uniformBlockIndex, paramName, ptr);
		}
	}
		
	/// <summary>
	/// Query information about an active uniform block.
	/// </summary>
	[NativeMethod("glGetActiveUniformBlockiv")]
	public static int GetActiveUniformBlock(Program program, int uniformBlockIndex, UniformBlockPName paramName)
	{
		int value;
		glGetActiveUniformBlockiv(program, uniformBlockIndex, paramName, &value);
		return value;
	}
		
	/// <summary>
	/// Query the name of an active uniform.
	/// </summary>
	[NativeMethod("glGetActiveUniformName"), CLSCompliant(false)]
	public static void GetActiveUniformName(Program program, int uniformIndex, int bufSize, int* length, byte* uniformName) => glGetActiveUniformName(program, uniformIndex, bufSize, length, uniformName);


	/// <summary>
	/// Query the name of an active uniform.
	/// </summary>
	[NativeMethod("glGetActiveUniformName")]
	public static void GetActiveUniformName(Program program, int uniformIndex, out int length, Span<byte> buffer)
	{
		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			int nameLength;
			glGetActiveUniformName(program, uniformIndex, buffer.Length, &nameLength, ptr);
			length = nameLength;
		}
	}
		
	/// <summary>
	/// Query the name of an active uniform.
	/// </summary>
	[NativeMethod("glGetActiveUniformName"), NativeMethod("glGetProgramiv")]
	public static string? GetActiveUniformName(Program program, int uniformIndex)
	{
		int length;
		glGetProgramiv(program, ProgramProperty.ActiveUniformMaxLength, &length);
		if (length == 0)
			return null;
			
		var buffer = length > MAX_STACK_ALLOC ? new byte[length] : stackalloc byte[length];
		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			glGetActiveUniformName(program, uniformIndex, length, &length, ptr);
			return Encoding.UTF8.GetString(ptr, length);
		}
	}
		
	/// <summary>
	/// Returns information about several active uniform variables for the specified program object.
	/// </summary>
	[NativeMethod("glGetActiveUniformsiv"), CLSCompliant(false)]
	public static void GetActiveUniforms(Program program, int uniformCount, int* uniformIndices, UniformPName paramName, int* parameters) => glGetActiveUniformsiv(program, uniformCount, uniformIndices, paramName, parameters);
		
	/// <summary>
	/// Returns information about several active uniform variables for the specified program object.
	/// </summary>
	[NativeMethod("glGetActiveUniformsiv")]
	public static void GetActiveUniforms(Program program, ReadOnlySpan<int> uniformIndices, UniformPName paramName, Span<int> parameters)
	{
		var uniformCount = Math.Min(uniformIndices.Length, parameters.Length);
		fixed (int* indexPtr = &uniformIndices.GetPinnableReference())
		{
			fixed (int* paramPtr = &parameters.GetPinnableReference())
			{
				glGetActiveUniformsiv(program, uniformCount, indexPtr, paramName, paramPtr);	
			}
		}
	}
		
	/// <summary>
	/// Returns the handles of the shader objects attached to a program object.
	/// </summary>
	[NativeMethod("glGetAttachedShaders"), CLSCompliant(false)]
	public static void GetAttachedShaders(Program program, int maxCount, int* count, Shader* shaders) => glGetAttachedShaders(program, maxCount, count, shaders);
		
	/// <summary>
	/// Returns the handles of the shader objects attached to a program object.
	/// </summary>
	[NativeMethod("glGetAttachedShaders")]
	public static void GetAttachedShaders(Program program, out int count, Span<Shader> shaders)
	{
		fixed (Shader* ptr = &shaders.GetPinnableReference())
		{
			int size;
			glGetAttachedShaders(program, shaders.Length, &size, ptr);
			count = size;
		}
	}

	/// <summary>
	/// Returns the location of an attribute variable.
	/// </summary>
	[NativeMethod("glGetAttribLocation")]
	public static int GetAttribLocation(Program program, string name)
	{
		fixed (byte* ptr = &UTF8String.Pin(name))
		{
			return glGetAttribLocation(program, ptr);
		}
	}

	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetBooleani_v"), CLSCompliant(false)]
	public static void GetBoolean(BufferTarget target, int index, bool* data) => glGetBooleani_v(target, index, data);

		/// <summary>
    	/// Return the value or values of a selected parameter
    	/// </summary>
	[NativeMethod("glGetBooleanv"), CLSCompliant(false)]
	public static void GetBoolean(GetPName paramName, bool* data) => glGetBooleanv(paramName, data);

		/// <summary>
		/// Return the value or values of a selected parameter
		/// </summary>
	[NativeMethod("glGetBooleani_v")]
	public static bool GetBoolean(BufferTarget target, int index)
	{
		bool value;
		glGetBooleani_v(target, index, &value);
		return value;
	}

	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetBooleanv")]
	public static bool GetBoolean(GetPName paramName)
	{
		bool value;
		glGetBooleanv(paramName, &value);
		return value;
	}

	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetBooleani_v")]
	public static void GetBoolean(BufferTarget target, int index, Span<bool> data)
	{
		fixed (bool* ptr = &data.GetPinnableReference())
		{
			glGetBooleani_v(target, index, ptr);
		}
	}

	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetBooleanv")]
	public static void GetBoolean(GetPName paramName, Span<bool> data)
	{
		fixed (bool* ptr = &data.GetPinnableReference())
		{
			glGetBooleanv(paramName, ptr);
		}
	}
		
	/// <summary>
	/// Return parameters of a buffer object.
	/// </summary>
	[NativeMethod("glGetBufferParameteri64v"), CLSCompliant(false)]
	public static void GetBufferParameter64(BufferTarget target, BufferPName paramName, long* parameters) => glGetBufferParameteri64v(target, paramName, parameters);
		
	/// <summary>
	/// Return parameters of a buffer object.
	/// </summary>
	[NativeMethod("glGetBufferParameteriv"), CLSCompliant(false)]
	public static void GetBufferParameter(BufferTarget target, BufferPName paramName, int* parameters) => glGetBufferParameteriv(target, paramName, parameters);
		
	/// <summary>
	/// Return parameters of a buffer object.
	/// </summary>
	[NativeMethod("glGetBufferParameteri64v")]
	public static void GetBufferParameter64(BufferTarget target, BufferPName paramName, Span<long> parameters)
	{
		fixed (long* ptr = &parameters.GetPinnableReference())
		{
			glGetBufferParameteri64v(target, paramName, ptr);
		}
	}

	/// <summary>
	/// Return parameters of a buffer object.
	/// </summary>
	[NativeMethod("glGetBufferParameteriv")]
	public static void GetBufferParameter(BufferTarget target, BufferPName paramName, Span<int> parameters)
	{
		fixed (int* ptr = &parameters.GetPinnableReference())
		{
			glGetBufferParameteriv(target, paramName, ptr);
		}
	}
		
	/// <summary>
	/// Return parameters of a buffer object.
	/// </summary>
	[NativeMethod("glGetBufferParameteri64v")]
	public static long GetBufferParameter64(BufferTarget target, BufferPName paramName)
	{
		long value;
		glGetBufferParameteri64v(target, paramName, &value);
		return value;
	}

	/// <summary>
	/// Return parameters of a buffer object.
	/// </summary>
	[NativeMethod("glGetBufferParameteriv")]
	public static int GetBufferParameter(BufferTarget target, BufferPName paramName)
	{
		int value;
		glGetBufferParameteriv(target, paramName, &value);
		return value;
	}

	/// <summary>
	/// Return the pointer to a mapped buffer object's data store.
	/// </summary>
	[NativeMethod("glGetBufferPointerv"), CLSCompliant(false)]
	public static void GetBufferPointer(BufferTarget target, BufferPointerName paramName, void** parameters)
	{
		glGetBufferPointerv(target, paramName, parameters);
	}
		
	/// <summary>
	/// Return the pointer to a mapped buffer object's data store.
	/// </summary>
	[NativeMethod("glGetBufferPointerv")]
	public static void GetBufferPointer(BufferTarget target, BufferPointerName paramName, Span<IntPtr> parameters)
	{
		fixed (IntPtr *ptr = &parameters.GetPinnableReference())
		{
			glGetBufferPointerv(target, paramName, (void**) ptr);
		}
	}
		
	/// <summary>
	/// Return the pointer to a mapped buffer object's data store.
	/// </summary>
	[NativeMethod("glGetBufferPointerv")]
	public static IntPtr GetBufferPointer(BufferTarget target, BufferPointerName paramName)
	{
		void* value;
		glGetBufferPointerv(target, paramName, &value);
		return new IntPtr(value);
	}

	/// <summary>
	/// Returns a subset of a buffer object's data store.
	/// </summary>
	[NativeMethod("glGetBufferSubData"), CLSCompliant(false)]
	public static void GetBufferSubData(BufferTarget target, nint offset, nint size, void* data) => glGetBufferSubData(target, offset, size, data);
		
	/// <summary>
	/// Returns a subset of a buffer object's data store.
	/// </summary>
	[NativeMethod("glGetBufferSubData")]
	public static void GetBufferSubData(BufferTarget target, nint offset, nint size, IntPtr data) => glGetBufferSubData(target, offset, size, data.ToPointer());

	/// <summary>
	/// Returns a subset of a buffer object's data store.
	/// </summary>
	[NativeMethod("glGetBufferSubData")]
	public static void GetBufferSubData<T>(BufferTarget target, nint offset, Span<T> data) where T : unmanaged
	{
		var size = Unsafe.SizeOf<T>() * data.Length;
		fixed (T* ptr = &data.GetPinnableReference())
		{
			glGetBufferSubData(target, offset, size, ptr);
		}
	}
		
	/// <summary>
	/// Return a compressed texture image.
	/// </summary>
	[NativeMethod("glGetCompressedTexImage"), CLSCompliant(false)]
	public static void GetCompressedTexImage(TextureTarget target, int level, void* img) => glGetCompressedTexImage(target, level, img);
		
	/// <summary>
	/// Return a compressed texture image.
	/// </summary>
	[NativeMethod("glGetCompressedTexImage")]
	public static void GetCompressedTexImage(TextureTarget target, int level, IntPtr img) => glGetCompressedTexImage(target, level, img.ToPointer());

	/// <summary>
	/// Return a compressed texture image.
	/// </summary>
	[NativeMethod("glGetCompressedTexImage")]
	public static void GetCompressedTexImage<T>(TextureTarget target, int level, Span<T> img) where T : unmanaged
	{
		fixed (T* ptr = &img.GetPinnableReference())
		{
			glGetCompressedTexImage(target, level, ptr);
		}
	}
		
	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetDoublev"), CLSCompliant(false)]
	public static void GetDouble(GetPName paramName, double* data) => glGetDoublev(paramName, data);

	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetDoublev")]
	public static void GetDouble(GetPName paramName, Span<double> data)
	{
		fixed (double* ptr = &data.GetPinnableReference())
		{
			glGetDoublev(paramName, ptr);
		}
	}
		
	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetDoublev")]
	public static double GetDouble(GetPName paramName)
	{
		double value;
		glGetDoublev(paramName, &value);
		return value;
	}

	/// <summary>
	/// Return error information.
	/// </summary>
	[NativeMethod("glGetError")]
	public static ErrorCode GetError() => glGetError();

	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetFloatv"), CLSCompliant(false)]
	public static void GetFloat(GetPName paramName, float* data) => glGetFloatv(paramName, data);

	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetFloatv")]
	public static void GetFloat(GetPName paramName, Span<float> data)
	{
		fixed (float *ptr = &data.GetPinnableReference())
		{
			glGetFloatv(paramName, ptr);
		}
	}
		
	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetFloatv")]
	public static float GetFloat(GetPName paramName)
	{
		float value;
		glGetFloatv(paramName, &value);
		return value;
	}

	/// <summary>
	/// Query the bindings of color indices to user-defined varying out variables.
	/// </summary>
	[NativeMethod("glGetFragDataIndex")]
	public static int GetFragDataIndex(Program program, string name)
	{
		fixed (byte* ptr = &UTF8String.Pin(name))
		{
			return glGetFragDataIndex(program, ptr);
		}
	}

	/// <summary>
	/// Query the bindings of color numbers to user-defined varying out variables.
	/// </summary>
	[NativeMethod("glGetFragDataLocation")]
	public static int GetFragDataLocation(Program program, string name)
	{
		fixed (byte* ptr = &UTF8String.Pin(name))
		{
			return glGetFragDataLocation(program, ptr);
		}
	}

	/// <summary>
	/// Retrieve information about attachments of a bound framebuffer object.
	/// </summary>
	[NativeMethod("glGetFramebufferAttachmentParameteriv"), CLSCompliant(false)]
	public static void GetFramebufferAttachmentParameter(FramebufferTarget target, FramebufferAttachment attachment, FramebufferAttachmentParameter paramName, int* parameters) 
	{
		glGetFramebufferAttachmentParameteriv(target, attachment, paramName, parameters);
	}
		
	/// <summary>
	/// Retrieve information about attachments of a bound framebuffer object.
	/// </summary>
	[NativeMethod("glGetFramebufferAttachmentParameteriv")]
	public static void GetFramebufferAttachmentParameter(FramebufferTarget target, FramebufferAttachment attachment, FramebufferAttachmentParameter paramName, Span<int> parameters) 
	{
		fixed (int* ptr = &parameters.GetPinnableReference())
		{
			glGetFramebufferAttachmentParameteriv(target, attachment, paramName, ptr);	
		}
	}
		
	/// <summary>
	/// Retrieve information about attachments of a bound framebuffer object.
	/// </summary>
	[NativeMethod("glGetFramebufferAttachmentParameteriv")]
	public static int GetFramebufferAttachmentParameter(FramebufferTarget target, FramebufferAttachment attachment, FramebufferAttachmentParameter paramName)
	{
		int value;
		glGetFramebufferAttachmentParameteriv(target, attachment, paramName, &value);
		return value;
	}

	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetInteger64i_v"), CLSCompliant(false)]
	public static void GetInteger64(GetPName target, int index, long* data) => glGetInteger64i_v(target, index, data);

	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetInteger64v"), CLSCompliant(false)]
	public static void GetInteger64(GetPName paramName, long* data) => glGetInteger64v(paramName, data);

	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetIntegeri_v"), CLSCompliant(false)]
	public static void GetInteger(GetPName target, int index, int* data) => glGetIntegeri_v(target, index, data);

	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetIntegerv"), CLSCompliant(false)]
	public static void GetInteger(GetPName paramName, int* data) => glGetIntegerv(paramName, data);
	
	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetInteger64i_v")]
	public static void GetInteger64(GetPName target, int index, Span<long> data)
	{
		fixed (long* ptr = &data.GetPinnableReference())
		{
			glGetInteger64i_v(target, index, ptr);
		}
	}

	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetInteger64v")]
	public static void GetInteger64(GetPName paramName, Span<long> data)
	{
		fixed (long* ptr = &data.GetPinnableReference())
		{
			glGetInteger64v(paramName, ptr);
		}
	}

	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetIntegeri_v")]
	public static void GetInteger(GetPName target, int index, Span<int> data)
	{
		fixed (int* ptr = &data.GetPinnableReference())
		{
			glGetIntegeri_v(target, index, ptr);
		}
	}

	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetIntegerv")]
	public static void GetInteger(GetPName paramName, Span<int> data)
	{
		fixed (int* ptr = &data.GetPinnableReference())
		{
			glGetIntegerv(paramName, ptr);
		}
	}
		
	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetInteger64i_v")]
	public static long GetInteger64(GetPName target, int index) 
	{
		long value;
		glGetInteger64i_v(target, index, &value);
		return value;
	}

	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetInteger64v")]
	public static long GetInteger64(GetPName paramName) 
	{
		long value;
		glGetInteger64v(paramName, &value);
		return value;
	}

	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetIntegeri_v")]
	public static int GetInteger(GetPName target, int index) 
	{
		int value;
		glGetIntegeri_v(target, index, &value);
		return value;
	}

	/// <summary>
	/// Return the value or values of a selected parameter
	/// </summary>
	[NativeMethod("glGetIntegerv")]
	public static int GetInteger(GetPName paramName) 
	{
		int value;
		glGetIntegerv(paramName, &value);
		return value;
	}
		
	/// <summary>
	/// Retrieve the location of a sample.
	/// </summary>
	[NativeMethod("glGetMultisamplefv"), CLSCompliant(false)]
	public static void GetMultisample(GetMultisamplePName paramName, int index, float* val) => glGetMultisamplefv(paramName, index, val);

	/// <summary>
	/// Retrieve the location of a sample.
	/// </summary>
	[NativeMethod("glGetMultisamplefv")]
	public static void GetMultisample(GetMultisamplePName paramName, int index, Span<float> val)
	{
		fixed (float* ptr = &val.GetPinnableReference())
		{
			glGetMultisamplefv(paramName, index, ptr);
		}
	}
		
	/// <summary>
	/// Retrieve the location of a sample.
	/// </summary>
	[NativeMethod("glGetMultisamplefv")]
	public static float GetMultisample(GetMultisamplePName paramName, int index)
	{
		float value;
		glGetMultisamplefv(paramName, index, &value);
		return value;
	}
		
	/// <summary>
	/// Retrieve the label of a named object identified within a namespace.
	/// </summary>
	[NativeMethod("glGetObjectLabel"), CLSCompliant(false)]
	public static void GetObjectLabel(ObjectIdentifier identifier, int name, int bufSize, int* length, byte* label) => glGetObjectLabel(identifier, name, bufSize, length, label);

	/// <summary>
	/// Retrieve the label of a named object identified within a namespace.
	/// </summary>
	[NativeMethod("glGetObjectLabel")]
	public static void GetObjectLabel(ObjectIdentifier identifier, int name, out int length, Span<byte> buffer)
	{
		int labelLength;
		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			glGetObjectLabel(identifier, name, buffer.Length, &labelLength, ptr);
		}
		length = labelLength;
	}
		
	/// <summary>
	/// Retrieve the label of a named object identified within a namespace.
	/// </summary>
	[NativeMethod("glGetObjectLabel"), NativeMethod("glGetIntegerv")]
	public static string? GetObjectLabel(ObjectIdentifier identifier, int name)
	{
		int labelLength;
		glGetIntegerv(GetPName.MaxLabelLength, &labelLength);
		if (labelLength == 0)
			return null;
		
		var buffer = labelLength > MAX_STACK_ALLOC ? new byte[labelLength] : stackalloc byte[labelLength];
		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			glGetObjectLabel(identifier, name, buffer.Length, &labelLength, ptr);
			return Encoding.UTF8.GetString(ptr, labelLength);
		}
	}
		
	/// <summary>
	/// Retrieve the label of a sync object identified by a pointer.
	/// </summary>
	[NativeMethod("glGetObjectPtrLabel"), CLSCompliant(false)]
	public static void GetObjectPtrLabel(void* ptr, int bufSize, int* length, byte* label) => glGetObjectPtrLabel(ptr, bufSize, length, label);

	/// <summary>
	/// Retrieve the label of a sync object identified by a pointer.
	/// </summary>
	[NativeMethod("glGetObjectPtrLabel")]
	public static void GetObjectPtrLabel(IntPtr pointer, out int length, Span<byte> buffer)
	{
		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			int labelLength;
			glGetObjectPtrLabel(pointer.ToPointer(), buffer.Length, &labelLength, ptr);
			length = labelLength;
		}
	}
		
	/// <summary>
	/// Retrieve the label of a sync object identified by a pointer.
	/// </summary>
	[NativeMethod("glGetObjectPtrLabel"), NativeMethod("glGetIntegerv")]
	public static string? GetObjectPtrLabel(IntPtr pointer)
	{
		int labelLength;
		glGetIntegerv(GetPName.MaxLabelLength, &labelLength);
		if (labelLength == 0)
			return null;
		
		var buffer = labelLength > MAX_STACK_ALLOC ? new byte[labelLength] : stackalloc byte[labelLength];
		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			glGetObjectPtrLabel(pointer.ToPointer(), buffer.Length, &labelLength, ptr);
			return Encoding.UTF8.GetString(ptr, labelLength);
		}
	}
	
	/// <summary>
	/// Return the address of the specified pointer.
	/// </summary>
	[NativeMethod("glGetPointerv"), CLSCompliant(false)]
	public static void GetPointer(GetPointervPName paramName, void** parameters) => glGetPointerv(paramName, parameters);

	/// <summary>
	/// Return the address of the specified pointer.
	/// </summary>
	[NativeMethod("glGetPointerv")]
	public static void GetPointer(GetPointervPName paramName, Span<IntPtr> parameters)
	{
		fixed (IntPtr* ptr = &parameters.GetPinnableReference())
		{
			glGetPointerv(paramName, (void**) ptr);
		}
	}
		
	/// <summary>
	/// Return the address of the specified pointer.
	/// </summary>
	[NativeMethod("glGetPointerv")]
	public static IntPtr GetPointer(GetPointervPName paramName)
	{
		void* value;
		glGetPointerv(paramName, &value);
		return new IntPtr(value);
	}

	/// <summary>
	/// Returns the information log for a program object.
	/// </summary>
	[NativeMethod("glGetProgramInfoLog"), CLSCompliant(false)]
	public static void GetProgramInfoLog(Program program, int bufSize, int* length, byte* infoLog) => glGetProgramInfoLog(program, bufSize, length, infoLog);


	/// <summary>
	/// Returns the information log for a program object.
	/// </summary>
	[NativeMethod("glGetProgramInfoLog")]
	public static void GetProgramInfoLog(Program program, out int length, Span<byte> buffer)
	{
		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			int logLength;
			glGetProgramInfoLog(program, buffer.Length, &logLength, ptr);
			length = logLength;
		}
	}
		
	/// <summary>
	/// Returns the information log for a program object.
	/// </summary>
	[NativeMethod("glGetProgramInfoLog"), NativeMethod("glGetProgramiv")]
	public static string? GetProgramInfoLog(Program program)
	{
		int logLength;
		glGetProgramiv(program, ProgramProperty.InfoLogLength, &logLength);
		if (logLength == 0)
			return null;
		
		var buffer = logLength > MAX_STACK_ALLOC ? new byte[logLength] : stackalloc byte[logLength];
		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			glGetProgramInfoLog(program, logLength, &logLength, ptr);
			return Encoding.UTF8.GetString(ptr, logLength);
		}
	}
		
	/// <summary>
	/// Returns a parameter from a program object.
	/// </summary>
	[NativeMethod("glGetProgramiv"), CLSCompliant(false)]
	public static void GetProgram(Program program, ProgramProperty paramName, int* parameters) => glGetProgramiv(program, paramName, parameters);

	/// <summary>
	/// Returns a parameter from a program object.
	/// </summary>
	[NativeMethod("glGetProgramiv")]
	public static void GetProgram(Program program, ProgramProperty paramName, Span<int> parameters)
	{
		fixed (int* ptr = &parameters.GetPinnableReference())
		{
			glGetProgramiv(program, paramName, ptr);
		}
	}

	/// <summary>
	/// Returns a parameter from a program object.
	/// </summary>
	[NativeMethod("glGetProgramiv")]
	public static int GetProgram(Program program, ProgramProperty paramName)
	{
		int value;
		glGetProgramiv(program, paramName, &value);
		return value;
	}
		
	/// <summary>
	/// Return parameters of a query object.
	/// </summary>
	[NativeMethod("glGetQueryObjecti64v"), CLSCompliant(false)]
	public static void GetQueryObject(Query id, QueryObjectParameterName paramName, long* parameters) => glGetQueryObjecti64v(id, paramName, parameters);

	/// <summary>
	/// Return parameters of a query object.
	/// </summary>
	[NativeMethod("glGetQueryObjectiv"), CLSCompliant(false)]
	public static void GetQueryObject(Query id, QueryObjectParameterName paramName, int* parameters) => glGetQueryObjectiv(id, paramName, parameters);

	/// <summary>
	/// Return parameters of a query object.
	/// </summary>
	[NativeMethod("glGetQueryObjectui64v"), CLSCompliant(false)]
	public static void GetQueryObject(Query id, QueryObjectParameterName paramName, ulong* parameters) => glGetQueryObjectui64v(id, paramName, parameters);

	/// <summary>
	/// Return parameters of a query object.
	/// </summary>
	[NativeMethod("glGetQueryObjectuiv"), CLSCompliant(false)]
	public static void GetQueryObject(Query id, QueryObjectParameterName paramName, uint* parameters) => glGetQueryObjectuiv(id, paramName, parameters);
		
	/// <summary>
	/// Return parameters of a query object.
	/// </summary>
	[NativeMethod("glGetQueryObjecti64v")]
	public static void GetQueryObject(Query id, QueryObjectParameterName paramName, Span<long> parameters)
	{
		fixed (long* ptr = &parameters.GetPinnableReference())
		{
			glGetQueryObjecti64v(id, paramName, ptr);
		}
	}

	/// <summary>
	/// Return parameters of a query object.
	/// </summary>
	[NativeMethod("glGetQueryObjectiv")]
	public static void GetQueryObject(Query id, QueryObjectParameterName paramName, Span<int> parameters)
	{
		fixed (int* ptr = &parameters.GetPinnableReference())
		{
			glGetQueryObjectiv(id, paramName, ptr);
		}
	}

	/// <summary>
	/// Return parameters of a query object.
	/// </summary>
	[NativeMethod("glGetQueryObjectui64v"), CLSCompliant(false)]
	public static void GetQueryObject(Query id, QueryObjectParameterName paramName, Span<ulong> parameters)
	{
		fixed (ulong* ptr = &parameters.GetPinnableReference())
		{
			glGetQueryObjectui64v(id, paramName, ptr);
		}
	}

	/// <summary>
	/// Return parameters of a query object.
	/// </summary>
	[NativeMethod("glGetQueryObjectuiv"), CLSCompliant(false)]
	public static void GetQueryObject(Query id, QueryObjectParameterName paramName, Span<uint> parameters)
	{
		fixed (uint* ptr = &parameters.GetPinnableReference())
		{
			glGetQueryObjectuiv(id, paramName, ptr);
		}
	}
		
	/// <summary>
	/// Return parameters of a query object.
	/// </summary>
	[NativeMethod("glGetQueryObjecti64v")]
	public static void GetQueryObject(Query id, QueryObjectParameterName paramName, out long parameter)
	{
		long value;
		glGetQueryObjecti64v(id, paramName, &value);
		parameter = value;
	}

	/// <summary>
	/// Return parameters of a query object.
	/// </summary>
	[NativeMethod("glGetQueryObjectiv")]
	public static void GetQueryObject(Query id, QueryObjectParameterName paramName, out int parameter)
	{
		int value;
		glGetQueryObjectiv(id, paramName, &value);
		parameter = value;
	}

	/// <summary>
	/// Return parameters of a query object.
	/// </summary>
	[NativeMethod("glGetQueryObjectui64v"), CLSCompliant(false)]
	public static void GetQueryObject(Query id, QueryObjectParameterName paramName, out ulong parameter)
	{
		ulong value;
		glGetQueryObjectui64v(id, paramName, &value);
		parameter = value;
	}

	/// <summary>
	/// Return parameters of a query object.
	/// </summary>
	[NativeMethod("glGetQueryObjectuiv"), CLSCompliant(false)]
	public static void GetQueryObject(Query id, QueryObjectParameterName paramName, out uint parameter)
	{
		uint value;
		glGetQueryObjectuiv(id, paramName, &value);
		parameter = value;
	}
		
	/// <summary>
	/// Return parameters of a query object target.
	/// </summary>
	[NativeMethod("glGetQueryiv"), CLSCompliant(false)]
	public static void GetQuery(QueryTarget target, QueryParameterName paramName, int* parameters) => glGetQueryiv(target, paramName, parameters);

	/// <summary>
	/// Return parameters of a query object target.
	/// </summary>
	[NativeMethod("glGetQueryiv")]
	public static void GetQuery(QueryTarget target, QueryParameterName paramName, Span<int> parameters)
	{
		fixed (int* ptr = &parameters.GetPinnableReference())
		{
			glGetQueryiv(target, paramName, ptr);
		}
	}
	
	/// <summary>
	/// Return parameters of a query object target.
	/// </summary>
	[NativeMethod("glGetQueryiv")]
	public static int GetQuery(QueryTarget target, QueryParameterName paramName)
	{
		int value;
		glGetQueryiv(target, paramName, &value);
		return value;
	}
		
	/// <summary>
	/// Retrieve information about a bound renderbuffer object.
	/// </summary>
	[NativeMethod("glGetRenderbufferParameteriv"), CLSCompliant(false)]
	public static void GetRenderbufferParameter(RenderbufferTarget target, RenderbufferParameter paramName, int* parameters) => glGetRenderbufferParameteriv(target, paramName, parameters);
		
	/// <summary>
	/// Retrieve information about a bound renderbuffer object.
	/// </summary>
	[NativeMethod("glGetRenderbufferParameteriv")]
	public static void GetRenderbufferParameter(RenderbufferTarget target, RenderbufferParameter paramName, Span<int> parameters)
	{
		fixed (int* ptr = &parameters.GetPinnableReference())
		{
			glGetRenderbufferParameteriv(target, paramName, ptr);
		}
	}

	/// <summary>
	/// Retrieve information about a bound renderbuffer object.
	/// </summary>
	[NativeMethod("glGetRenderbufferParameteriv")]
	public static int GetRenderbufferParameter(RenderbufferTarget target, RenderbufferParameter paramName)
	{
		int value;
		glGetRenderbufferParameteriv(target, paramName, &value);
		return value;
	}
		
	/// <summary>
	/// Return sampler parameter values.
	/// </summary>
	[NativeMethod("glGetSamplerParameterIiv"), CLSCompliant(false)]
	public static void GetSamplerParameterI(Sampler sampler, SamplerParameterI paramName, int* parameters) => glGetSamplerParameterIiv(sampler, paramName, parameters);

	/// <summary>
	/// Return sampler parameter values.
	/// </summary>
	[NativeMethod("glGetSamplerParameterIuiv"), CLSCompliant(false)]
	public static void GetSamplerParameterI(Sampler sampler, SamplerParameterI paramName, uint* parameters) => glGetSamplerParameterIuiv(sampler, paramName, parameters);

	/// <summary>
	/// Return sampler parameter values.
	/// </summary>
	[NativeMethod("glGetSamplerParameterIiv")]
	public static void GetSamplerParameterI(Sampler sampler, SamplerParameterI paramName, Span<int> parameters)
	{
		fixed (int* ptr = &parameters.GetPinnableReference())
		{
			glGetSamplerParameterIiv(sampler, paramName, ptr);
		}
	}

	/// <summary>
	/// Return sampler parameter values.
	/// </summary>
	[NativeMethod("glGetSamplerParameterIuiv"), CLSCompliant(false)]
	public static void GetSamplerParameterI(Sampler sampler, SamplerParameterI paramName, Span<uint> parameters)
	{
		fixed (uint* ptr = &parameters.GetPinnableReference())
		{
			glGetSamplerParameterIuiv(sampler, paramName, ptr);
		}
	}

	/// <summary>
	/// Return sampler parameter values.
	/// </summary>
	[NativeMethod("glGetSamplerParameterIiv")]
	public static void GetSamplerParameterI(Sampler sampler, SamplerParameterI paramName, out int parameter)
	{
		int value;
		glGetSamplerParameterIiv(sampler, paramName, &value);
		parameter = value;
	}

	/// <summary>
	/// Return sampler parameter values.
	/// </summary>
	[NativeMethod("glGetSamplerParameterIuiv"), CLSCompliant(false)]
	public static void GetSamplerParameterI(Sampler sampler, SamplerParameterI paramName, out uint parameter)
	{
		uint value;
		glGetSamplerParameterIuiv(sampler, paramName, &value);
		parameter = value;
	}
		
	/// <summary>
	/// Return sampler parameter values.
	/// </summary>
	[NativeMethod("glGetSamplerParameterfv"), CLSCompliant(false)]
	public static void GetSamplerParameter(Sampler sampler, SamplerParameterF paramName, float* parameters) => glGetSamplerParameterfv(sampler, paramName, parameters);

	/// <summary>
	/// Return sampler parameter values.
	/// </summary>
	[NativeMethod("glGetSamplerParameteriv"), CLSCompliant(false)]
	public static void GetSamplerParameter(Sampler sampler, SamplerParameterI paramName, int* parameters) => glGetSamplerParameteriv(sampler, paramName, parameters);
		
	/// <summary>
	/// Return sampler parameter values.
	/// </summary>
	[NativeMethod("glGetSamplerParameterfv")]
	public static void GetSamplerParameter(Sampler sampler, SamplerParameterF paramName, Span<float> parameters)
	{
		fixed (float* ptr = &parameters.GetPinnableReference())
		{
			glGetSamplerParameterfv(sampler, paramName, ptr);
		}
	}

	/// <summary>
	/// Return sampler parameter values.
	/// </summary>
	[NativeMethod("glGetSamplerParameteriv")]
	public static void GetSamplerParameter(Sampler sampler, SamplerParameterI paramName, Span<int> parameters)
	{
		fixed (int* ptr = &parameters.GetPinnableReference())
		{
			glGetSamplerParameteriv(sampler, paramName, ptr);
		}
	}

	/// <summary>
	/// Return sampler parameter values.
	/// </summary>
	[NativeMethod("glGetSamplerParameterfv")]
	public static void GetSamplerParameter(Sampler sampler, SamplerParameterF paramName, out float parameter)
	{
		float value;
		glGetSamplerParameterfv(sampler, paramName, &value);
		parameter = value;
	}

	/// <summary>
	/// Return sampler parameter values.
	/// </summary>
	[NativeMethod("glGetSamplerParameteriv")]
	public static void GetSamplerParameter(Sampler sampler, SamplerParameterI paramName, out int parameter)
	{
		int value;
		glGetSamplerParameteriv(sampler, paramName, &value);
		parameter = value;
	}
		
	/// <summary>
	/// Returns the information log for a shader object.
	/// </summary>
	[NativeMethod("glGetShaderInfoLog"), CLSCompliant(false)]
	public static void GetShaderInfoLog(Shader shader, int bufSize, int* length, byte* infoLog) => glGetShaderInfoLog(shader, bufSize, length, infoLog);

	/// <summary>
	/// Returns the information log for a shader object.
	/// </summary>
	[NativeMethod("glGetShaderInfoLog")]
	public static void GetShaderInfoLog(Shader shader, out int length, Span<byte> buffer)
	{
		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			int logLength;
			glGetShaderInfoLog(shader, buffer.Length, &logLength, ptr);
			length = logLength;
		}
	}
		
	/// <summary>
	/// Returns the information log for a shader object.
	/// </summary>
	[NativeMethod("glGetShaderInfoLog"), NativeMethod("glGetShaderiv")]
	public static string? GetShaderInfoLog(Shader shader)
	{
		int logLength;
		glGetShaderiv(shader, ShaderParameterName.InfoLogLength, &logLength);
		if (logLength == 0)
			return null;
		
		var buffer = logLength > MAX_STACK_ALLOC ? new byte[logLength] : stackalloc byte[logLength];
			
		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			glGetShaderInfoLog(shader, logLength, &logLength, ptr);
			return Encoding.UTF8.GetString(ptr, logLength);
		}
	}
		
	/// <summary>
	/// Returns the source code string from a shader object.
	/// </summary>
	[NativeMethod("glGetShaderSource"), CLSCompliant(false)]
	public static void GetShaderSource(Shader shader, int bufSize, int* length, byte* source) => glGetShaderSource(shader, bufSize, length, source);

	/// <summary>
	/// Returns the source code string from a shader object.
	/// </summary>
	[NativeMethod("glGetShaderSource")]
	public static void GetShaderSource(Shader shader, out int length, Span<byte> buffer)
	{
		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			int srcLength;
			glGetShaderSource(shader, buffer.Length, &srcLength, ptr);
			length = srcLength;
		}
	}
		
	/// <summary>
	/// Returns the source code string from a shader object.
	/// </summary>
	[NativeMethod("glGetShaderSource"), NativeMethod("glGetShaderiv")]
	public static string? GetShaderSource(Shader shader)
	{
		int srcLength;
		glGetShaderiv(shader, ShaderParameterName.ShaderSourceLength, &srcLength);
		if (srcLength == 0)
			return null;
		
		var buffer = srcLength > MAX_STACK_ALLOC ? new byte[srcLength] : stackalloc byte[srcLength];
		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			glGetShaderSource(shader, buffer.Length, &srcLength, ptr);
			return Encoding.UTF8.GetString(ptr, srcLength);
		}
	}

	/// <summary>
	/// Returns a parameter from a shader object.
	/// </summary>
	[NativeMethod("glGetShaderiv"), CLSCompliant(false)]
	public static void GetShader(Shader shader, ShaderParameterName paramName, int* parameters) => glGetShaderiv(shader, paramName, parameters);

	/// <summary>
	/// Returns a parameter from a shader object.
	/// </summary>
	[NativeMethod("glGetShaderiv")]
	public static void GetShader(Shader shader, ShaderParameterName paramName, Span<int> parameters)
	{
		fixed (int* ptr = &parameters.GetPinnableReference())
		{
			glGetShaderiv(shader, paramName, ptr);
		}
	}
		
	/// <summary>
	/// Returns a parameter from a shader object.
	/// </summary>
	[NativeMethod("glGetShaderiv")]
	public static int GetShader(Shader shader, ShaderParameterName paramName)
	{
		int value;
		glGetShaderiv(shader, paramName,  &value);
		return value;
	}
		
	/// <summary>
	/// Return a string describing the current GL connection.
	/// </summary>
	[NativeMethod("glGetString")]
	public static string? GetString(StringName name) => Marshal.PtrToStringUTF8(glGetString(name));

	/// <summary>
	/// Return a string describing the current GL connection.
	/// </summary>
	[NativeMethod("glGetStringi")]
	public static string? GetString(StringName name, int index) => Marshal.PtrToStringUTF8(glGetStringi(name, index));
		
	/// <summary>
	/// Query the properties of a sync object.
	/// </summary>
	[NativeMethod("glGetSynciv"), CLSCompliant(false)]
	public static void GetSync(Sync sync, SyncParameterName paramName, int count, int* length, int* values) => glGetSynciv(sync, paramName, count, length, values);


	/// <summary>
	/// Query the properties of a sync object.
	/// </summary>
	[NativeMethod("glGetSynciv")]
	public static void GetSync(Sync sync, SyncParameterName paramName, out int length, Span<int> values)
	{
		fixed (int* ptr = &values.GetPinnableReference())
		{
			int valueLength;
			glGetSynciv(sync, paramName, values.Length, &valueLength, ptr);
			length = valueLength;
		}
	}
		
	/// <summary>
	/// Return a texture image.
	/// </summary>
	[NativeMethod("glGetTexImage"), CLSCompliant(false)]
	public static void GetTexImage(TextureTarget target, int level, PixelFormat format, PixelType type, void* pixels) => glGetTexImage(target, level, format, type, pixels);

	/// <summary>
	/// Return a texture image.
	/// </summary>
	[NativeMethod("glGetTexImage")]
	public static void GetTexImage(TextureTarget target, int level, PixelFormat format, PixelType type, IntPtr pixels) => glGetTexImage(target, level, format, type, pixels.ToPointer());

	/// <summary>
	/// Return a texture image.
	/// </summary>
	[NativeMethod("glGetTexImage")]
	public static void GetTexImage<T>(TextureTarget target, int level, PixelFormat format, PixelType type, Span<T> pixels) where T : unmanaged
	{
		fixed (T* ptr = &pixels.GetPinnableReference())
		{
			glGetTexImage(target, level, format, type, ptr);	
		}
	}
		
	/// <summary>
	/// Return texture parameter values for a specific level of detail.
	/// </summary>
	[NativeMethod("glGetTexLevelParameterfv"), CLSCompliant(false)]
	public static void GetTexLevelParameter(TextureTarget target, int level, GetTextureParameter paramName, float* parameters) => glGetTexLevelParameterfv(target, level, paramName, parameters);

	/// <summary>
	/// Return texture parameter values for a specific level of detail.
	/// </summary>
	[NativeMethod("glGetTexLevelParameteriv"), CLSCompliant(false)]
	public static void GetTexLevelParameter(TextureTarget target, int level, GetTextureParameter paramName, int* parameters) => glGetTexLevelParameteriv(target, level, paramName, parameters);

	/// <summary>
	/// Return texture parameter values for a specific level of detail.
	/// </summary>
	[NativeMethod("glGetTexLevelParameterfv")]
	public static void GetTexLevelParameter(TextureTarget target, int level, GetTextureParameter paramName, Span<float> parameters)
	{
		fixed (float* ptr = &parameters.GetPinnableReference())
		{
			glGetTexLevelParameterfv(target, level, paramName, ptr);
		}
	}

	/// <summary>
	/// Return texture parameter values for a specific level of detail.
	/// </summary>
	[NativeMethod("glGetTexLevelParameteriv")]
	public static void GetTexLevelParameter(TextureTarget target, int level, GetTextureParameter paramName, Span<int> parameters)
	{
		fixed (int* ptr = &parameters.GetPinnableReference())
		{
			glGetTexLevelParameteriv(target, level, paramName, ptr);
		}
	}

	/// <summary>
	/// Return texture parameter values for a specific level of detail.
	/// </summary>
	[NativeMethod("glGetTexLevelParameterfv")]
	public static void GetTexLevelParameter(TextureTarget target, int level, GetTextureParameter paramName, out float parameter)
	{
		float value;
		glGetTexLevelParameterfv(target, level, paramName, &value);
		parameter = value;
	}

	/// <summary>
	/// Return texture parameter values for a specific level of detail.
	/// </summary>
	[NativeMethod("glGetTexLevelParameteriv")]
	public static void GetTexLevelParameter(TextureTarget target, int level, GetTextureParameter paramName, out int parameter)
	{
		int value;
		glGetTexLevelParameteriv(target, level, paramName, &value);
		parameter = value;
	}
		
	/// <summary>
	/// Return texture parameter values.
	/// </summary>
	[NativeMethod("glGetTexParameterIiv"), CLSCompliant(false)]
	public static void GetTexParameterI(TextureTarget target, GetTextureParameter paramName, int* parameters) => glGetTexParameterIiv(target, paramName, parameters);

	/// <summary>
	/// Return texture parameter values.
	/// </summary>
	[NativeMethod("glGetTexParameterIuiv"), CLSCompliant(false)]
	public static void GetTexParameterI(TextureTarget target, GetTextureParameter paramName, uint* parameters) => glGetTexParameterIuiv(target, paramName, parameters);

	/// <summary>
	/// Return texture parameter values.
	/// </summary>
	[NativeMethod("glGetTexParameterfv"), CLSCompliant(false)]
	public static void GetTexParameter(TextureTarget target, GetTextureParameter paramName, float* parameters) => glGetTexParameterfv(target, paramName, parameters);

	/// <summary>
	/// Return texture parameter values.
	/// </summary>
	[NativeMethod("glGetTexParameteriv"), CLSCompliant(false)]
	public static void GetTexParameter(TextureTarget target, GetTextureParameter paramName, int* parameters) => glGetTexParameteriv(target, paramName, parameters);
		
	/// <summary>
	/// Return texture parameter values.
	/// </summary>
	[NativeMethod("glGetTexParameterIiv")]
	public static void GetTexParameterI(TextureTarget target, GetTextureParameter paramName, Span<int> parameters)
	{
		fixed (int* ptr = &parameters.GetPinnableReference())
		{
			glGetTexParameterIiv(target, paramName, ptr);
		}
	}
	
	/// <summary>
	/// Return texture parameter values.
	/// </summary>
	[NativeMethod("glGetTexParameterIuiv"), CLSCompliant(false)]
	public static void GetTexParameterI(TextureTarget target, GetTextureParameter paramName, Span<uint> parameters)
	{
		fixed (uint* ptr = &parameters.GetPinnableReference())
		{
			glGetTexParameterIuiv(target, paramName, ptr);
		}
	}

	/// <summary>
	/// Return texture parameter values.
	/// </summary>
	[NativeMethod("glGetTexParameterfv")]
	public static void GetTexParameter(TextureTarget target, GetTextureParameter paramName, Span<float> parameters)
	{
		fixed (float* ptr = &parameters.GetPinnableReference())
		{
			glGetTexParameterfv(target, paramName, ptr);
		}
	}

	/// <summary>
	/// Return texture parameter values.
	/// </summary>
	[NativeMethod("glGetTexParameteriv")]
	public static void GetTexParameter(TextureTarget target, GetTextureParameter paramName, Span<int> parameters)
	{
		fixed (int* ptr = &parameters.GetPinnableReference())
		{
			glGetTexParameteriv(target, paramName, ptr);
		}
	}

	/// <summary>
	/// Set texture parameters.
	/// </summary>
	[NativeMethod("glGetTexParameterIiv")]
	public static void GetTexParameterI(TextureTarget target, GetTextureParameter paramName, out int parameter)
	{
		int value;
		glGetTexParameterIiv(target, paramName, &value);
		parameter = value;
	}

	/// <summary>
	/// Set texture parameters.
	/// </summary>
	[NativeMethod("glGetTexParameterIuiv"), CLSCompliant(false)]
	public static void GetTexParameterI(TextureTarget target, GetTextureParameter paramName, out uint parameter)
	{
		uint value;
		glGetTexParameterIuiv(target, paramName, &value);
		parameter = value;
	}

	/// <summary>
	/// Return texture parameter values.
	/// </summary>
	[NativeMethod("glGetTexParameterfv")]
	public static void GetTexParameter(TextureTarget target, GetTextureParameter paramName, out float parameter)
	{
		float value;
		glGetTexParameterfv(target, paramName, &value);
		parameter = value;
	}

	/// <summary>
	/// Return texture parameter values.
	/// </summary>
	[NativeMethod("glGetTexParameteriv")]
	public static void GetTexParameter(TextureTarget target, GetTextureParameter paramName, out int parameter)
	{
		int value;
		glGetTexParameteriv(target, paramName, &value);
		parameter = value;
	}
		
	/// <summary>
	/// Return texture parameter values.
	/// </summary>
	[NativeMethod("glGetTexParameteriv")]
	public static void GetTexParameter<TEnum>(TextureTarget target, GetTextureParameter paramName, out TEnum parameter) where TEnum : Enum
	{
		if (Marshal.SizeOf(Enum.GetUnderlyingType(typeof(TEnum))) != sizeof(int))
			throw new InvalidCastException("Underlying integer type of enum must be 32-bits.");
			
		int value;
		glGetTexParameteriv(target, paramName, &value);
		parameter = Unsafe.As<int, TEnum>(ref value);
	}
		
	/// <summary>
	/// Retrieve information about varying variables selected for transform feedback.
	/// </summary>
	[NativeMethod("glGetTransformFeedbackVarying"), CLSCompliant(false)]
	public static void GetTransformFeedbackVarying(Program program, int index, int bufSize, int* length, int* size, AttributeType* type, byte* name) => glGetTransformFeedbackVarying(program, index, bufSize, length, size, type, name);
		
	/// <summary>
	/// Retrieve information about varying variables selected for transform feedback.
	/// </summary>
	[NativeMethod("glGetTransformFeedbackVarying")]
	public static void GetTransformFeedbackVarying(Program program, int index, out int length, out int size, out AttributeType type, Span<byte> buffer)
	{
		AttributeType attrType;
		int attrSize, nameLength;

		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			glGetTransformFeedbackVarying(program, index, buffer.Length, &nameLength, &attrSize, &attrType, ptr);	
		}

		length = nameLength;
		size = attrSize;
		type = attrType;
	}

	/// <summary>
	/// Retrieve information about varying variables selected for transform feedback.
	/// </summary>
	[NativeMethod("glGetTransformFeedbackVarying"), NativeMethod("glGetProgramiv")]
	public static void GetTransformFeedbackVarying(Program program, int index, out int size, out AttributeType type, out string? name)
	{
		AttributeType attrType;
		int attrSize, nameLength;

		glGetProgramiv(program, ProgramProperty.TransformFeedbackVaryingMaxLength, &nameLength);
		var buffer = nameLength > MAX_STACK_ALLOC ? new byte[nameLength] : stackalloc byte[nameLength];
			
		fixed (byte* ptr = &buffer.GetPinnableReference())
		{
			glGetTransformFeedbackVarying(program, index, buffer.Length, &nameLength, &attrSize, &attrType, ptr);
			name = Encoding.UTF8.GetString(ptr, nameLength);
		}
			
		size = attrSize;
		type = attrType;
	}
		
	/// <summary>
	/// Retrieve the index of a named uniform block.
	/// </summary>
	[NativeMethod("glGetUniformBlockIndex")]
	public static int GetUniformBlockIndex(Program program, string uniformBlockName)
	{
		fixed (byte* ptr = &UTF8String.Pin(uniformBlockName))
		{
			return glGetUniformBlockIndex(program, ptr);
		}
	}
		
	/// <summary>
	/// Retrieve the index of a named uniform block.
	/// </summary>
	[NativeMethod("glGetUniformIndices"), CLSCompliant(false)]
	public static void GetUniformIndices(Program program, int uniformCount, byte** uniformNames, int* uniformIndices) => glGetUniformIndices(program, uniformCount, uniformNames, uniformIndices);

	/// <summary>
	/// Retrieve the index of a named uniform block.
	/// </summary>
	[NativeMethod("glGetUniformIndices")]
	public static void GetUniformIndices(Program program, Span<int> uniformIndices, params string[] uniformNames)
	{
		var uniformCount = Math.Min(uniformIndices.Length, uniformIndices.Length);
		var names = stackalloc IntPtr[uniformCount];

		try
		{
			for (var i = 0; i < uniformCount; i++)
			{
				var utf8 = Encoding.UTF8.GetBytes(uniformNames[i]);
				names[i] = Marshal.AllocHGlobal(utf8.Length + 1);
				Marshal.Copy(utf8, 0, names[i], utf8.Length);
			}

			fixed (int* indices = &uniformIndices.GetPinnableReference())
			{
				glGetUniformIndices(program, uniformCount, (byte**)names, indices);
			}
		}
		finally
		{
			for (var i = 0; i < uniformCount; i++)
			{
				if (names[i] != IntPtr.Zero)
					Marshal.FreeHGlobal(names[i]);
			}
		}
	}
	
	/// <summary>
	/// Retrieve the index of a named uniform block.
	/// </summary>
	[NativeMethod("glGetUniformIndices")]
	public static int GetUniformIndex(Program program, string uniformName)
	{
		fixed (byte* ptr = &UTF8String.Pin(uniformName))
		{
			int index;
			glGetUniformIndices(program, 1, &ptr, &index);
			return index;
		}
	}

	/// <summary>
	/// Returns the location of a uniform variable.
	/// </summary>
	[NativeMethod("glGetUniformLocation")]
	public static int GetUniformLocation(Program program, string name)
	{
		fixed (byte* ptr = &UTF8String.Pin(name))
		{
			return glGetUniformLocation(program, ptr);
		}
	}

	/// <summary>
	/// Returns the value of a uniform variable.
	/// </summary>
	[NativeMethod("glGetUniformfv"), CLSCompliant(false)]
	public static void GetUniform(Program program, int location, float* parameters) => glGetUniformfv(program, location, parameters);

	/// <summary>
	/// Returns the value of a uniform variable.
	/// </summary>
	[NativeMethod("glGetUniformiv"), CLSCompliant(false)]
	public static void GetUniform(Program program, int location, int* parameters) => glGetUniformiv(program, location, parameters);

	/// <summary>
	/// Returns the value of a uniform variable.
	/// </summary>
	[NativeMethod("glGetUniformuiv"), CLSCompliant(false)]
	public static void GetUniform(Program program, int location, uint* parameters) => glGetUniformuiv(program, location, parameters);
		
	/// <summary>
	/// Returns the value of a uniform variable.
	/// </summary>
	[NativeMethod("glGetUniformfv")]
	public static void GetUniform(Program program, int location, Span<float> parameters)
	{
		fixed (float* ptr = &parameters.GetPinnableReference())
		{
			glGetUniformfv(program, location, ptr);
		}
	}

	/// <summary>
	/// Returns the value of a uniform variable.
	/// </summary>
	[NativeMethod("glGetUniformiv")]
	public static void GetUniform(Program program, int location, Span<int> parameters)
	{
		fixed (int* ptr = &parameters.GetPinnableReference())
		{
			glGetUniformiv(program, location, ptr);
		}
	}

	/// <summary>
	/// Returns the value of a uniform variable.
	/// </summary>
	[NativeMethod("glGetUniformuiv"), CLSCompliant(false)]
	public static void GetUniform(Program program, int location, Span<uint> parameters)
	{
		fixed (uint* ptr = &parameters.GetPinnableReference())
		{
			glGetUniformuiv(program, location, ptr);
		}
	}
		
	/// <summary>
	/// Returns the value of a uniform variable.
	/// </summary>
	[NativeMethod("glGetUniformfv")]
	public static void GetUniform(Program program, int location, out float parameter)
	{
		float value;
		glGetUniformfv(program, location, &value);
		parameter = value;
	}

	/// <summary>
	/// Returns the value of a uniform variable.
	/// </summary>
	[NativeMethod("glGetUniformiv")]
	public static void GetUniform(Program program, int location, out int parameter)
	{
		int value;
		glGetUniformiv(program, location, &value);
		parameter = value;
	}

	/// <summary>
	/// Returns the value of a uniform variable.
	/// </summary>
	[NativeMethod("glGetUniformuiv"), CLSCompliant(false)]
	public static void GetUniform(Program program, int location, out uint parameter)
	{
		uint value;
		glGetUniformuiv(program, location, &value);
		parameter = value;
	}
	
	/// <summary>
	/// Return a generic vertex attribute parameter.
	/// </summary>
	[NativeMethod("glGetVertexAttribIiv"), CLSCompliant(false)]
	public static void GetVertexAttribI(int index, VertexAttrib paramName, int* parameters) => glGetVertexAttribIiv(index, paramName, parameters);

	/// <summary>
	/// Return a generic vertex attribute parameter.
	/// </summary>
	[NativeMethod("glGetVertexAttribIuiv"), CLSCompliant(false)]
	public static void GetVertexAttribI(int index, VertexAttrib paramName, uint* parameters) => glGetVertexAttribIuiv(index, paramName, parameters);
		
	/// <summary>
	/// Return a generic vertex attribute parameter.
	/// </summary>
	[NativeMethod("glGetVertexAttribIiv")]
	public static void GetVertexAttribI(int index, VertexAttrib paramName, Span<int> parameters)
	{
		fixed (int* ptr = &parameters.GetPinnableReference())
		{
			glGetVertexAttribIiv(index, paramName, ptr);
		}
	}

	/// <summary>
	/// Return a generic vertex attribute parameter.
	/// </summary>
	[NativeMethod("glGetVertexAttribIuiv"), CLSCompliant(false)]
	public static void GetVertexAttribI(int index, VertexAttrib paramName, Span<uint> parameters)
	{
		fixed (uint* ptr = &parameters.GetPinnableReference())
		{
			glGetVertexAttribIuiv(index, paramName, ptr);
		}
	}

	/// <summary>
	/// Return a generic vertex attribute parameter.
	/// </summary>
	[NativeMethod("glGetVertexAttribIiv")]
	public static void GetVertexAttribI(int index, VertexAttrib paramName, out int parameter)
	{
		int value;
		glGetVertexAttribIiv(index, paramName, &value);
		parameter = value;
	}

	/// <summary>
	/// Return a generic vertex attribute parameter.
	/// </summary>
	[NativeMethod("glGetVertexAttribIuiv"), CLSCompliant(false)]
	public static void GetVertexAttribI(int index, VertexAttrib paramName, out uint parameter)
	{
		uint value;
		glGetVertexAttribIuiv(index, paramName, &value);
		parameter = value;
	}

	/// <summary>
	/// Return the address of the specified generic vertex attribute pointer.
	/// </summary>
	[NativeMethod("glGetVertexAttribPointerv"), CLSCompliant(false)]
	public static void GetVertexAttribPointer(int index, VertexAttribPointerProperty paramName, void** pointer) => glGetVertexAttribPointerv(index, paramName, pointer);

	/// <summary>
	/// Return the address of the specified generic vertex attribute pointer.
	/// </summary>
	[NativeMethod("glGetVertexAttribPointerv")]
	public static void GetVertexAttribPointer(int index, VertexAttribPointerProperty paramName, Span<IntPtr> pointer)
	{
		fixed (IntPtr* ptr = &pointer.GetPinnableReference())
		{
			glGetVertexAttribPointerv(index, paramName, (void**) ptr);
		}
	}
		
	/// <summary>
	/// Return the address of the specified generic vertex attribute pointer.
	/// </summary>
	[NativeMethod("glGetVertexAttribPointerv")]
	public static IntPtr GetVertexAttribPointer(int index, VertexAttribPointerProperty paramName)
	{
		void* value;
		glGetVertexAttribPointerv(index, paramName, &value);
		return new IntPtr(value);
	}
		
	/// <summary>
	/// Return a generic vertex attribute parameter.
	/// </summary>
	[NativeMethod("glGetVertexAttribdv"), CLSCompliant(false)]
	public static void GetVertexAttrib(int index, VertexAttrib paramName, double* parameters) => glGetVertexAttribdv(index, paramName, parameters);

	/// <summary>
	/// Return a generic vertex attribute parameter.
	/// </summary>
	[NativeMethod("glGetVertexAttribfv"), CLSCompliant(false)]
	public static void GetVertexAttrib(int index, VertexAttrib paramName, float* parameters) => glGetVertexAttribfv(index, paramName, parameters);

	/// <summary>
	/// Return a generic vertex attribute parameter.
	/// </summary>
	[NativeMethod("glGetVertexAttribiv"), CLSCompliant(false)]
	public static void GetVertexAttrib(int index, VertexAttrib paramName, int* parameters) => glGetVertexAttribiv(index, paramName, parameters);
		
	/// <summary>
	/// Return a generic vertex attribute parameter.
	/// </summary>
	[NativeMethod("glGetVertexAttribdv")]
	public static void GetVertexAttrib(int index, VertexAttrib paramName, Span<double> parameters)
	{
		fixed (double* ptr = &parameters.GetPinnableReference())
		{
			glGetVertexAttribdv(index, paramName, ptr);
		}
	}

	/// <summary>
	/// Return a generic vertex attribute parameter.
	/// </summary>
	[NativeMethod("glGetVertexAttribfv")]
	public static void GetVertexAttrib(int index, VertexAttrib paramName, Span<float> parameters)
	{
		fixed (float* ptr = &parameters.GetPinnableReference())
		{
			glGetVertexAttribfv(index, paramName, ptr);
		}
	}

	/// <summary>
	/// Return a generic vertex attribute parameter.
	/// </summary>
	[NativeMethod("glGetVertexAttribiv")]
	public static void GetVertexAttrib(int index, VertexAttrib paramName, Span<int> parameters)
	{
		fixed (int* ptr = &parameters.GetPinnableReference())
		{
			glGetVertexAttribiv(index, paramName, ptr);
		}
	}
		
	/// <summary>
	/// Return a generic vertex attribute parameter.
	/// </summary>
	[NativeMethod("glGetVertexAttribdv")]
	public static void GetVertexAttrib(int index, VertexAttrib paramName, out double parameter)
	{
		double value;
		glGetVertexAttribdv(index, paramName, &value);
		parameter = value;
	}

	/// <summary>
	/// Return a generic vertex attribute parameter.
	/// </summary>
	[NativeMethod("glGetVertexAttribfv")]
	public static void GetVertexAttrib(int index, VertexAttrib paramName, out float parameter)
	{
		float value;
		glGetVertexAttribfv(index, paramName, &value);
		parameter = value;
	}

	/// <summary>
	/// Return a generic vertex attribute parameter.
	/// </summary>
	[NativeMethod("glGetVertexAttribiv")]
	public static void GetVertexAttrib(int index, VertexAttrib paramName, out int parameter)
	{
		int value;
		glGetVertexAttribiv(index, paramName, &value);
		parameter = value;
	}
		
	/// <summary>
	/// Specify implementation-specific hints.
	/// </summary>
	[NativeMethod("glHint")]
	public static void Hint(HintTarget target, HintMode mode) => glHint(target, mode);

	/// <summary>
	/// Test whether a capability is enabled.
	/// </summary>
	[NativeMethod("glIsEnabled")]
	public static bool IsEnabled(EnableCap cap) => glIsEnabled(cap);

	/// <summary>
	/// Test whether a capability is enabled.
	/// </summary>
	[NativeMethod("glIsEnabledi")]
	public static bool IsEnabled(EnableCap target, int index) => glIsEnabledi(target, index);
		
	/// <summary>
	/// Test whether a capability is enabled.
	/// </summary>
	[NativeMethod("glIsEnabled")]
	public static bool IsEnabled(int cap) => glIsEnabled(Unsafe.As<int, EnableCap>(ref cap));

	/// <summary>
	/// Test whether a capability is enabled.
	/// </summary>
	[NativeMethod("glIsEnabledi")]
	public static bool IsEnabled(int target, int index) => glIsEnabledi(Unsafe.As<int, EnableCap>(ref target), index);

	/// <summary>
	/// Specify the width of rasterized lines.
	/// </summary>
	[NativeMethod("glLineWidth")]
	public static void LineWidth(float width) => glLineWidth(width);

	/// <summary>
	/// Links a program object.
	/// </summary>
	[NativeMethod("glLinkProgram")]
	public static void LinkProgram(Program program) => glLinkProgram(program);

	/// <summary>
	/// Specify a logical pixel operation for rendering.
	/// </summary>
	[NativeMethod("glLogicOp")]
	public static void LogicOp(LogicOp opcode) => glLogicOp(opcode);

	/// <summary>
	/// Map a buffer object's data store.
	/// </summary>
	[NativeMethod("glMapBuffer")]
	public static IntPtr MapBuffer(BufferTarget target, BufferAccess access) => glMapBuffer(target, access);

	/// <summary>
	/// Map a section of a buffer object's data store.
	/// </summary>
	[NativeMethod("glMapBufferRange")]
	public static IntPtr MapBufferRange(BufferTarget target, nint offset, nint length, MapBufferAccessMask access) => glMapBufferRange(target, offset, length, access);

	/// <summary>
	/// Render multiple sets of primitives from array data.
	/// </summary>
	[NativeMethod("glMultiDrawArrays"), CLSCompliant(false)]
	public static void MultiDrawArrays(PrimitiveType mode, int* first, int* count, int drawCount) => glMultiDrawArrays(mode, first, count, drawCount);

	/// <summary>
	/// Render multiple sets of primitives from array data.
	/// </summary>
	[NativeMethod("glMultiDrawArrays")]
	public static void MultiDrawArrays(PrimitiveType mode, ReadOnlySpan<int> first, ReadOnlySpan<int> count)
	{
		var drawCount = Math.Min(first.Length, count.Length);
		fixed (int* firstPtr = &first.GetPinnableReference())
		{
			fixed (int* countPtr = &count.GetPinnableReference())
			{
				glMultiDrawArrays(mode, firstPtr, countPtr, drawCount);	
			}
		}
	}

	/// <summary>
	/// Render multiple sets of primitives by specifying indices of array data elements.
	/// </summary>
	[NativeMethod("glMultiDrawElements"), CLSCompliant(false)]
	public static void MultiDrawElements(PrimitiveType mode, int* count, DrawElementsType type, void** indices, int drawCount) => glMultiDrawElements(mode, count, type, indices, drawCount);

	/// <summary>
	/// Render multiple sets of primitives by specifying indices of array data elements.
	/// </summary>
	[NativeMethod("glMultiDrawElements")]
	public static void MultiDrawElements(PrimitiveType mode, ReadOnlySpan<int> count, DrawElementsType type, ReadOnlySpan<IntPtr> indices)
	{
		fixed (int* countPtr = &count.GetPinnableReference())
		{
			fixed (IntPtr* ptr = &indices.GetPinnableReference())
			{
				glMultiDrawElements(mode, countPtr, type, (void**) ptr, indices.Length);
			}
		}
	}
		
	/// <summary>
	/// Render multiple sets of primitives by specifying indices of array data elements and an index to apply to each index.
	/// </summary>
	[NativeMethod("glMultiDrawElementsBaseVertex"), CLSCompliant(false)]
	public static void MultiDrawElementsBaseVertex(PrimitiveType mode, int* count, DrawElementsType type, void** indices, int drawCount, int* baseVertex) => glMultiDrawElementsBaseVertex(mode, count, type, indices, drawCount, baseVertex);

	/// <summary>
	/// Render multiple sets of primitives by specifying indices of array data elements and an index to apply to each index.
	/// </summary>
	[NativeMethod("glMultiDrawElementsBaseVertex")]
	public static void MultiDrawElementsBaseVertex(PrimitiveType mode, ReadOnlySpan<int> count, DrawElementsType type, ReadOnlySpan<IntPtr> indices, int drawCount, ReadOnlySpan<int> baseVertex)
	{
		fixed (int* countPtr = &count.GetPinnableReference())
		{
			fixed (IntPtr* ptr = &indices.GetPinnableReference())
			{
				fixed (int* basePtr = &baseVertex.GetPinnableReference())
				{
					glMultiDrawElementsBaseVertex(mode, countPtr, type, (void**) ptr, drawCount, basePtr);
				}
			}
		}
	}
		
	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP1ui")]
	public static void MultiTexCoordP1(TextureUnit texture, TexCoordPointerType type, int coords) => glMultiTexCoordP1ui(texture, type, coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP1uiv"), CLSCompliant(false)]
	public static void MultiTexCoordP1(TextureUnit texture, TexCoordPointerType type, int* coords) => glMultiTexCoordP1uiv(texture, type, coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP1uiv")]
	public static void MultiTexCoordP1(TextureUnit texture, TexCoordPointerType type, ReadOnlySpan<int> coords)
	{
		fixed (int* ptr = &coords.GetPinnableReference())
		{
			glMultiTexCoordP1uiv(texture, type, ptr);
		}
	}

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP2ui")]
	public static void MultiTexCoordP2(TextureUnit texture, TexCoordPointerType type, int coords) => glMultiTexCoordP2ui(texture, type, coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP2uiv"), CLSCompliant(false)]
	public static void MultiTexCoordP2(TextureUnit texture, TexCoordPointerType type, int* coords) => glMultiTexCoordP2uiv(texture, type, coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP2uiv")]
	public static void MultiTexCoordP2(TextureUnit texture, TexCoordPointerType type, ReadOnlySpan<int> coords)
	{
		fixed (int* ptr = &coords.GetPinnableReference())
		{
			glMultiTexCoordP2uiv(texture, type, ptr);
		}
	}
		
	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP3ui")]
	public static void MultiTexCoordP3(TextureUnit texture, TexCoordPointerType type, int coords) => glMultiTexCoordP3ui(texture, type, coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP3uiv"), CLSCompliant(false)]
	public static void MultiTexCoordP3(TextureUnit texture, TexCoordPointerType type, int* coords) => glMultiTexCoordP3uiv(texture, type, coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP3uiv")]
	public static void MultiTexCoordP3(TextureUnit texture, TexCoordPointerType type, ReadOnlySpan<int> coords)
	{
		fixed (int* ptr = &coords.GetPinnableReference())
		{
			glMultiTexCoordP3uiv(texture, type, ptr);
		}
	}
		
	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP4ui")]
	public static void MultiTexCoordP4(TextureUnit texture, TexCoordPointerType type, int coords) => glMultiTexCoordP4ui(texture, type, coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP4uiv"), CLSCompliant(false)]
	public static void MultiTexCoordP4(TextureUnit texture, TexCoordPointerType type, int* coords) => glMultiTexCoordP4uiv(texture, type, coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP4uiv"), CLSCompliant(false)]
	public static void MultiTexCoordP4(TextureUnit texture, TexCoordPointerType type, ReadOnlySpan<int> coords)
	{
		fixed (int* ptr = &coords.GetPinnableReference())
		{
			glMultiTexCoordP4uiv(texture, type, ptr);
		}
	}

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP1ui")]
	public static void MultiTexCoordP1(TextureUnit texture, TexCoordPointerType type, BitVector32 coords) => glMultiTexCoordP1ui(texture, type, Unsafe.As<BitVector32, int>(ref coords));

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP1uiv"), CLSCompliant(false)]
	public static void MultiTexCoordP1(TextureUnit texture, TexCoordPointerType type, BitVector32* coords) => glMultiTexCoordP1uiv(texture, type, (int*) coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP1uiv")]
	public static void MultiTexCoordP1(TextureUnit texture, TexCoordPointerType type, ReadOnlySpan<BitVector32> coords)
	{
		fixed (BitVector32* ptr = &coords.GetPinnableReference())
		{
			glMultiTexCoordP1uiv(texture, type, (int*) ptr);
		}
	}

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP2ui")]
	public static void MultiTexCoordP2(TextureUnit texture, TexCoordPointerType type, BitVector32 coords) => glMultiTexCoordP2ui(texture, type, Unsafe.As<BitVector32, int>(ref coords));

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP2uiv"), CLSCompliant(false)]
	public static void MultiTexCoordP2(TextureUnit texture, TexCoordPointerType type, BitVector32* coords) => glMultiTexCoordP2uiv(texture, type, (int*) coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP2uiv")]
	public static void MultiTexCoordP2(TextureUnit texture, TexCoordPointerType type, ReadOnlySpan<BitVector32> coords)
	{
		fixed (BitVector32* ptr = &coords.GetPinnableReference())
		{
			glMultiTexCoordP2uiv(texture, type, (int*) ptr);
		}
	}
		
	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP3ui")]
	public static void MultiTexCoordP3(TextureUnit texture, TexCoordPointerType type, BitVector32 coords) => glMultiTexCoordP3ui(texture, type, Unsafe.As<BitVector32, int>(ref coords));

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP3uiv"), CLSCompliant(false)]
	public static void MultiTexCoordP3(TextureUnit texture, TexCoordPointerType type, BitVector32* coords) => glMultiTexCoordP3uiv(texture, type, (int*) coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP3uiv")]
	public static void MultiTexCoordP3(TextureUnit texture, TexCoordPointerType type, ReadOnlySpan<BitVector32> coords)
	{
		fixed (BitVector32* ptr = &coords.GetPinnableReference())
		{
			glMultiTexCoordP3uiv(texture, type, (int*) ptr);
		}
	}
	
	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP4ui")]
	public static void MultiTexCoordP4(TextureUnit texture, TexCoordPointerType type, BitVector32 coords) => glMultiTexCoordP4ui(texture, type, Unsafe.As<BitVector32, int>(ref coords));

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP4uiv"), CLSCompliant(false)]
	public static void MultiTexCoordP4(TextureUnit texture, TexCoordPointerType type, BitVector32* coords) => glMultiTexCoordP4uiv(texture, type, (int*) coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP4uiv"), CLSCompliant(false)]
	public static void MultiTexCoordP4(TextureUnit texture, TexCoordPointerType type, ReadOnlySpan<BitVector32> coords)
	{
		fixed (BitVector32* ptr = &coords.GetPinnableReference())
		{
			glMultiTexCoordP4uiv(texture, type, (int*) ptr);
		}
	}

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP1ui"), CLSCompliant(false)]
	public static void MultiTexCoordP1(TextureUnit texture, TexCoordPointerType type, uint coords) => glMultiTexCoordP1ui(texture, type, Unsafe.As<uint, int>(ref coords));

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP1uiv"), CLSCompliant(false)]
	public static void MultiTexCoordP1(TextureUnit texture, TexCoordPointerType type, uint* coords) => glMultiTexCoordP1uiv(texture, type, (int*) coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP1uiv"), CLSCompliant(false)]
	public static void MultiTexCoordP1(TextureUnit texture, TexCoordPointerType type, ReadOnlySpan<uint> coords)
	{
		fixed (uint* ptr = &coords.GetPinnableReference())
		{
			glMultiTexCoordP1uiv(texture, type, (int*) ptr);
		}
	}

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP2ui"), CLSCompliant(false)]
	public static void MultiTexCoordP2(TextureUnit texture, TexCoordPointerType type, uint coords) => glMultiTexCoordP2ui(texture, type, Unsafe.As<uint, int>(ref coords));

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP2uiv"), CLSCompliant(false)]
	public static void MultiTexCoordP2(TextureUnit texture, TexCoordPointerType type, uint* coords) => glMultiTexCoordP2uiv(texture, type, (int*) coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP2uiv"), CLSCompliant(false)]
	public static void MultiTexCoordP2(TextureUnit texture, TexCoordPointerType type, ReadOnlySpan<uint> coords)
	{
		fixed (uint* ptr = &coords.GetPinnableReference())
		{
			glMultiTexCoordP2uiv(texture, type, (int*) ptr);
		}
	}
	
	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP3ui"), CLSCompliant(false)]
	public static void MultiTexCoordP3(TextureUnit texture, TexCoordPointerType type, uint coords) => glMultiTexCoordP3ui(texture, type, Unsafe.As<uint, int>(ref coords));

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP3uiv"), CLSCompliant(false)]
	public static void MultiTexCoordP3(TextureUnit texture, TexCoordPointerType type, uint* coords) => glMultiTexCoordP3uiv(texture, type, (int*) coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP3uiv"), CLSCompliant(false)]
	public static void MultiTexCoordP3(TextureUnit texture, TexCoordPointerType type, ReadOnlySpan<uint> coords)
	{
		fixed (uint* ptr = &coords.GetPinnableReference())
		{
			glMultiTexCoordP3uiv(texture, type, (int*) ptr);
		}
	}
		
	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP4ui"), CLSCompliant(false)]
	public static void MultiTexCoordP4(TextureUnit texture, TexCoordPointerType type, uint coords) => glMultiTexCoordP4ui(texture, type, Unsafe.As<uint, int>(ref coords));

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP4uiv"), CLSCompliant(false)]
	public static void MultiTexCoordP4(TextureUnit texture, TexCoordPointerType type, uint* coords) => glMultiTexCoordP4uiv(texture, type, (int*) coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glMultiTexCoordP4uiv"), CLSCompliant(false)]
	public static void MultiTexCoordP4(TextureUnit texture, TexCoordPointerType type, ReadOnlySpan<uint> coords)
	{
		fixed (uint* ptr = &coords.GetPinnableReference())
		{
			glMultiTexCoordP4uiv(texture, type, (int*) ptr);
		}
	}
	
	/// <summary>
	/// Set the current normal vector.
	/// </summary>
	[NativeMethod("glNormalP3ui")]
	public static void NormalP3(NormalPointerType type, int coords) => glNormalP3ui(type, coords);

	/// <summary>
	/// Set the current normal vector.
	/// </summary>
	[NativeMethod("glNormalP3uiv"), CLSCompliant(false)]
	public static void NormalP3(NormalPointerType type, int* coords) => glNormalP3uiv(type, coords);

	/// <summary>
	/// Set the current normal vector.
	/// </summary>
	[NativeMethod("glNormalP3uiv")]
	public static void NormalP3(NormalPointerType type, ReadOnlySpan<int> coords)
	{
		fixed (int* ptr = &coords.GetPinnableReference())
		{
			glNormalP3uiv(type, ptr);
		}
	}
		
	/// <summary>
	/// Set the current normal vector.
	/// </summary>
	[NativeMethod("glNormalP3ui"), CLSCompliant(false)]
	public static void NormalP3(NormalPointerType type, uint coords) => glNormalP3ui(type, Unsafe.As<uint, int>(ref coords));

	/// <summary>
	/// Set the current normal vector.
	/// </summary>
	[NativeMethod("glNormalP3uiv"), CLSCompliant(false)]
	public static void NormalP3(NormalPointerType type, uint* coords) => glNormalP3uiv(type, (int*) coords);

	/// <summary>
	/// Set the current normal vector.
	/// </summary>
	[NativeMethod("glNormalP3uiv"), CLSCompliant(false)]
	public static void NormalP3(NormalPointerType type, ReadOnlySpan<uint> coords)
	{
		fixed (uint* ptr = &coords.GetPinnableReference())
		{
			glNormalP3uiv(type, (int*) ptr);
		}
	}
		
	/// <summary>
	/// Set the current normal vector.
	/// </summary>
	[NativeMethod("glNormalP3ui")]
	public static void NormalP3(NormalPointerType type, BitVector32 coords) => glNormalP3ui(type, Unsafe.As<BitVector32, int>(ref coords));

	/// <summary>
	/// Set the current normal vector.
	/// </summary>
	[NativeMethod("glNormalP3uiv"), CLSCompliant(false)]
	public static void NormalP3(NormalPointerType type, BitVector32* coords) => glNormalP3uiv(type, (int*) coords);

	/// <summary>
	/// Set the current normal vector.
	/// </summary>
	[NativeMethod("glNormalP3uiv")]
	public static void NormalP3(NormalPointerType type, ReadOnlySpan<BitVector32> coords)
	{
		fixed (BitVector32* ptr = &coords.GetPinnableReference())
		{
			glNormalP3uiv(type, (int*) ptr);
		}
	}
		
	/// <summary>
	/// Label a named object identified within a namespace.
	/// </summary>
	[NativeMethod("glObjectLabel")]
	public static void ObjectLabel(ObjectIdentifier identifier, int name, string label)
	{
		var utf8 = new UTF8String(label);
		fixed (byte* ptr = &utf8.GetPinnableReference())
		{
			glObjectLabel(identifier, name, utf8.Length, ptr);
		}
	}

	/// <summary>
	/// Label a a sync object identified by a pointer.
	/// </summary>
	[NativeMethod("glObjectPtrLabel")]
	public static void ObjectPtrLabel(IntPtr ptr, string label)
	{
		var utf8 = new UTF8String(label);
		fixed (byte* p = &utf8.GetPinnableReference())
		{
			glObjectPtrLabel(ptr.ToPointer(), utf8.Length, p);	
		}
	}
		
	/// <summary>
	/// Label a a sync object identified by a pointer.
	/// </summary>
	[NativeMethod("glObjectPtrLabel"), CLSCompliant(false)]
	public static void ObjectPtrLabel(void* ptr, string label)
	{
		var utf8 = new UTF8String(label);
		fixed (byte* p = &utf8.GetPinnableReference())
		{
			glObjectPtrLabel(ptr, utf8.Length, p);	
		}
	}

	/// <summary>
	/// Set pixel storage modes.
	/// </summary>
	[NativeMethod("glPixelStoref")]
	public static void PixelStore(PixelStoreParameter paramName, float param) => glPixelStoref(paramName, param);

	/// <summary>
	/// Set pixel storage modes.
	/// </summary>
	[NativeMethod("glPixelStorei")]
	public static void PixelStore(PixelStoreParameter paramName, int param) => glPixelStorei(paramName, param);

	/// <summary>
	/// Specify point parameters.
	/// </summary>
	[NativeMethod("glPointParameterf")]
	public static void PointParameter(PointParameterName paramName, float param) => glPointParameterf(paramName, param);

	/// <summary>
	/// Specify point parameters.
	/// </summary>
	[NativeMethod("glPointParameterfv"), CLSCompliant(false)]
	public static void PointParameter(PointParameterName paramName, float* parameters) => glPointParameterfv(paramName, parameters);

	/// <summary>
	/// Specify point parameters.
	/// </summary>
	[NativeMethod("glPointParameterfv")]
	public static void PointParameter(PointParameterName paramName, ReadOnlySpan<float> parameters)
	{
		fixed (float* ptr = &parameters.GetPinnableReference())
		{
			glPointParameterfv(paramName, ptr);
		}
	}

	/// <summary>
	/// Specify point parameters.
	/// </summary>
	[NativeMethod("glPointParameteri")]
	public static void PointParameter(PointParameterName paramName, int param) => glPointParameteri(paramName, param);

	/// <summary>
	/// Specify point parameters.
	/// </summary>
	[NativeMethod("glPointParameteriv"), CLSCompliant(false)]
	public static void PointParameter(PointParameterName paramName, int* parameters) => glPointParameteriv(paramName, parameters);

	/// <summary>
	/// Specify point parameters.
	/// </summary>
	[NativeMethod("glPointParameteriv")]
	public static void PointParameter(PointParameterName paramName, ReadOnlySpan<int> parameters)
	{
		fixed (int* ptr = &parameters.GetPinnableReference())
		{
			glPointParameteriv(paramName, ptr);
		}
	}
		
	/// <summary>
	/// Specify the diameter of rasterized points.
	/// </summary>
	[NativeMethod("glPointSize")]
	public static void PointSize(float size) => glPointSize(size);

	/// <summary>
	/// Select a polygon rasterization mode.
	/// </summary>
	[NativeMethod("glPolygonMode")]
	public static void PolygonMode(MaterialFace face, PolygonMode mode) => glPolygonMode(face, mode);

	/// <summary>
	/// Set the scale and units used to calculate depth values.
	/// </summary>
	[NativeMethod("glPolygonOffset")]
	public static void PolygonOffset(float factor, float units) => glPolygonOffset(factor, units);

	/// <summary>
	/// Record the GL time into a query object after all previous commands have reached the GL server but have not yet necessarily executed.
	/// </summary>
	[NativeMethod("glQueryCounter")]
	public static void QueryCounter(Query id, QueryCounterTarget target) => glQueryCounter(id, target);

	/// <summary>
	/// Select a color buffer source for pixels.
	/// </summary>
	[NativeMethod("glReadBuffer")]
	public static void ReadBuffer(ReadBufferMode src) => glReadBuffer(src);

	/// <summary>
	/// Read a block of pixels from the frame buffer.
	/// </summary>
	[NativeMethod("glReadPixels"), CLSCompliant(false)]
	public static void ReadPixels(int x, int y, int width, int height, PixelFormat format, PixelType type, void* pixels) => glReadPixels(x, y, width, height, format, type, pixels);

	/// <summary>
	/// Read a block of pixels from the frame buffer.
	/// </summary>
	[NativeMethod("glReadPixels"), CLSCompliant(false)]
	public static void ReadPixels(Rectangle rect, PixelFormat format, PixelType type, void* pixels) => glReadPixels(rect.X, rect.Y, rect.Width, rect.Height, format, type, pixels);

	/// <summary>
	/// Read a block of pixels from the frame buffer.
	/// </summary>
	[NativeMethod("glReadPixels"), CLSCompliant(false)]
	public static void ReadPixels(Point location, Size size, PixelFormat format, PixelType type, void* pixels) => glReadPixels(location.X, location.Y, size.Width, size.Height, format, type, pixels);
		
	/// <summary>
	/// Read a block of pixels from the frame buffer.
	/// </summary>
	[NativeMethod("glReadPixels")]
	public static void ReadPixels(int x, int y, int width, int height, PixelFormat format, PixelType type, IntPtr pixels) => glReadPixels(x, y, width, height, format, type, pixels.ToPointer());

	/// <summary>
	/// Read a block of pixels from the frame buffer.
	/// </summary>
	[NativeMethod("glReadPixels")]
	public static void ReadPixels(Rectangle rect, PixelFormat format, PixelType type, IntPtr pixels) => glReadPixels(rect.X, rect.Y, rect.Width, rect.Height, format, type, pixels.ToPointer());

	/// <summary>
	/// Read a block of pixels from the frame buffer.
	/// </summary>
	[NativeMethod("glReadPixels")]
	public static void ReadPixels(Point location, Size size, PixelFormat format, PixelType type, IntPtr pixels) => glReadPixels(location.X, location.Y, size.Width, size.Height, format, type, pixels.ToPointer());
		
	/// <summary>
	/// Read a block of pixels from the frame buffer.
	/// </summary>
	[NativeMethod("glReadPixels")]
	public static void ReadPixels<T>(int x, int y, int width, int height, PixelFormat format, PixelType type, Span<T> pixels) where T : unmanaged
	{
		fixed (T* ptr = &pixels.GetPinnableReference())
		{
			glReadPixels(x, y, width, height, format, type, ptr);
		}
	}

	/// <summary>
	/// Read a block of pixels from the frame buffer.
	/// </summary>
	[NativeMethod("glReadPixels")]
	public static void ReadPixels<T>(Rectangle rect, PixelFormat format, PixelType type, Span<T> pixels) where T : unmanaged
	{
		fixed (T* ptr = &pixels.GetPinnableReference())
		{
			glReadPixels(rect.X, rect.Y, rect.Width, rect.Height, format, type, ptr);
		}
	}

	/// <summary>
	/// Read a block of pixels from the frame buffer.
	/// </summary>
	[NativeMethod("glReadPixels")]
	public static void ReadPixels<T>(Point location, Size size, PixelFormat format, PixelType type, Span<T> pixels) where T : unmanaged
	{
		fixed (T* ptr = &pixels.GetPinnableReference())
		{
			glReadPixels(location.X, location.Y, size.Width, size.Height, format, type, ptr);
		}
	}

	/// <summary>
	/// Establish data storage, format and dimensions of a renderbuffer object's image.
	/// </summary>
	[NativeMethod("glRenderbufferStorage")]
	public static void RenderbufferStorage(RenderbufferTarget target, InternalFormat internalFormat, int width, int height) => glRenderbufferStorage(target, internalFormat, width, height);

	/// <summary>
	/// Establish data storage, format, dimensions and sample count of a renderbuffer object's image.
	/// </summary>
	[NativeMethod("glRenderbufferStorageMultisample")]
	public static void RenderbufferStorageMultisample(RenderbufferTarget target, int samples, InternalFormat internalFormat, int width, int height) => glRenderbufferStorageMultisample(target, samples, internalFormat, width, height);

	/// <summary>
	/// Establish data storage, format and dimensions of a renderbuffer object's image.
	/// </summary>
	[NativeMethod("glRenderbufferStorage")]
	public static void RenderbufferStorage(RenderbufferTarget target, InternalFormat internalFormat, Size size) => glRenderbufferStorage(target, internalFormat, size.Width, size.Height);

	/// <summary>
	/// Establish data storage, format, dimensions and sample count of a renderbuffer object's image.
	/// </summary>
	[NativeMethod("glRenderbufferStorageMultisample")]
	public static void RenderbufferStorageMultisample(RenderbufferTarget target, int samples, InternalFormat internalFormat, Size size) => glRenderbufferStorageMultisample(target, samples, internalFormat, size.Width, size.Height);

	/// <summary>
	/// Specify multisample coverage parameters.
	/// </summary>
	[NativeMethod("glSampleCoverage")]
	public static void SampleCoverage(float value, bool invert) => glSampleCoverage(value, invert);

	/// <summary>
	/// Set the value of a sub-word of the sample mask.
	/// </summary>
	[NativeMethod("glSampleMaski")]
	public static void SampleMask(int maskNumber, int mask) => glSampleMaski(maskNumber, mask);
		
	/// <summary>
	/// Set the value of a sub-word of the sample mask.
	/// </summary>
	[NativeMethod("glSampleMaski"), CLSCompliant(false)]
	public static void SampleMask(int maskNumber, uint mask) => glSampleMaski(maskNumber, Unsafe.As<uint, int>(ref mask));
		
	/// <summary>
	/// Set the value of a sub-word of the sample mask.
	/// </summary>
	[NativeMethod("glSampleMaski")]
	public static void SampleMask(int maskNumber, BitVector32 mask) => glSampleMaski(maskNumber, mask.Data);
		
	/// <summary>
	/// Set sampler parameters.
	/// </summary>
	[NativeMethod("glSamplerParameterIiv"), CLSCompliant(false)]
	public static void SamplerParameterI(Sampler sampler, SamplerParameterI paramName, int* param) => glSamplerParameterIiv(sampler, paramName, param);

	/// <summary>
	/// Set sampler parameters.
	/// </summary>
	[NativeMethod("glSamplerParameterIuiv"), CLSCompliant(false)]
	public static void SamplerParameterI(Sampler sampler, SamplerParameterI paramName, uint* param) => glSamplerParameterIuiv(sampler, paramName, param);

	/// <summary>
	/// Set sampler parameters.
	/// </summary>
	[NativeMethod("glSamplerParameterIiv")]
	public static void SamplerParameterI(Sampler sampler, SamplerParameterI paramName, int param) => glSamplerParameterIiv(sampler, paramName, &param);

	/// <summary>
	/// Set sampler parameters.
	/// </summary>
	[NativeMethod("glSamplerParameterIuiv"), CLSCompliant(false)]
	public static void SamplerParameterI(Sampler sampler, SamplerParameterI paramName, uint param) => glSamplerParameterIuiv(sampler, paramName, &param);
		
	/// <summary>
	/// Set sampler parameters.
	/// </summary>
	[NativeMethod("glSamplerParameterIiv")]
	public static void SamplerParameterI(Sampler sampler, SamplerParameterI paramName, ReadOnlySpan<int> param)
	{
		fixed (int* ptr = &param.GetPinnableReference())
		{
			glSamplerParameterIiv(sampler, paramName, ptr);
		}
	}

	/// <summary>
	/// Set sampler parameters.
	/// </summary>
	[NativeMethod("glSamplerParameterIuiv"), CLSCompliant(false)]
	public static void SamplerParameterI(Sampler sampler, SamplerParameterI paramName, ReadOnlySpan<uint> param)
	{
		fixed (uint* ptr = &param.GetPinnableReference())
		{
			glSamplerParameterIuiv(sampler, paramName, ptr);
		}
	}
		
	/// <summary>
	/// Set sampler parameters.
	/// </summary>
	[NativeMethod("glSamplerParameterf")]
	public static void SamplerParameter(Sampler sampler, SamplerParameterF paramName, float param) => glSamplerParameterf(sampler, paramName, param);

	/// <summary>
	/// Set sampler parameters.
	/// </summary>
	[NativeMethod("glSamplerParameterfv"), CLSCompliant(false)]
	public static void SamplerParameter(Sampler sampler, SamplerParameterF paramName, float* param) => glSamplerParameterfv(sampler, paramName, param);

	/// <summary>
	/// Set sampler parameters.
	/// </summary>
	[NativeMethod("glSamplerParameterfv")]
	public static void SamplerParameter(Sampler sampler, SamplerParameterF paramName, ReadOnlySpan<float> param)
	{
		fixed (float* ptr = &param.GetPinnableReference())
		{
			glSamplerParameterfv(sampler, paramName, ptr);
		}
	}

	/// <summary>
	/// Set sampler parameters.
	/// </summary>
	[NativeMethod("glSamplerParameteri")]
	public static void SamplerParameter(Sampler sampler, SamplerParameterI paramName, int param) => glSamplerParameteri(sampler, paramName, param);

	/// <summary>
	/// Set sampler parameters.
	/// </summary>
	[NativeMethod("glSamplerParameteriv"), CLSCompliant(false)]
	public static void SamplerParameter(Sampler sampler, SamplerParameterI paramName, int* param) => glSamplerParameteriv(sampler, paramName, param);

	/// <summary>
	/// Set sampler parameters.
	/// </summary>
	[NativeMethod("glSamplerParameteriv")]
	public static void SamplerParameter(Sampler sampler, SamplerParameterI paramName, ReadOnlySpan<int> param)
	{
		fixed (int* ptr = &param.GetPinnableReference())
		{
			glSamplerParameteriv(sampler, paramName, ptr);
		}
	}
		
	/// <summary>
	/// Define the scissor box.
	/// </summary>
	[NativeMethod("glScissor")]
	public static void Scissor(int x, int y, int width, int height) => glScissor(x, y, width, height);
		
	/// <summary>
	/// Define the scissor box.
	/// </summary>
	[NativeMethod("glScissor")]
	public static void Scissor(Rectangle rect) => glScissor(rect.X, rect.Y, rect.Width, rect.Height);
		
	/// <summary>
	/// Define the scissor box.
	/// </summary>
	[NativeMethod("glScissor")]
	public static void Scissor(Point location, Size size) => glScissor(location.X, location.Y, size.Width, size.Height);
		
	/// <summary>
	/// Set the current secondary color.
	/// </summary>
	[NativeMethod("glSecondaryColorP3ui")]
	public static void SecondaryColorP3(ColorPointerType type, int color) => glSecondaryColorP3ui(type, color);

	/// <summary>
	/// Set the current secondary color.
	/// </summary>
	[NativeMethod("glSecondaryColorP3uiv"), CLSCompliant(false)]
	public static void SecondaryColorP3(ColorPointerType type, int* color) => glSecondaryColorP3uiv(type, color);

	/// <summary>
	/// Set the current secondary color.
	/// </summary>
	[NativeMethod("glSecondaryColorP3uiv")]
	public static void SecondaryColorP3(ColorPointerType type, ReadOnlySpan<int> color)
	{
		fixed (int* ptr = &color.GetPinnableReference())
		{
			glSecondaryColorP3uiv(type, ptr);
		}
	}

	/// <summary>
	/// Set the current secondary color.
	/// </summary>
	[NativeMethod("glSecondaryColorP3ui")]
	public static void SecondaryColorP3(ColorPointerType type, BitVector32 color) => glSecondaryColorP3ui(type, Unsafe.As<BitVector32, int>(ref color));

	/// <summary>
	/// Set the current secondary color.
	/// </summary>
	[NativeMethod("glSecondaryColorP3uiv"), CLSCompliant(false)]
	public static void SecondaryColorP3(ColorPointerType type, BitVector32* color) => glSecondaryColorP3uiv(type, (int*) color);

	/// <summary>
	/// Set the current secondary color.
	/// </summary>
	[NativeMethod("glSecondaryColorP3uiv")]
	public static void SecondaryColorP3(ColorPointerType type, ReadOnlySpan<BitVector32> color)
	{
		fixed (BitVector32* ptr = &color.GetPinnableReference())
		{
			glSecondaryColorP3uiv(type, (int*) ptr);
		}
	}
		
	/// <summary>
	/// Set the current secondary color.
	/// </summary>
	[NativeMethod("glSecondaryColorP3ui"), CLSCompliant(false)]
	public static void SecondaryColorP3(ColorPointerType type, uint color) => glSecondaryColorP3ui(type, Unsafe.As<uint, int>(ref color));

	/// <summary>
	/// Set the current secondary color.
	/// </summary>
	[NativeMethod("glSecondaryColorP3uiv"), CLSCompliant(false)]
	public static void SecondaryColorP3(ColorPointerType type, uint* color) => glSecondaryColorP3uiv(type, (int*) color);

	/// <summary>
	/// Set the current secondary color.
	/// </summary>
	[NativeMethod("glSecondaryColorP3uiv"), CLSCompliant(false)]
	public static void SecondaryColorP3(ColorPointerType type, ReadOnlySpan<uint> color)
	{
		fixed (uint* ptr = &color.GetPinnableReference())
		{
			glSecondaryColorP3uiv(type, (int*) ptr);
		}
	}
		
	/// <summary>
	/// Replaces the source code in a shader object.
	/// </summary>
	[NativeMethod("glShaderSource"), CLSCompliant(false)]
	public static void ShaderSource(Shader shader, int count, byte** str, int* length) => glShaderSource(shader, count, str, length);
		
	/// <summary>
	/// Replaces the source code in a shader object.
	/// </summary>
	[NativeMethod("glShaderSource")]
	public static void ShaderSource(Shader shader, string source)
	{
		var str = new UTF8String(source);
		fixed (byte* ptr = &str.GetPinnableReference())
		{
			var len = str.Length;
			glShaderSource(shader, 1, &ptr, &len);
		}
	}
		
	/// <summary>
	/// Replaces the source code in a shader object.
	/// </summary>
	[NativeMethod("glShaderSource")]
	public static void ShaderSource(Shader shader, params string[] source)
	{
		var strings = stackalloc IntPtr[source.Length];
		var lengths = stackalloc int[source.Length];

		try
		{
			for (var i = 0; i < source.Length; i++)
			{
				var utf8 = Encoding.UTF8.GetBytes(source[i]);
				strings[i] = Marshal.AllocHGlobal(utf8.Length);
				lengths[i] = utf8.Length;
				Marshal.Copy(utf8, 0, strings[i], utf8.Length);
			}
			glShaderSource(shader, 1, (byte**) strings, lengths);
		}
		finally
		{
			for (var i = 0; i < source.Length; i++)
			{
				if (strings[i] != IntPtr.Zero)
					Marshal.FreeHGlobal(strings[i]);
			}
		}
	}
		
	/// <summary>
	/// Set front and back function and reference value for stencil testing.
	/// </summary>
	[NativeMethod("glStencilFunc"), CLSCompliant(false)]
	public static void StencilFunc(StencilFunction func, int reference, uint mask) => glStencilFunc(func, reference, mask);
		
	/// <summary>
	/// Set front and back function and reference value for stencil testing.
	/// </summary>
	[NativeMethod("glStencilFunc")]
	public static void StencilFunc(StencilFunction func, int reference, int mask) => glStencilFunc(func, reference, Unsafe.As<int, uint>(ref mask));

	/// <summary>
	/// Set front and back function and reference value for stencil testing.
	/// </summary>
	[NativeMethod("glStencilFunc")]
	public static void StencilFunc(StencilFunction func, int reference, BitVector32 mask) => glStencilFunc(func, reference, Unsafe.As<BitVector32, uint>(ref mask));
		
	/// <summary>
	/// Set front and/or back function and reference value for stencil testing.
	/// </summary>
	[NativeMethod("glStencilFuncSeparate"), CLSCompliant(false)]
	public static void StencilFuncSeparate(StencilFaceDirection face, StencilFunction func, int reference, uint mask) => glStencilFuncSeparate(face, func, reference, mask);

	/// <summary>
	/// Set front and/or back function and reference value for stencil testing.
	/// </summary>
	[NativeMethod("glStencilFuncSeparate")]
	public static void StencilFuncSeparate(StencilFaceDirection face, StencilFunction func, int reference, int mask)
	{
		glStencilFuncSeparate(face, func, reference, Unsafe.As<int, uint>(ref mask));
	}

	/// <summary>
	/// Set front and/or back function and reference value for stencil testing.
	/// </summary>
	[NativeMethod("glStencilFuncSeparate")]
	public static void StencilFuncSeparate(StencilFaceDirection face, StencilFunction func, int reference, BitVector32 mask)
	{
		glStencilFuncSeparate(face, func, reference, Unsafe.As<BitVector32, uint>(ref mask));
	}
		
	/// <summary>
	/// Control the front and back writing of individual bits in the stencil planes.
	/// </summary>
	[NativeMethod("glStencilMask"), CLSCompliant(false)]
	public static void StencilMask(uint mask) => glStencilMask(mask);
		
	/// <summary>
	/// Control the front and back writing of individual bits in the stencil planes.
	/// </summary>
	[NativeMethod("glStencilMask")]
	public static void StencilMask(int mask) => glStencilMask(Unsafe.As<int, uint>(ref mask));
		
	/// <summary>
	/// Control the front and back writing of individual bits in the stencil planes.
	/// </summary>
	[NativeMethod("glStencilMask")]
	public static void StencilMask(BitVector32 mask) => glStencilMask(Unsafe.As<BitVector32, uint>(ref mask));

	/// <summary>
	/// Control the front and/or back writing of individual bits in the stencil planes.
	/// </summary>
	[NativeMethod("glStencilMaskSeparate"), CLSCompliant(false)]
	public static void StencilMaskSeparate(StencilFaceDirection face, uint mask) => glStencilMaskSeparate(face, mask);

	/// <summary>
	/// Control the front and/or back writing of individual bits in the stencil planes.
	/// </summary>
	[NativeMethod("glStencilMaskSeparate")]
	public static void StencilMaskSeparate(StencilFaceDirection face, int mask) => glStencilMaskSeparate(face, Unsafe.As<int, uint>(ref mask));
		
	/// <summary>
	/// Control the front and/or back writing of individual bits in the stencil planes.
	/// </summary>
	[NativeMethod("glStencilMaskSeparate")]
	public static void StencilMaskSeparate(StencilFaceDirection face, BitVector32 mask) => glStencilMaskSeparate(face, Unsafe.As<BitVector32, uint>(ref mask));
		
	/// <summary>
	/// Set front and back stencil test actions.
	/// </summary>
	[NativeMethod("glStencilOp")]
	public static void StencilOp(StencilOp fail, StencilOp zFail, StencilOp zPass) => glStencilOp(fail, zFail, zPass);

	/// <summary>
	/// Set front and/or back stencil test actions.
	/// </summary>
	[NativeMethod("glStencilOpSeparate")]
	public static void StencilOpSeparate(StencilFaceDirection face, StencilOp sFail, StencilOp dpFail, StencilOp dpPass) => glStencilOpSeparate(face, sFail, dpFail, dpPass);

	/// <summary>
	/// Attach the storage for a buffer object to the active buffer texture.
	/// </summary>
	[NativeMethod("glTexBuffer")]
	public static void TexBuffer(TextureTarget target, SizedInternalFormat internalFormat, Buffer buffer) => glTexBuffer(target, internalFormat, buffer);
		
	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP1ui")]
	public static void TexCoordP1(TexCoordPointerType type, int coords) => glTexCoordP1ui(type, coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP1uiv"), CLSCompliant(false)]
	public static void TexCoordP1(TexCoordPointerType type, int* coords) => glTexCoordP1uiv(type, coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP1uiv")]
	public static void TexCoordP1(TexCoordPointerType type, ReadOnlySpan<int> coords)
	{
		fixed (int* ptr = &coords.GetPinnableReference())
		{
			glTexCoordP1uiv(type, ptr);
		}
	}

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP2ui")]
	public static void TexCoordP2(TexCoordPointerType type, int coords) => glTexCoordP2ui(type, coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP2uiv"), CLSCompliant(false)]
	public static void TexCoordP2(TexCoordPointerType type, int* coords) => glTexCoordP2uiv(type, coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP2uiv")]
	public static void TexCoordP2(TexCoordPointerType type, ReadOnlySpan<int> coords)
	{
		fixed (int* ptr = &coords.GetPinnableReference())
		{
			glTexCoordP2uiv(type, ptr);
		}
	}
		
	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP3ui")]
	public static void TexCoordP3(TexCoordPointerType type, int coords) => glTexCoordP3ui(type, coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP3uiv"), CLSCompliant(false)]
	public static void TexCoordP3(TexCoordPointerType type, int* coords) => glTexCoordP3uiv(type, coords);
		
	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP3uiv")]
	public static void TexCoordP3(TexCoordPointerType type, ReadOnlySpan<int> coords)
	{
		fixed (int* ptr = &coords.GetPinnableReference())
		{
			glTexCoordP3uiv(type, ptr);
		}
	}

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP4ui")]
	public static void TexCoordP4(TexCoordPointerType type, int coords) => glTexCoordP4ui(type, coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP4uiv"), CLSCompliant(false)]
	public static void TexCoordP4(TexCoordPointerType type, int* coords) => glTexCoordP4uiv(type, coords);
		
	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP4uiv")]
	public static void TexCoordP4(TexCoordPointerType type, ReadOnlySpan<int> coords)
	{
		fixed (int* ptr = &coords.GetPinnableReference())
		{
			glTexCoordP4uiv(type, ptr);
		}
	}
		
	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP1ui")]
	public static void TexCoordP1(TexCoordPointerType type, BitVector32 coords) => glTexCoordP1ui(type, Unsafe.As<BitVector32, int>(ref coords));

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP1uiv"), CLSCompliant(false)]
	public static void TexCoordP1(TexCoordPointerType type, BitVector32* coords) => glTexCoordP1uiv(type, (int*) coords);

	/// <summary>
    /// Set the current texture coordinates.
    /// </summary>
	[NativeMethod("glTexCoordP1uiv")]
	public static void TexCoordP1(TexCoordPointerType type, ReadOnlySpan<BitVector32> coords)
	{
		fixed (BitVector32* ptr = &coords.GetPinnableReference())
		{
			glTexCoordP1uiv(type, (int*) ptr);
		}
	}

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP2ui")]
	public static void TexCoordP2(TexCoordPointerType type, BitVector32 coords) => glTexCoordP2ui(type, Unsafe.As<BitVector32, int>(ref coords));

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP2uiv"), CLSCompliant(false)]
	public static void TexCoordP2(TexCoordPointerType type, BitVector32* coords) => glTexCoordP2uiv(type, (int*) coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP2uiv")]
	public static void TexCoordP2(TexCoordPointerType type, ReadOnlySpan<BitVector32> coords)
	{
		fixed (BitVector32* ptr = &coords.GetPinnableReference())
		{
			glTexCoordP2uiv(type, (int*) ptr);
		}
	}
		
	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP3ui")]
	public static void TexCoordP3(TexCoordPointerType type, BitVector32 coords) => glTexCoordP3ui(type, Unsafe.As<BitVector32, int>(ref coords));

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP3uiv"), CLSCompliant(false)]
	public static void TexCoordP3(TexCoordPointerType type, BitVector32* coords) => glTexCoordP3uiv(type, (int*) coords);
		
	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP3uiv")]
	public static void TexCoordP3(TexCoordPointerType type, ReadOnlySpan<BitVector32> coords)
	{
		fixed (BitVector32* ptr = &coords.GetPinnableReference())
		{
			glTexCoordP3uiv(type, (int*) ptr);
		}
	}

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP4ui")]
	public static void TexCoordP4(TexCoordPointerType type, BitVector32 coords) => glTexCoordP4ui(type, Unsafe.As<BitVector32, int>(ref coords));

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP4uiv"), CLSCompliant(false)]
	public static void TexCoordP4(TexCoordPointerType type, BitVector32* coords) => glTexCoordP4uiv(type, (int*) coords);
		
	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP4uiv")]
	public static void TexCoordP4(TexCoordPointerType type, ReadOnlySpan<BitVector32> coords)
	{
		fixed (BitVector32* ptr = &coords.GetPinnableReference())
		{
			glTexCoordP4uiv(type, (int*) ptr);
		}
	}
		
	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP1ui"), CLSCompliant(false)]
	public static void TexCoordP1(TexCoordPointerType type, uint coords) => glTexCoordP1ui(type, Unsafe.As<uint, int>(ref coords));

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP1uiv"), CLSCompliant(false)]
	public static void TexCoordP1(TexCoordPointerType type, uint* coords) => glTexCoordP1uiv(type, (int*) coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP1uiv"), CLSCompliant(false)]
	public static void TexCoordP1(TexCoordPointerType type, ReadOnlySpan<uint> coords)
	{
		fixed (uint* ptr = &coords.GetPinnableReference())
		{
			glTexCoordP1uiv(type, (int*) ptr);
		}
	}

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP2ui"), CLSCompliant(false)]
	public static void TexCoordP2(TexCoordPointerType type, uint coords) => glTexCoordP2ui(type, Unsafe.As<uint, int>(ref coords));

	/// <summary>
    /// Set the current texture coordinates.
    /// </summary>
	[NativeMethod("glTexCoordP2uiv"), CLSCompliant(false)]
	public static void TexCoordP2(TexCoordPointerType type, uint* coords) => glTexCoordP2uiv(type, (int*) coords);

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP2uiv"), CLSCompliant(false)]
	public static void TexCoordP2(TexCoordPointerType type, ReadOnlySpan<uint> coords)
	{
		fixed (uint* ptr = &coords.GetPinnableReference())
		{
			glTexCoordP2uiv(type, (int*) ptr);
		}
	}
		
	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP3ui"), CLSCompliant(false)]
	public static void TexCoordP3(TexCoordPointerType type, uint coords) => glTexCoordP3ui(type, Unsafe.As<uint, int>(ref coords));

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP3uiv"), CLSCompliant(false)]
	public static void TexCoordP3(TexCoordPointerType type, uint* coords) => glTexCoordP3uiv(type, (int*) coords);
		
	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP3uiv"), CLSCompliant(false)]
	public static void TexCoordP3(TexCoordPointerType type, ReadOnlySpan<uint> coords)
	{
		fixed (uint* ptr = &coords.GetPinnableReference())
		{
			glTexCoordP3uiv(type, (int*) ptr);
		}
	}

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP4ui"), CLSCompliant(false)]
	public static void TexCoordP4(TexCoordPointerType type, uint coords) => glTexCoordP4ui(type, Unsafe.As<uint, int>(ref coords));

	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP4uiv"), CLSCompliant(false)]
	public static void TexCoordP4(TexCoordPointerType type, uint* coords) => glTexCoordP4uiv(type, (int*) coords);
		
	/// <summary>
	/// Set the current texture coordinates.
	/// </summary>
	[NativeMethod("glTexCoordP4uiv"), CLSCompliant(false)]
	public static void TexCoordP4(TexCoordPointerType type, ReadOnlySpan<uint> coords)
	{
		fixed (uint* ptr = &coords.GetPinnableReference())
		{
			glTexCoordP4uiv(type, (int*) ptr);
		}
	}
		
	/// <summary>
	/// Specify a one-dimensional texture image.
	/// </summary>
	[NativeMethod("glTexImage1D"), CLSCompliant(false)]
	public static void TexImage1D(TextureTarget target, int level, InternalFormat internalFormat, int width, int border, PixelFormat format, PixelType type, void* pixels) => glTexImage1D(target, level, internalFormat, width, border, format, type, pixels);

	/// <summary>
	/// Specify a two-dimensional texture image.
	/// </summary>
	[NativeMethod("glTexImage2D"), CLSCompliant(false)]
	public static void TexImage2D(TextureTarget target, int level, InternalFormat internalFormat, int width, int height, int border, PixelFormat format, PixelType type, void* pixels) => glTexImage2D(target, level, internalFormat, width, height, border, format, type, pixels);

	/// <summary>
	/// Specify a three-dimensional texture image.
	/// </summary>
	[NativeMethod("glTexImage3D"), CLSCompliant(false)]
	public static void TexImage3D(TextureTarget target, int level, InternalFormat internalFormat, int width, int height, int depth, int border, PixelFormat format, PixelType type, void* pixels) => glTexImage3D(target, level, internalFormat, width, height, depth, border, format, type, pixels);

	/// <summary>
	/// Specify a one-dimensional texture image.
	/// </summary>
	[NativeMethod("glTexImage1D")]
	public static void TexImage1D(TextureTarget target, int level, InternalFormat internalFormat, int width, int border, PixelFormat format, PixelType type, IntPtr pixels) => glTexImage1D(target, level, internalFormat, width, border, format, type, pixels.ToPointer());

	/// <summary>
	/// Specify a two-dimensional texture image.
	/// </summary>
	[NativeMethod("glTexImage2D")]
	public static void TexImage2D(TextureTarget target, int level, InternalFormat internalFormat, int width, int height, int border, PixelFormat format, PixelType type, IntPtr pixels) => glTexImage2D(target, level, internalFormat, width, height, border, format, type, pixels.ToPointer());

	/// <summary>
	/// Specify a three-dimensional texture image.
	/// </summary>
	[NativeMethod("glTexImage3D")]
	public static void TexImage3D(TextureTarget target, int level, InternalFormat internalFormat, int width, int height, int depth, int border, PixelFormat format, PixelType type, IntPtr pixels) => glTexImage3D(target, level, internalFormat, width, height, depth, border, format, type, pixels.ToPointer());
		
	/// <summary>
	/// Specify a one-dimensional texture image.
	/// </summary>
	[NativeMethod("glTexImage1D")]
	public static void TexImage1D<T>(TextureTarget target, int level, InternalFormat internalFormat, int width, int border, PixelFormat format, PixelType type, ReadOnlySpan<T> pixels) where T : unmanaged
	{
		fixed (T* ptr = &pixels.GetPinnableReference())
		{
			glTexImage1D(target, level, internalFormat, width, border, format, type, ptr);
		}
	}
		
	/// <summary>
	/// Specify a two-dimensional texture image.
	/// </summary>
	[NativeMethod("glTexImage2D")]
	public static void TexImage2D<T>(TextureTarget target, int level, InternalFormat internalFormat, int width, int height, int border, PixelFormat format, PixelType type, ReadOnlySpan<T> pixels) where T : unmanaged
	{
		fixed (T* ptr = &pixels.GetPinnableReference())
		{
			glTexImage2D(target, level, internalFormat, width, height, border, format, type, ptr);
		}
	}
		
	/// <summary>
	/// Specify a three-dimensional texture image.
	/// </summary>
	[NativeMethod("glTexImage3D")]
	public static void TexImage3D<T>(TextureTarget target, int level, InternalFormat internalFormat, int width, int height, int depth, int border, PixelFormat format, PixelType type, ReadOnlySpan<T> pixels) where T : unmanaged
	{
		fixed (T* ptr = &pixels.GetPinnableReference())
		{
			glTexImage3D(target, level, internalFormat, width, height, depth, border, format, type, ptr);
		}
	}
		
	/// <summary>
	/// Establish the data storage, format, dimensions, and number of samples of a multisample texture's image.
	/// </summary>
	[NativeMethod("glTexImage2DMultisample")]
	public static void TexImage2DMultisample(TextureTarget target, int samples, InternalFormat internalFormat, int width, int height, bool fixedSampleLocations) => glTexImage2DMultisample(target, samples, internalFormat, width, height, fixedSampleLocations);
		
	/// <summary>
	/// Establish the data storage, format, dimensions, and number of samples of a multisample texture's image.
	/// </summary>
	[NativeMethod("glTexImage3DMultisample")]
	public static void TexImage3DMultisample(TextureTarget target, int samples, InternalFormat internalFormat, int width, int height, int depth, bool fixedSampleLocations) => glTexImage3DMultisample(target, samples, internalFormat, width, height, depth, fixedSampleLocations);

	/// <summary>
	/// Set texture parameters.
	/// </summary>
	[NativeMethod("glTexParameterIiv"), CLSCompliant(false)]
	public static void TexParameterI(TextureTarget target, TextureParameter paramName, int* parameters) => glTexParameterIiv(target, paramName, parameters);

	/// <summary>
	/// Set texture parameters.
	/// </summary>
	[NativeMethod("glTexParameterIuiv"), CLSCompliant(false)]
	public static void TexParameterI(TextureTarget target, TextureParameter paramName, uint* parameters) => glTexParameterIuiv(target, paramName, parameters);

	/// <summary>
	/// Set texture parameters.
	/// </summary>
	[NativeMethod("glTexParameterIiv")]
	public static void TexParameterI(TextureTarget target, TextureParameter paramName, Span<int> parameters)
	{
		fixed (int* ptr = &parameters.GetPinnableReference())
		{
			glTexParameterIiv(target, paramName, ptr);
		}
	}

	/// <summary>
	/// Set texture parameters.
	/// </summary>
	[NativeMethod("glTexParameterIuiv"), CLSCompliant(false)]
	public static void TexParameterI(TextureTarget target, TextureParameter paramName, Span<uint> parameters)
	{
		fixed (uint* ptr = &parameters.GetPinnableReference())
		{
			glTexParameterIuiv(target, paramName, ptr);
		}
	}

	/// <summary>
	/// Set texture parameters.
	/// </summary>
	[NativeMethod("glTexParameterIiv")]
	public static void TexParameterI(TextureTarget target, TextureParameter paramName, int parameter) => glTexParameterIiv(target, paramName, &parameter);

	/// <summary>
	/// Set texture parameters.
	/// </summary>
	[NativeMethod("glTexParameterIuiv"), CLSCompliant(false)]
	public static void TexParameterI(TextureTarget target, TextureParameter paramName, uint parameter) => glTexParameterIuiv(target, paramName, &parameter);
	
	/// <summary>
	/// Set texture parameters.
	/// </summary>
	[NativeMethod("glTexParameterf")]
	public static void TexParameter(TextureTarget target, TextureParameter paramName, float param) => glTexParameterf(target, paramName, param);

	/// <summary>
	/// Set texture parameters.
	/// </summary>
	[NativeMethod("glTexParameterfv"), CLSCompliant(false)]
	public static void TexParameter(TextureTarget target, TextureParameter paramName, float* parameters) => glTexParameterfv(target, paramName, parameters);

	/// <summary>
	/// Set texture parameters.
	/// </summary>
	[NativeMethod("glTexParameterfv")]
	public static void TexParameter(TextureTarget target, TextureParameter paramName, ReadOnlySpan<float> parameters)
	{
		fixed (float* ptr = &parameters.GetPinnableReference())
		{
			glTexParameterfv(target, paramName, ptr);
		}
	}
		
	/// <summary>
	/// Set texture parameters.
	/// </summary>
	[NativeMethod("glTexParameteri")]
	public static void TexParameter(TextureTarget target, TextureParameter paramName, int param) => glTexParameteri(target, paramName, param);

	/// <summary>
	/// Set texture parameters.
	/// </summary>
	[NativeMethod("glTexParameteri")]
	public static void TexParameter<TEnum>(TextureTarget target, TextureParameter paramName, TEnum param) where TEnum : Enum
	{
		if (Marshal.SizeOf(Enum.GetUnderlyingType(typeof(TEnum))) != sizeof(int))
			throw new InvalidCastException("Underlying integer type of enum must be 32-bits.");
		glTexParameteri(target, paramName, Unsafe.As<TEnum, int>(ref param));
	}
		
	/// <summary>
	/// Set texture parameters.
	/// </summary>
	[NativeMethod("glTexParameteriv"), CLSCompliant(false)]
	public static void TexParameter(TextureTarget target, TextureParameter paramName, int* parameters) => glTexParameteriv(target, paramName, parameters);
		
	/// <summary>
	/// Set texture parameters.
	/// </summary>
	[NativeMethod("glTexParameteriv")]
	public static void TexParameter(TextureTarget target, TextureParameter paramName, ReadOnlySpan<int> parameters)
	{
		fixed (int* ptr = &parameters.GetPinnableReference())
		{
			glTexParameteriv(target, paramName, ptr);
		}
	}
	
	/// <summary>
	/// Specify a one-dimensional texture subimage.
	/// </summary>
	[NativeMethod("glTexSubImage1D"), CLSCompliant(false)]
	public static void TexSubImage1D(TextureTarget target, int level, int xOffset, int width, PixelFormat format, PixelType type, void* pixels) => glTexSubImage1D(target, level, xOffset, width, format, type, pixels);

	/// <summary>
	/// Specify a two-dimensional texture subimage.
	/// </summary>
	[NativeMethod("glTexSubImage2D"), CLSCompliant(false)]
	public static void TexSubImage2D(TextureTarget target, int level, int xOffset, int yOffset, int width, int height, PixelFormat format, PixelType type, void* pixels) => glTexSubImage2D(target, level, xOffset, yOffset, width, height, format, type, pixels);

	/// <summary>
	/// Specify a three-dimensional texture subimage.
	/// </summary>
	[NativeMethod("glTexSubImage3D"), CLSCompliant(false)]
	public static void TexSubImage3D(TextureTarget target, int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, PixelFormat format, PixelType type, void* pixels) => glTexSubImage3D(target, level, xOffset, yOffset, zOffset, width, height, depth, format, type, pixels);
		
	/// <summary>
	/// Specify a one-dimensional texture subimage.
	/// </summary>
	[NativeMethod("glTexSubImage1D")]
	public static void TexSubImage1D(TextureTarget target, int level, int xOffset, int width, PixelFormat format, PixelType type, IntPtr pixels) => glTexSubImage1D(target, level, xOffset, width, format, type, pixels.ToPointer());

	/// <summary>
	/// Specify a two-dimensional texture subimage.
	/// </summary>
	[NativeMethod("glTexSubImage2D")]
	public static void TexSubImage2D(TextureTarget target, int level, int xOffset, int yOffset, int width, int height, PixelFormat format, PixelType type, IntPtr pixels) => glTexSubImage2D(target, level, xOffset, yOffset, width, height, format, type, pixels.ToPointer());

	/// <summary>
	/// Specify a three-dimensional texture subimage.
	/// </summary>
	[NativeMethod("glTexSubImage3D")]
	public static void TexSubImage3D(TextureTarget target, int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, PixelFormat format, PixelType type, IntPtr pixels) => glTexSubImage3D(target, level, xOffset, yOffset, zOffset, width, height, depth, format, type, pixels.ToPointer());
	
	/// <summary>
	/// Specify a one-dimensional texture subimage.
	/// </summary>
	[NativeMethod("glTexSubImage1D")]
	public static void TexSubImage1D<T>(TextureTarget target, int level, int xOffset, int width, PixelFormat format, PixelType type, ReadOnlySpan<T> pixels) where T : unmanaged
	{
		fixed (T* ptr = &pixels.GetPinnableReference())
		{
			glTexSubImage1D(target, level, xOffset, width, format, type, ptr);	
		}
	}

	/// <summary>
	/// Specify a two-dimensional texture subimage.
	/// </summary>
	[NativeMethod("glTexSubImage2D")]
	public static void TexSubImage2D<T>(TextureTarget target, int level, int xOffset, int yOffset, int width, int height, PixelFormat format, PixelType type, ReadOnlySpan<T> pixels) where T : unmanaged
	{
		fixed (T* ptr = &pixels.GetPinnableReference())
		{
			glTexSubImage2D(target, level, xOffset, yOffset, width, height, format, type, ptr);
		}
	}

	/// <summary>
	/// Specify a three-dimensional texture subimage.
	/// </summary>
	[NativeMethod("glTexSubImage3D")]
	public static void TexSubImage3D<T>(TextureTarget target, int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, PixelFormat format, PixelType type, ReadOnlySpan<T> pixels) where T : unmanaged
	{
		fixed (T* ptr = &pixels.GetPinnableReference())
		{
			glTexSubImage3D(target, level, xOffset, yOffset, zOffset, width, height, depth, format, type, ptr);	
		}
	}
		
	/// <summary>
	/// Specify values to record in transform feedback buffers.
	/// </summary>
	[NativeMethod("glTransformFeedbackVaryings"), CLSCompliant(false)]
	public static void TransformFeedbackVaryings(Program program, int count, byte** varyings, TransformFeedbackBufferMode bufferMode) => glTransformFeedbackVaryings(program, count, varyings, bufferMode);
		
	/// <summary>
	/// Specify values to record in transform feedback buffers.
	/// </summary>
	[NativeMethod("glTransformFeedbackVaryings")]
	public static void TransformFeedbackVaryings(Program program, TransformFeedbackBufferMode bufferMode, params string[] varyings)
	{
		var strings = stackalloc IntPtr[varyings.Length];
		try
		{
			for (var i = 0; i < varyings.Length; i++)
			{
				var utf8 = Encoding.UTF8.GetBytes(varyings[i]);
				strings[i] = Marshal.AllocHGlobal(utf8.Length + 1);
				Marshal.Copy(utf8, 0, strings[i], utf8.Length);
			}
			glTransformFeedbackVaryings(program, varyings.Length, (byte**) strings, bufferMode);
		}
		finally
		{
			for (var i = 0; i < varyings.Length; i++)
			{
				if (strings[i] != IntPtr.Zero)
					Marshal.FreeHGlobal(strings[i]);
			}
		}
	}

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform1f")]
	public static void Uniform1(int location, float v0) => glUniform1f(location, v0);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform1fv"), CLSCompliant(false)]
	public static void Uniform1(int location, int count, float* value) => glUniform1fv(location, count, value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform1fv")]
	public static void Uniform1(int location, ReadOnlySpan<float> value)
	{
		fixed (float* ptr = &value.GetPinnableReference())
		{
			glUniform1fv(location, value.Length, ptr);
		}
	}

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform1i")]
	public static void Uniform1(int location, int v0) => glUniform1i(location, v0);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform1iv"), CLSCompliant(false)]
	public static void Uniform1(int location, int count, int* value) => glUniform1iv(location, count, value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform1iv")]
	public static void Uniform1(int location, ReadOnlySpan<int> value)
	{
		fixed (int* ptr = &value.GetPinnableReference())
		{
			glUniform1iv(location, value.Length, ptr);	
		}
	}
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform1ui"), CLSCompliant(false)]
	public static void Uniform1(int location, uint v0) => glUniform1ui(location, v0);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform1uiv"), CLSCompliant(false)]
	public static void Uniform1(int location, int count, uint* value) => glUniform1uiv(location, count, value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform1uiv"), CLSCompliant(false)]
	public static void Uniform1(int location, ReadOnlySpan<uint> value)
	{
		fixed (uint* ptr = &value.GetPinnableReference())
		{
			glUniform1uiv(location, value.Length, ptr);	
		}
	}
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform2f")]
	public static void Uniform2(int location, float v0, float v1) => glUniform2f(location, v0, v1);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform2fv"), CLSCompliant(false)]
	public static void Uniform2(int location, int count, float* value) => glUniform2fv(location, count, value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform2fv")]
	public static void Uniform2(int location, ReadOnlySpan<float> value)
	{
		fixed (float* ptr = &value.GetPinnableReference())
		{
			glUniform2fv(location, value.Length / 2, ptr);
		}
	}
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform2fv")]
	public static void Uniform2(int location, ReadOnlySpan<Vector2> value)
	{
		fixed (Vector2* ptr = &value.GetPinnableReference())
		{
			glUniform2fv(location, value.Length, (float*) ptr);
		}
	}
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform2fv")]
	public static void Uniform2(int location, Vector2 value) => glUniform2fv(location, 1, &value.X);
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform2fv"), CLSCompliant(false)]
	public static void Uniform2(int location, int count, Vector2* value) => glUniform2fv(location, count, (float*) value);
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform2i")]
	public static void Uniform2(int location, int v0, int v1) => glUniform2i(location, v0, v1);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform2iv"), CLSCompliant(false)]
	public static void Uniform2(int location, int count, int* value) => glUniform2iv(location, count, value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform2iv")]
	public static void Uniform2(int location, ReadOnlySpan<int> value)
	{
		fixed (int* ptr = &value.GetPinnableReference())
		{
			glUniform2iv(location, value.Length / 2, ptr);	
		}
	}
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform2ui"), CLSCompliant(false)]
	public static void Uniform2(int location, uint v0, uint v1) => glUniform2ui(location, v0, v1);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform2uiv"), CLSCompliant(false)]
	public static void Uniform2(int location, int count, uint* value) => glUniform2uiv(location, count, value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform2uiv"), CLSCompliant(false)]
	public static void Uniform2(int location, ReadOnlySpan<uint> value)
	{
		fixed (uint* ptr = &value.GetPinnableReference())
		{
			glUniform2uiv(location, value.Length / 2, ptr);	
		}
	}

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform3f")]
	public static void Uniform3(int location, float v0, float v1, float v2) => glUniform3f(location, v0, v1, v2);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform3fv"), CLSCompliant(false)]
	public static void Uniform3(int location, int count, float* value) => glUniform3fv(location, count, value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform3fv")]
	public static void Uniform3(int location, ReadOnlySpan<float> value)
	{
		fixed (float* ptr = &value.GetPinnableReference())
		{
			glUniform3fv(location, value.Length / 3, ptr);
		}
	}
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform3fv")]
	public static void Uniform3(int location, ReadOnlySpan<Vector3> value)
	{
		fixed (Vector3* ptr = &value.GetPinnableReference())
		{
			glUniform3fv(location, value.Length, (float*) ptr);
		}
	}
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform3fv")]
	public static void Uniform3(int location, Vector3 value) => glUniform3fv(location, 1, &value.X);
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform3fv"), CLSCompliant(false)]
	public static void Uniform3(int location, int count, Vector3* value) => glUniform3fv(location, count, (float*) value);
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform3i")]
	public static void Uniform3(int location, int v0, int v1, int v2) => glUniform3i(location, v0, v1, v2);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform3iv"), CLSCompliant(false)]
	public static void Uniform3(int location, int count, int* value) => glUniform3iv(location, count, value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform3iv")]
	public static void Uniform3(int location, ReadOnlySpan<int> value)
	{
		fixed (int* ptr = &value.GetPinnableReference())
		{
			glUniform3iv(location, value.Length / 3, ptr);	
		}
	}
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform3ui"), CLSCompliant(false)]
	public static void Uniform3(int location, uint v0, uint v1, uint v2) => glUniform3ui(location, v0, v1, v2);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform3uiv"), CLSCompliant(false)]
	public static void Uniform3(int location, int count, uint* value) => glUniform3uiv(location, count, value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform3uiv"), CLSCompliant(false)]
	public static void Uniform3(int location, ReadOnlySpan<uint> value)
	{
		fixed (uint* ptr = &value.GetPinnableReference())
		{
			glUniform3uiv(location, value.Length / 3, ptr);	
		}
	}
	
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>	
	[NativeMethod("glUniform4f")]
	public static void Uniform4(int location, float v0, float v1, float v2, float v3) => glUniform4f(location, v0, v1, v2, v3);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform4fv"), CLSCompliant(false)]
	public static void Uniform4(int location, int count, float* value) => glUniform4fv(location, count, value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform4fv")]
	public static void Uniform4(int location, ReadOnlySpan<float> value)
	{
		fixed (float* ptr = &value.GetPinnableReference())
		{
			glUniform4fv(location, value.Length / 4, ptr);
		}
	}
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform4fv")]
	public static void Uniform4(int location, ReadOnlySpan<Vector4> value)
	{
		fixed (Vector4* ptr = &value.GetPinnableReference())
		{
			glUniform4fv(location, value.Length, (float*) ptr);
		}
	}
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform4fv")]
	public static void Uniform4(int location, Vector4 value) => glUniform4fv(location, 1, &value.X);
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform4fv"), CLSCompliant(false)]
	public static void Uniform4(int location, int count, Vector4* value) => glUniform4fv(location, count, (float*) value);
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform4fv")]
	public static void Uniform4(int location, ReadOnlySpan<ColorF> value)
	{
		fixed (ColorF* ptr = &value.GetPinnableReference())
		{
			glUniform4fv(location, value.Length, (float*) ptr);
		}
	}
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform4fv")]
	public static void Uniform4(int location, ColorF value) => glUniform4fv(location, 1, &value.R);
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform4fv"), CLSCompliant(false)]
	public static void Uniform4(int location, int count, ColorF* value) => glUniform4fv(location, count, (float*) value);
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform4i")]
	public static void Uniform4(int location, int v0, int v1, int v2, int v3) => glUniform4i(location, v0, v1, v2, v3);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform4iv"), CLSCompliant(false)]
	public static void Uniform4(int location, int count, int* value) => glUniform4iv(location, count, value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform4iv")]
	public static void Uniform4(int location, ReadOnlySpan<int> value)
	{
		fixed (int* ptr = &value.GetPinnableReference())
		{
			glUniform4iv(location, value.Length / 4, ptr);	
		}
	}
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform4ui"), CLSCompliant(false)]
	public static void Uniform4(int location, uint v0, uint v1, uint v2, uint v3) => glUniform4ui(location, v0, v1, v2, v3);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform4uiv"), CLSCompliant(false)]
	public static void Uniform4(int location, int count, uint* value) => glUniform4uiv(location, count, value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniform4uiv"), CLSCompliant(false)]
	public static void Uniform4(int location, ReadOnlySpan<uint> value)
	{
		fixed (uint* ptr = &value.GetPinnableReference())
		{
			glUniform4uiv(location, value.Length / 4, ptr);	
		}
	}

	/// <summary>
	/// Assign a binding point to an active uniform block.
	/// </summary>
	[NativeMethod("glUniformBlockBinding")]
	public static void UniformBlockBinding(Program program, int uniformBlockIndex, int uniformBlockBinding) => glUniformBlockBinding(program, uniformBlockIndex, uniformBlockBinding);
		
	// ReSharper disable InconsistentNaming
	
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix2fv"), CLSCompliant(false)]
	public static void UniformMatrix2(int location, int count, bool transpose, float* value) => glUniformMatrix2fv(location, count, transpose, value);
	
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix2x3fv"), CLSCompliant(false)]
	public static void UniformMatrix2x3(int location, int count, bool transpose, float* value) => glUniformMatrix2x3fv(location, count, transpose, value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix2x4fv"), CLSCompliant(false)]
	public static void UniformMatrix2x4(int location, int count, bool transpose, float* value) => glUniformMatrix2x4fv(location, count, transpose, value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix3fv"), CLSCompliant(false)]
	public static void UniformMatrix3(int location, int count, bool transpose, float* value) => glUniformMatrix3fv(location, count, transpose, value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix3x2fv"), CLSCompliant(false)]
	public static void UniformMatrix3x2(int location, int count, bool transpose, float* value) => glUniformMatrix3x2fv(location, count, transpose, value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix3x4fv"), CLSCompliant(false)]
	public static void UniformMatrix3x4(int location, int count, bool transpose, float* value) => glUniformMatrix3x4fv(location, count, transpose, value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix4fv"), CLSCompliant(false)]
	public static void UniformMatrix4(int location, int count, bool transpose, float* value) => glUniformMatrix4fv(location, count, transpose, value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix4x2fv"), CLSCompliant(false)]
	public static void UniformMatrix4x2(int location, int count, bool transpose, float* value) => glUniformMatrix4x2fv(location, count, transpose, value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix4x3fv"), CLSCompliant(false)]
	public static void UniformMatrix4x3(int location, int count, bool transpose, float* value) => glUniformMatrix4x3fv(location, count, transpose, value);
	
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix2fv")]
	public static void UniformMatrix2(int location, int count, bool transpose, ReadOnlySpan<float> value)
	{
		fixed (float* ptr = &value.GetPinnableReference())
		{
			glUniformMatrix2fv(location, count, transpose, ptr);
		}
	}

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix2x3fv")]
	public static void UniformMatrix2x3(int location, int count, bool transpose, ReadOnlySpan<float> value)
	{
		fixed (float* ptr = &value.GetPinnableReference())
		{
			glUniformMatrix2x3fv(location, count, transpose, ptr);
		}
	}

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix2x4fv")]
	public static void UniformMatrix2x4(int location, int count, bool transpose, ReadOnlySpan<float> value)
	{
		fixed (float* ptr = &value.GetPinnableReference())
		{
			glUniformMatrix2x4fv(location, count, transpose, ptr);
		}
	}

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix3fv")]
	public static void UniformMatrix3(int location, int count, bool transpose, ReadOnlySpan<float> value)
	{
		fixed (float* ptr = &value.GetPinnableReference())
		{
			glUniformMatrix3fv(location, count, transpose, ptr);
		}
	}

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix3x2fv")]
	public static void UniformMatrix3x2(int location, int count, bool transpose, ReadOnlySpan<float> value)
	{
		fixed (float* ptr = &value.GetPinnableReference())
		{
			glUniformMatrix3x2fv(location, count, transpose, ptr);
		}
	}
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix3x2fv")]
	public static void UniformMatrix3x2(int location, bool transpose, ReadOnlySpan<Matrix3x2> value)
	{
		fixed (Matrix3x2* ptr = &value.GetPinnableReference())
		{
			glUniformMatrix3x2fv(location, value.Length, transpose, (float*)ptr);
		}
	}
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix3x2fv")]
	public static void UniformMatrix3x2(int location, bool transpose, Matrix3x2 value) => glUniformMatrix3x2fv(location, 1, transpose, (float*) &value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix3x2fv"), CLSCompliant(false)]
	public static void UniformMatrix3x2(int location, bool transpose, Matrix3x2* value) => glUniformMatrix3x2fv(location, 1, transpose, (float*) value);
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix3x2fv"), CLSCompliant(false)]
	public static void UniformMatrix3x2(int location, int count, bool transpose, Matrix3x2* value) => glUniformMatrix3x2fv(location, count, transpose, (float*) value);
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix3x4fv")]
	public static void UniformMatrix3x4(int location, int count, bool transpose, ReadOnlySpan<float> value)
	{
		fixed (float* ptr = &value.GetPinnableReference())
		{
			glUniformMatrix3x4fv(location, count, transpose, ptr);
		}
	}
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix4fv")]
	public static void UniformMatrix4(int location, int count, bool transpose, ReadOnlySpan<float> value)
	{
		fixed (float* ptr = &value.GetPinnableReference())
		{
			glUniformMatrix4fv(location, count, transpose, ptr);
		}
	}
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix4fv")]
	public static void UniformMatrix4(int location, bool transpose, ReadOnlySpan<Matrix4x4> value)
	{
		fixed (Matrix4x4* ptr = &value.GetPinnableReference())
		{
			glUniformMatrix4fv(location, value.Length, transpose, (float*)ptr);
		}
	}
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix4fv")]
	public static void UniformMatrix4(int location, bool transpose, Matrix4x4 value) => glUniformMatrix4fv(location, 1, transpose, (float*) &value);

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix4fv"), CLSCompliant(false)]
	public static void UniformMatrix4(int location, bool transpose, Matrix4x4* value) => glUniformMatrix4fv(location, 1, transpose, (float*) value);
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix4fv"), CLSCompliant(false)]
	public static void UniformMatrix4(int location, int count, bool transpose, Matrix4x4* value) => glUniformMatrix4fv(location, count, transpose, (float*) value);
		
	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix4x2fv")]
	public static void UniformMatrix4x2(int location, int count, bool transpose, ReadOnlySpan<float> value)
	{
		fixed (float* ptr = &value.GetPinnableReference())
		{
			glUniformMatrix4x2fv(location, count, transpose, ptr);
		}
	}

	/// <summary>
	/// Specify the value of a uniform variable for the current program object.
	/// </summary>
	[NativeMethod("glUniformMatrix4x3fv")]
	public static void UniformMatrix4x3(int location, int count, bool transpose, ReadOnlySpan<float> value)
	{
		fixed (float* ptr = &value.GetPinnableReference())
		{
			glUniformMatrix4x3fv(location, count, transpose, ptr);
		}
	}
		
	// ReSharper restore InconsistentNaming
		
	/// <summary>
	/// Unmap a buffer object's data store.
	/// </summary>
	[NativeMethod("glUnmapBuffer")]
	public static bool UnmapBuffer(BufferTarget target) => glUnmapBuffer(target);

	/// <summary>
	/// Installs a program object as part of current rendering state.
	/// </summary>
	[NativeMethod("glUseProgram")]
	public static void UseProgram(Program program) => glUseProgram(program);

	/// <summary>
	/// Validates a program object.
	/// </summary>
	[NativeMethod("glValidateProgram")]
	public static void ValidateProgram(Program program) => glValidateProgram(program);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib1d")]
	public static void VertexAttrib1(int index, double x) => glVertexAttrib1d(index, x);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib1dv"), CLSCompliant(false)]
	public static void VertexAttrib1(int index, double* v) => glVertexAttrib1dv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib1dv")]
	public static void VertexAttrib1(int index, ReadOnlySpan<double> v)
	{
		fixed (double* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib1dv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib1f")]
	public static void VertexAttrib1(int index, float x) => glVertexAttrib1f(index, x);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib1fv"), CLSCompliant(false)]
	public static void VertexAttrib1(int index, float* v) => glVertexAttrib1fv(index, v);
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib1fv")]
	public static void VertexAttrib1(int index, ReadOnlySpan<float> v)
	{
		fixed (float* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib1fv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib1s")]
	public static void VertexAttrib1(int index, short x) => glVertexAttrib1s(index, x);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib1sv"), CLSCompliant(false)]
	public static void VertexAttrib1(int index, short* v) => glVertexAttrib1sv(index, v);
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib1sv")]
	public static void VertexAttrib1(int index, ReadOnlySpan<short> v)
	{
		fixed (short* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib1sv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib2d")]
	public static void VertexAttrib2(int index, double x, double y) => glVertexAttrib2d(index, x, y);

	/// <summary>
    /// Specifies the value of a generic vertex attribute.
    /// </summary>
	[NativeMethod("glVertexAttrib2dv"), CLSCompliant(false)]
	public static void VertexAttrib2(int index, double* v) => glVertexAttrib2dv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib2dv")]
	public static void VertexAttrib2(int index, ReadOnlySpan<double> v)
	{
		fixed (double* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib2dv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib2f")]
	public static void VertexAttrib2(int index, float x, float y) => glVertexAttrib2f(index, x, y);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib2fv"), CLSCompliant(false)]
	public static void VertexAttrib2(int index, float* v) => glVertexAttrib2fv(index, v);
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib2fv")]
	public static void VertexAttrib2(int index, ReadOnlySpan<float> v)
	{
		fixed (float* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib2fv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib2s")]
	public static void VertexAttrib2(int index, short x, short y) => glVertexAttrib2s(index, x, y);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib2sv"), CLSCompliant(false)]
	public static void VertexAttrib2(int index, short* v) => glVertexAttrib2sv(index, v);
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib2sv")]
	public static void VertexAttrib2(int index, ReadOnlySpan<short> v)
	{
		fixed (short* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib2sv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib3d")]
	public static void VertexAttrib3(int index, double x, double y, double z) => glVertexAttrib3d(index, x, y, z);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib3dv"), CLSCompliant(false)]
	public static void VertexAttrib3(int index, double* v) => glVertexAttrib3dv(index, v);
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib3dv")]
	public static void VertexAttrib3(int index, ReadOnlySpan<double> v)
	{
		fixed (double* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib3dv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib3f")]
	public static void VertexAttrib3(int index, float x, float y, float z) => glVertexAttrib3f(index, x, y, z);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib3fv"), CLSCompliant(false)]
	public static void VertexAttrib3(int index, float* v) => glVertexAttrib3fv(index, v);
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib3fv")]
	public static void VertexAttrib3(int index, ReadOnlySpan<float> v)
	{
		fixed (float* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib3fv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib3s")]
	public static void VertexAttrib3(int index, short x, short y, short z) => glVertexAttrib3s(index, x, y, z);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib3sv"), CLSCompliant(false)]
	public static void VertexAttrib3(int index, short* v) => glVertexAttrib3sv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib3sv")]
	public static void VertexAttrib3(int index, ReadOnlySpan<short> v)
	{
		fixed (short* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib3sv(index, ptr);
		}
	}
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4Nub")]
	public static void VertexAttrib4N(int index, byte x, byte y, byte z, byte w) => glVertexAttrib4Nub(index, x, y, z, w);
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4Nbv"), CLSCompliant(false)]
	public static void VertexAttrib4N(int index, sbyte* v) => glVertexAttrib4Nbv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4Niv"), CLSCompliant(false)]
	public static void VertexAttrib4N(int index, int* v) => glVertexAttrib4Niv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4Nsv"), CLSCompliant(false)]
	public static void VertexAttrib4N(int index, short* v) => glVertexAttrib4Nsv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4Nubv"), CLSCompliant(false)]
	public static void VertexAttrib4N(int index, byte* v) => glVertexAttrib4Nubv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4Nuiv"), CLSCompliant(false)]
	public static void VertexAttrib4N(int index, uint* v) => glVertexAttrib4Nuiv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4Nusv"), CLSCompliant(false)]
	public static void VertexAttrib4N(int index, ushort* v) => glVertexAttrib4Nusv(index, v);
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4Nbv"), CLSCompliant(false)]
	public static void VertexAttrib4N(int index, ReadOnlySpan<sbyte> v)
	{
		fixed (sbyte* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib4Nbv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4Niv")]
	public static void VertexAttrib4N(int index, ReadOnlySpan<int> v)
	{
		fixed (int* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib4Niv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4Nsv")]
	public static void VertexAttrib4N(int index, ReadOnlySpan<short> v)
	{
		fixed (short* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib4Nsv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4Nubv")]
	public static void VertexAttrib4N(int index, ReadOnlySpan<byte> v)
	{
		fixed (byte* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib4Nubv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4Nuiv"), CLSCompliant(false)]
	public static void VertexAttrib4N(int index, ReadOnlySpan<uint> v)
	{
		fixed (uint* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib4Nuiv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4Nusv"), CLSCompliant(false)]
	public static void VertexAttrib4N(int index, ReadOnlySpan<ushort> v)
	{
		fixed (ushort* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib4Nusv(index, ptr);
		}
	}
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4d")]
	public static void VertexAttrib4(int index, double x, double y, double z, double w) => glVertexAttrib4d(index, x, y, z, w);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4f")]
	public static void VertexAttrib4(int index, float x, float y, float z, float w) => glVertexAttrib4f(index, x, y, z, w);
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4s")]
	public static void VertexAttrib4(int index, short x, short y, short z, short w) => glVertexAttrib4s(index, x, y, z, w);
        
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4bv"), CLSCompliant(false)]
	public static void VertexAttrib4(int index, sbyte* v) => glVertexAttrib4bv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4dv"), CLSCompliant(false)]
	public static void VertexAttrib4(int index, double* v) => glVertexAttrib4dv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4fv"), CLSCompliant(false)]
	public static void VertexAttrib4(int index, float* v) => glVertexAttrib4fv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4iv"), CLSCompliant(false)]
	public static void VertexAttrib4(int index, int* v) => glVertexAttrib4iv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4sv"), CLSCompliant(false)]
	public static void VertexAttrib4(int index, short* v) => glVertexAttrib4sv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4ubv"), CLSCompliant(false)]
	public static void VertexAttrib4(int index, byte* v) => glVertexAttrib4ubv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4uiv"), CLSCompliant(false)]
	public static void VertexAttrib4(int index, uint* v) => glVertexAttrib4uiv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4usv"), CLSCompliant(false)]
	public static void VertexAttrib4(int index, ushort* v) => glVertexAttrib4usv(index, v);
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4bv"), CLSCompliant(false)]
	public static void VertexAttrib4(int index, ReadOnlySpan<sbyte> v)
	{
		fixed (sbyte* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib4bv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4dv")]
	public static void VertexAttrib4(int index, ReadOnlySpan<double> v)
	{
		fixed (double* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib4dv(index, ptr);
		}
	}
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4fv")]
	public static void VertexAttrib4(int index, ReadOnlySpan<float> v)
	{
		fixed (float* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib4fv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4iv")]
	public static void VertexAttrib4(int index, ReadOnlySpan<int> v)
	{
		fixed (int* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib4iv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4sv")]
	public static void VertexAttrib4(int index, ReadOnlySpan<short> v)
	{
		fixed (short* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib4sv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4ubv")]
	public static void VertexAttrib4(int index, ReadOnlySpan<byte> v)
	{
		fixed (byte* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib4ubv(index, ptr);
		}
	}

	/// <summary>
    /// Specifies the value of a generic vertex attribute.
    /// </summary>
	[NativeMethod("glVertexAttrib4uiv"), CLSCompliant(false)]
	public static void VertexAttrib4(int index, ReadOnlySpan<uint> v)
	{
		fixed (uint* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib4uiv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttrib4usv"), CLSCompliant(false)]
	public static void VertexAttrib4(int index, ReadOnlySpan<ushort> v)
	{
		fixed (ushort* ptr = &v.GetPinnableReference())
		{
			glVertexAttrib4usv(index, ptr);
		}
	}
		
	/// <summary>
	/// Modify the rate at which generic vertex attributes advance during instanced rendering.
	/// </summary>
	[NativeMethod("glVertexAttribDivisor")]
	public static void VertexAttribDivisor(int index, int divisor) => glVertexAttribDivisor(index, divisor);
	
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI1i")]
	public static void VertexAttribI1(int index, int x) => glVertexAttribI1i(index, x);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI1ui"), CLSCompliant(false)]
	public static void VertexAttribI1(int index, uint x) => glVertexAttribI1ui(index, x);
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI2i")]
	public static void VertexAttribI2(int index, int x, int y) => glVertexAttribI2i(index, x, y);
		
	/// <summary>
    /// Specifies the value of a generic vertex attribute.
    /// </summary>
	[NativeMethod("glVertexAttribI2ui"), CLSCompliant(false)]
	public static void VertexAttribI2(int index, uint x, uint y) => glVertexAttribI2ui(index, x, y);
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI3i")]
	public static void VertexAttribI3(int index, int x, int y, int z) => glVertexAttribI3i(index, x, y, z);
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI3ui"), CLSCompliant(false)]
	public static void VertexAttribI3(int index, uint x, uint y, uint z) => glVertexAttribI3ui(index, x, y, z);
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI4i")]
	public static void VertexAttribI4(int index, int x, int y, int z, int w) => glVertexAttribI4i(index, x, y, z, w);
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI4ui"), CLSCompliant(false)]
	public static void VertexAttribI4(int index, uint x, uint y, uint z, uint w) => glVertexAttribI4ui(index, x, y, z, w);
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI1iv"), CLSCompliant(false)]
	public static void VertexAttribI1(int index, int* v) => glVertexAttribI1iv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI1uiv"), CLSCompliant(false)]
	public static void VertexAttribI1(int index, uint* v) => glVertexAttribI1uiv(index, v);

	/// <summary>
    /// Specifies the value of a generic vertex attribute.
    /// </summary>
	[NativeMethod("glVertexAttribI2iv"), CLSCompliant(false)]
	public static void VertexAttribI2(int index, int* v) => glVertexAttribI2iv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI2uiv"), CLSCompliant(false)]
	public static void VertexAttribI2(int index, uint* v) => glVertexAttribI2uiv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI3iv"), CLSCompliant(false)]
	public static void VertexAttribI3(int index, int* v) => glVertexAttribI3iv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI3uiv"), CLSCompliant(false)]
	public static void VertexAttribI3(int index, uint* v) => glVertexAttribI3uiv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI4bv"), CLSCompliant(false)]
	public static void VertexAttribI4(int index, sbyte* v) => glVertexAttribI4bv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI4iv"), CLSCompliant(false)]
	public static void VertexAttribI4(int index, int* v) => glVertexAttribI4iv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI4sv"), CLSCompliant(false)]
	public static void VertexAttribI4(int index, short* v) => glVertexAttribI4sv(index, v);

	/// <summary>
    /// Specifies the value of a generic vertex attribute.
    /// </summary>
	[NativeMethod("glVertexAttribI4ubv"), CLSCompliant(false)]
	public static void VertexAttribI4(int index, byte* v) => glVertexAttribI4ubv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI4uiv"), CLSCompliant(false)]
	public static void VertexAttribI4(int index, uint* v) => glVertexAttribI4uiv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI4usv"), CLSCompliant(false)]
	public static void VertexAttribI4(int index, ushort* v) => glVertexAttribI4usv(index, v);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI1iv")]
	public static void VertexAttribI1(int index, ReadOnlySpan<int> v)
	{
		fixed (int* ptr = &v.GetPinnableReference())
		{
			glVertexAttribI1iv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI1uiv"), CLSCompliant(false)]
	public static void VertexAttribI1(int index, ReadOnlySpan<uint> v)
	{
		fixed (uint* ptr = &v.GetPinnableReference())
		{
			glVertexAttribI1uiv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI2iv")]
	public static void VertexAttribI2(int index, ReadOnlySpan<int> v)
	{
		fixed (int* ptr = &v.GetPinnableReference())
		{
			glVertexAttribI2iv(index, ptr);
		}
	}
	
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI2uiv"), CLSCompliant(false)]
	public static void VertexAttribI2(int index, ReadOnlySpan<uint> v)
	{
		fixed (uint* ptr = &v.GetPinnableReference())
		{
			glVertexAttribI2uiv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI3iv")]
	public static void VertexAttribI3(int index, ReadOnlySpan<int> v)
	{
		fixed (int* ptr = &v.GetPinnableReference())
		{
			glVertexAttribI3iv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI3uiv"), CLSCompliant(false)]
	public static void VertexAttribI3(int index, ReadOnlySpan<uint> v)
	{
		fixed (uint* ptr = &v.GetPinnableReference())
		{
			glVertexAttribI3uiv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI4bv"), CLSCompliant(false)]
	public static void VertexAttribI4(int index, ReadOnlySpan<sbyte> v)
	{
		fixed (sbyte* ptr = &v.GetPinnableReference())
		{
			glVertexAttribI4bv(index, ptr);
		}
	}
	
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI4iv")]
	public static void VertexAttribI4(int index, ReadOnlySpan<int> v)
	{
		fixed (int* ptr = &v.GetPinnableReference())
		{
			glVertexAttribI4iv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI4sv")]
	public static void VertexAttribI4(int index, ReadOnlySpan<short> v)
	{
		fixed (short* ptr = &v.GetPinnableReference())
		{
			glVertexAttribI4sv(index, ptr);
		}
	}

	/// <summary>
    /// Specifies the value of a generic vertex attribute.
    /// </summary>
	[NativeMethod("glVertexAttribI4ubv")]
	public static void VertexAttribI4(int index, ReadOnlySpan<byte> v)
	{
		fixed (byte* ptr = &v.GetPinnableReference())
		{
			glVertexAttribI4ubv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI4uiv"), CLSCompliant(false)]
	public static void VertexAttribI4(int index, ReadOnlySpan<uint> v)
	{
		fixed (uint* ptr = &v.GetPinnableReference())
		{
			glVertexAttribI4uiv(index, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribI4usv"), CLSCompliant(false)]
	public static void VertexAttribI4(int index, ReadOnlySpan<ushort> v)
	{
		fixed (ushort* ptr = &v.GetPinnableReference())
		{
			glVertexAttribI4usv(index, ptr);
		}
	}
	
	/// <summary>
	/// Define an array of generic vertex attribute data.
	/// </summary>
	[NativeMethod("glVertexAttribIPointer"), CLSCompliant(false)]
	public static void VertexAttribIPointer(int index, int size, VertexAttribIType type, int stride, nint pointer) => glVertexAttribIPointer(index, size, type, stride, pointer);

	/// <summary>
	/// Define an array of generic vertex attribute data.
	/// </summary>
	[NativeMethod("glVertexAttribPointer"), CLSCompliant(false)]
	public static void VertexAttribPointer(int index, int size, VertexAttribType type, bool normalized, int stride, nint pointer) => glVertexAttribPointer(index, size, type, normalized, stride, pointer);
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP1ui")]
	public static void VertexAttribP1(int index, VertexAttribType type, bool normalized, int value) => glVertexAttribP1ui(index, type, normalized, value);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP1uiv"), CLSCompliant(false)]
	public static void VertexAttribP1(int index, VertexAttribType type, bool normalized, int* value) => glVertexAttribP1uiv(index, type, normalized, value);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP1uiv")]
	public static void VertexAttribP1(int index, VertexAttribType type, bool normalized, ReadOnlySpan<int> value)
	{
		fixed (int* ptr = &value.GetPinnableReference())
		{
			glVertexAttribP1uiv(index, type, normalized, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP2ui")]
	public static void VertexAttribP2(int index, VertexAttribType type, bool normalized, int value) => glVertexAttribP2ui(index, type, normalized, value);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP2uiv"), CLSCompliant(false)]
	public static void VertexAttribP2(int index, VertexAttribType type, bool normalized, int* value) => glVertexAttribP2uiv(index, type, normalized, value);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP2uiv")]
	public static void VertexAttribP2(int index, VertexAttribType type, bool normalized, ReadOnlySpan<int> value)
	{
		fixed (int* ptr = &value.GetPinnableReference())
		{
			glVertexAttribP2uiv(index, type, normalized, ptr);
		}
	}
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP3ui")]
	public static void VertexAttribP3(int index, VertexAttribType type, bool normalized, int value) => glVertexAttribP3ui(index, type, normalized, value);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP3uiv"), CLSCompliant(false)]
	public static void VertexAttribP3(int index, VertexAttribType type, bool normalized, int* value) => glVertexAttribP3uiv(index, type, normalized, value);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP3uiv")]
	public static void VertexAttribP3(int index, VertexAttribType type, bool normalized, ReadOnlySpan<int> value)
	{
		fixed (int* ptr = &value.GetPinnableReference())
		{
			glVertexAttribP3uiv(index, type, normalized, ptr);
		}
	}
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP4ui")]
	public static void VertexAttribP4(int index, VertexAttribType type, bool normalized, int value) => glVertexAttribP4ui(index, type, normalized, value);

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP4uiv"), CLSCompliant(false)]
	public static void VertexAttribP4(int index, VertexAttribType type, bool normalized, int* value) => glVertexAttribP4uiv(index, type, normalized, value);
		
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP1uiv")]
	public static void VertexAttribP(int index, VertexAttribType type, bool normalized, ReadOnlySpan<int> value)
	{
		fixed (int* ptr = &value.GetPinnableReference())
		{
			glVertexAttribP4uiv(index, type, normalized, ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP1ui")]
	public static void VertexAttribP1(int index, VertexAttribType type, bool normalized, BitVector32 value)
	{
		glVertexAttribP1ui(index, type, normalized, Unsafe.As<BitVector32, int>(ref value));
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP1uiv"), CLSCompliant(false)]
	public static void VertexAttribP1(int index, VertexAttribType type, bool normalized, BitVector32* value)
	{
		glVertexAttribP1uiv(index, type, normalized, (int*) value);
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP1uiv")]
	public static void VertexAttribP1(int index, VertexAttribType type, bool normalized, ReadOnlySpan<BitVector32> value)
	{
		fixed (BitVector32* ptr = &value.GetPinnableReference())
		{
			glVertexAttribP1uiv(index, type, normalized, (int*) ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP2ui")]
	public static void VertexAttribP2(int index, VertexAttribType type, bool normalized, BitVector32 value)
	{
		glVertexAttribP2ui(index, type, normalized, Unsafe.As<BitVector32, int>(ref value));
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP2uiv"), CLSCompliant(false)]
	public static void VertexAttribP2(int index, VertexAttribType type, bool normalized, BitVector32* value)
	{
		glVertexAttribP2uiv(index, type, normalized, (int*) value);
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP2uiv")]
	public static void VertexAttribP2(int index, VertexAttribType type, bool normalized, ReadOnlySpan<BitVector32> value)
	{
		fixed (BitVector32* ptr = &value.GetPinnableReference())
		{
			glVertexAttribP2uiv(index, type, normalized, (int*) ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP3ui")]
	public static void VertexAttribP3(int index, VertexAttribType type, bool normalized, BitVector32 value)
	{
		glVertexAttribP3ui(index, type, normalized, Unsafe.As<BitVector32, int>(ref value));
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP3uiv"), CLSCompliant(false)]
	public static void VertexAttribP3(int index, VertexAttribType type, bool normalized, BitVector32* value)
	{
		glVertexAttribP3uiv(index, type, normalized, (int*) value);
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP3uiv")]
	public static void VertexAttribP3(int index, VertexAttribType type, bool normalized, ReadOnlySpan<BitVector32> value)
	{
		fixed (BitVector32* ptr = &value.GetPinnableReference())
		{
			glVertexAttribP3uiv(index, type, normalized, (int*) ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP4ui")]
	public static void VertexAttribP4(int index, VertexAttribType type, bool normalized, BitVector32 value)
	{
		glVertexAttribP4ui(index, type, normalized, Unsafe.As<BitVector32, int>(ref value));
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP4uiv"), CLSCompliant(false)]
	public static void VertexAttribP4(int index, VertexAttribType type, bool normalized, BitVector32* value)
	{
		glVertexAttribP4uiv(index, type, normalized, (int*) value);
	}
        
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP1uiv")]
	public static void VertexAttribP(int index, VertexAttribType type, bool normalized, ReadOnlySpan<BitVector32> value)
	{
		fixed (BitVector32* ptr = &value.GetPinnableReference())
		{
			glVertexAttribP4uiv(index, type, normalized, (int*) ptr);
		}
	}
        
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP1ui"), CLSCompliant(false)]
	public static void VertexAttribP1(int index, VertexAttribType type, bool normalized, uint value)
	{
		glVertexAttribP1ui(index, type, normalized, Unsafe.As<uint, int>(ref value));
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP1uiv"), CLSCompliant(false)]
	public static void VertexAttribP1(int index, VertexAttribType type, bool normalized, uint* value)
	{
		glVertexAttribP1uiv(index, type, normalized, (int*) value);
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP1uiv"), CLSCompliant(false)]
	public static void VertexAttribP1(int index, VertexAttribType type, bool normalized, ReadOnlySpan<uint> value)
	{
		fixed (uint* ptr = &value.GetPinnableReference())
		{
			glVertexAttribP1uiv(index, type, normalized, (int*) ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP2ui"), CLSCompliant(false)]
	public static void VertexAttribP2(int index, VertexAttribType type, bool normalized, uint value)
	{
		glVertexAttribP2ui(index, type, normalized, Unsafe.As<uint, int>(ref value));
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP2uiv"), CLSCompliant(false)]
	public static void VertexAttribP2(int index, VertexAttribType type, bool normalized, uint* value)
	{
		glVertexAttribP2uiv(index, type, normalized, (int*) value);
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP2uiv"), CLSCompliant(false)]
	public static void VertexAttribP2(int index, VertexAttribType type, bool normalized, ReadOnlySpan<uint> value)
	{
		fixed (uint* ptr = &value.GetPinnableReference())
		{
			glVertexAttribP2uiv(index, type, normalized, (int*) ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP3ui"), CLSCompliant(false)]
	public static void VertexAttribP3(int index, VertexAttribType type, bool normalized, uint value)
	{
		glVertexAttribP3ui(index, type, normalized, Unsafe.As<uint, int>(ref value));
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP3uiv"), CLSCompliant(false)]
	public static void VertexAttribP3(int index, VertexAttribType type, bool normalized, uint* value)
	{
		glVertexAttribP3uiv(index, type, normalized, (int*) value);
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP3uiv"), CLSCompliant(false)]
	public static void VertexAttribP3(int index, VertexAttribType type, bool normalized, ReadOnlySpan<uint> value)
	{
		fixed (uint* ptr = &value.GetPinnableReference())
		{
			glVertexAttribP3uiv(index, type, normalized, (int*) ptr);
		}
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP4ui"), CLSCompliant(false)]
	public static void VertexAttribP4(int index, VertexAttribType type, bool normalized, uint value)
	{
		glVertexAttribP4ui(index, type, normalized, Unsafe.As<uint, int>(ref value));
	}

	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP4uiv"), CLSCompliant(false)]
	public static void VertexAttribP4(int index, VertexAttribType type, bool normalized, uint* value)
	{
		glVertexAttribP4uiv(index, type, normalized, (int*) value);
	}
        
	/// <summary>
	/// Specifies the value of a generic vertex attribute.
	/// </summary>
	[NativeMethod("glVertexAttribP1uiv"), CLSCompliant(false)]
	public static void VertexAttribP(int index, VertexAttribType type, bool normalized, ReadOnlySpan<uint> value)
	{
		fixed (uint* ptr = &value.GetPinnableReference())
		{
			glVertexAttribP4uiv(index, type, normalized, (int*) ptr);
		}
	}

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP2ui")]
	public static void VertexP2(VertexPointerType type, int value) => glVertexP2ui(type, value);

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP2uiv"), CLSCompliant(false)]
	public static void VertexP2(VertexPointerType type, int* value) => glVertexP2uiv(type, value);

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP2uiv")]
	public static void VertexP2(VertexPointerType type, ReadOnlySpan<int> value)
	{
		fixed (int* ptr = &value.GetPinnableReference())
		{
			glVertexP2uiv(type, ptr);
		}
	}

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP3ui")]
	public static void VertexP3(VertexPointerType type, int value) => glVertexP3ui(type, value);

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP3uiv"), CLSCompliant(false)]
	public static void VertexP3(VertexPointerType type, int* value) => glVertexP3uiv(type, value);

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP3uiv")]
	public static void VertexP3(VertexPointerType type, ReadOnlySpan<int> value)
	{
		fixed (int* ptr = &value.GetPinnableReference())
		{
			glVertexP3uiv(type, ptr);
		}
	}

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP4ui")]
	public static void VertexP4(VertexPointerType type, int value) => glVertexP4ui(type, value);

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP4uiv"), CLSCompliant(false)]
	public static void VertexP4(VertexPointerType type, int* value) => glVertexP4uiv(type, value);

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP4uiv")]
	public static void VertexP4(VertexPointerType type, ReadOnlySpan<int> value)
	{
		fixed (int* ptr = &value.GetPinnableReference())
		{
			glVertexP4uiv(type, ptr);
		}
	}

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP2ui")]
	public static void VertexP2(VertexPointerType type, BitVector32 value) => glVertexP2ui(type, value.Data);

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP2uiv"), CLSCompliant(false)]
	public static void VertexP2(VertexPointerType type, BitVector32* value) => glVertexP2uiv(type, (int*) value);

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP2uiv")]
	public static void VertexP2(VertexPointerType type, ReadOnlySpan<BitVector32> value)
	{
		fixed (BitVector32* ptr = &value.GetPinnableReference())
		{
			glVertexP2uiv(type, (int*) ptr);
		}
	}

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP3ui")]
	public static void VertexP3(VertexPointerType type, BitVector32 value) => glVertexP3ui(type, value.Data);

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP3uiv"), CLSCompliant(false)]
	public static void VertexP3(VertexPointerType type, BitVector32* value) => glVertexP3uiv(type, (int*) value);

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP3uiv")]
	public static void VertexP3(VertexPointerType type, ReadOnlySpan<BitVector32> value)
	{
		fixed (BitVector32* ptr = &value.GetPinnableReference())
		{
			glVertexP3uiv(type, (int*) ptr);
		}
	}

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP4ui")]
	public static void VertexP4(VertexPointerType type, BitVector32 value) => glVertexP4ui(type, value.Data);

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP4uiv"), CLSCompliant(false)]
	public static void VertexP4(VertexPointerType type, BitVector32* value) => glVertexP4uiv(type, (int*) value);

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP4uiv")]
	public static void VertexP4(VertexPointerType type, ReadOnlySpan<BitVector32> value)
	{
		fixed (BitVector32* ptr = &value.GetPinnableReference())
		{
			glVertexP4uiv(type, (int*) ptr);
		}
	}

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP2ui"), CLSCompliant(false)]
	public static void VertexP2(VertexPointerType type, uint value) => glVertexP2ui(type, Unsafe.As<uint, int>(ref value));

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP2uiv"), CLSCompliant(false)]
	public static void VertexP2(VertexPointerType type, uint* value) => glVertexP2uiv(type, (int*) value);

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP2uiv"), CLSCompliant(false)]
	public static void VertexP2(VertexPointerType type, ReadOnlySpan<uint> value)
	{
		fixed (uint* ptr = &value.GetPinnableReference())
		{
			glVertexP2uiv(type, (int*) ptr);
		}
	}

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP3ui"), CLSCompliant(false)]
	public static void VertexP3(VertexPointerType type, uint value) => glVertexP3ui(type, Unsafe.As<uint, int>(ref value));

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP3uiv"), CLSCompliant(false)]
	public static void VertexP3(VertexPointerType type, uint* value) => glVertexP3uiv(type, (int*) value);

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP3uiv"), CLSCompliant(false)]
	public static void VertexP3(VertexPointerType type, ReadOnlySpan<uint> value)
	{
		fixed (uint* ptr = &value.GetPinnableReference())
		{
			glVertexP3uiv(type, (int*) ptr);
		}
	}

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP4ui"), CLSCompliant(false)]
	public static void VertexP4(VertexPointerType type, uint value) => glVertexP4ui(type, Unsafe.As<uint, int>(ref value));

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP4uiv"), CLSCompliant(false)]
	public static void VertexP4(VertexPointerType type, uint* value) => glVertexP4uiv(type, (int*) value);

	/// <summary>
	/// Specify a vertex.
	/// </summary>
	[NativeMethod("glVertexP4uiv"), CLSCompliant(false)]
	public static void VertexP4(VertexPointerType type, ReadOnlySpan<uint> value)
	{
		fixed (uint* ptr = &value.GetPinnableReference())
		{
			glVertexP4uiv(type, (int*) ptr);
		}
	}
		
	/// <summary>
	/// Set the viewport.
	/// </summary>
	[NativeMethod("glViewport")]
	public static void Viewport(int x, int y, int width, int height) => glViewport(x, y, width, height);
		
	/// <summary>
	/// Set the viewport.
	/// </summary>
	[NativeMethod("glViewport")]
	public static void Viewport(Rectangle rect) => glViewport(rect.X, rect.Y, rect.Width, rect.Height);

	/// <summary>
	/// Instruct the GL server to block until the specified sync object becomes signaled.
	/// </summary>
	[NativeMethod("glWaitSync"), CLSCompliant(false)]
	public static void WaitSync(Sync sync, SyncBehaviorFlags flags, ulong timeout) => glWaitSync(sync, flags, timeout);

	/// <summary>
	/// Instruct the GL server to block until the specified sync object becomes signaled.
	/// </summary>
	[NativeMethod("glWaitSync")]
	public static void WaitSync(Sync sync, SyncBehaviorFlags flags, long timeout) => glWaitSync(sync, flags, unchecked((ulong) timeout));
}