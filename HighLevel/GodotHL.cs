using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GodotCLR.HighLevel
{
    public static unsafe class GodotHL
    {
        public delegate void Exchange(Variant subject, VariantMethodArgs args, VariantArgBuilder ret);

        private static Action _onUpdate;
        private static Action _onClean;
        private static Exchange _onExchange;

        public static void Load(IntPtr ptr, Action onUpdate, Action onClean, Exchange onExchange)
        {
            _onUpdate = onUpdate;
            _onClean = onClean;
            _onExchange = onExchange;
            
            Native.Load((nuint*) ptr, &UpdateCore, &CleanCore, &ExchangeCore);
        }
        
        private static void UpdateCore()
        {
            _onUpdate();
        }

        private static void CleanCore()
        {
            _onClean();
        }
        
        private static unsafe Variant* ExchangeCore(Variant** args, int argLength, Variant subject, int* retLength)
        {
            var builder = new VariantArgBuilder();

            _onExchange(
                subject,
                new VariantMethodArgs {Length = argLength, Native = args},
                builder
            );

            var span = builder.AsSpan();
            var count = span.Length;
            
            Unsafe.Copy(retLength, ref count);

            return (Variant*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(span));
        }
    }
}