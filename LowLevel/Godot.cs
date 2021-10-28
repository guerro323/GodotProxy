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
            if (span.Length < 128)
            {
                Span<byte> output = stackalloc byte[span.Length];
                Encoding.UTF8.GetBytes(span, output);
                system.godot_print(to_ptr(output));

                return;
            }

            var array = ArrayPool<byte>.Shared.Rent(span.Length);
            try
            {
                Span<byte> output = array.AsSpan(0, span.Length);
                Encoding.UTF8.GetBytes(span, output);
                system.godot_print(to_ptr(output));
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(array);
            }
        }
    }
}