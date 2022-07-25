namespace GDNative
{
    public unsafe partial struct GDNativeExtensionClassCreationInfo
    {
        [NativeTypeName("GDNativeExtensionClassSet")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte> set_func;

        [NativeTypeName("GDNativeExtensionClassGet")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte> get_func;

        [NativeTypeName("GDNativeExtensionClassGetPropertyList")]
        public delegate* unmanaged[Cdecl]<void*, uint*, GDNativePropertyInfo*> get_property_list_func;

        [NativeTypeName("GDNativeExtensionClassFreePropertyList")]
        public delegate* unmanaged[Cdecl]<void*, GDNativePropertyInfo*, void> free_property_list_func;

        [NativeTypeName("GDNativeExtensionClassNotification")]
        public delegate* unmanaged[Cdecl]<void*, int, void> notification_func;

        [NativeTypeName("GDNativeExtensionClassToString")]
        public delegate* unmanaged[Cdecl]<void*, void*, void> to_string_func;

        [NativeTypeName("GDNativeExtensionClassReference")]
        public delegate* unmanaged[Cdecl]<void*, void> reference_func;

        [NativeTypeName("GDNativeExtensionClassUnreference")]
        public delegate* unmanaged[Cdecl]<void*, void> unreference_func;

        [NativeTypeName("GDNativeExtensionClassCreateInstance")]
        public delegate* unmanaged[Cdecl]<void*, void*> create_instance_func;

        [NativeTypeName("GDNativeExtensionClassFreeInstance")]
        public delegate* unmanaged[Cdecl]<void*, void*, void> free_instance_func;

        [NativeTypeName("GDNativeExtensionClassGetVirtual")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, delegate* unmanaged[Cdecl]<void*, void**, void*, void>> get_virtual_func;

        [NativeTypeName("GDNativeExtensionClassGetRID")]
        public delegate* unmanaged[Cdecl]<void*, nuint> get_rid_func;

        public void* class_userdata;
    }
}
