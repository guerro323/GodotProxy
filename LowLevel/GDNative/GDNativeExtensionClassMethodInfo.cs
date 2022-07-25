namespace GDNative
{
    public unsafe partial struct GDNativeExtensionClassMethodInfo
    {
        [NativeTypeName("const char *")]
        public sbyte* name;

        public void* method_userdata;

        [NativeTypeName("GDNativeExtensionClassMethodCall")]
        public delegate* unmanaged[Cdecl]<void*, void*, void**, nint, void*, GDNativeCallError*, void> call_func;

        [NativeTypeName("GDNativeExtensionClassMethodPtrCall")]
        public delegate* unmanaged[Cdecl]<void*, void*, void**, void*, void> ptrcall_func;

        [NativeTypeName("uint32_t")]
        public uint method_flags;

        [NativeTypeName("uint32_t")]
        public uint argument_count;

        [NativeTypeName("GDNativeBool")]
        public byte has_return_value;

        [NativeTypeName("GDNativeExtensionClassMethodGetArgumentType")]
        public delegate* unmanaged[Cdecl]<void*, int, GDNativeVariantType> get_argument_type_func;

        [NativeTypeName("GDNativeExtensionClassMethodGetArgumentInfo")]
        public delegate* unmanaged[Cdecl]<void*, int, GDNativePropertyInfo*, void> get_argument_info_func;

        [NativeTypeName("GDNativeExtensionClassMethodGetArgumentMetadata")]
        public delegate* unmanaged[Cdecl]<void*, int, GDNativeExtensionClassMethodArgumentMetadata> get_argument_metadata_func;

        [NativeTypeName("uint32_t")]
        public uint default_argument_count;

        [NativeTypeName("GDNativeVariantPtr *")]
        public void** default_arguments;
    }
}
