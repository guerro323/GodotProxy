namespace GDNative
{
    public unsafe partial struct GDNativeMethodInfo
    {
        [NativeTypeName("const char *")]
        public sbyte* name;

        public GDNativePropertyInfo return_value;

        [NativeTypeName("uint32_t")]
        public uint flags;

        [NativeTypeName("int32_t")]
        public int id;

        public GDNativePropertyInfo* arguments;

        [NativeTypeName("uint32_t")]
        public uint argument_count;

        [NativeTypeName("GDNativeVariantPtr")]
        public void* default_arguments;

        [NativeTypeName("uint32_t")]
        public uint default_argument_count;
    }
}
