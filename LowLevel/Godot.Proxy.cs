using System;
using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using static GodotCLR.Native;

namespace GodotCLR
{
    public unsafe partial class Godot
    {
        public static class Proxy
        {
            public static ref ProxyInternal CreateProxy(ReadOnlySpan<char> name, ReadOnlySpan<char> resource)
            {
                var nameArray = ArrayPool<byte>.Shared.Rent(name.Length);
                var resourceArray = ArrayPool<byte>.Shared.Rent(resource.Length);
                try
                {
                    Span<byte> nameOutput = nameArray.AsSpan(0, name.Length);
                    Span<byte> resourceOutput = resourceArray.AsSpan(0, resource.Length);
                    Encoding.UTF8.GetBytes(name, nameOutput);
                    Encoding.UTF8.GetBytes(resource, resourceOutput);
                    return ref *proxy.create_proxy(Host, to_ptr(MemoryMarshal.Cast<byte, char>(nameOutput)),
                        to_ptr(MemoryMarshal.Cast<byte, char>(resourceOutput)));
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(nameArray);
                    ArrayPool<byte>.Shared.Return(resourceArray);
                }
            }

            public static void SetProxyProperty(ref ProxyInternal proxyObj, ReadOnlySpan<char> path, ref Variant value)
            {
                var proxyPtr = (ProxyInternal*) Unsafe.AsPointer(ref proxyObj);
                var variantPtr = (Variant*) Unsafe.AsPointer(ref value);
                if (path.Length < 128)
                {
                    Span<byte> output = stackalloc byte[path.Length];
                    Encoding.UTF8.GetBytes(path, output);
                    proxy.set_proxy_property(proxyPtr, to_ptr(output), variantPtr);
                    return;
                }

                var array = ArrayPool<byte>.Shared.Rent(path.Length);
                try
                {
                    Span<byte> output = array.AsSpan(0, path.Length);
                    Encoding.UTF8.GetBytes(path, output);
                    proxy.set_proxy_property(proxyPtr, to_ptr(output), variantPtr);
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(array);
                }
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
    }
}