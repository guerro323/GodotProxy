using System;

namespace GodotCLR;

public partial class GD
{
    public static unsafe class Engine
    {
        private const string ClassName = "Engine";

        private static void* singleton;
        private static void* get_main_loop;
        
        public static IntPtr GetMainLoop()
        {
            return InvokePtr<IntPtr>(get_main_loop, singleton, null);
        }

        internal static void GdLoad()
        {
            singleton = Native.GetSingleton(nameof(Engine));
            get_main_loop = GetMethodBind(ClassName, nameof(get_main_loop), 1016888095);
        }
    }
}