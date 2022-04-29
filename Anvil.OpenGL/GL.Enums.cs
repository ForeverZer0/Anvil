
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

using JetBrains.Annotations;
#pragma warning disable CS1591

namespace Anvil.OpenGL;

[Flags, PublicAPI]
public enum ClearBufferMask
{
	Depth = 0x00000100,
	Stencil = 0x00000400,
	Color = 0x00004000,
}

[Flags, PublicAPI]
public enum MapBufferAccessMask
{
	ReadBit = 0x0001,
	WriteBit = 0x0002,
	InvalidateRangeBit = 0x0004,
	InvalidateBufferBit = 0x0008,
	FlushExplicitBit = 0x0010,
	UnsynchronizedBit = 0x0020,
}

[Flags, PublicAPI]
public enum SyncObjectMask
{
	FlushCommandsBit = 0x00000001,
}
	
[PublicAPI]
public enum ClampColorMode
{
	False = 0,
	True = 1,
	FixedOnly = 0x891D,
}

[PublicAPI]
public enum ErrorCode
{
	NoError = 0,
	InvalidEnum = 0x0500,
	InvalidValue = 0x0501,
	InvalidOperation = 0x0502,
	StackOverflow = 0x0503,
	StackUnderflow = 0x0504,
	OutOfMemory = 0x0505,
	InvalidFramebufferOperation = 0x0506,
}

[PublicAPI]
public enum StencilOp
{
	Zero = 0,
	Invert = 0x150A,
	Keep = 0x1E00,
	Replace = 0x1E01,
	Incr = 0x1E02,
	Decr = 0x1E03,
	IncrWrap = 0x8507,
	DecrWrap = 0x8508,
}

[PublicAPI]
public enum BlendingFactor
{
	Zero = 0,
	One = 1,
	SrcColor = 0x0300,
	OneMinusSrcColor = 0x0301,
	SrcAlpha = 0x0302,
	OneMinusSrcAlpha = 0x0303,
	DstAlpha = 0x0304,
	OneMinusDstAlpha = 0x0305,
	DstColor = 0x0306,
	OneMinusDstColor = 0x0307,
	SrcAlphaSaturate = 0x0308,
	ConstantColor = 0x8001,
	OneMinusConstantColor = 0x8002,
	ConstantAlpha = 0x8003,
	OneMinusConstantAlpha = 0x8004,
	Src1Alpha = 0x8589,
	Src1Color = 0x88F9,
	OneMinusSrc1Color = 0x88FA,
	OneMinusSrc1Alpha = 0x88FB,
}

[PublicAPI]
public enum SyncBehaviorFlags
{
	None = 0,
}
		
[PublicAPI]
public enum DrawBufferMode
{
	None = 0,
	FrontLeft = 0x0400,
	FrontRight = 0x0401,
	BackLeft = 0x0402,
	BackRight = 0x0403,
	Front = 0x0404,
	Back = 0x0405,
	Left = 0x0406,
	Right = 0x0407,
	FrontAndBack = 0x0408,
	ColorAttachment0 = 0x8CE0,
	ColorAttachment1 = 0x8CE1,
	ColorAttachment2 = 0x8CE2,
	ColorAttachment3 = 0x8CE3,
	ColorAttachment4 = 0x8CE4,
	ColorAttachment5 = 0x8CE5,
	ColorAttachment6 = 0x8CE6,
	ColorAttachment7 = 0x8CE7,
	ColorAttachment8 = 0x8CE8,
	ColorAttachment9 = 0x8CE9,
	ColorAttachment10 = 0x8CEA,
	ColorAttachment11 = 0x8CEB,
	ColorAttachment12 = 0x8CEC,
	ColorAttachment13 = 0x8CED,
	ColorAttachment14 = 0x8CEE,
	ColorAttachment15 = 0x8CEF,
	ColorAttachment16 = 0x8CF0,
	ColorAttachment17 = 0x8CF1,
	ColorAttachment18 = 0x8CF2,
	ColorAttachment19 = 0x8CF3,
	ColorAttachment20 = 0x8CF4,
	ColorAttachment21 = 0x8CF5,
	ColorAttachment22 = 0x8CF6,
	ColorAttachment23 = 0x8CF7,
	ColorAttachment24 = 0x8CF8,
	ColorAttachment25 = 0x8CF9,
	ColorAttachment26 = 0x8CFA,
	ColorAttachment27 = 0x8CFB,
	ColorAttachment28 = 0x8CFC,
	ColorAttachment29 = 0x8CFD,
	ColorAttachment30 = 0x8CFE,
	ColorAttachment31 = 0x8CFF,
}
	
[PublicAPI]
public enum ReadBufferMode
{
	None = 0,
	FrontLeft = 0x0400,
	FrontRight = 0x0401,
	BackLeft = 0x0402,
	BackRight = 0x0403,
	Front = 0x0404,
	Back = 0x0405,
	Left = 0x0406,
	Right = 0x0407,
	ColorAttachment0 = 0x8CE0,
	ColorAttachment1 = 0x8CE1,
	ColorAttachment2 = 0x8CE2,
	ColorAttachment3 = 0x8CE3,
	ColorAttachment4 = 0x8CE4,
	ColorAttachment5 = 0x8CE5,
	ColorAttachment6 = 0x8CE6,
	ColorAttachment7 = 0x8CE7,
	ColorAttachment8 = 0x8CE8,
	ColorAttachment9 = 0x8CE9,
	ColorAttachment10 = 0x8CEA,
	ColorAttachment11 = 0x8CEB,
	ColorAttachment12 = 0x8CEC,
	ColorAttachment13 = 0x8CED,
	ColorAttachment14 = 0x8CEE,
	ColorAttachment15 = 0x8CEF,
}

[PublicAPI]
public enum ColorBuffer
{
	None = 0,
	FrontLeft = 0x0400,
	FrontRight = 0x0401,
	BackLeft = 0x0402,
	BackRight = 0x0403,
	Front = 0x0404,
	Back = 0x0405,
	Left = 0x0406,
	Right = 0x0407,
	FrontAndBack = 0x0408,
	ColorAttachment0 = 0x8CE0,
	ColorAttachment1 = 0x8CE1,
	ColorAttachment2 = 0x8CE2,
	ColorAttachment3 = 0x8CE3,
	ColorAttachment4 = 0x8CE4,
	ColorAttachment5 = 0x8CE5,
	ColorAttachment6 = 0x8CE6,
	ColorAttachment7 = 0x8CE7,
	ColorAttachment8 = 0x8CE8,
	ColorAttachment9 = 0x8CE9,
	ColorAttachment10 = 0x8CEA,
	ColorAttachment11 = 0x8CEB,
	ColorAttachment12 = 0x8CEC,
	ColorAttachment13 = 0x8CED,
	ColorAttachment14 = 0x8CEE,
	ColorAttachment15 = 0x8CEF,
	ColorAttachment16 = 0x8CF0,
	ColorAttachment17 = 0x8CF1,
	ColorAttachment18 = 0x8CF2,
	ColorAttachment19 = 0x8CF3,
	ColorAttachment20 = 0x8CF4,
	ColorAttachment21 = 0x8CF5,
	ColorAttachment22 = 0x8CF6,
	ColorAttachment23 = 0x8CF7,
	ColorAttachment24 = 0x8CF8,
	ColorAttachment25 = 0x8CF9,
	ColorAttachment26 = 0x8CFA,
	ColorAttachment27 = 0x8CFB,
	ColorAttachment28 = 0x8CFC,
	ColorAttachment29 = 0x8CFD,
	ColorAttachment30 = 0x8CFE,
	ColorAttachment31 = 0x8CFF,
}
	
