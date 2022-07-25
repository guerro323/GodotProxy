namespace GDNative
{
    public unsafe partial struct GDNativeInitialization
    {
        public GDNativeInitializationLevel minimum_initialization_level;

        public void* userdata;

        [NativeTypeName("void (*)(void *, GDNativeInitializationLevel)")]
        public delegate* unmanaged[Cdecl]<void*, GDNativeInitializationLevel, void> initialize;

        [NativeTypeName("void (*)(void *, GDNativeInitializationLevel)")]
        public delegate* unmanaged[Cdecl]<void*, GDNativeInitializationLevel, void> deinitialize;
    }
}
