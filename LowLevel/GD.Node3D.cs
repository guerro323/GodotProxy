using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GodotCLR.NativeStructs;

namespace GodotCLR;

public partial class GD
{
    public interface INode3D : INode
    {
    }

    public unsafe struct Node3D : INode3D
    {
        public IntPtr Pointer { get; }

        private const string ClassName = "Node3D";

        private static void* get_position;
        private static void* set_position;

        public Node3D(IntPtr ptr)
        {
            Pointer = ptr;
        }

        public static Vector3 GetPosition(IntPtr ptr)
        {
            Debug.Assert(ptr != IntPtr.Zero, "Pointer != IntPtr.Zero");
            
            return InvokePtr<Vector3>(get_position, (void*) ptr, null);
        }
        
        public static void SetPosition(IntPtr ptr, Vector3 position)
        {
            Debug.Assert(ptr != IntPtr.Zero, "Pointer != IntPtr.Zero");
            
            InvokePtr<IntPtr>(set_position, (void*) ptr, stackalloc nuint[]
            {
                (nuint) (&position)
            });
        }

        internal static void GdLoad()
        {
            get_position = GetMethodBind(ClassName, nameof(get_position), 135338183);
            set_position = GetMethodBind(ClassName, nameof(set_position), 134188166);
        }
    }
}

public static class Node3DExtension
{
    public static Vector3 GetPosition<T>(this T t)
        where T : GD.INode3D
    {
        return GD.Node3D.GetPosition(t.Pointer);
    }
    
    public static void SetPosition<T>(this T t, Vector3 position)
        where T : GD.INode3D
    {
        GD.Node3D.SetPosition(t.Pointer, position);
    }
}