[PublicAPI]
public enum PrimitiveType
{
	Points = 0x0000,
	Lines = 0x0001,
	LineLoop = 0x0002,
	LineStrip = 0x0003,
	Triangles = 0x0004,
	TriangleStrip = 0x0005,
	TriangleFan = 0x0006,
	LinesAdjacency = 0x000A,
	LineStripAdjacency = 0x000B,
	TrianglesAdjacency = 0x000C,
	TriangleStripAdjacency = 0x000D,
}

[PublicAPI]
public enum StencilFunction
{
	Never = 0x0200,
	Less = 0x0201,
	Equal = 0x0202,
	LEqual = 0x0203,
	Greater = 0x0204,
	NotEqual = 0x0205,
	GEqual = 0x0206,
	Always = 0x0207,
}
	
[PublicAPI]
public enum DepthFunction
{
	Never = 0x0200,
	Less = 0x0201,
	Equal = 0x0202,
	LEqual = 0x0203,
	Greater = 0x0204,
	NotEqual = 0x0205,
	GEqual = 0x0206,
	Always = 0x0207,
}
	
[PublicAPI]
public enum CullFaceMode
{
	Front = 0x0404,
	Back = 0x0405,
	FrontAndBack = 0x0408,
}

[PublicAPI]
public enum StencilFaceDirection
{
	Front = 0x0404,
	Back = 0x0405,
	FrontAndBack = 0x0408,
}

[PublicAPI]
public enum MaterialFace
{
	Front = 0x0404,
	Back = 0x0405,
	FrontAndBack = 0x0408,
}

[PublicAPI]
public enum FrontFaceDirection
{
	Clockwise = 0x0900,
	CounterClockwise = 0x0901,
}

[PublicAPI]
public enum GetPName
{
	PointSize = 0x0B11,
	PointSizeRange = 0x0B12,
	SmoothPointSizeRange = 0x0B12,
	PointSizeGranularity = 0x0B13,
	SmoothPointSizeGranularity = 0x0B13,
	LineSmooth = 0x0B20,
	LineWidth = 0x0B21,
	LineWidthRange = 0x0B22,
	SmoothLineWidthRange = 0x0B22,
	LineWidthGranularity = 0x0B23,
	SmoothLineWidthGranularity = 0x0B23,
	PolygonMode = 0x0B40,
	PolygonSmooth = 0x0B41,
	CullFace = 0x0B44,
	CullFaceMode = 0x0B45,
	FrontFace = 0x0B46,
	DepthRange = 0x0B70,
	DepthTest = 0x0B71,
	DepthWritemask = 0x0B72,
	DepthClearValue = 0x0B73,
	DepthFunc = 0x0B74,
	StencilTest = 0x0B90,
	StencilClearValue = 0x0B91,
	StencilFunc = 0x0B92,
	StencilValueMask = 0x0B93,
	StencilFail = 0x0B94,
	StencilPassDepthFail = 0x0B95,
	StencilPassDepthPass = 0x0B96,
	StencilRef = 0x0B97,
	StencilWritemask = 0x0B98,
	Viewport = 0x0BA2,
	Dither = 0x0BD0,
	BlendDst = 0x0BE0,
	BlendSrc = 0x0BE1,
	Blend = 0x0BE2,
	LogicOpMode = 0x0BF0,
	ColorLogicOp = 0x0BF2,
	DrawBuffer = 0x0C01,
	ReadBuffer = 0x0C02,
	ScissorBox = 0x0C10,
	ScissorTest = 0x0C11,
	ColorClearValue = 0x0C22,
	ColorWritemask = 0x0C23,
	Doublebuffer = 0x0C32,
	Stereo = 0x0C33,
	LineSmoothHint = 0x0C52,
	PolygonSmoothHint = 0x0C53,
	UnpackSwapBytes = 0x0CF0,
	UnpackLsbFirst = 0x0CF1,
	UnpackRowLength = 0x0CF2,
	UnpackSkipRows = 0x0CF3,
	UnpackSkipPixels = 0x0CF4,
	UnpackAlignment = 0x0CF5,
	PackSwapBytes = 0x0D00,
	PackLsbFirst = 0x0D01,
	PackRowLength = 0x0D02,
	PackSkipRows = 0x0D03,
	PackSkipPixels = 0x0D04,
	PackAlignment = 0x0D05,
	MaxClipDistances = 0x0D32,
	MaxTextureSize = 0x0D33,
	MaxViewportDims = 0x0D3A,
	SubpixelBits = 0x0D50,
	Texture1D = 0x0DE0,
	Texture2D = 0x0DE1,
	PolygonOffsetUnits = 0x2A00,
	PolygonOffsetPoint = 0x2A01,
	PolygonOffsetLine = 0x2A02,
	BlendColor = 0x8005,
	BlendEquationRgb = 0x8009,
	PolygonOffsetFill = 0x8037,
	PolygonOffsetFactor = 0x8038,
	TextureBinding1D = 0x8068,
	TextureBinding2D = 0x8069,
	TextureBinding3D = 0x806A,
	PackSkipImages = 0x806B,
	PackImageHeight = 0x806C,
	UnpackSkipImages = 0x806D,
	UnpackImageHeight = 0x806E,
	Max3DTextureSize = 0x8073,
	VertexArray = 0x8074,
	SampleBuffers = 0x80A8,
	Samples = 0x80A9,
	SampleCoverageValue = 0x80AA,
	SampleCoverageInvert = 0x80AB,
	BlendDstRgb = 0x80C8,
	BlendSrcRgb = 0x80C9,
	BlendDstAlpha = 0x80CA,
	BlendSrcAlpha = 0x80CB,
	MaxElementsVertices = 0x80E8,
	MaxElementsIndices = 0x80E9,
	PointFadeThresholdSize = 0x8128,
	MajorVersion = 0x821B,
	MinorVersion = 0x821C,
	NumExtensions = 0x821D,
	ContextFlags = 0x821E,
	MaxDebugGroupStackDepth = 0x826C,
	DebugGroupStackDepth = 0x826D,
	MaxLabelLength = 0x82E8,
	AliasedLineWidthRange = 0x846E,
	ActiveTexture = 0x84E0,
	MaxRenderbufferSize = 0x84E8,
	TextureCompressionHint = 0x84EF,
	TextureBindingRectangle = 0x84F6,
	MaxRectangleTextureSize = 0x84F8,
	MaxTextureLodBias = 0x84FD,
	TextureBindingCubeMap = 0x8514,
	MaxCubeMapTextureSize = 0x851C,
	VertexArrayBinding = 0x85B5,
	ProgramPointSize = 0x8642,
	NumCompressedTextureFormats = 0x86A2,
	CompressedTextureFormats = 0x86A3,
	StencilBackFunc = 0x8800,
	StencilBackFail = 0x8801,
	StencilBackPassDepthFail = 0x8802,
	StencilBackPassDepthPass = 0x8803,
	MaxDrawBuffers = 0x8824,
	BlendEquationAlpha = 0x883D,
	MaxVertexAttribs = 0x8869,
	MaxTextureImageUnits = 0x8872,
	ArrayBufferBinding = 0x8894,
	ElementArrayBufferBinding = 0x8895,
	PixelPackBufferBinding = 0x88ED,
	PixelUnpackBufferBinding = 0x88EF,
	MaxDualSourceDrawBuffers = 0x88FC,
	MaxArrayTextureLayers = 0x88FF,
	MinProgramTexelOffset = 0x8904,
	MaxProgramTexelOffset = 0x8905,
	SamplerBinding = 0x8919,
	UniformBufferBinding = 0x8A28,
	UniformBufferStart = 0x8A29,
	UniformBufferSize = 0x8A2A,
	MaxVertexUniformBlocks = 0x8A2B,
	MaxGeometryUniformBlocks = 0x8A2C,
	MaxFragmentUniformBlocks = 0x8A2D,
	MaxCombinedUniformBlocks = 0x8A2E,
	MaxUniformBufferBindings = 0x8A2F,
	MaxUniformBlockSize = 0x8A30,
	MaxCombinedVertexUniformComponents = 0x8A31,
	MaxCombinedGeometryUniformComponents = 0x8A32,
	MaxCombinedFragmentUniformComponents = 0x8A33,
	UniformBufferOffsetAlignment = 0x8A34,
	MaxFragmentUniformComponents = 0x8B49,
	MaxVertexUniformComponents = 0x8B4A,
	MaxVaryingFloats = 0x8B4B,
	MaxVaryingComponents = 0x8B4B,
	MaxVertexTextureImageUnits = 0x8B4C,
	MaxCombinedTextureImageUnits = 0x8B4D,
	FragmentShaderDerivativeHint = 0x8B8B,
	CurrentProgram = 0x8B8D,
	TextureBinding1DArray = 0x8C1C,
	TextureBinding2DArray = 0x8C1D,
	MaxGeometryTextureImageUnits = 0x8C29,
	MaxTextureBufferSize = 0x8C2B,
	TextureBindingBuffer = 0x8C2C,
	TransformFeedbackBufferStart = 0x8C84,
	TransformFeedbackBufferSize = 0x8C85,
	TransformFeedbackBufferBinding = 0x8C8F,
	StencilBackRef = 0x8CA3,
	StencilBackValueMask = 0x8CA4,
	StencilBackWritemask = 0x8CA5,
	DrawFramebufferBinding = 0x8CA6,
	RenderbufferBinding = 0x8CA7,
	ReadFramebufferBinding = 0x8CAA,
	MaxGeometryUniformComponents = 0x8DDF,
	Timestamp = 0x8E28,
	ProvokingVertex = 0x8E4F,
	MaxSampleMaskWords = 0x8E59,
	PrimitiveRestartIndex = 0x8F9E,
	TextureBinding2DMultisample = 0x9104,
	TextureBinding2DMultisampleArray = 0x9105,
	MaxColorTextureSamples = 0x910E,
	MaxDepthTextureSamples = 0x910F,
	MaxIntegerSamples = 0x9110,
	MaxServerWaitTimeout = 0x9111,
	MaxVertexOutputComponents = 0x9122,
	MaxGeometryInputComponents = 0x9123,
	MaxGeometryOutputComponents = 0x9124,
	MaxFragmentInputComponents = 0x9125,
	ContextProfileMask = 0x9126,
}

