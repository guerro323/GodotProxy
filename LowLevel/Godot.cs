using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using static GodotCLR.Native;

namespace GodotCLR
{
    public static unsafe partial class Godot
    {
        private static T* to_ptr<T>(Span<T> span) where T : unmanaged =>
            (T*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(span));

        private static T* to_ptr<T>(ReadOnlySpan<T> span) where T : unmanaged =>
            (T*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(span));


        public static void Print(ReadOnlySpan<char> span)
        {
            using var utf8 = new Utf8Array(span);
            system.godot_print(to_ptr(utf8.ByteSpan));
        }
    }
}