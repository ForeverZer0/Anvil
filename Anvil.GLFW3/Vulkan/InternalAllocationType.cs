using JetBrains.Annotations;

namespace Anvil.GLFW3.Vulkan;

/// <summary>
/// Allocation type.
/// </summary>
/// <seealso href="https://www.khronos.org/registry/vulkan/specs/1.3-extensions/man/html/VkInternalAllocationType.html"/>
[PublicAPI]
public enum InternalAllocationType
{
    /// <summary>
    /// Specifies that the allocation is intended for execution by the host.
    /// </summary>
    Executable = 0
}