[PublicAPI]
public enum EnableCap
{
	LineSmooth = 0x0B20,
	PolygonSmooth = 0x0B41,
	CullFace = 0x0B44,
	DepthTest = 0x0B71,
	StencilTest = 0x0B90,
	Dither = 0x0BD0,
	Blend = 0x0BE2,
	ColorLogicOp = 0x0BF2,
	ScissorTest = 0x0C11,
	Texture1D = 0x0DE0,
	Texture2D = 0x0DE1,
	PolygonOffsetPoint = 0x2A01,
	PolygonOffsetLine = 0x2A02,
	ClipDistance0 = 0x3000,
	ClipDistance1 = 0x3001,
	ClipDistance2 = 0x3002,
	ClipDistance3 = 0x3003,
	ClipDistance4 = 0x3004,
	ClipDistance5 = 0x3005,
	ClipDistance6 = 0x3006,
	ClipDistance7 = 0x3007,
	PolygonOffsetFill = 0x8037,
	VertexArray = 0x8074,
	Multisample = 0x809D,
	SampleAlphaToCoverage = 0x809E,
	SampleAlphaToOne = 0x809F,
	SampleCoverage = 0x80A0,
	DebugOutputSynchronous = 0x8242,
	ProgramPointSize = 0x8642,
	DepthClamp = 0x864F,
	TextureCubeMapSeamless = 0x884F,
	RasterizerDiscard = 0x8C89,
	FramebufferSrgb = 0x8DB9,
	SampleMask = 0x8E51,
	PrimitiveRestart = 0x8F9D,
	DebugOutput = 0x92E0,
}

[PublicAPI]
public enum GetFramebufferParameter
{
	Doublebuffer = 0x0C32,
	Stereo = 0x0C33,
	SampleBuffers = 0x80A8,
	Samples = 0x80A9,
}

[PublicAPI]
public enum HintTarget
{
	LineSmooth = 0x0C52,
	PolygonSmooth = 0x0C53,
	TextureCompression = 0x84EF,
	FragmentShaderDerivative = 0x8B8B,
}

[PublicAPI]
public enum PixelStoreParameter
{
	UnpackSwapBytes = 0x0CF0,
	UnpackLsbFirst = 0x0CF1,
	UnpackRowLength = 0x0CF2,
	UnpackSkipRows = 0x0CF3,
	UnpackSkipPixels = 0x0CF4,
	UnpackAlignment = 0x0CF5,
	PackSwapBytes = 0x0D00,
	PackLsbFirst = 0x0D01,
	PackRowLength = 0x0D02,
	PackSkipRows = 0x0D03,
	PackSkipPixels = 0x0D04,
	PackAlignment = 0x0D05,
	PackSkipImages = 0x806B,
	PackImageHeight = 0x806C,
	UnpackSkipImages = 0x806D,
	UnpackImageHeight = 0x806E,
}

[PublicAPI]
public enum CopyImageSubDataTarget
{
	Texture1D = 0x0DE0,
	Texture2D = 0x0DE1,
	Texture3D = 0x806F,
	TextureRectangle = 0x84F5,
	TextureCubeMap = 0x8513,
	Texture1DArray = 0x8C18,
	Texture2DArray = 0x8C1A,
	Renderbuffer = 0x8D41,
	Texture2DMultisample = 0x9100,
	Texture2DMultisampleArray = 0x9102,
}

[PublicAPI]
public enum TextureTarget
{
	Texture1D = 0x0DE0,
	Texture2D = 0x0DE1,
	ProxyTexture1D = 0x8063,
	ProxyTexture2D = 0x8064,
	Texture3D = 0x806F,
	ProxyTexture3D = 0x8070,
	TextureRectangle = 0x84F5,
	ProxyTextureRectangle = 0x84F7,
	TextureCubeMap = 0x8513,
	TextureCubeMapPositiveX = 0x8515,
	TextureCubeMapNegativeX = 0x8516,
	TextureCubeMapPositiveY = 0x8517,
	TextureCubeMapNegativeY = 0x8518,
	TextureCubeMapPositiveZ = 0x8519,
	TextureCubeMapNegativeZ = 0x851A,
	ProxyTextureCubeMap = 0x851B,
	Texture1DArray = 0x8C18,
	ProxyTexture1DArray = 0x8C19,
	Texture2DArray = 0x8C1A,
	ProxyTexture2DArray = 0x8C1B,
	TextureBuffer = 0x8C2A,
	Renderbuffer = 0x8D41,
	Texture2DMultisample = 0x9100,
	ProxyTexture2DMultisample = 0x9101,
	Texture2DMultisampleArray = 0x9102,
	ProxyTexture2DMultisampleArray = 0x9103,
}

