namespace GDNative
{
    public partial struct GDNativeCallError
    {
        public GDNativeCallErrorType error;

        [NativeTypeName("int32_t")]
        public int argument;

        [NativeTypeName("int32_t")]
        public int expected;
    }
}
