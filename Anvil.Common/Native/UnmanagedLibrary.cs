using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Anvil.Native;


public sealed class UnmanagedLibrary : SafeHandle
{
    public Assembly Assembly { get; }
    
    public UnmanagedLibrary(string libraryName, DllImportResolver? resolver = null) : base(IntPtr.Zero, true)
    {
        Assembly = Assembly.GetCallingAssembly();
        if (resolver != null)
            NativeLibrary.SetDllImportResolver(Assembly, resolver);
        
        SetHandle(NativeLibrary.Load(libraryName)); // TODO: Resolve paths
        if (IsInvalid)
            throw new Exception("Can't load some stuff"); // TODO
    }

    protected override bool ReleaseHandle()
    {
        if (handle == IntPtr.Zero)
            return true;
        try
        {
            NativeLibrary.Free(handle);
            SetHandle(IntPtr.Zero);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IntPtr Import(string symbolName, bool required = true)
    {
        if (NativeLibrary.TryGetExport(handle, symbolName, out var address))
            return address;

        if (required)
            throw new EntryPointNotFoundException($"Failed to fund entry point for \"{symbolName}\"");
        
        Debug.WriteLine($"Failed to find entry point for \"{symbolName}\"");
        return default;
    }

    public TDelegate Import<TDelegate>(string symbolName) where TDelegate : Delegate
    {
        var address = NativeLibrary.GetExport(handle, symbolName);
        return Marshal.GetDelegateForFunctionPointer<TDelegate>(address);
    }
    
    public TDelegate? Import<TDelegate>(string symbolName, bool required) where TDelegate : Delegate
    {
        var address = Import(symbolName, required);
        return address == IntPtr.Zero ? default : Marshal.GetDelegateForFunctionPointer<TDelegate>(address);
    }

    public override bool IsInvalid => handle == IntPtr.Zero;
}