[PublicAPI]
public enum TextureParameter
{
	Width = 0x1000,
	Height = 0x1001,
	InternalFormat = 0x1003,
	BorderColor = 0x1004,
	MagFilter = 0x2800,
	MinFilter = 0x2801,
	WrapS = 0x2802,
	WrapT = 0x2803,
	RedSize = 0x805C,
	GreenSize = 0x805D,
	BlueSize = 0x805E,
	AlphaSize = 0x805F,
	WrapR = 0x8072,
	MinLod = 0x813A,
	MaxLod = 0x813B,
	BaseLevel = 0x813C,
	MaxLevel = 0x813D,
	LodBias = 0x8501,
	CompareMode = 0x884C,
	CompareFunc = 0x884D,
	SwizzleR = 0x8E42,
	SwizzleG = 0x8E43,
	SwizzleB = 0x8E44,
	SwizzleA = 0x8E45,
	SwizzleRgba = 0x8E46,
}

[PublicAPI]
public enum GetTextureParameter
{
	Width = 0x1000,
	Height = 0x1001,
	InternalFormat = 0x1003,
	BorderColor = 0x1004,
	MagFilter = 0x2800,
	MinFilter = 0x2801,
	WrapS = 0x2802,
	WrapT = 0x2803,
	RedSize = 0x805C,
	GreenSize = 0x805D,
	BlueSize = 0x805E,
	AlphaSize = 0x805F,
}

[PublicAPI]
public enum SamplerParameterF
{
	BorderColor = 0x1004,
	MinLod = 0x813A,
	MaxLod = 0x813B,
	LodBias = 0x8501,
}

[PublicAPI]
public enum DebugSeverity
{
	DontCare = 0x1100,
	Notification = 0x826B,
	High = 0x9146,
	Medium = 0x9147,
	Low = 0x9148,
}

[PublicAPI]
public enum HintMode
{
	DontCare = 0x1100,
	Fastest = 0x1101,
	Nicest = 0x1102,
}

[PublicAPI]
public enum DebugSource
{
	DontCare = 0x1100,
	Api = 0x8246,
	WindowSystem = 0x8247,
	ShaderCompiler = 0x8248,
	ThirdParty = 0x8249,
	Application = 0x824A,
	Other = 0x824B,
}

[PublicAPI]
public enum DebugType
{
	DontCare = 0x1100,
	Error = 0x824C,
	DeprecatedBehavior = 0x824D,
	UndefinedBehavior = 0x824E,
	Portability = 0x824F,
	Performance = 0x8250,
	Other = 0x8251,
	Marker = 0x8268,
	PushGroup = 0x8269,
	PopGroup = 0x826A,
}

[PublicAPI]
public enum VertexAttribIType
{
	Byte = 0x1400,
	UnsignedByte = 0x1401,
	Short = 0x1402,
	UnsignedShort = 0x1403,
	Int = 0x1404,
	UnsignedInt = 0x1405,
}

[PublicAPI]
public enum ColorPointerType
{
	Byte = 0x1400,
	UnsignedByte = 0x1401,
	UnsignedShort = 0x1403,
	UnsignedInt = 0x1405,
}

[PublicAPI]
public enum ListNameType
{
	Byte = 0x1400,
	UnsignedByte = 0x1401,
	Short = 0x1402,
	UnsignedShort = 0x1403,
	Int = 0x1404,
	UnsignedInt = 0x1405,
	Float = 0x1406,
}

[PublicAPI]
public enum NormalPointerType
{
	Byte = 0x1400,
	Short = 0x1402,
	Int = 0x1404,
	Float = 0x1406,
	Double = 0x140A,
}

[PublicAPI]
public enum PixelType
{
	Byte = 0x1400,
	UnsignedByte = 0x1401,
	Short = 0x1402,
	UnsignedShort = 0x1403,
	Int = 0x1404,
	UnsignedInt = 0x1405,
	Float = 0x1406,
	UnsignedByte332 = 0x8032,
	UnsignedShort4444 = 0x8033,
	UnsignedShort5551 = 0x8034,
	UnsignedInt8888 = 0x8035,
	UnsignedInt1010102 = 0x8036,
}

[PublicAPI]
public enum VertexAttribType
{
	Byte = 0x1400,
	UnsignedByte = 0x1401,
	Short = 0x1402,
	UnsignedShort = 0x1403,
	Int = 0x1404,
	UnsignedInt = 0x1405,
	Float = 0x1406,
	Double = 0x140A,
	HalfFloat = 0x140B,
	UnsignedInt2101010Rev = 0x8368,
	UnsignedInt10F11F11FRev = 0x8C3B,
	Int2101010Rev = 0x8D9F,
}

[PublicAPI]
public enum ScalarType
{
	UnsignedByte = 0x1401,
	UnsignedShort = 0x1403,
	UnsignedInt = 0x1405,
}
	
[PublicAPI]
public enum DrawElementsType
{
	UnsignedByte = 0x1401,
	UnsignedShort = 0x1403,
	UnsignedInt = 0x1405,
}

[PublicAPI]
public enum IndexPointerType
{
	Short = 0x1402,
	Int = 0x1404,
	Float = 0x1406,
	Double = 0x140A,
}

[PublicAPI]
public enum TexCoordPointerType
{
	Short = 0x1402,
	Int = 0x1404,
	Float = 0x1406,
	Double = 0x140A,
}

[PublicAPI]
public enum VertexPointerType
{
	Short = 0x1402,
	Int = 0x1404,
	Float = 0x1406,
	Double = 0x140A,
}

[PublicAPI]
public enum PixelFormat
{
	UnsignedShort = 0x1403,
	UnsignedInt = 0x1405,
	StencilIndex = 0x1901,
	DepthComponent = 0x1902,
	Red = 0x1903,
	Green = 0x1904,
	Blue = 0x1905,
	Alpha = 0x1906,
	Rgb = 0x1907,
	Rgba = 0x1908,
	Bgr = 0x80E0,
	Bgra = 0x80E1,
	Rg = 0x8227,
	RgInteger = 0x8228,
	DepthStencil = 0x84F9,
	RedInteger = 0x8D94,
	GreenInteger = 0x8D95,
	BlueInteger = 0x8D96,
	RgbInteger = 0x8D98,
	RgbaInteger = 0x8D99,
	BgrInteger = 0x8D9A,
	BgraInteger = 0x8D9B,
}

[PublicAPI]
public enum AttributeType
{
	Int = 0x1404,
	UnsignedInt = 0x1405,
	Float = 0x1406,
	Double = 0x140A,
	FloatVec2 = 0x8B50,
	FloatVec3 = 0x8B51,
	FloatVec4 = 0x8B52,
	IntVec2 = 0x8B53,
	IntVec3 = 0x8B54,
	IntVec4 = 0x8B55,
	Bool = 0x8B56,
	BoolVec2 = 0x8B57,
	BoolVec3 = 0x8B58,
	BoolVec4 = 0x8B59,
	FloatMat2 = 0x8B5A,
	FloatMat3 = 0x8B5B,
	FloatMat4 = 0x8B5C,
	Sampler1D = 0x8B5D,
	Sampler2D = 0x8B5E,
	Sampler3D = 0x8B5F,
	SamplerCube = 0x8B60,
	Sampler1DShadow = 0x8B61,
	Sampler2DShadow = 0x8B62,
	Sampler2DRect = 0x8B63,
	Sampler2DRectShadow = 0x8B64,
	FloatMat2x3 = 0x8B65,
	FloatMat2x4 = 0x8B66,
	FloatMat3x2 = 0x8B67,
	FloatMat3x4 = 0x8B68,
	FloatMat4x2 = 0x8B69,
	FloatMat4x3 = 0x8B6A,
	SamplerBuffer = 0x8DC2,
	Sampler1DArrayShadow = 0x8DC3,
	Sampler2DArrayShadow = 0x8DC4,
	SamplerCubeShadow = 0x8DC5,
	UnsignedIntVec2 = 0x8DC6,
	UnsignedIntVec3 = 0x8DC7,
	UnsignedIntVec4 = 0x8DC8,
	IntSampler1D = 0x8DC9,
	IntSampler2D = 0x8DCA,
	IntSampler3D = 0x8DCB,
	IntSamplerCube = 0x8DCC,
	IntSampler2DRect = 0x8DCD,
	IntSampler1DArray = 0x8DCE,
	IntSampler2DArray = 0x8DCF,
	IntSamplerBuffer = 0x8DD0,
	UnsignedIntSampler1D = 0x8DD1,
	UnsignedIntSampler2D = 0x8DD2,
	UnsignedIntSampler3D = 0x8DD3,
	UnsignedIntSamplerCube = 0x8DD4,
	UnsignedIntSampler2DRect = 0x8DD5,
	UnsignedIntSampler1DArray = 0x8DD6,
	UnsignedIntSampler2DArray = 0x8DD7,
	UnsignedIntSamplerBuffer = 0x8DD8,
	Sampler2DMultisample = 0x9108,
	IntSampler2DMultisample = 0x9109,
	UnsignedIntSampler2DMultisample = 0x910A,
	Sampler2DMultisampleArray = 0x910B,
	IntSampler2DMultisampleArray = 0x910C,
	UnsignedIntSampler2DMultisampleArray = 0x910D,
}

