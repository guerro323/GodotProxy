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
        private static void* call;
        private static void* set;
        private static void* get;

        public static Variant CallDeferred(IntPtr objectPtr, ReadOnlySpan<char> method, ReadOnlySpan<Variant> variants)
        {
            Debug.Assert(objectPtr != IntPtr.Zero, "objectPtr != IntPtr.Zero");

            Span<Variant> span = stackalloc Variant[variants.Length + 1];
            span[0].SetString(method);
            variants.CopyTo(span[1..]);
            
            return InvokeVariant(call_deferred, (void*) objectPtr, span, out _);
        }
        
        public static Variant Call(IntPtr objectPtr, ReadOnlySpan<char> method, ReadOnlySpan<Variant> variants)
        {
            Debug.Assert(objectPtr != IntPtr.Zero, "objectPtr != IntPtr.Zero");

            Span<Variant> span = stackalloc Variant[variants.Length + 1];
            span[0].SetString(method);
            variants.CopyTo(span[1..]);
            
            return InvokeVariant(call, (void*) objectPtr, span, out _);
        }

        public static void Set(IntPtr objectPtr, ReadOnlySpan<char> property, Variant variant)
        {
            Debug.Assert(objectPtr != IntPtr.Zero, "objectPtr != IntPtr.Zero");

            Span<Variant> span = stackalloc Variant[2];
            span[0].SetString(property);
            span[1] = variant;
            
            InvokeVariant(call, (void*) objectPtr, span, out _);
        }
        
        public static Variant Get(IntPtr objectPtr, ReadOnlySpan<char> property)
        {
            Debug.Assert(objectPtr != IntPtr.Zero, "objectPtr != IntPtr.Zero");

            Span<Variant> span = stackalloc Variant[1];
            span[0].SetString(property);

            return InvokeVariant(call, (void*) objectPtr, span, out _);
        }

        internal static void GdLoad()
        {
            call_deferred = GetMethodBind(ClassName, nameof(call_deferred), 135374088);
            call = GetMethodBind(ClassName, nameof(call), 135374088);
            set = GetMethodBind(ClassName, nameof(set), 134224103);
            get = GetMethodBind(ClassName, nameof(get), 135374120);
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
    
    public static Variant Call<T>(this T t, ReadOnlySpan<char> method, ReadOnlySpan<Variant> variants)
        where T : GD.IObject
    {
        return GD.Object.Call(t.Pointer, method, variants);
    }
    
    public static void SetProperty<T>(this T t, ReadOnlySpan<char> property, Variant variant)
        where T : GD.IObject
    {
        GD.Object.Set(t.Pointer, property, variant);
    }
    
    public static Variant SetProperty<T>(this T t, ReadOnlySpan<char> property)
        where T : GD.IObject
    {
        return GD.Object.Get(t.Pointer, property);
    }
}