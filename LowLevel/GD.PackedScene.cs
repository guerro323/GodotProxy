using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GodotCLR.NativeStructs;

namespace GodotCLR;

public partial class GD
{
    public interface IPackedScene : IResource
    {
        
    }
    
    public unsafe struct PackedScene : IPackedScene
    {
        public enum GenEditState
        {
            Disabled = 0,
            Instance = 1,
            Main = 2,
            Inherited = 3
        }

        public IntPtr Pointer { get; }

        private const string ClassName = "PackedScene";

        private static void* instantiate;

        public PackedScene(IntPtr ptr)
        {
            Pointer = ptr;
        }
        
        public static Node Instantiate(IntPtr ptr, GenEditState editState)
        {
            Debug.Assert(ptr != IntPtr.Zero, "Pointer != IntPtr.Zero");
            
            return new Node
            (
                InvokePtr<IntPtr>(instantiate, (void*) ptr, stackalloc nuint[]
                {
                    (nuint) (&editState),
                })
            );
        }

        internal static void GdLoad()
        {
            instantiate = GetMethodBind(ClassName, nameof(instantiate), 2628778455);
        }
    }
}

public static class PackedSceneExtension
{
    public static GD.Node Instantiate<T>(this T t, GD.PackedScene.GenEditState editState = GD.PackedScene.GenEditState.Disabled)
        where T : GD.IPackedScene
    {
        return GD.PackedScene.Instantiate(t.Pointer, editState);
    }
}