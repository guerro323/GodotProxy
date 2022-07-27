using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GodotCLR;

public static unsafe class UtilityFunctions
{
    private static bool _loaded;

    private static readonly Dictionary<string, (long hash, bool isVarArg)> Methods = new()
    {
        {"print", (2086509575, true)}
        //{"print", (2648703342, true)}
    };

    public static void Load()
    {
        Debug.Assert(!_loaded, "!_loaded");
        Debug.Assert(
            Native.Interface.variant_get_ptr_utility_function != null,
            "Native.Interface.variant_get_ptr_utility_function != null"
        );

        _loaded = true;

        var getDelegate = Native.Interface.variant_get_ptr_utility_function;

        delegate*unmanaged[Cdecl]<out Variant, Variant**, int, void> Get(string name)
        {
            sbyte* ptr(Utf8Array array)
            {
                return (sbyte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(array.ByteSpanWithNull));
            }
            
            using var utf8 = new Utf8Array(name);
            return (delegate*unmanaged[Cdecl]<out Variant, Variant**, int, void>) getDelegate(
                ptr(utf8),
                Methods[name].hash
            );
        }
        
        print = Get("print");
    }

    private static delegate*unmanaged[Cdecl]<out Variant, Variant**, int, void> print;

    public static void Print(ReadOnlySpan<char> message)
    {
        var arg0 = new Variant();
        arg0.SetString(message);

        var args = stackalloc Variant*[1]
        {
            &arg0
        };
        print(out _, args, 1);
    }
}