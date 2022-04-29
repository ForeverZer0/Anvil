namespace Anvil.Native;

/// <summary>
/// Signature of a method capable of retrieving an unmanaged pointer to member exported from a C-style library.
/// </summary>
/// <param name="name">The name of the symbol to retrieve the address of.</param>
/// <returns>The address of the export with the specified <paramref name="name"/>.</returns>
public delegate IntPtr GetProcAddressHandler(string name);