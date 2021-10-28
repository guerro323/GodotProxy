using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GodotCLR
{
    [StructLayout(LayoutKind.Explicit, Size = 20 /* Same size as godot_variant */)]
    public unsafe struct Variant : IDisposable
    {
        public void Dispose()
        {
            Native.variant.godot_variant_destroy((Variant*) Unsafe.AsPointer(ref this));
        }

        public static void New(int value, out Variant variant)
        {
            variant = default;
            Native.variant.godot_variant_new_int((Variant*) Unsafe.AsPointer(ref variant), value);
        }

        public static void New(Vector2 value, out Variant variant)
        {
            variant = default;
            Native.variant.godot_variant_new_vector2((Variant*) Unsafe.AsPointer(ref variant), (Vector2*) Unsafe.AsPointer(ref value));
        }

        public static void New(Vector3 value, out Variant variant)
        {
            variant = default;
            Native.variant.godot_variant_new_vector3((Variant*) Unsafe.AsPointer(ref variant), (Vector3*) Unsafe.AsPointer(ref value));
        }
    }
}