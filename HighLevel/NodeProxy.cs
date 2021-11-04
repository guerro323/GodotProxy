using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using GodotCLR;
using GodotCLR.HighLevel;
using Proxy = GodotCLR.Godot.Proxy;

namespace RustTest
{
    public unsafe struct NodeProxy : IDisposable
    {
        private Native.ProxyInternal* _internal;

        public bool IsCreated { get; }

        public NodeProxy(string name, string resource)
        {
            IsCreated = true;

            _internal = (Native.ProxyInternal*) Unsafe.AsPointer(ref Proxy.CreateProxy(name, resource));
        }

        public void SetPosition2D(in Vector2 vector)
        {
            Proxy.SetPosition2D(ref *_internal, vector);
        }
        
        public void SetPosition3D(in Vector3 vector)
        {
            Proxy.SetPosition3D(ref *_internal, vector);
        }

        public Variant Call(string name, VariantArgBuilder args = default, bool autoDispose = true)
        {
            var span = args.AsSpan();
            var result = Proxy.CallProxyMethod(ref *_internal, name, span);

            if (autoDispose)
            {
                foreach (var variant in span)
                    variant.Dispose();
            }

            return result;
        }

        public void SetProperty(string name, ref Variant variant, bool autoDispose = true)
        {
            Proxy.SetProxyProperty(ref *_internal, name, ref variant);
            if (autoDispose)
                variant.Dispose();
        }

        public void SetProperty(string name, int value)
        {
            Variant.New(value, out var variant);
            SetProperty(name, ref variant);
        }

        public void SetProperty(string name, Vector2 value)
        {
            Variant.New(value, out var variant);
            SetProperty(name, ref variant);
        }

        public void SetProperty(string name, Vector3 value)
        {
            Variant.New(value, out var variant);
            SetProperty(name, ref variant);
        }

        public void Dispose()
        {
            if (!IsCreated)
                throw new InvalidOperationException(nameof(IsCreated));

            Proxy.FreeProxy(_internal);
        }
    }
}