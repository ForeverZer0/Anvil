using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;

namespace Anvil;

public static class Factory
{
    // TODO: Value types
    
    public const BindingFlags DefaultFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
    
    private static TDelegate Generate<TDelegate, TResult>(BindingFlags flags, params Type[] parameters) where TDelegate : Delegate
    {
        var type = typeof(TResult);
        var ctor = type.GetConstructor(flags, parameters);
        if (ctor is null)
            throw new ConstraintException($"No compatible constructor for {type} matching the specified parameters was found.");

        var dynamicMethod = new DynamicMethod($"Create{type.Name}Instance", type, parameters, type);
        var il = dynamicMethod.GetILGenerator();
        il.Emit(OpCodes.Newobj, ctor);
        il.Emit(OpCodes.Ret);

        return dynamicMethod.CreateDelegate<TDelegate>();
    }
    
    /// <summary>
    /// Dynamically creates and compiles a delegate for directly calling the an object constructor without repeated
    /// queries using reflection.
    /// </summary>
    /// <param name="flags">
    /// Binding flags dictating the constructor to retrieve, defaults to <see cref="BindingFlags.Public"/> and
    /// <see cref="BindingFlags.Instance"/> when not specified.
    /// </param>
    /// <typeparam name="TResult">The type of object being returned.</typeparam>
    /// <returns>A delegate that can be used to create instances of the specified type.</returns>
    public static Func<TResult> CreateActivator<TResult>(BindingFlags flags = DefaultFlags) where TResult : new()
    {
        return Generate<Func<TResult>, TResult>(flags);
    }
    
    /// <summary>
    /// Dynamically creates and compiles a delegate for directly calling the an object constructor without repeated
    /// queries using reflection.
    /// </summary>
    /// <param name="flags">
    /// Binding flags dictating the constructor to retrieve, defaults to <see cref="BindingFlags.Public"/> and
    /// <see cref="BindingFlags.Instance"/> when not specified.
    /// </param>
    /// <typeparam name="TResult">The type of object being returned.</typeparam>
    /// <typeparam name="T1">The type of the argument at index 0.</typeparam>
    /// <returns>A delegate that can be used to create instances of the specified type.</returns>
    public static Func<T1, TResult> CreateActivator<TResult, T1>(BindingFlags flags = DefaultFlags)
    {
        return Generate<Func<T1, TResult>, TResult>(flags);
    }
    
    /// <summary>
    /// Dynamically creates and compiles a delegate for directly calling the an object constructor without repeated
    /// queries using reflection.
    /// </summary>
    /// <param name="flags">
    /// Binding flags dictating the constructor to retrieve, defaults to <see cref="BindingFlags.Public"/> and
    /// <see cref="BindingFlags.Instance"/> when not specified.
    /// </param>
    /// <typeparam name="TResult">The type of object being returned.</typeparam>
    /// <typeparam name="T1">The type of the argument at index 0.</typeparam>
    /// <typeparam name="T2">The type of the argument at index 1.</typeparam>
    /// <returns>A delegate that can be used to create instances of the specified type.</returns>
    public static Func<T1, T2, TResult> CreateActivator<TResult, T1, T2>(BindingFlags flags = DefaultFlags)
    {
        return Generate<Func<T1, T2, TResult>, TResult>(flags);
    }
    
    /// <summary>
    /// Dynamically creates and compiles a delegate for directly calling the an object constructor without repeated
    /// queries using reflection.
    /// </summary>
    /// <param name="flags">
    /// Binding flags dictating the constructor to retrieve, defaults to <see cref="BindingFlags.Public"/> and
    /// <see cref="BindingFlags.Instance"/> when not specified.
    /// </param>
    /// <typeparam name="TResult">The type of object being returned.</typeparam>
    /// <typeparam name="T1">The type of the argument at index 0.</typeparam>
    /// <typeparam name="T2">The type of the argument at index 1.</typeparam>
    /// <typeparam name="T3">The type of the argument at index 2.</typeparam>
    /// <returns>A delegate that can be used to create instances of the specified type.</returns>
    public static Func<T1, T2, T3, TResult> CreateActivator<TResult, T1, T2, T3>(BindingFlags flags = DefaultFlags)
    {
        return Generate<Func<T1, T2, T3, TResult>, TResult>(flags);
    }
    
    /// <summary>
    /// Dynamically creates and compiles a delegate for directly calling the an object constructor without repeated
    /// queries using reflection.
    /// </summary>
    /// <param name="flags">
    /// Binding flags dictating the constructor to retrieve, defaults to <see cref="BindingFlags.Public"/> and
    /// <see cref="BindingFlags.Instance"/> when not specified.
    /// </param>
    /// <typeparam name="TResult">The type of object being returned.</typeparam>
    /// <typeparam name="T1">The type of the argument at index 0.</typeparam>
    /// <typeparam name="T2">The type of the argument at index 1.</typeparam>
    /// <typeparam name="T3">The type of the argument at index 2.</typeparam>
    /// <typeparam name="T4">The type of the argument at index 3.</typeparam>
    /// <returns>A delegate that can be used to create instances of the specified type.</returns>
    public static Func<T1, T2, T3, T4, TResult> CreateActivator<TResult, T1, T2, T3, T4>(BindingFlags flags = DefaultFlags)
    {
        return Generate<Func<T1, T2, T3, T4, TResult>, TResult>(flags);
    }

