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
            Variant.New(value, out var variant);
            Add(variant);
        }

        public void Add(double value)
        {
            Variant.New(value, out var variant);
            Add(variant);
        }
        
        public void Add(string value)
        {
            Variant.New(value, out var variant);
            Add(variant);
        }
        
        public void Add(Vector2 value)
        {
            Variant.New(value, out var variant);
            Add(variant);
        }
        
        public void Add(Vector3 value)
        {
            Variant.New(value, out var variant);
            Add(variant);
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