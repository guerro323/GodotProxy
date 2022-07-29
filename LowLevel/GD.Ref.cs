using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GodotCLR.NativeStructs;

namespace GodotCLR;

public partial class GD
{
    public interface IRefCounted : IHasPointer
    {
        
    }
    
    public static unsafe class Ref
    {
        private const string ClassName = "RefCounted";

        private static void* reference;
        private static void* unreference;
        
        public static bool Reference(IntPtr pointer)
        {
            Debug.Assert(pointer != IntPtr.Zero, "Pointer != IntPtr.Zero");

            return InvokePtr<byte>(reference, (void*) pointer, null) != 0;
        }
        
        public static bool Unreference(IntPtr pointer)
        {
            Debug.Assert(pointer != IntPtr.Zero, "Pointer != IntPtr.Zero");

            return InvokePtr<byte>(unreference, (void*) pointer, null) != 0;
        }

        internal static void GdLoad()
        {
            reference = GetMethodBind(ClassName, nameof(reference), 2240911060);
            unreference = GetMethodBind(ClassName, nameof(unreference), 2240911060);
        }
    }

    public static bool Reference<T>(this T t)
        where T : IRefCounted
    {
        return Ref.Reference(t.Pointer);
    }
    
    public static bool Unreference<T>(this T t)
        where T : IRefCounted
    {
        return Ref.Unreference(t.Pointer);
    }
}