    /// <summary>
    /// Dynamically creates and compiles a delegate for directly calling the an object constructor without repeated
    /// queries using reflection.
    /// </summary>
    /// <param name="flags">
    /// Binding flags dictating the constructor to retrieve, defaults to <see cref="BindingFlags.Public"/> and
    /// <see cref="BindingFlags.Instance"/> when not specified.
    /// </param>
    /// <typeparam name="TResult">The type of object being returned.</typeparam>
    /// <typeparam name="T1">The type of the argument at index 0.</typeparam>
    /// <typeparam name="T2">The type of the argument at index 1.</typeparam>
    /// <typeparam name="T3">The type of the argument at index 2.</typeparam>
    /// <typeparam name="T4">The type of the argument at index 3.</typeparam>
    /// <typeparam name="T5">The type of the argument at index 4.</typeparam>
    /// <returns>A delegate that can be used to create instances of the specified type.</returns>
    public static Func<T1, T2, T3, T4, T5, TResult> CreateActivator<TResult, T1, T2, T3, T4, T5>(BindingFlags flags = DefaultFlags)
    {
        return Generate<Func<T1, T2, T3, T4, T5, TResult>, TResult>(flags);
    }
    
    /// <summary>
    /// Dynamically creates and compiles a delegate for directly calling the an object constructor without repeated
    /// queries using reflection.
    /// </summary>
    /// <param name="flags">
    /// Binding flags dictating the constructor to retrieve, defaults to <see cref="BindingFlags.Public"/> and
    /// <see cref="BindingFlags.Instance"/> when not specified.
    /// </param>
    /// <typeparam name="TResult">The type of object being returned.</typeparam>
    /// <typeparam name="T1">The type of the argument at index 0.</typeparam>
    /// <typeparam name="T2">The type of the argument at index 1.</typeparam>
    /// <typeparam name="T3">The type of the argument at index 2.</typeparam>
    /// <typeparam name="T4">The type of the argument at index 3.</typeparam>
    /// <typeparam name="T5">The type of the argument at index 4.</typeparam>
    /// <typeparam name="T6">The type of the argument at index 5.</typeparam>
    /// <returns>A delegate that can be used to create instances of the specified type.</returns>
    public static Func<T1, T2, T3, T4, T5, T6, TResult> CreateActivator<TResult, T1, T2, T3, T4, T5, T6>(BindingFlags flags = DefaultFlags)
    {
        return Generate<Func<T1, T2, T3, T4, T5, T6, TResult>, TResult>(flags);
    }
    
    /// <summary>
    /// Dynamically creates and compiles a delegate for directly calling the an object constructor without repeated
    /// queries using reflection.
    /// </summary>
    /// <param name="flags">
    /// Binding flags dictating the constructor to retrieve, defaults to <see cref="BindingFlags.Public"/> and
    /// <see cref="BindingFlags.Instance"/> when not specified.
    /// </param>
    /// <typeparam name="TResult">The type of object being returned.</typeparam>
    /// <typeparam name="T1">The type of the argument at index 0.</typeparam>
    /// <typeparam name="T2">The type of the argument at index 1.</typeparam>
    /// <typeparam name="T3">The type of the argument at index 2.</typeparam>
    /// <typeparam name="T4">The type of the argument at index 3.</typeparam>
    /// <typeparam name="T5">The type of the argument at index 4.</typeparam>
    /// <typeparam name="T6">The type of the argument at index 5.</typeparam>
    /// <typeparam name="T7">The type of the argument at index 6.</typeparam>
    /// <returns>A delegate that can be used to create instances of the specified type.</returns>
    public static Func<T1, T2, T3, T4, T5, T6, T7, TResult> CreateActivator<TResult, T1, T2, T3, T4, T5, T6, T7>(BindingFlags flags = DefaultFlags)
    {
        return Generate<Func<T1, T2, T3, T4, T5, T6, T7, TResult>, TResult>(flags);
    }
    
    /// <summary>
    /// Dynamically creates and compiles a delegate for directly calling the an object constructor without repeated
    /// queries using reflection.
    /// </summary>
    /// <param name="flags">
    /// Binding flags dictating the constructor to retrieve, defaults to <see cref="BindingFlags.Public"/> and
    /// <see cref="BindingFlags.Instance"/> when not specified.
    /// </param>
    /// <typeparam name="TResult">The type of object being returned.</typeparam>
    /// <typeparam name="T1">The type of the argument at index 0.</typeparam>
    /// <typeparam name="T2">The type of the argument at index 1.</typeparam>
    /// <typeparam name="T3">The type of the argument at index 2.</typeparam>
    /// <typeparam name="T4">The type of the argument at index 3.</typeparam>
    /// <typeparam name="T5">The type of the argument at index 4.</typeparam>
    /// <typeparam name="T6">The type of the argument at index 5.</typeparam>
    /// <typeparam name="T7">The type of the argument at index 6.</typeparam>
    /// <typeparam name="T8">The type of the argument at index 7.</typeparam>
    /// <returns>A delegate that can be used to create instances of the specified type.</returns>
    public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> CreateActivator<TResult, T1, T2, T3, T4, T5, T6, T7, T8>(BindingFlags flags = DefaultFlags)
    {
        return Generate<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>, TResult>(flags);
    }
}