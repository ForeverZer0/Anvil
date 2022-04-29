using JetBrains.Annotations;

namespace Anvil.GLFW3.Vulkan;

/// <summary>
/// Opaque handle to a physical device object.
/// <para/>
/// This is merely a type-safe <see cref="IntPtr"/>.
/// </summary>
/// <param name="Value">The native pointer value.</param>
/// <seealso href="https://www.khronos.org/registry/vulkan/specs/1.3-extensions/man/html/VkPhysicalDevice.html"/>
[PublicAPI]
public record struct PhysicalDevice(IntPtr Value);