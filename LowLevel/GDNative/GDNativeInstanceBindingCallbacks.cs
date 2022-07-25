namespace GDNative
{
    public unsafe partial struct GDNativeInstanceBindingCallbacks
    {
        [NativeTypeName("GDNativeInstanceBindingCreateCallback")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*> create_callback;

        [NativeTypeName("GDNativeInstanceBindingFreeCallback")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, void> free_callback;

        [NativeTypeName("GDNativeInstanceBindingReferenceCallback")]
        public delegate* unmanaged[Cdecl]<void*, void*, byte, byte> reference_callback;
    }
}
