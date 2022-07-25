using System;
using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using static GodotCLR.Native;

namespace GodotCLR
{
    /*public unsafe partial class Godot
    {
        public static class Proxy
        {
            public static ref ProxyInternal CreateProxy(ReadOnlySpan<char> name, ReadOnlySpan<char> resource)
            {
                using var nameArray = new Utf8Array(name);
                using var resourceArray = new Utf8Array(resource);
                return ref *proxy.create_proxy(Host, to_ptr(nameArray.CharSpan), to_ptr(resourceArray.CharSpan));
            }

            public static void SetProxyProperty(ref ProxyInternal proxyObj, ReadOnlySpan<char> path, ref Variant value)
            {
                var proxyPtr = (ProxyInternal*) Unsafe.AsPointer(ref proxyObj);
                var variantPtr = (Variant*) Unsafe.AsPointer(ref value);

                using var pathArray = new Utf8Array(path);
                proxy.set_proxy_property(proxyPtr, to_ptr(pathArray.ByteSpan), variantPtr);
            }

            public static Variant CallProxyMethod(ref ProxyInternal proxyObj, ReadOnlySpan<char> path, Span<Variant> args)
            {
                var proxyPtr = (ProxyInternal*) Unsafe.AsPointer(ref proxyObj);
                var variantPtr = (Variant*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(args));

                using var pathArray = new Utf8Array(path);
                return proxy.call_proxy_method(proxyPtr, to_ptr(pathArray.ByteSpan), args.Length, variantPtr);
            }

            public static void FreeProxy(ProxyInternal* proxyObj)
            {
                proxy.free_proxy(proxyObj);
            }

            public static void SetPosition2D(ref ProxyInternal proxyInternal, in Vector2 vector)
            {
                proxy.set_position_2d_proxy(ref proxyInternal, in vector);
            }
            
            public static void SetPosition3D(ref ProxyInternal proxyInternal, in Vector3 vector)
            {
                proxy.set_position_3d_proxy(ref proxyInternal, in vector);
            }
        }
    }*/
}