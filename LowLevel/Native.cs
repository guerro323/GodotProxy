using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace GodotCLR
{
    public static unsafe partial class Native
    {
        public static void* Host;

        public static delegate*<void*, delegate*<void>, void> set_update_function;
        public static delegate*<void*, delegate*<void>, void> set_clean_function;
        public static delegate*<char*> get_directory;

        public static class system
        {
            public static delegate*<byte*, void> godot_print;

            public static void Load(ref int index, nuint* methods)
            {
                godot_print = (delegate*<byte*, void>) methods[index++];
            }
        }

        public static class proxy
        {
            public static delegate*<void*, char*, char*, ProxyInternal*> create_proxy;
            public static delegate*<ProxyInternal*, byte*, Variant*, void> set_proxy_property;
            public static delegate*<ProxyInternal*, void> free_proxy;
            public static delegate*<ref ProxyInternal, in Vector2, void> set_position_2d_proxy;
            public static delegate*<ref ProxyInternal, in Vector3, void> set_position_3d_proxy;

            public static void Load(ref int index, nuint* methods)
            {
                create_proxy = (delegate*<void*, char*, char*, ProxyInternal*>) methods[index++];
                set_proxy_property = (delegate*<ProxyInternal*, byte*, Variant*, void>) methods[index++];
                free_proxy = (delegate*<ProxyInternal*, void>) methods[index++];
                set_position_2d_proxy = (delegate*<ref ProxyInternal, in Vector2, void>) methods[index++];
                set_position_3d_proxy = (delegate*<ref ProxyInternal, in Vector3, void>) methods[index++];
            }
        }

        public static class variant
        {
            public static delegate*<Variant*, int, void> godot_variant_new_int;
            public static delegate*<Variant*, Vector2*, void> godot_variant_new_vector2;
            public static delegate*<Variant*, Vector3*, void> godot_variant_new_vector3;
            public static delegate*<Variant*, void> godot_variant_destroy;

            public static void Load(ref int index, nuint* methods)
            {
                godot_variant_new_int = (delegate*<Variant*, int, void>) methods[index++];
                godot_variant_new_vector2 = (delegate*<Variant*, Vector2*, void>) methods[index++];
                godot_variant_new_vector3 = (delegate*<Variant*, Vector3*, void>) methods[index++];
                godot_variant_destroy = (delegate*<Variant*, void>) methods[index++];
            }
        }

        public struct ProxyInternal
        {

        }

        public static string GetDirectory()
        {
            var buffer = new char[256];
            var i = 0;

            var result = get_directory();
            for (; i < buffer.Length; i++)
            {
                if (result[i] == '\0')
                    break;
                buffer[i] = result[i];
            }
            
            return Encoding.UTF8.GetString(MemoryMarshal.Cast<char, byte>(buffer.AsSpan(0, i)));
        }
    }
}