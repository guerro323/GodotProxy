namespace GDNative
{
    public unsafe partial struct GDNativePropertyInfo
    {
        [NativeTypeName("uint32_t")]
        public uint type;

        [NativeTypeName("const char *")]
        public sbyte* name;

        [NativeTypeName("const char *")]
        public sbyte* class_name;

        [NativeTypeName("uint32_t")]
        public uint hint;

        [NativeTypeName("const char *")]
        public sbyte* hint_string;

        [NativeTypeName("uint32_t")]
        public uint usage;
    }
}
