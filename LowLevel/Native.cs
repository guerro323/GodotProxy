using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GDNative;

namespace GodotCLR;

public unsafe partial class Native
{
    private static GDNativeInterface* _interface;
    private static void* _library;

    public static ref readonly GDNativeInterface Interface => ref Unsafe.AsRef<GDNativeInterface>(_interface);
    public static IntPtr Library => (IntPtr) _library;

    public static void* GetSingleton(string engineName)
    {
        using var engineNameUtf8 = new Utf8Array(engineName);
        return _interface->global_get_singleton((sbyte*) engineNameUtf8.Pointer);
    }
}