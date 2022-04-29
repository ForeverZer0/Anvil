using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using JetBrains.Annotations;

namespace Anvil;

/// <summary>
/// Utility class providing methods for compilation of IL code that is generated dynamically during runtime.
/// </summary>
[PublicAPI]
public static class Emit
{
    private static Dictionary<int, Delegate> constructors;

    /// <summary>
    /// Static constructor.
    /// </summary>
    static Emit()
    {
        constructors = new Dictionary<int, Delegate>();
    }

    /// <summary>
    /// Flags suitable for finding public members only.
    /// </summary>
    public const BindingFlags Public = BindingFlags.Public | BindingFlags.Instance;

    /// <summary>
    /// Flags suitable for finding public and non-public members.
    /// </summary>
    public const BindingFlags PublicAndPrivate = Public | BindingFlags.NonPublic;
    
    /// <summary>
    /// Finds a compatible constructor of <paramref name="instanceType"/> that matches the signature of
    /// <paramref name="delegateType"/>, and then generates and compiles the IL to call it directly without further
    /// use of reflection.
    /// </summary>
    /// <param name="delegateType">A delegate type that is compatible with the desired constructor.</param>
    /// <param name="instanceType">
    /// The type of the instance that will be constructed, whose type must be assignable to the return type of
    /// <paramref name="delegateType"/> (e.g. delegate returns Stream, instance must be derived from stream)
    /// </param>
    /// <param name="flags">Binding flags that used when searching for the constructor.</param>
    /// <returns>A delegate that can call the constructor.</returns>
    /// <exception cref="ArgumentException">Incompatible delegate type or delegate type is not a Delegate.</exception>
    /// <remarks>
    /// This is significantly faster than using <see cref="Activator"/> or even calling <c>new T()</c>, by orders of
    /// magnitude.
    /// <para/>
    /// By default only public constructors are searched, use the <see cref="PublicAndPrivate"/> constant for a set of
    /// flags capable of finding public and private constructors.
    /// </remarks>
    /// <see cref="Public"/>
    /// <see cref="PublicAndPrivate"/>
    [Pure]
    public static Delegate Ctor(Type delegateType, Type instanceType, BindingFlags flags = Public)
    {
        var hash = HashCode.Combine(delegateType, instanceType);
        if (constructors.TryGetValue(hash, out var lambda))
            return lambda;
        
        if (!typeof(Delegate).IsAssignableFrom(delegateType))
            throw new ArgumentException($"{delegateType} is not a Delegate type.", nameof(delegateType));
        
        var invoke = delegateType.GetMethod("Invoke") ?? throw new ArgumentException("Invalid delegate type.", nameof(delegateType));
        var parameterTypes = invoke.GetParameters().Select(pi => pi.ParameterType).ToArray();
        
        var resultType = invoke.ReturnType;
        if(!resultType.IsAssignableFrom(instanceType))
            throw new ArgumentException($"Delegate return type ({resultType}) is not assignable from {instanceType}.");
        
        var ctor = instanceType.GetConstructor(flags, null, parameterTypes, null);
        if(ctor is null)
            throw new ArgumentException("Cannot find matching constructor with delegate's signature and specified flags.", nameof(instanceType));
        
        var args = parameterTypes.Select(Expression.Parameter).ToArray();
        
        // False-positive warning issued by Rider/ReSharper, which is not applicable to this scenario.
        // ReSharper disable once CoVariantArrayConversion
        var expr = Expression.Lambda(delegateType, Expression.Convert(Expression.New(ctor, args), resultType), args);
        lambda = expr.Compile();
        constructors.Add(hash, lambda);
        return lambda;
    }
    
    /// <summary>
    /// Finds a constructor of <paramref name="instanceType"/> that has a compatible signature of the given
    /// delegate, and then generates and compiles the IL to call it directly without further use of reflection.
    /// </summary>
    /// <param name="instanceType">
    /// The type of the instance that will be constructed, whose type must be assignable to the return type of the
    /// given delegate (e.g. delegate returns Stream, instance must be derived from stream).
    /// </param>
    /// <param name="flags">Binding flags that used when searching for the constructor.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Incompatible delegate for the <paramref name="instanceType"/>.</exception>
    /// <typeparam name="TDelegate">A delegate type that is compatible with the desired constructor.</typeparam>
    /// <returns>A delegate that can call the constructor.</returns>
    /// <remarks>
    /// This is significantly faster than using <see cref="Activator"/> or even calling <c>new T()</c>, by orders of
    /// magnitude.
    /// <para/>
    /// By default only public constructors are searched, use the <see cref="PublicAndPrivate"/> constant for a set of
    /// flags capable of finding public and private constructors.
    /// </remarks>
    /// <see cref="Public"/>
    /// <see cref="PublicAndPrivate"/>
    [Pure]
    public static TDelegate Ctor<TDelegate>(Type instanceType, BindingFlags flags = Public) where TDelegate : Delegate
    {
        return (TDelegate) Ctor(typeof (TDelegate), instanceType, flags);
    }
}