using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;

namespace GodotCLR.HighLevel
{
    public struct VariantArgBuilder : IEnumerable<Variant>
    {
        [ThreadStatic] private static List<Variant> _list = new();

        public VariantArgBuilder()
        {
            _list.Clear();
        }
        
        public void Add(Variant variant)
        {
            _list ??= new List<Variant>();
            _list.Add(variant);
        }

        public void Add(long value)
        {
            Add(new Variant {Type = Variant.EType.INT, Int = value});
        }

        public void Add(double value)
        {
            Add(new Variant {Type = Variant.EType.FLOAT, Float = value});
        }
        
        public void Add(string value)
        {
            var variant = new Variant {Type = Variant.EType.STRING};
            variant.SetString(value);
            Add(variant);
        }
        
        public void Add(Vector2 value)
        {
            Add(new Variant {Type = Variant.EType.VECTOR2, Vector2 = value});
        }
        
        public void Add(Vector3 value)
        {
            Add(new Variant {Type = Variant.EType.VECTOR3, Vector3 = value});
        }

        public Span<Variant> AsSpan()
        {
            return CollectionsMarshal.AsSpan(_list);
        }

        IEnumerator<Variant> IEnumerable<Variant>.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}