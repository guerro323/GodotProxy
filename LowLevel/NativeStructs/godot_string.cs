using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GodotCLR.NativeStructs;

[StructLayout(LayoutKind.Sequential)]
// ReSharper disable once InconsistentNaming
public unsafe struct godot_string : IDisposable
{
    private IntPtr _ptr;

    public godot_string(ReadOnlySpan<char> span)
    {
        if (span == default)
        {
            this = default;
            return;
        }
        
        Native.Interface.string_new_with_utf16_chars(
            Unsafe.AsPointer(ref this),
            (ushort*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(span))
        );
    }

    public void Dispose()
    {
        if (_ptr == IntPtr.Zero)
            return;
        Native.Interface.mem_free((void*) _ptr);
        _ptr = IntPtr.Zero;
    }

    public readonly IntPtr Buffer
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _ptr;
    }

    // Size including the null termination character
    public readonly unsafe int Size
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _ptr != IntPtr.Zero ? *((int*)_ptr - 1) : 0;
    }

    public override string ToString()
    {
        if (Buffer == IntPtr.Zero)
            return string.Empty;

        const int sizeOfChar32 = 4;
        byte* bytes = (byte*)Buffer;
        int size = Size;
        if (size == 0)
            return string.Empty;
        size -= 1; // zero at the end
        int sizeInBytes = size * sizeOfChar32;
        return System.Text.Encoding.UTF32.GetString(bytes, sizeInBytes);
    }
}