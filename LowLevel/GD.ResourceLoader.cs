using System;
using System.Runtime.CompilerServices;
using GodotCLR.NativeStructs;

namespace GodotCLR;

public partial class GD
{
    public static unsafe class ResourceLoader
    {
        public enum CacheMode
        {
            Ignore = 0,
            Reuse = 1,
            Replace = 2
        }
        
        private const string ClassName = "ResourceLoader";
        
        private static void* singleton;
        private static void* load;
        
        public static Resource Load(
            ReadOnlySpan<char> path, 
            ReadOnlySpan<char> typeHint = default,
            CacheMode cacheMode = CacheMode.Ignore)
        {
            using var pathGdString = new godot_string(path);
            using var typeHintGdString = new godot_string(typeHint);
            return new Resource(InvokePtr<IntPtr>(load, singleton, stackalloc nuint[]
            {
                (nuint) (&pathGdString),
                (nuint) (&typeHintGdString),
                (nuint) (&cacheMode)
            }));
        }

        internal static void GdLoad()
        {
            singleton = Native.GetSingleton(nameof(ResourceLoader));
            load = GetMethodBind(ClassName, nameof(load), 1479995216);
        }
    }
}