using System;
using GodotCLR.HighLevel;

namespace GodotCLR
{
    public unsafe partial class Native
    {
        public static void Load(nuint* ptr,
            delegate*<void> onUpdate, 
            delegate*<void> onClean,
            delegate*<Variant**, int, Variant, int*, Variant*> onExchangeData)
        {
            if (ptr == null)
                throw new InvalidOperationException(nameof(ptr));

            var index = 0;
            Host = (void*) ptr[index++];
            set_update_function = (delegate*<void*, delegate*<void>, void>) ptr[index++];
            set_clean_function = (delegate*<void*, delegate*<void>, void>) ptr[index++];
            set_data_function = (delegate*<
                void*,
                delegate*<Variant**, int, Variant, int*, Variant*>,
                void>) ptr[index++];
            get_directory = (delegate*<byte*>) ptr[index++];

            system.Load(ref index, ptr);
            variant.Load(ref index, ptr);
            proxy.Load(ref index, ptr);

            set_update_function(Host, onUpdate);
            set_clean_function(Host, onClean);
            set_data_function(Host, onExchangeData);
        }
    }
}