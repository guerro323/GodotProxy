namespace GDNative
{
    [NativeTypeName("unsigned int")]
    public enum GDNativeCallErrorType : uint
    {
        GDNATIVE_CALL_OK,
        GDNATIVE_CALL_ERROR_INVALID_METHOD,
        GDNATIVE_CALL_ERROR_INVALID_ARGUMENT,
        GDNATIVE_CALL_ERROR_TOO_MANY_ARGUMENTS,
        GDNATIVE_CALL_ERROR_TOO_FEW_ARGUMENTS,
        GDNATIVE_CALL_ERROR_INSTANCE_IS_NULL,
    }
}