[PublicAPI]
public enum UniformType
{
	Int = 0x1404,
	UnsignedInt = 0x1405,
	Float = 0x1406,
	Double = 0x140A,
	FloatVec2 = 0x8B50,
	FloatVec3 = 0x8B51,
	FloatVec4 = 0x8B52,
	IntVec2 = 0x8B53,
	IntVec3 = 0x8B54,
	IntVec4 = 0x8B55,
	Bool = 0x8B56,
	BoolVec2 = 0x8B57,
	BoolVec3 = 0x8B58,
	BoolVec4 = 0x8B59,
	FloatMat2 = 0x8B5A,
	FloatMat3 = 0x8B5B,
	FloatMat4 = 0x8B5C,
	Sampler1D = 0x8B5D,
	Sampler2D = 0x8B5E,
	Sampler3D = 0x8B5F,
	SamplerCube = 0x8B60,
	Sampler1DShadow = 0x8B61,
	Sampler2DShadow = 0x8B62,
	Sampler2DRect = 0x8B63,
	Sampler2DRectShadow = 0x8B64,
	FloatMat2x3 = 0x8B65,
	FloatMat2x4 = 0x8B66,
	FloatMat3x2 = 0x8B67,
	FloatMat3x4 = 0x8B68,
	FloatMat4x2 = 0x8B69,
	FloatMat4x3 = 0x8B6A,
	Sampler1DArray = 0x8DC0,
	Sampler2DArray = 0x8DC1,
	SamplerBuffer = 0x8DC2,
	Sampler1DArrayShadow = 0x8DC3,
	Sampler2DArrayShadow = 0x8DC4,
	SamplerCubeShadow = 0x8DC5,
	UnsignedIntVec2 = 0x8DC6,
	UnsignedIntVec3 = 0x8DC7,
	UnsignedIntVec4 = 0x8DC8,
	IntSampler1D = 0x8DC9,
	IntSampler2D = 0x8DCA,
	IntSampler3D = 0x8DCB,
	IntSamplerCube = 0x8DCC,
	IntSampler2DRect = 0x8DCD,
	IntSampler1DArray = 0x8DCE,
	IntSampler2DArray = 0x8DCF,
	IntSamplerBuffer = 0x8DD0,
	UnsignedIntSampler1D = 0x8DD1,
	UnsignedIntSampler2D = 0x8DD2,
	UnsignedIntSampler3D = 0x8DD3,
	UnsignedIntSamplerCube = 0x8DD4,
	UnsignedIntSampler2DRect = 0x8DD5,
	UnsignedIntSampler1DArray = 0x8DD6,
	UnsignedIntSampler2DArray = 0x8DD7,
	UnsignedIntSamplerBuffer = 0x8DD8,
	Sampler2DMultisample = 0x9108,
	IntSampler2DMultisample = 0x9109,
	UnsignedIntSampler2DMultisample = 0x910A,
	Sampler2DMultisampleArray = 0x910B,
	IntSampler2DMultisampleArray = 0x910C,
	UnsignedIntSampler2DMultisampleArray = 0x910D,
}

[PublicAPI]
public enum GlslTypeToken
{
	Int = 0x1404,
	UnsignedInt = 0x1405,
	Float = 0x1406,
	Double = 0x140A,
	FloatVec2 = 0x8B50,
	FloatVec3 = 0x8B51,
	FloatVec4 = 0x8B52,
	IntVec2 = 0x8B53,
	IntVec3 = 0x8B54,
	IntVec4 = 0x8B55,
	Bool = 0x8B56,
	BoolVec2 = 0x8B57,
	BoolVec3 = 0x8B58,
	BoolVec4 = 0x8B59,
	FloatMat2 = 0x8B5A,
	FloatMat3 = 0x8B5B,
	FloatMat4 = 0x8B5C,
	Sampler1D = 0x8B5D,
	Sampler2D = 0x8B5E,
	Sampler3D = 0x8B5F,
	SamplerCube = 0x8B60,
	Sampler1DShadow = 0x8B61,
	Sampler2DShadow = 0x8B62,
	Sampler2DRect = 0x8B63,
	Sampler2DRectShadow = 0x8B64,
	FloatMat2x3 = 0x8B65,
	FloatMat2x4 = 0x8B66,
	FloatMat3x2 = 0x8B67,
	FloatMat3x4 = 0x8B68,
	FloatMat4x2 = 0x8B69,
	FloatMat4x3 = 0x8B6A,
	Sampler1DArray = 0x8DC0,
	Sampler2DArray = 0x8DC1,
	SamplerBuffer = 0x8DC2,
	Sampler1DArrayShadow = 0x8DC3,
	Sampler2DArrayShadow = 0x8DC4,
	SamplerCubeShadow = 0x8DC5,
	UnsignedIntVec2 = 0x8DC6,
	UnsignedIntVec3 = 0x8DC7,
	UnsignedIntVec4 = 0x8DC8,
	IntSampler1D = 0x8DC9,
	IntSampler2D = 0x8DCA,
	IntSampler3D = 0x8DCB,
	IntSamplerCube = 0x8DCC,
	IntSampler2DRect = 0x8DCD,
	IntSampler1DArray = 0x8DCE,
	IntSampler2DArray = 0x8DCF,
	IntSamplerBuffer = 0x8DD0,
	UnsignedIntSampler1D = 0x8DD1,
	UnsignedIntSampler2D = 0x8DD2,
	UnsignedIntSampler3D = 0x8DD3,
	UnsignedIntSamplerCube = 0x8DD4,
	UnsignedIntSampler2DRect = 0x8DD5,
	UnsignedIntSampler1DArray = 0x8DD6,
	UnsignedIntSampler2DArray = 0x8DD7,
	UnsignedIntSamplerBuffer = 0x8DD8,
	Sampler2DMultisample = 0x9108,
	IntSampler2DMultisample = 0x9109,
	UnsignedIntSampler2DMultisample = 0x910A,
	Sampler2DMultisampleArray = 0x910B,
	IntSampler2DMultisampleArray = 0x910C,
	UnsignedIntSampler2DMultisampleArray = 0x910D,
}
		
[PublicAPI]
public enum LogicOp
{
	Clear = 0x1500,
	And = 0x1501,
	AndReverse = 0x1502,
	Copy = 0x1503,
	AndInverted = 0x1504,
	Noop = 0x1505,
	Xor = 0x1506,
	Or = 0x1507,
	Nor = 0x1508,
	Equiv = 0x1509,
	Invert = 0x150A,
	OrReverse = 0x150B,
	CopyInverted = 0x150C,
	OrInverted = 0x150D,
	Nand = 0x150E,
	Set = 0x150F,
}
	
