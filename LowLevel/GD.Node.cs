using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GodotCLR.NativeStructs;

namespace GodotCLR;

public partial class GD
{
    public interface INode : IObject
    {
    }

    public unsafe struct Node : INode
    {
        public enum InternalMode
        {
            Disabled = 0,
            Font = 1,
            Back = 2
        }

        public IntPtr Pointer { get; }

        private const string ClassName = "Node";

        private static void* add_child;
        private static void* set_name;

        public Node(IntPtr ptr)
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

        public static void AddChild(IntPtr ptr, IntPtr node, bool legitUniqueName = false,
            InternalMode internalMode = default)
        {
            Debug.Assert(ptr != IntPtr.Zero, "Pointer != IntPtr.Zero");
            Debug.Assert(node != IntPtr.Zero, "node.Pointer != IntPtr.Zero");

            var bLegitUniqueName = (byte) (legitUniqueName ? 1 : 0);
            InvokePtr<IntPtr>(add_child, (void*) ptr, stackalloc nuint[]
            {
                (nuint) node.ToPointer(),
                (nuint) (&bLegitUniqueName),
                (nuint) (&internalMode)
            });
            /*InvokeVariant(add_child, (void*) ptr, stackalloc[]
            {
                new Variant {Type = Variant.EType.OBJECT, Object = node},
                new Variant {Type = Variant.EType.BOOL, Bool = legitUniqueName},
                new Variant {Type = Variant.EType.INT, Int = (long) internalMode},
            }, out _);*/
        }

        internal static void GdLoad()
        {
            add_child = GetMethodBind(ClassName, nameof(add_child), 182667338);
            set_name = GetMethodBind(ClassName, nameof(set_name), 134188166);
        }
    }
}

public static class NodeExtension
{
    public static void SetName<T>(this T t, ReadOnlySpan<char> name)
        where T : GD.INode
    {
        GD.Node.SetName(t.Pointer, name);
    }

    public static void AddChild<T, TNode>(this T t, TNode node, bool legitUniqueName = false,
        GD.Node.InternalMode internalMode = default)
        where T : GD.INode
        where TNode : GD.INode
    {
        GD.Node.AddChild(t.Pointer, node.Pointer, legitUniqueName, internalMode);
    }

    public static TTo To<TTo>(this GD.Node t)
        where TTo : struct, GD.INode
    {
        return Unsafe.As<GD.Node, TTo>(ref t);
    }
}