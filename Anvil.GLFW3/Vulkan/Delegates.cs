using System.Runtime.InteropServices;
using System.Security;
using JetBrains.Annotations;

namespace Anvil.GLFW3.Vulkan;

/// <summary>
/// Provides an application-defined memory allocation function.
/// </summary>
/// <seealso href="https://www.khronos.org/registry/vulkan/specs/1.3-extensions/man/html/PFN_vkAllocationFunction.html"/>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate IntPtr AllocationFunction(IntPtr userData, int size, int alignment, SystemAllocationScope scope);

/// <summary>
/// Provides an application-defined memory reallocation function.
/// </summary>
/// <seealso href="https://www.khronos.org/registry/vulkan/specs/1.3-extensions/man/html/PFN_vkReallocationFunction.html"/>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate IntPtr ReallocationFunction(IntPtr userData, IntPtr original, int size, int alignment, SystemAllocationScope scope);

/// <summary>
/// Provides an application-defined memory free function.
/// </summary>
/// <seealso href="https://www.khronos.org/registry/vulkan/specs/1.3-extensions/man/html/PFN_vkFreeFunction.html"/>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate void FreeFunction(IntPtr userData, IntPtr memory);

/// <summary>
/// Provides an application-defined function that is called by the implementation when the implementation makes internal
/// allocations.
/// </summary>
/// <seealso href="https://www.khronos.org/registry/vulkan/specs/1.3-extensions/man/html/PFN_vkInternalAllocationNotification.html"/>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate void InternalAllocationNotification(IntPtr userData, int size, InternalAllocationType type, SystemAllocationScope scope);

/// <summary>
/// Provides an application-defined function that is called by the implementation when the implementation frees internal
/// allocations.
/// </summary>
/// <seealso href="https://www.khronos.org/registry/vulkan/specs/1.3-extensions/man/html/PFN_vkInternalFreeNotification.html"/>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate void InternalFreeNotification(IntPtr userData, int size, InternalAllocationType type, SystemAllocationScope scope);