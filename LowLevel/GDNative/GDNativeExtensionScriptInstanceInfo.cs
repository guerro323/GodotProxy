namespace GDNative
{
    public unsafe partial struct GDNativeExtensionScriptInstanceInfo
    {
        [NativeTypeName("GDNativeExtensionScriptInstanceSet")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte> set_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceGet")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte> get_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceGetPropertyList")]
        public delegate* unmanaged[Cdecl]<void*, uint*, GDNativePropertyInfo*> get_property_list_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceFreePropertyList")]
        public delegate* unmanaged[Cdecl]<void*, GDNativePropertyInfo*, void> free_property_list_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceGetPropertyType")]
        public delegate* unmanaged[Cdecl]<void*, void*, byte*, GDNativeVariantType> get_property_type_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceGetOwner")]
        public delegate* unmanaged[Cdecl]<void*, void*> get_owner_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceGetPropertyState")]
        public delegate* unmanaged[Cdecl]<void*, delegate* unmanaged[Cdecl]<void*, void*, void*, void>, void*, void> get_property_state_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceGetMethodList")]
        public delegate* unmanaged[Cdecl]<void*, uint*, GDNativeMethodInfo*> get_method_list_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceFreeMethodList")]
        public delegate* unmanaged[Cdecl]<void*, GDNativeMethodInfo*, void> free_method_list_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceHasMethod")]
        public delegate* unmanaged[Cdecl]<void*, void*, byte> has_method_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceCall")]
        public delegate* unmanaged[Cdecl]<void*, void*, void**, nint, void*, GDNativeCallError*, void> call_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceNotification")]
        public delegate* unmanaged[Cdecl]<void*, int, void> notification_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceToString")]
        public delegate* unmanaged[Cdecl]<void*, byte*, sbyte*> to_string_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceRefCountIncremented")]
        public delegate* unmanaged[Cdecl]<void*, void> refcount_incremented_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceRefCountDecremented")]
        public delegate* unmanaged[Cdecl]<void*, byte> refcount_decremented_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceGetScript")]
        public delegate* unmanaged[Cdecl]<void*, void*> get_script_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceIsPlaceholder")]
        public delegate* unmanaged[Cdecl]<void*, byte> is_placeholder_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceSet")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte> set_fallback_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceGet")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte> get_fallback_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceGetLanguage")]
        public delegate* unmanaged[Cdecl]<void*, void*> get_language_func;

        [NativeTypeName("GDNativeExtensionScriptInstanceFree")]
        public delegate* unmanaged[Cdecl]<void*, void> free_func;
    }
}
