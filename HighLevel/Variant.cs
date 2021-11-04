using System;
using System.Buffers;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace GodotCLR;

[StructLayout(LayoutKind.Explicit, Size = 16 + sizeof(long) /* Same size as godot_variant */)]
public unsafe struct Variant : IDisposable
{
    public enum EType
    {
        NIL,

        // atomic types
        BOOL,
        INT,
        FLOAT,
        STRING,

        // math types
        VECTOR2,
        VECTOR2I,
        RECT2,
        RECT2I,
        VECTOR3,
        VECTOR3I,
        TRANSFORM2D,
        PLANE,
        QUATERNION,
        AABB,
        BASIS,
        TRANSFORM3D,

        // misc types
        COLOR,
        STRING_NAME,
        NODE_PATH,
        RID,
        OBJECT,
        CALLABLE,
        SIGNAL,
        DICTIONARY,
        ARRAY,

        // typed arrays
        PACKED_BYTE_ARRAY,
        PACKED_INT32_ARRAY,
        PACKED_INT64_ARRAY,
        PACKED_FLOAT32_ARRAY,
        PACKED_FLOAT64_ARRAY,
        PACKED_STRING_ARRAY,
        PACKED_VECTOR2_ARRAY,
        PACKED_VECTOR3_ARRAY,
        PACKED_COLOR_ARRAY,

        VARIANT_MAX
    }

    [FieldOffset(0)] public EType Type;

    [FieldOffset(8)] public byte Union;

    public override string ToString()
    {
        var additional = Type switch
        {
            EType.NIL => "null",
            EType.BOOL => this.AsBool().ToString(),
            EType.INT => this.AsInt().ToString(),
            EType.FLOAT => this.AsReal().ToString(CultureInfo.InvariantCulture),
            EType.STRING => this.AsString(),
            EType.VECTOR2 => this.AsVector2().ToString(),
            EType.VECTOR3 => this.AsVector3().ToString(),
            _ => "<<not implemented>>"
        };

        return $"Variant({Type}, {additional})";
    }

    public void Dispose()
    {
        Native.variant.godot_variant_destroy((Variant*) Unsafe.AsPointer(ref this));
    }

    public static void New(long value, out Variant variant)
    {
        variant = default;
        Native.variant.godot_variant_new_int((Variant*) Unsafe.AsPointer(ref variant), value);
    }

    public static void New(double value, out Variant variant)
    {
        variant = default;
        Native.variant.godot_variant_new_real((Variant*) Unsafe.AsPointer(ref variant), value);
    }

    public static void New(ReadOnlySpan<char> str, out Variant variant)
    {
        using var utf8 = new Utf8Array(str);

        variant = default;
        Native.variant.godot_variant_new_string((Variant*) Unsafe.AsPointer(ref variant),
            (char*) Unsafe.AsPointer(ref utf8.FirstChar));
    }

    public static void New(Vector2 value, out Variant variant)
    {
        variant = default;
        Native.variant.godot_variant_new_vector2((Variant*) Unsafe.AsPointer(ref variant),
            (Vector2*) Unsafe.AsPointer(ref value));
    }

    public static void New(Vector3 value, out Variant variant)
    {
        variant = default;
        Native.variant.godot_variant_new_vector3((Variant*) Unsafe.AsPointer(ref variant),
            (Vector3*) Unsafe.AsPointer(ref value));
    }
}

public static class VariantExtension
{
    public record struct PooledCharArray(char[] Chars, int Length) : IDisposable
    {
        public Span<char> AsSpan()
        {
            return Chars.AsSpan(0, Length);
        }

        public void Dispose()
        {
            ArrayPool<char>.Shared.Return(Chars);
        }
    }

    public static bool AsBool(this Variant variant)
    {
        return Unsafe.As<byte, bool>(ref variant.Union);
    }

    public static long AsInt(this Variant variant)
    {
        return Unsafe.As<byte, int>(ref variant.Union);
    }

    public static double AsReal(this Variant variant)
    {
        return Unsafe.As<byte, double>(ref variant.Union);
    }

    public static Vector2 AsVector2(this Variant variant)
    {
        return Unsafe.As<byte, Vector2>(ref variant.Union);
    }

    public static Vector3 AsVector3(this Variant variant)
    {
        return Unsafe.As<byte, Vector3>(ref variant.Union);
    }

    // from a 32bit string
    // ref: https://github.com/godotengine/godot/blob/9e3733612461d9cf9edb19ab29244b0834e2d1b1/core/string/ustring.h#L183
    public static unsafe PooledCharArray AsPooledCharArray(this Variant variant)
    {
        Span<byte> GetSpan<T>()
            where T : unmanaged
        {
            static bool IsZero(ref T address)
            {
                var size = Unsafe.SizeOf<T>();
                var ptr = (byte*) Unsafe.AsPointer(ref address);

                for (var i = 0; i < size; i++)
                {
                    if (ptr[i] != 0)
                        return false;
                }

                return true;
            }

            ref var ptr = ref Unsafe.AsRef<T>((void*) Unsafe.As<byte, IntPtr>(ref variant.Union));
            var size = 0;
            while (true)
            {
                // null terminated
                if (IsZero(ref Unsafe.Add(ref ptr, size++)))
                    break;
            }

            if (size <= 1)
                return Span<byte>.Empty;

            return MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref ptr, size));
        }

        // It seems that on windows it's completely different.
        // If we use int we need to use the unicode encoding, but then we can still have some errors left.
        // If we use int with UTF8 then we get garbage;
        // If we use short, then we have the same result as using int on Linux / Unix systems (but we still need to use
        // the Unicode encoding..........)
        // ????????????????????????????????????????????????????????????????????????????????????????????
        var span = OperatingSystem.IsWindows()
            ? GetSpan<short>()[..^2] // skip that garbage at the end
            : GetSpan<int>()[..^4]; // skip that garbage at the end (int size)

        var disposable = new PooledCharArray(ArrayPool<char>.Shared.Rent(span.Length), span.Length);

        var size = span.IsEmpty
            ? default
            : (OperatingSystem.IsWindows()
                ? Encoding.Unicode
                : Encoding.UTF32
            ).GetChars(span, disposable.AsSpan());

        return disposable with {Length = size};
    }

    // from a 32bit string
    // ref: https://github.com/godotengine/godot/blob/9e3733612461d9cf9edb19ab29244b0834e2d1b1/core/string/ustring.h#L183
    public static unsafe string AsString(this Variant variant)
    {
        using var chars = AsPooledCharArray(variant);
        return new string(chars.AsSpan());
    }

    public static bool Contains(this Variant variant, ReadOnlySpan<char> str)
    {
        using var chars = AsPooledCharArray(variant);
        return chars.AsSpan().IndexOf(str) >= 0;
    }

    public static bool StartsWith(this Variant variant, ReadOnlySpan<char> str)
    {
        using var chars = AsPooledCharArray(variant);
        return chars.AsSpan().StartsWith(str);
    }

    public static bool SequenceEquals(this Variant variant, ReadOnlySpan<char> str)
    {
        using var chars = AsPooledCharArray(variant);
        return chars.AsSpan().SequenceEqual(str);
    }
}