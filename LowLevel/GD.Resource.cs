using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GodotCLR.NativeStructs;

namespace GodotCLR;

public partial class GD
{
    public interface IResource : IRefCounted
    {

    }

    public unsafe struct Resource : IResource
    {
        public IntPtr Pointer { get; }

        private const string ClassName = "Resource";

        private static void* duplicate;
        private static void* set_name;

        public Resource(IntPtr ptr)
        {
            Pointer = ptr;
        }

        public static void SetName(IntPtr ptr, ReadOnlySpan<char> span)
        {
            Debug.Assert(ptr != IntPtr.Zero, "Pointer != IntPtr.Zero");

            using var spanGdString = new godot_string(span);
            InvokePtr<IntPtr>(set_name, (void*) ptr, stackalloc nuint[]
            {
                (nuint) (&spanGdString)
            });
        }

        public static Resource Duplicate(IntPtr ptr, bool subResources = false)
        {
            Debug.Assert(ptr != IntPtr.Zero, "Pointer != IntPtr.Zero");

            var bSubResources = (byte) (subResources ? 1 : 0);
            return new Resource
            (
                InvokePtr<IntPtr>(duplicate, (void*) ptr, stackalloc nuint[]
                {
                    (nuint) (&bSubResources),
                })
            );
        }

        internal static void GdLoad()
        {
            duplicate = GetMethodBind(ClassName, nameof(duplicate), 172413545);
            set_name = GetMethodBind(ClassName, nameof(set_name), 134188166);
        }
    }
}

public static class ResourceExtension
{
    public static void SetName<T>(this T t, ReadOnlySpan<char> name)
        where T : struct, GD.IResource
    {
        GD.Resource.SetName(t.Pointer, name);
    }

    public static T Duplicate<T>(this T t, bool subResources = false)
        where T : struct, GD.IResource
    {
        var ret = GD.Resource.Duplicate(t.Pointer, subResources);
        return Unsafe.As<GD.Resource, T>(ref ret);
    }

    public static TTo To<TTo>(this GD.Resource t)
        where TTo : struct, GD.IResource
    {
        return Unsafe.As<GD.Resource, TTo>(ref t);
    }
}