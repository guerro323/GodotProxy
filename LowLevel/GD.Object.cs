using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GodotCLR.NativeStructs;

namespace GodotCLR;

public partial class GD
{
    public interface IObject : IHasPointer
    {
    }

    public static unsafe class Object
    {
        private const string ClassName = "Object";

        private static void* call_deferred;

        public static Variant CallDeferred(IntPtr objectPtr, ReadOnlySpan<char> method, ReadOnlySpan<Variant> variants)
        {
            Debug.Assert(objectPtr != IntPtr.Zero, "objectPtr != IntPtr.Zero");

            Span<Variant> span = stackalloc Variant[variants.Length + 1];
            span[0].SetString(method);
            variants.CopyTo(span[1..]);
            
            return InvokeVariant(call_deferred, (void*) objectPtr, span, out _);
        }

        internal static void GdLoad()
        {
            call_deferred = GetMethodBind(ClassName, nameof(call_deferred), 135374088);
        }
    }
}

public static class ObjectExtension
{
    public static Variant CallDeferred<T>(this T t, ReadOnlySpan<char> method, ReadOnlySpan<Variant> variants)
        where T : GD.IObject
    {
        return GD.Object.CallDeferred(t.Pointer, method, variants);
    }
}