using System;

namespace GodotCLR
{
    public unsafe partial class Native
    {
        public static void Load(nuint* ptr, delegate*<void> onUpdate, delegate*<void> onClean)
        {
            if (ptr == null)
                throw new InvalidOperationException(nameof(ptr));

            var index = 0;
            Host = (void*) ptr[index++];
            set_update_function = (delegate*<void*, delegate*<void>, void>) ptr[index++];
            set_clean_function = (delegate*<void*, delegate*<void>, void>) ptr[index++];
            get_directory = (delegate*<char*>) ptr[index++];

            system.Load(ref index, ptr);
            variant.Load(ref index, ptr);
            proxy.Load(ref index, ptr);

            set_update_function(Host, onUpdate);
            set_clean_function(Host, onClean);
        }
    }
}