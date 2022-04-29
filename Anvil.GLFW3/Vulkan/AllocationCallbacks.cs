using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Anvil.GLFW3.Vulkan;

/// <summary>
/// Contains callback delegates for memory allocation.
/// </summary>
/// <seealso href="https://www.khronos.org/registry/vulkan/specs/1.3-extensions/man/html/VkAllocationCallbacks.html"/>
[StructLayout(LayoutKind.Sequential), PublicAPI]
public sealed class AllocationCallbacks
{
    /// <summary>
    /// Gets or sets an arbitrary user-pointer that will be supplied with callbacks.
    /// </summary>
    public IntPtr UserData { get; set; }

    /// <summary>
    /// Gets or sets an application-defined memory allocation handler.
    /// </summary>
    public AllocationFunction? Allocation { get; set; }

    /// <summary>
    /// Gets or sets an application-defined memory reallocation handler.
    /// </summary>
    public ReallocationFunction? Reallocation { get; set; }

    /// <summary>
    /// Gets or sets an application-defined memory free handler.
    /// </summary>
    public FreeFunction? Free { get; set; }

    /// <summary>
    /// Gets or sets an application-defined callback that is called by the implementation when the implementation makes
    /// internal allocations.
    /// </summary>
    public InternalAllocationNotification? InternalAllocation { get; set; }

    /// <summary>
    /// Gets or sets an application-defined callback that is called by the implementation when the implementation frees
    /// internal allocations.
    /// </summary>
    public InternalFreeNotification? InternalFree { get; set; }
}