[PublicAPI]
public enum ObjectIdentifier
{
	Texture = 0x1702,
	VertexArray = 0x8074,
	Buffer = 0x82E0,
	Shader = 0x82E1,
	Program = 0x82E2,
	Query = 0x82E3,
	ProgramPipeline = 0x82E4,
	Sampler = 0x82E6,
	Framebuffer = 0x8D40,
	Renderbuffer = 0x8D41,
}

[PublicAPI]
public enum ClearBufferTarget
{
	Color = 0x1800,
	Depth = 0x1801,
	Stencil = 0x1802,
}
	
[PublicAPI]
public enum InternalFormat
{
	StencilIndex = 0x1901,
	DepthComponent = 0x1902,
	Red = 0x1903,
	Rgb = 0x1907,
	Rgba = 0x1908,
	R3G3B2 = 0x2A10,
	Rgb4 = 0x804F,
	Rgb5 = 0x8050,
	Rgb8 = 0x8051,
	Rgb10 = 0x8052,
	Rgb12 = 0x8053,
	Rgb16 = 0x8054,
	Rgba2 = 0x8055,
	Rgba4 = 0x8056,
	Rgb5A1 = 0x8057,
	Rgba8 = 0x8058,
	Rgb10A2 = 0x8059,
	Rgba12 = 0x805A,
	Rgba16 = 0x805B,
	DepthComponent16 = 0x81A5,
	DepthComponent24 = 0x81A6,
	DepthComponent32 = 0x81A7,
	CompressedRed = 0x8225,
	CompressedRg = 0x8226,
	Rg = 0x8227,
	R8 = 0x8229,
	R16 = 0x822A,
	Rg8 = 0x822B,
	Rg16 = 0x822C,
	R16f = 0x822D,
	R32f = 0x822E,
	Rg16f = 0x822F,
	Rg32f = 0x8230,
	R8i = 0x8231,
	R8ui = 0x8232,
	R16i = 0x8233,
	R16ui = 0x8234,
	R32i = 0x8235,
	R32ui = 0x8236,
	Rg8i = 0x8237,
	Rg8ui = 0x8238,
	Rg16i = 0x8239,
	Rg16ui = 0x823A,
	Rg32i = 0x823B,
	Rg32ui = 0x823C,
	CompressedRgb = 0x84ED,
	CompressedRgba = 0x84EE,
	DepthStencil = 0x84F9,
	Rgba32f = 0x8814,
	Rgb32f = 0x8815,
	Rgba16f = 0x881A,
	Rgb16f = 0x881B,
	Depth24Stencil8 = 0x88F0,
	R11fG11fB10f = 0x8C3A,
	Rgb9E5 = 0x8C3D,
	Srgb = 0x8C40,
	Srgb8 = 0x8C41,
	SrgbAlpha = 0x8C42,
	Srgb8Alpha8 = 0x8C43,
	CompressedSrgb = 0x8C48,
	CompressedSrgbAlpha = 0x8C49,
	DepthComponent32f = 0x8CAC,
	Depth32fStencil8 = 0x8CAD,
	StencilIndex1 = 0x8D46,
	StencilIndex4 = 0x8D47,
	StencilIndex8 = 0x8D48,
	StencilIndex16 = 0x8D49,
	Rgba32ui = 0x8D70,
	Rgb32ui = 0x8D71,
	Rgba16ui = 0x8D76,
	Rgb16ui = 0x8D77,
	Rgba8ui = 0x8D7C,
	Rgb8ui = 0x8D7D,
	Rgba32i = 0x8D82,
	Rgb32i = 0x8D83,
	Rgba16i = 0x8D88,
	Rgb16i = 0x8D89,
	Rgba8i = 0x8D8E,
	Rgb8i = 0x8D8F,
	CompressedRedRgtc1 = 0x8DBB,
	CompressedSignedRedRgtc1 = 0x8DBC,
	CompressedRgRgtc2 = 0x8DBD,
	CompressedSignedRgRgtc2 = 0x8DBE,
	R8Snorm = 0x8F94,
	Rg8Snorm = 0x8F95,
	Rgb8Snorm = 0x8F96,
	Rgba8Snorm = 0x8F97,
	R16Snorm = 0x8F98,
	Rg16Snorm = 0x8F99,
	Rgb16Snorm = 0x8F9A,
	Rgba16Snorm = 0x8F9B,
	Rgb10A2ui = 0x906F,
}
	
[PublicAPI]
public enum PolygonMode
{
	Point = 0x1B00,
	Line = 0x1B01,
	Fill = 0x1B02,
}

[PublicAPI]
public enum StringName
{
	Vendor = 0x1F00,
	Renderer = 0x1F01,
	Version = 0x1F02,
	Extensions = 0x1F03,
	ShadingLanguageVersion = 0x8B8C,
}

[PublicAPI]
public enum BlitFramebufferFilter
{
	Nearest = 0x2600,
	Linear = 0x2601,
}

[PublicAPI]
public enum TextureMagFilter
{
	Nearest = 0x2600,
	Linear = 0x2601,
}

[PublicAPI]
public enum TextureMinFilter
{
	Nearest = 0x2600,
	Linear = 0x2601,
	NearestMipmapNearest = 0x2700,
	LinearMipmapNearest = 0x2701,
	NearestMipmapLinear = 0x2702,
	LinearMipmapLinear = 0x2703,
}

[PublicAPI]
public enum FogMode
{
	Linear = 0x2601,
}

[PublicAPI]
public enum TextureWrapMode
{
	LinearMipmapLinear = 0x2703,
	Repeat = 0x2901,
	ClampToBorder = 0x812D,
	ClampToEdge = 0x812F,
	MirroredRepeat = 0x8370,
}

[PublicAPI]
public enum SamplerParameterI
{
	TextureMagFilter = 0x2800,
	TextureMinFilter = 0x2801,
	TextureWrapS = 0x2802,
	TextureWrapT = 0x2803,
	TextureWrapR = 0x8072,
	TextureCompareMode = 0x884C,
	TextureCompareFunc = 0x884D,
}

