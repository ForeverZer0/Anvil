using JetBrains.Annotations;
#pragma warning disable CS1591

namespace Anvil.GLFW3.Vulkan;

/// <summary>
/// Allocation scope.
/// </summary>
/// <see href="https://www.khronos.org/registry/vulkan/specs/1.3-extensions/man/html/VkSystemAllocationScope.html"/>
[PublicAPI]
public enum SystemAllocationScope
{
    Command = 0,
    Object = 1,
    Cache = 2,
    Device = 3,
    Instance = 4,
}