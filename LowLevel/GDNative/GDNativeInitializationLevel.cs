namespace GDNative
{
    [NativeTypeName("unsigned int")]
    public enum GDNativeInitializationLevel : uint
    {
        GDNATIVE_INITIALIZATION_CORE,
        GDNATIVE_INITIALIZATION_SERVERS,
        GDNATIVE_INITIALIZATION_SCENE,
        GDNATIVE_INITIALIZATION_EDITOR,
        GDNATIVE_MAX_INITIALIZATION_LEVEL,
    }
}