[PublicAPI]
public enum SizedInternalFormat
{
	R3G3B2 = 0x2A10,
	Rgb4 = 0x804F,
	Rgb5 = 0x8050,
	Rgb8 = 0x8051,
	Rgb10 = 0x8052,
	Rgb12 = 0x8053,
	Rgb16 = 0x8054,
	Rgba2 = 0x8055,
	Rgba4 = 0x8056,
	Rgb5A1 = 0x8057,
	Rgba8 = 0x8058,
	Rgb10A2 = 0x8059,
	Rgba12 = 0x805A,
	Rgba16 = 0x805B,
	DepthComponent16 = 0x81A5,
	DepthComponent24 = 0x81A6,
	DepthComponent32 = 0x81A7,
	R8 = 0x8229,
	R16 = 0x822A,
	Rg8 = 0x822B,
	Rg16 = 0x822C,
	R16f = 0x822D,
	R32f = 0x822E,
	Rg16f = 0x822F,
	Rg32f = 0x8230,
	R8i = 0x8231,
	R8ui = 0x8232,
	R16i = 0x8233,
	R16ui = 0x8234,
	R32i = 0x8235,
	R32ui = 0x8236,
	Rg8i = 0x8237,
	Rg8ui = 0x8238,
	Rg16i = 0x8239,
	Rg16ui = 0x823A,
	Rg32i = 0x823B,
	Rg32ui = 0x823C,
	Rgba32f = 0x8814,
	Rgb32f = 0x8815,
	Rgba16f = 0x881A,
	Rgb16f = 0x881B,
	Depth24Stencil8 = 0x88F0,
	R11fG11fB10f = 0x8C3A,
	Rgb9E5 = 0x8C3D,
	Srgb8 = 0x8C41,
	Srgb8Alpha8 = 0x8C43,
	DepthComponent32f = 0x8CAC,
	Depth32fStencil8 = 0x8CAD,
	StencilIndex1 = 0x8D46,
	StencilIndex4 = 0x8D47,
	StencilIndex8 = 0x8D48,
	StencilIndex16 = 0x8D49,
	Rgba32ui = 0x8D70,
	Rgb32ui = 0x8D71,
	Rgba16ui = 0x8D76,
	Rgb16ui = 0x8D77,
	Rgba8ui = 0x8D7C,
	Rgb8ui = 0x8D7D,
	Rgba32i = 0x8D82,
	Rgb32i = 0x8D83,
	Rgba16i = 0x8D88,
	Rgb16i = 0x8D89,
	Rgba8i = 0x8D8E,
	Rgb8i = 0x8D8F,
	CompressedRedRgtc1 = 0x8DBB,
	CompressedSignedRedRgtc1 = 0x8DBC,
	CompressedRgRgtc2 = 0x8DBD,
	CompressedSignedRgRgtc2 = 0x8DBE,
	R8Snorm = 0x8F94,
	Rg8Snorm = 0x8F95,
	Rgb8Snorm = 0x8F96,
	Rgba8Snorm = 0x8F97,
	R16Snorm = 0x8F98,
	Rg16Snorm = 0x8F99,
	Rgb16Snorm = 0x8F9A,
	Rgba16Snorm = 0x8F9B,
	Rgb10A2ui = 0x906F,
}

[PublicAPI]
public enum ClipPlaneName
{
	ClipDistance0 = 0x3000,
	ClipDistance1 = 0x3001,
	ClipDistance2 = 0x3002,
	ClipDistance3 = 0x3003,
	ClipDistance4 = 0x3004,
	ClipDistance5 = 0x3005,
	ClipDistance6 = 0x3006,
	ClipDistance7 = 0x3007,
}

[PublicAPI]
public enum BlendEquationMode
{
	FuncAdd = 0x8006,
	Min = 0x8007,
	Max = 0x8008,
	FuncSubtract = 0x800A,
	FuncReverseSubtract = 0x800B,
}

[PublicAPI]
public enum InternalFormatPName
{
	Samples = 0x80A9,
	TextureCompressed = 0x86A1,
}

[PublicAPI]
public enum PointParameterName
{
	PointFadeThresholdSize = 0x8128,
}

[PublicAPI]
public enum FramebufferAttachmentParameter
{
	ColorEncoding = 0x8210,
	ComponentType = 0x8211,
	RedSize = 0x8212,
	GreenSize = 0x8213,
	BlueSize = 0x8214,
	AlphaSize = 0x8215,
	DepthSize = 0x8216,
	StencilSize = 0x8217,
	ObjectType = 0x8CD0,
	ObjectName = 0x8CD1,
	TextureLevel = 0x8CD2,
	TextureCubeMapFace = 0x8CD3,
	TextureLayer = 0x8CD4,
	Layered = 0x8DA7,
}

[PublicAPI]
public enum FramebufferStatus
{
	Undefined = 0x8219,
	Complete = 0x8CD5,
	IncompleteAttachment = 0x8CD6,
	IncompleteMissingAttachment = 0x8CD7,
	IncompleteDrawBuffer = 0x8CDB,
	IncompleteReadBuffer = 0x8CDC,
	Unsupported = 0x8CDD,
	IncompleteMultisample = 0x8D56,
	IncompleteLayerTargets = 0x8DA8,
}

[PublicAPI]
public enum GetPointervPName
{
	Function = 0x8244,
	UserParam = 0x8245,
}

[PublicAPI]
public enum TextureUnit
{
	Texture0 = 0x84C0,
	Texture1 = 0x84C1,
	Texture2 = 0x84C2,
	Texture3 = 0x84C3,
	Texture4 = 0x84C4,
	Texture5 = 0x84C5,
	Texture6 = 0x84C6,
	Texture7 = 0x84C7,
	Texture8 = 0x84C8,
	Texture9 = 0x84C9,
	Texture10 = 0x84CA,
	Texture11 = 0x84CB,
	Texture12 = 0x84CC,
	Texture13 = 0x84CD,
	Texture14 = 0x84CE,
	Texture15 = 0x84CF,
	Texture16 = 0x84D0,
	Texture17 = 0x84D1,
	Texture18 = 0x84D2,
	Texture19 = 0x84D3,
	Texture20 = 0x84D4,
	Texture21 = 0x84D5,
	Texture22 = 0x84D6,
	Texture23 = 0x84D7,
	Texture24 = 0x84D8,
	Texture25 = 0x84D9,
	Texture26 = 0x84DA,
	Texture27 = 0x84DB,
	Texture28 = 0x84DC,
	Texture29 = 0x84DD,
	Texture30 = 0x84DE,
	Texture31 = 0x84DF,
}

[PublicAPI]
public enum VertexAttrib
{
	Enabled = 0x8622,
	Size = 0x8623,
	Stride = 0x8624,
	Type = 0x8625,
	Current = 0x8626,
	Normalized = 0x886A,
	BufferBinding = 0x889F,
	Integer = 0x88FD,
	Divisor = 0x88FE,
}

[PublicAPI]
public enum VertexAttribPointerProperty
{
	ArrayPointer = 0x8645,
}

[PublicAPI]
public enum BufferPName
{
	Size = 0x8764,
	Usage = 0x8765,
	Access = 0x88BB,
	Mapped = 0x88BC,
	AccessFlags = 0x911F,
	MapLength = 0x9120,
	MapOffset = 0x9121,
}

[PublicAPI]
public enum QueryParameterName
{
	QueryCounterBits = 0x8864,
	CurrentQuery = 0x8865,
}

[PublicAPI]
public enum QueryObjectParameterName
{
	QueryResult = 0x8866,
	QueryResultAvailable = 0x8867,
}

[PublicAPI]
public enum CopyBufferSubDataTarget
{
	ArrayBuffer = 0x8892,
	ElementArrayBuffer = 0x8893,
	PixelPackBuffer = 0x88EB,
	PixelUnpackBuffer = 0x88EC,
	UniformBuffer = 0x8A11,
	TextureBuffer = 0x8C2A,
	TransformFeedbackBuffer = 0x8C8E,
	CopyReadBuffer = 0x8F36,
	CopyWriteBuffer = 0x8F37,
}
	
[PublicAPI]
public enum BufferTarget
{
	ArrayBuffer = 0x8892,
	ElementArrayBuffer = 0x8893,
	PixelPackBuffer = 0x88EB,
	PixelUnpackBuffer = 0x88EC,
	UniformBuffer = 0x8A11,
	TextureBuffer = 0x8C2A,
	TransformFeedbackBuffer = 0x8C8E,
	CopyReadBuffer = 0x8F36,
	CopyWriteBuffer = 0x8F37,
}

[PublicAPI]
public enum BufferAccess
{
	ReadOnly = 0x88B8,
	WriteOnly = 0x88B9,
	ReadWrite = 0x88BA,
}

[PublicAPI]
public enum BufferPointerName
{
	BufferMapPointer = 0x88BD,
}

[PublicAPI]
public enum QueryTarget
{
	TimeElapsed = 0x88BF,
	SamplesPassed = 0x8914,
	AnySamplesPassed = 0x8C2F,
	PrimitivesGenerated = 0x8C87,
	TransformFeedbackPrimitivesWritten = 0x8C88,
}

