namespace Anvil;

public interface INativeLoader
{
    IntPtr LoadLibrary(string path);

    IntPtr GetProcAddress(string name);
}