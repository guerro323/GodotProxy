using System;
using System.Runtime.CompilerServices;

namespace GodotCLR.HighLevel
{
    public unsafe struct VariantMethodArgs
    {
        public Variant** Native;

        public int Length;

        public ref Variant this[int index]
        {
            get
            {
                if (index < 0 || index >= Length)
                    throw new IndexOutOfRangeException();

                return ref Unsafe.AsRef<Variant>(Native[index]);
            }
        }
    }
}