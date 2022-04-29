using JetBrains.Annotations;
#pragma warning disable CS1591

namespace Anvil.GLFW3.Vulkan;

/// <summary>
/// Vulkan command return codes.
/// </summary>
/// <see href="https://www.khronos.org/registry/vulkan/specs/1.3-extensions/man/html/VkResult.html"/>
[PublicAPI]
public enum Result
{
    Success = 0,
    NotReady = 1,
    Timeout = 2,
    EventSet = 3,
    EventReset = 4,
    Incomplete = 5,
    ErrorOutOfHostMemory = -1,
    ErrorOutOfDeviceMemory = -2,
    ErrorInitializationFailed = -3,
    ErrorDeviceLost = -4,
    ErrorMemoryMapFailed = -5,
    ErrorLayerNotPresent = -6,
    ErrorExtensionNotPresent = -7,
    ErrorFeatureNotPresent = -8,
    ErrorIncompatibleDriver = -9,
    ErrorTooManyObjects = -10,
    ErrorFormatNotSupported = -11,
    ErrorFragmentedPool = -12,
    ErrorUnknown = -13,

    // Provided by VERSION_1_1
    ErrorOutOfPoolMemory = -1000069000,

    // Provided by VERSION_1_1
    ErrorInvalidExternalHandle = -1000072003,

    // Provided by VERSION_1_2
    ErrorFragmentation = -1000161000,

    // Provided by VERSION_1_2
    ErrorInvalidOpaqueCaptureAddress = -1000257000,

    // Provided by VERSION_1_3
    PipelineCompileRequired = 1000297000,

    // Provided by KHR_surface
    ErrorSurfaceLostKhr = -1000000000,

    // Provided by KHR_surface
    ErrorNativeWindowInUseKhr = -1000000001,

    // Provided by KHR_swapchain
    SuboptimalKhr = 1000001003,

    // Provided by KHR_swapchain
    ErrorOutOfDateKhr = -1000001004,

    // Provided by KHR_display_swapchain
    ErrorIncompatibleDisplayKhr = -1000003001,

    // Provided by EXT_debug_report
    ErrorValidationFailedExt = -1000011001,

    // Provided by NV_glsl_shader
    ErrorInvalidShaderNv = -1000012000,

    // Provided by EXT_image_drm_format_modifier
    ErrorInvalidDrmFormatModifierPlaneLayoutExt = -1000158000,

    // Provided by KHR_global_priority
    ErrorNotPermittedKhr = -1000174001,

    // Provided by EXT_full_screen_exclusive
    ErrorFullScreenExclusiveModeLostExt = -1000255000,

    // Provided by KHR_deferred_host_operations
    ThreadIdleKhr = 1000268000,

    // Provided by KHR_deferred_host_operations
    ThreadDoneKhr = 1000268001,

    // Provided by KHR_deferred_host_operations
    OperationDeferredKhr = 1000268002,

    // Provided by KHR_deferred_host_operations
    OperationNotDeferredKhr = 1000268003,

    // Provided by KHR_maintenance1
    ErrorOutOfPoolMemoryKhr = ErrorOutOfPoolMemory,

    // Provided by KHR_external_memory
    ErrorInvalidExternalHandleKhr = ErrorInvalidExternalHandle,

    // Provided by EXT_descriptor_indexing
    ErrorFragmentationExt = ErrorFragmentation,

    // Provided by EXT_global_priority
    ErrorNotPermittedExt = ErrorNotPermittedKhr,

    // Provided by EXT_buffer_device_address
    ErrorInvalidDeviceAddressExt = ErrorInvalidOpaqueCaptureAddress,

    // Provided by KHR_buffer_device_address
    ErrorInvalidOpaqueCaptureAddressKhr = ErrorInvalidOpaqueCaptureAddress,

    // Provided by EXT_pipeline_creation_cache_control
    PipelineCompileRequiredExt = PipelineCompileRequired,

    // Provided by EXT_pipeline_creation_cache_control
    ErrorPipelineCompileRequiredExt = PipelineCompileRequired,
}