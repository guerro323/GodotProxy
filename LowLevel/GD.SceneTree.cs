using System;

namespace GodotCLR;

public partial class GD
{
    public static unsafe class SceneTree
    {
        private const string ClassName = "SceneTree";

        private static void* get_current_scene;
        
        public static IntPtr GetCurrentScene(IntPtr sceneTreePtr)
        {
            return InvokePtr<IntPtr>(get_current_scene, sceneTreePtr.ToPointer(), null);
        }

        internal static void GdLoad()
        {
            get_current_scene = GetMethodBind(ClassName, nameof(get_current_scene), 135338183);
        }
    }
}