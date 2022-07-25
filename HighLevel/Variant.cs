using System;
using System.Buffers;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Text;
using GodotCLR.NativeStructs;

namespace GodotCLR;

[StructLayout(LayoutKind.Explicit, Size = 16 + sizeof(long) /* Same size as godot_variant */)]
public unsafe struct Variant
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

    public ref bool Bool
    {
        get
        {
            Debug.Assert(Type == EType.BOOL, "Type == EType.BOOL");
            return ref Unsafe.AsRef<bool>(Unsafe.AsPointer(ref Union));
        }
    }
    
    public ref long Int
    {
        get
        {
            Debug.Assert(Type == EType.INT, "Type == EType.INT");
            return ref Unsafe.AsRef<long>(Unsafe.AsPointer(ref Union));
        }
    }
    
    public ref double Float
    {
        get
        {
            Debug.Assert(Type == EType.FLOAT, "Type == EType.FLOAT");
            return ref Unsafe.AsRef<double>(Unsafe.AsPointer(ref Union));
        }
    }
    
    public ref Vector2 Vector2 
    {
        get
        {
            Debug.Assert(Type == EType.VECTOR2, "Type == EType.VECTOR2");
            return ref Unsafe.AsRef<Vector2>(Unsafe.AsPointer(ref Union));
        }
    }
    
    public ref Vector3 Vector3
    {
        get
        {
            Debug.Assert(Type == EType.VECTOR3, "Type == EType.VECTOR3");
            return ref Unsafe.AsRef<Vector3>(Unsafe.AsPointer(ref Union));
        }
    }
    
    public ref Quaternion Quaternion
    {
        get
        {
            Debug.Assert(Type == EType.QUATERNION, "Type == EType.QUATERNION");
            return ref Unsafe.AsRef<Quaternion>(Unsafe.AsPointer(ref Union));
        }
    }

    public IntPtr Object
    {
        get
        {
            Debug.Assert(Type == EType.OBJECT, "Type == EType.OBJECT");
            return Unsafe.AsRef<ObjectData>(Unsafe.AsPointer(ref Union)).Pointer;
        }
        set
        {
            Debug.Assert(Type == EType.OBJECT, "Type == EType.OBJECT");
            ref var data = ref Unsafe.AsRef<ObjectData>(Unsafe.AsPointer(ref Union));
            data.Id = Native.Interface.object_get_instance_id(value.ToPointer());
            data.Pointer = value;
        }
    }

    public struct ObjectData
    {
        public nuint Id;
        public IntPtr Pointer;
    }

    public override string ToString()
    {
        var additional = Type switch
        {
            EType.NIL => "nil",
            EType.BOOL => Bool.ToString(),
            EType.INT => Int.ToString(),
            EType.FLOAT => Float.ToString(CultureInfo.InvariantCulture),
            EType.STRING => this.AsString(),
            EType.VECTOR2 => Vector2.ToString(),
            EType.VECTOR3 => Vector3.ToString(),
            _ => variantToString(Unsafe.AsPointer(ref this))
        };

        string variantToString(void* t)
        {
            godot_string gdString;
            Native.Interface.variant_stringify(t, &gdString);
            Debug.Assert(gdString.Buffer != IntPtr.Zero, "gdString.Buffer != null");

            return gdString.ToString();
        }

        return $"Variant({Type}, {additional})";
    }

    public void SetString(ReadOnlySpan<char> str)
    {
        /*using var utf8 = new Utf8Array(str);

        void* ptr = null;
        Native.Interface.string_new_with_utf8_chars(&ptr, (sbyte*) Unsafe.AsPointer(ref utf8.FirstChar));
        Unsafe.As<byte, IntPtr>(ref Union) = (IntPtr) ptr;*/
        Type = EType.STRING;
        Unsafe.As<byte, godot_string>(ref Union) = new godot_string(str);
    }

    public Variant(ReadOnlySpan<char> span)
    {
        SetString(span);
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