using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MauiNativeAotQuirks;

public static class CodeKeeper
{
    public static readonly bool AlwaysFalse = Random.Shared.NextDouble() > 2;
    public static readonly bool AlwaysTrue = !AlwaysFalse;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Keep<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>()
    {
        if (AlwaysTrue) return;

        var type = typeof(T);
        type.GetConstructors();
        type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance);
        type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Keep(
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] string assemblyQualifiedTypeName)
    {
        if (AlwaysTrue) return;

        var type = Type.GetType(assemblyQualifiedTypeName);
        type!.GetConstructors();
        type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance);
        type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Keep(Type type)
    {
        if (AlwaysTrue) return;

        type.GetConstructors();
        type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance);
        type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
    }
}
