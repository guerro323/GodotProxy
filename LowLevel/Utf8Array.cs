using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace GodotCLR
{
    public readonly struct Utf8Array : IDisposable
    {
        private readonly int _byteCount;
        private readonly byte[] _array;

        public Utf8Array(ReadOnlySpan<char> original)
        {
            var encoding = Encoding.UTF8;

            _byteCount = encoding.GetByteCount(original);
            _array = ArrayPool<byte>.Shared.Rent(_byteCount + 2);
            _array[_byteCount +1] = 0;
            _array[_byteCount +0] = 0;

            encoding.GetBytes(original, _array);
        }

        public Span<char> CharSpan => MemoryMarshal.Cast<byte, char>(ByteSpan);
        public Span<byte> ByteSpan => _array.AsSpan(0, _byteCount);

        public unsafe ref char FirstChar => ref Unsafe.As<byte, char>(ref MemoryMarshal.GetArrayDataReference(_array));
        public unsafe void* Pointer => Unsafe.AsPointer(ref MemoryMarshal.GetArrayDataReference(_array));

        public void Dispose()
        {
            ArrayPool<byte>.Shared.Return(_array, true);
        }
    }
}