[PublicAPI]
public enum BufferUsage
{
	StreamDraw = 0x88E0,
	StreamRead = 0x88E1,
	StreamCopy = 0x88E2,
	StaticDraw = 0x88E4,
	StaticRead = 0x88E5,
	StaticCopy = 0x88E6,
	DynamicDraw = 0x88E8,
	DynamicRead = 0x88E9,
	DynamicCopy = 0x88EA,
}

[PublicAPI]
public enum ProgramProperty
{
	GeometryVerticesOut = 0x8916,
	GeometryInputType = 0x8917,
	GeometryOutputType = 0x8918,
	ActiveUniformBlockMaxNameLength = 0x8A35,
	ActiveUniformBlocks = 0x8A36,
	DeleteStatus = 0x8B80,
	LinkStatus = 0x8B82,
	ValidateStatus = 0x8B83,
	InfoLogLength = 0x8B84,
	AttachedShaders = 0x8B85,
	ActiveUniforms = 0x8B86,
	ActiveUniformMaxLength = 0x8B87,
	ActiveAttributes = 0x8B89,
	ActiveAttributeMaxLength = 0x8B8A,
	TransformFeedbackVaryingMaxLength = 0x8C76,
	TransformFeedbackBufferMode = 0x8C7F,
	TransformFeedbackVaryings = 0x8C83,
}

[PublicAPI]
public enum ClampColorTarget
{
	ClampReadColor = 0x891C,
}

[PublicAPI]
public enum UniformPName
{
	UniformType = 0x8A37,
	UniformSize = 0x8A38,
	UniformNameLength = 0x8A39,
	UniformBlockIndex = 0x8A3A,
	UniformOffset = 0x8A3B,
	UniformArrayStride = 0x8A3C,
	UniformMatrixStride = 0x8A3D,
	UniformIsRowMajor = 0x8A3E,
}

[PublicAPI]
public enum SubroutineParameterName
{
	UniformSize = 0x8A38,
	UniformNameLength = 0x8A39,
}

[PublicAPI]
public enum UniformBlockPName
{
	Binding = 0x8A3F,
	DataSize = 0x8A40,
	NameLength = 0x8A41,
	ActiveUniforms = 0x8A42,
	ActiveUniformIndices = 0x8A43,
	ReferencedByVertexShader = 0x8A44,
	ReferencedByGeometryShader = 0x8A45,
	ReferencedByFragmentShader = 0x8A46,
}

[PublicAPI]
public enum PipelineParameterName
{
	FragmentShader = 0x8B30,
	VertexShader = 0x8B31,
	InfoLogLength = 0x8B84,
	GeometryShader = 0x8DD9,
}

[PublicAPI]
public enum ShaderType
{
	Fragment = 0x8B30,
	Vertex = 0x8B31,
	Geometry = 0x8DD9,
}

[PublicAPI]
public enum ShaderParameterName
{
	ShaderType = 0x8B4F,
	DeleteStatus = 0x8B80,
	CompileStatus = 0x8B81,
	InfoLogLength = 0x8B84,
	ShaderSourceLength = 0x8B88,
}

[PublicAPI]
public enum TransformFeedbackPName
{
	BufferStart = 0x8C84,
	BufferSize = 0x8C85,
	BufferBinding = 0x8C8F,
}

[PublicAPI]
public enum TransformFeedbackBufferMode
{
	Interleaved = 0x8C8C,
	Separate = 0x8C8D,
}

[PublicAPI]
public enum ProgramInterface
{
	TransformFeedbackBuffer = 0x8C8E,
}

[PublicAPI]
public enum ClipControlOrigin
{
	LowerLeft = 0x8CA1,
	UpperLeft = 0x8CA2,
}

[PublicAPI]
public enum CheckFramebufferStatusTarget
{
	ReadFramebuffer = 0x8CA8,
	DrawFramebuffer = 0x8CA9,
	Framebuffer = 0x8D40,
}

[PublicAPI]
public enum FramebufferTarget
{
	ReadFramebuffer = 0x8CA8,
	DrawFramebuffer = 0x8CA9,
	Framebuffer = 0x8D40,
}

[PublicAPI]
public enum RenderbufferParameter
{
	RenderbufferSamples = 0x8CAB,
	RenderbufferWidth = 0x8D42,
	RenderbufferHeight = 0x8D43,
	RenderbufferInternalFormat = 0x8D44,
	RenderbufferRedSize = 0x8D50,
	RenderbufferGreenSize = 0x8D51,
	RenderbufferBlueSize = 0x8D52,
	RenderbufferAlphaSize = 0x8D53,
	RenderbufferDepthSize = 0x8D54,
	RenderbufferStencilSize = 0x8D55,
}

[PublicAPI]
public enum FramebufferAttachment
{
	ColorAttachment0 = 0x8CE0,
	ColorAttachment1 = 0x8CE1,
	ColorAttachment2 = 0x8CE2,
	ColorAttachment3 = 0x8CE3,
	ColorAttachment4 = 0x8CE4,
	ColorAttachment5 = 0x8CE5,
	ColorAttachment6 = 0x8CE6,
	ColorAttachment7 = 0x8CE7,
	ColorAttachment8 = 0x8CE8,
	ColorAttachment9 = 0x8CE9,
	ColorAttachment10 = 0x8CEA,
	ColorAttachment11 = 0x8CEB,
	ColorAttachment12 = 0x8CEC,
	ColorAttachment13 = 0x8CED,
	ColorAttachment14 = 0x8CEE,
	ColorAttachment15 = 0x8CEF,
	ColorAttachment16 = 0x8CF0,
	ColorAttachment17 = 0x8CF1,
	ColorAttachment18 = 0x8CF2,
	ColorAttachment19 = 0x8CF3,
	ColorAttachment20 = 0x8CF4,
	ColorAttachment21 = 0x8CF5,
	ColorAttachment22 = 0x8CF6,
	ColorAttachment23 = 0x8CF7,
	ColorAttachment24 = 0x8CF8,
	ColorAttachment25 = 0x8CF9,
	ColorAttachment26 = 0x8CFA,
	ColorAttachment27 = 0x8CFB,
	ColorAttachment28 = 0x8CFC,
	ColorAttachment29 = 0x8CFD,
	ColorAttachment30 = 0x8CFE,
	ColorAttachment31 = 0x8CFF,
	DepthAttachment = 0x8D00,
	StencilAttachment = 0x8D20,
}

[PublicAPI]
public enum RenderbufferTarget
{
	Renderbuffer = 0x8D41,
}

[PublicAPI]
public enum ConditionalRenderMode
{
	QueryWait = 0x8E13,
	QueryNoWait = 0x8E14,
	QueryByRegionWait = 0x8E15,
	QueryByRegionNoWait = 0x8E16,
}

[PublicAPI]
public enum QueryCounterTarget
{
	Timestamp = 0x8E28,
}

[PublicAPI]
public enum VertexProvokingMode
{
	FirstVertexConvention = 0x8E4D,
	LastVertexConvention = 0x8E4E,
}

[PublicAPI]
public enum GetMultisamplePName
{
	SamplePosition = 0x8E50,
}

[PublicAPI]
public enum SyncParameterName
{
	ObjectType = 0x9112,
	SyncCondition = 0x9113,
	SyncStatus = 0x9114,
	SyncFlags = 0x9115,
}

[PublicAPI]
public enum SyncCondition
{
	SyncGpuCommandsComplete = 0x9117,
}

[PublicAPI]
public enum SyncStatus
{
	AlreadySignaled = 0x911A,
	TimeoutExpired = 0x911B,
	ConditionSatisfied = 0x911C,
	WaitFailed = 0x911D,
}