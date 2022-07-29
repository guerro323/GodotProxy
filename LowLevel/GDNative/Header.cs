using System;

namespace GDNative
{
    [NativeTypeName("unsigned int")]
    public enum GDNativeVariantType : uint
    {
        GDNATIVE_VARIANT_TYPE_NIL,
        GDNATIVE_VARIANT_TYPE_BOOL,
        GDNATIVE_VARIANT_TYPE_INT,
        GDNATIVE_VARIANT_TYPE_FLOAT,
        GDNATIVE_VARIANT_TYPE_STRING,
        GDNATIVE_VARIANT_TYPE_VECTOR2,
        GDNATIVE_VARIANT_TYPE_VECTOR2I,
        GDNATIVE_VARIANT_TYPE_RECT2,
        GDNATIVE_VARIANT_TYPE_RECT2I,
        GDNATIVE_VARIANT_TYPE_VECTOR3,
        GDNATIVE_VARIANT_TYPE_VECTOR3I,
        GDNATIVE_VARIANT_TYPE_TRANSFORM2D,
        GDNATIVE_VARIANT_TYPE_VECTOR4,
        GDNATIVE_VARIANT_TYPE_VECTOR4I,
        GDNATIVE_VARIANT_TYPE_PLANE,
        GDNATIVE_VARIANT_TYPE_QUATERNION,
        GDNATIVE_VARIANT_TYPE_AABB,
        GDNATIVE_VARIANT_TYPE_BASIS,
        GDNATIVE_VARIANT_TYPE_TRANSFORM3D,
        GDNATIVE_VARIANT_TYPE_PROJECTION,
        GDNATIVE_VARIANT_TYPE_COLOR,
        GDNATIVE_VARIANT_TYPE_STRING_NAME,
        GDNATIVE_VARIANT_TYPE_NODE_PATH,
        GDNATIVE_VARIANT_TYPE_RID,
        GDNATIVE_VARIANT_TYPE_OBJECT,
        GDNATIVE_VARIANT_TYPE_CALLABLE,
        GDNATIVE_VARIANT_TYPE_SIGNAL,
        GDNATIVE_VARIANT_TYPE_DICTIONARY,
        GDNATIVE_VARIANT_TYPE_ARRAY,
        GDNATIVE_VARIANT_TYPE_PACKED_BYTE_ARRAY,
        GDNATIVE_VARIANT_TYPE_PACKED_INT32_ARRAY,
        GDNATIVE_VARIANT_TYPE_PACKED_INT64_ARRAY,
        GDNATIVE_VARIANT_TYPE_PACKED_FLOAT32_ARRAY,
        GDNATIVE_VARIANT_TYPE_PACKED_FLOAT64_ARRAY,
        GDNATIVE_VARIANT_TYPE_PACKED_STRING_ARRAY,
        GDNATIVE_VARIANT_TYPE_PACKED_VECTOR2_ARRAY,
        GDNATIVE_VARIANT_TYPE_PACKED_VECTOR3_ARRAY,
        GDNATIVE_VARIANT_TYPE_PACKED_COLOR_ARRAY,
        GDNATIVE_VARIANT_TYPE_VARIANT_MAX,
    }

    [NativeTypeName("unsigned int")]
    public enum GDNativeVariantOperator : uint
    {
        GDNATIVE_VARIANT_OP_EQUAL,
        GDNATIVE_VARIANT_OP_NOT_EQUAL,
        GDNATIVE_VARIANT_OP_LESS,
        GDNATIVE_VARIANT_OP_LESS_EQUAL,
        GDNATIVE_VARIANT_OP_GREATER,
        GDNATIVE_VARIANT_OP_GREATER_EQUAL,
        GDNATIVE_VARIANT_OP_ADD,
        GDNATIVE_VARIANT_OP_SUBTRACT,
        GDNATIVE_VARIANT_OP_MULTIPLY,
        GDNATIVE_VARIANT_OP_DIVIDE,
        GDNATIVE_VARIANT_OP_NEGATE,
        GDNATIVE_VARIANT_OP_POSITIVE,
        GDNATIVE_VARIANT_OP_MODULE,
        GDNATIVE_VARIANT_OP_POWER,
        GDNATIVE_VARIANT_OP_SHIFT_LEFT,
        GDNATIVE_VARIANT_OP_SHIFT_RIGHT,
        GDNATIVE_VARIANT_OP_BIT_AND,
        GDNATIVE_VARIANT_OP_BIT_OR,
        GDNATIVE_VARIANT_OP_BIT_XOR,
        GDNATIVE_VARIANT_OP_BIT_NEGATE,
        GDNATIVE_VARIANT_OP_AND,
        GDNATIVE_VARIANT_OP_OR,
        GDNATIVE_VARIANT_OP_XOR,
        GDNATIVE_VARIANT_OP_NOT,
        GDNATIVE_VARIANT_OP_IN,
        GDNATIVE_VARIANT_OP_MAX,
    }

    [NativeTypeName("unsigned int")]
    public enum GDNativeCallErrorType : uint
    {
        GDNATIVE_CALL_OK,
        GDNATIVE_CALL_ERROR_INVALID_METHOD,
        GDNATIVE_CALL_ERROR_INVALID_ARGUMENT,
        GDNATIVE_CALL_ERROR_TOO_MANY_ARGUMENTS,
        GDNATIVE_CALL_ERROR_TOO_FEW_ARGUMENTS,
        GDNATIVE_CALL_ERROR_INSTANCE_IS_NULL,
        GDNATIVE_CALL_ERROR_METHOD_NOT_CONST,
    }

    public partial struct GDNativeCallError
    {
        public GDNativeCallErrorType error;

        [NativeTypeName("int32_t")]
        public int argument;

        [NativeTypeName("int32_t")]
        public int expected;
    }

    public unsafe partial struct GDNativeInstanceBindingCallbacks
    {
        [NativeTypeName("GDNativeInstanceBindingCreateCallback")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*> create_callback;

        [NativeTypeName("GDNativeInstanceBindingFreeCallback")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, void> free_callback;

        [NativeTypeName("GDNativeInstanceBindingReferenceCallback")]
        public delegate* unmanaged[Cdecl]<void*, void*, byte, byte> reference_callback;
    }

    public unsafe partial struct GDNativePropertyInfo
    {
        [NativeTypeName("uint32_t")]
        public uint type;

        [NativeTypeName("char *")]
        public sbyte* name;

        [NativeTypeName("char *")]
        public sbyte* class_name;

        [NativeTypeName("uint32_t")]
        public uint hint;

        [NativeTypeName("char *")]
        public sbyte* hint_string;

        [NativeTypeName("uint32_t")]
        public uint usage;
    }

    public unsafe partial struct GDNativeMethodInfo
    {
        [NativeTypeName("char *")]
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

    [NativeTypeName("unsigned int")]
    public enum GDNativeExtensionClassMethodFlags : uint
    {
        GDNATIVE_EXTENSION_METHOD_FLAG_NORMAL = 1,
        GDNATIVE_EXTENSION_METHOD_FLAG_EDITOR = 2,
        GDNATIVE_EXTENSION_METHOD_FLAG_= 4,
        GDNATIVE_EXTENSION_METHOD_FLAG_VIRTUAL = 8,
        GDNATIVE_EXTENSION_METHOD_FLAG_VARARG = 16,
        GDNATIVE_EXTENSION_METHOD_FLAG_STATIC = 32,
        GDNATIVE_EXTENSION_METHOD_FLAGS_DEFAULT = GDNATIVE_EXTENSION_METHOD_FLAG_NORMAL,
    }

    [NativeTypeName("unsigned int")]
    public enum GDNativeExtensionClassMethodArgumentMetadata : uint
    {
        GDNATIVE_EXTENSION_METHOD_ARGUMENT_METADATA_NONE,
        GDNATIVE_EXTENSION_METHOD_ARGUMENT_METADATA_INT_IS_INT8,
        GDNATIVE_EXTENSION_METHOD_ARGUMENT_METADATA_INT_IS_INT16,
        GDNATIVE_EXTENSION_METHOD_ARGUMENT_METADATA_INT_IS_INT32,
        GDNATIVE_EXTENSION_METHOD_ARGUMENT_METADATA_INT_IS_INT64,
        GDNATIVE_EXTENSION_METHOD_ARGUMENT_METADATA_INT_IS_UINT8,
        GDNATIVE_EXTENSION_METHOD_ARGUMENT_METADATA_INT_IS_UINT16,
        GDNATIVE_EXTENSION_METHOD_ARGUMENT_METADATA_INT_IS_UINT32,
        GDNATIVE_EXTENSION_METHOD_ARGUMENT_METADATA_INT_IS_UINT64,
        GDNATIVE_EXTENSION_METHOD_ARGUMENT_METADATA_REAL_IS_FLOAT,
        GDNATIVE_EXTENSION_METHOD_ARGUMENT_METADATA_REAL_IS_DOUBLE,
    }

    public unsafe partial struct GDNativeExtensionClassMethodInfo
    {
        [NativeTypeName("char *")]
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

    public unsafe partial struct GDNativeInterface
    {
        [NativeTypeName("uint32_t")]
        public uint version_major;

        [NativeTypeName("uint32_t")]
        public uint version_minor;

        [NativeTypeName("uint32_t")]
        public uint version_patch;

        [NativeTypeName("char *")]
        public sbyte* version_string;

        [NativeTypeName("void *(*)(size_t)")]
        public delegate* unmanaged[Cdecl]<nuint, void*> mem_alloc;

        [NativeTypeName("void *(*)(void *, size_t)")]
        public delegate* unmanaged[Cdecl]<void*, nuint, void*> mem_realloc;

        [NativeTypeName("void (*)(void *)")]
        public delegate* unmanaged[Cdecl]<void*, void> mem_free;

        [NativeTypeName("void (*)(char *, char *, char *, int32_t)")]
        public delegate* unmanaged[Cdecl]<sbyte*, sbyte*, sbyte*, int, void> print_error;

        [NativeTypeName("void (*)(char *, char *, char *, int32_t)")]
        public delegate* unmanaged[Cdecl]<sbyte*, sbyte*, sbyte*, int, void> print_warning;

        [NativeTypeName("void (*)(char *, char *, char *, int32_t)")]
        public delegate* unmanaged[Cdecl]<sbyte*, sbyte*, sbyte*, int, void> print_script_error;

        [NativeTypeName("uint64_t (*)(char *)")]
        public delegate* unmanaged[Cdecl]<sbyte*, nuint> get_native_struct_size;

        [NativeTypeName("void (*)(GDNativeVariantPtr, GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void> variant_new_copy;

        [NativeTypeName("void (*)(GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void> variant_new_nil;

        [NativeTypeName("void (*)(GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void> variant_destroy;

        [NativeTypeName("void (*)(GDNativeVariantPtr, GDNativeStringNamePtr, GDNativeVariantPtr *, GDNativeInt, GDNativeVariantPtr, GDNativeCallError *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void**, nint, void*, GDNativeCallError*, void> variant_call;

        [NativeTypeName("void (*)(GDNativeVariantType, GDNativeStringNamePtr, GDNativeVariantPtr *, GDNativeInt, GDNativeVariantPtr, GDNativeCallError *)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, void*, void**, nint, void*, GDNativeCallError*, void> variant_call_static;

        [NativeTypeName("void (*)(GDNativeVariantOperator, GDNativeVariantPtr, GDNativeVariantPtr, GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantOperator, void*, void*, void*, byte*, void> variant_evaluate;

        [NativeTypeName("void (*)(GDNativeVariantPtr, GDNativeVariantPtr, GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte*, void> variant_set;

        [NativeTypeName("void (*)(GDNativeVariantPtr, GDNativeStringNamePtr, GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte*, void> variant_set_named;

        [NativeTypeName("void (*)(GDNativeVariantPtr, GDNativeVariantPtr, GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte*, void> variant_set_keyed;

        [NativeTypeName("void (*)(GDNativeVariantPtr, GDNativeInt, GDNativeVariantPtr, GDNativeBool *, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*, byte*, byte*, void> variant_set_indexed;

        [NativeTypeName("void (*)(GDNativeVariantPtr, GDNativeVariantPtr, GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte*, void> variant_get;

        [NativeTypeName("void (*)(GDNativeVariantPtr, GDNativeStringNamePtr, GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte*, void> variant_get_named;

        [NativeTypeName("void (*)(GDNativeVariantPtr, GDNativeVariantPtr, GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte*, void> variant_get_keyed;

        [NativeTypeName("void (*)(GDNativeVariantPtr, GDNativeInt, GDNativeVariantPtr, GDNativeBool *, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*, byte*, byte*, void> variant_get_indexed;

        [NativeTypeName("GDNativeBool (*)(GDNativeVariantPtr, GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, byte*, byte> variant_iter_init;

        [NativeTypeName("GDNativeBool (*)(GDNativeVariantPtr, GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, byte*, byte> variant_iter_next;

        [NativeTypeName("void (*)(GDNativeVariantPtr, GDNativeVariantPtr, GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte*, void> variant_iter_get;

        [NativeTypeName("GDNativeInt (*)(GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, nint> variant_hash;

        [NativeTypeName("GDNativeInt (*)(GDNativeVariantPtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, nint> variant_recursive_hash;

        [NativeTypeName("GDNativeBool (*)(GDNativeVariantPtr, GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, byte> variant_hash_compare;

        [NativeTypeName("GDNativeBool (*)(GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, byte> variant_booleanize;

        [NativeTypeName("void (*)(GDNativeVariantPtr, GDNativeVariantPtr, GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, void> variant_sub;

        [NativeTypeName("void (*)(GDNativeVariantPtr, GDNativeVariantPtr, float, GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, float, void*, void> variant_blend;

        [NativeTypeName("void (*)(GDNativeVariantPtr, GDNativeVariantPtr, float, GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, float, void*, void> variant_interpolate;

        [NativeTypeName("void (*)(GDNativeVariantPtr, GDNativeVariantPtr, GDNativeBool)")]
        public delegate* unmanaged[Cdecl]<void*, void*, byte, void> variant_duplicate;

        [NativeTypeName("void (*)(GDNativeVariantPtr, GDNativeStringPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void> variant_stringify;

        [NativeTypeName("GDNativeVariantType (*)(GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, GDNativeVariantType> variant_get_type;

        [NativeTypeName("GDNativeBool (*)(GDNativeVariantPtr, GDNativeStringNamePtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, byte> variant_has_method;

        [NativeTypeName("GDNativeBool (*)(GDNativeVariantType, GDNativeStringNamePtr)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, void*, byte> variant_has_member;

        [NativeTypeName("GDNativeBool (*)(GDNativeVariantPtr, GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, byte*, byte> variant_has_key;

        [NativeTypeName("void (*)(GDNativeVariantType, GDNativeStringPtr)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, void*, void> variant_get_type_name;

        [NativeTypeName("GDNativeBool (*)(GDNativeVariantType, GDNativeVariantType)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, GDNativeVariantType, byte> variant_can_convert;

        [NativeTypeName("GDNativeBool (*)(GDNativeVariantType, GDNativeVariantType)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, GDNativeVariantType, byte> variant_can_convert_strict;

        [NativeTypeName("GDNativeVariantFromTypeConstructorFunc (*)(GDNativeVariantType)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, delegate* unmanaged[Cdecl]<void*, void*, void>> get_variant_from_type_constructor;

        [NativeTypeName("GDNativeTypeFromVariantConstructorFunc (*)(GDNativeVariantType)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, delegate* unmanaged[Cdecl]<void*, void*, void>> get_variant_to_type_constructor;

        [NativeTypeName("GDNativePtrOperatorEvaluator (*)(GDNativeVariantOperator, GDNativeVariantType, GDNativeVariantType)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantOperator, GDNativeVariantType, GDNativeVariantType, delegate* unmanaged[Cdecl]<void*, void*, void*, void>> variant_get_ptr_operator_evaluator;

        [NativeTypeName("GDNativePtrBuiltInMethod (*)(GDNativeVariantType, char *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, sbyte*, nint, delegate* unmanaged[Cdecl]<void*, void**, void*, int, void>> variant_get_ptr_builtin_method;

        [NativeTypeName("GDNativePtrConstructor (*)(GDNativeVariantType, int32_t)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, int, delegate* unmanaged[Cdecl]<void*, void**, void>> variant_get_ptr_constructor;

        [NativeTypeName("GDNativePtrDestructor (*)(GDNativeVariantType)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, delegate* unmanaged[Cdecl]<void*, void>> variant_get_ptr_destructor;

        [NativeTypeName("void (*)(GDNativeVariantType, GDNativeVariantPtr, GDNativeVariantPtr *, int32_t, GDNativeCallError *)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, void*, void**, int, GDNativeCallError*, void> variant_construct;

        [NativeTypeName("GDNativePtrSetter (*)(GDNativeVariantType, char *)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, sbyte*, delegate* unmanaged[Cdecl]<void*, void*, void>> variant_get_ptr_setter;

        [NativeTypeName("GDNativePtrGetter (*)(GDNativeVariantType, char *)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, sbyte*, delegate* unmanaged[Cdecl]<void*, void*, void>> variant_get_ptr_getter;

        [NativeTypeName("GDNativePtrIndexedSetter (*)(GDNativeVariantType)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, delegate* unmanaged[Cdecl]<void*, nint, void*, void>> variant_get_ptr_indexed_setter;

        [NativeTypeName("GDNativePtrIndexedGetter (*)(GDNativeVariantType)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, delegate* unmanaged[Cdecl]<void*, nint, void*, void>> variant_get_ptr_indexed_getter;

        [NativeTypeName("GDNativePtrKeyedSetter (*)(GDNativeVariantType)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, delegate* unmanaged[Cdecl]<void*, void*, void*, void>> variant_get_ptr_keyed_setter;

        [NativeTypeName("GDNativePtrKeyedGetter (*)(GDNativeVariantType)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, delegate* unmanaged[Cdecl]<void*, void*, void*, void>> variant_get_ptr_keyed_getter;

        [NativeTypeName("GDNativePtrKeyedChecker (*)(GDNativeVariantType)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, delegate* unmanaged[Cdecl]<void*, void*, uint>> variant_get_ptr_keyed_checker;

        [NativeTypeName("void (*)(GDNativeVariantType, char *, GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, sbyte*, void*, void> variant_get_constant_value;

        [NativeTypeName("GDNativePtrUtilityFunction (*)(char *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<sbyte*, nint, delegate* unmanaged[Cdecl]<void*, void**, int, void>> variant_get_ptr_utility_function;

        [NativeTypeName("void (*)(GDNativeStringPtr, char *)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, void> string_new_with_latin1_chars;

        [NativeTypeName("void (*)(GDNativeStringPtr, char *)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, void> string_new_with_utf8_chars;

        [NativeTypeName("void (*)(GDNativeStringPtr, char16_t *)")]
        public delegate* unmanaged[Cdecl]<void*, ushort*, void> string_new_with_utf16_chars;

        [NativeTypeName("void (*)(GDNativeStringPtr, sbyte *)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, void> string_new_with_utf32_chars;

        [NativeTypeName("void (*)(GDNativeStringPtr, wchar_t *)")]
        public delegate* unmanaged[Cdecl]<void*, uint*, void> string_new_with_wide_chars;

        [NativeTypeName("void (*)(GDNativeStringPtr, char *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, nint, void> string_new_with_latin1_chars_and_len;

        [NativeTypeName("void (*)(GDNativeStringPtr, char *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, nint, void> string_new_with_utf8_chars_and_len;

        [NativeTypeName("void (*)(GDNativeStringPtr, char16_t *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, ushort*, nint, void> string_new_with_utf16_chars_and_len;

        [NativeTypeName("void (*)(GDNativeStringPtr, sbyte *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, nint, void> string_new_with_utf32_chars_and_len;

        [NativeTypeName("void (*)(GDNativeStringPtr, wchar_t *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, uint*, nint, void> string_new_with_wide_chars_and_len;

        [NativeTypeName("GDNativeInt (*)(GDNativeStringPtr, char *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, nint, nint> string_to_latin1_chars;

        [NativeTypeName("GDNativeInt (*)(GDNativeStringPtr, char *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, nint, nint> string_to_utf8_chars;

        [NativeTypeName("GDNativeInt (*)(GDNativeStringPtr, char16_t *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, ushort*, nint, nint> string_to_utf16_chars;

        [NativeTypeName("GDNativeInt (*)(GDNativeStringPtr, sbyte *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, nint, nint> string_to_utf32_chars;

        [NativeTypeName("GDNativeInt (*)(GDNativeStringPtr, wchar_t *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, uint*, nint, nint> string_to_wide_chars;

        [NativeTypeName("sbyte *(*)(GDNativeStringPtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, sbyte*> string_operator_index;

        [NativeTypeName("sbyte *(*)(GDNativeStringPtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, sbyte*> string_operator_index_const;

        [NativeTypeName("uint8_t *(*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, byte*> packed_byte_array_operator_index;

        [NativeTypeName("uint8_t *(*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, byte*> packed_byte_array_operator_index_const;

        [NativeTypeName("GDNativeTypePtr (*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> packed_color_array_operator_index;

        [NativeTypeName("GDNativeTypePtr (*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> packed_color_array_operator_index_const;

        [NativeTypeName("float *(*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, float*> packed_float32_array_operator_index;

        [NativeTypeName("float *(*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, float*> packed_float32_array_operator_index_const;

        [NativeTypeName("double *(*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, double*> packed_float64_array_operator_index;

        [NativeTypeName("double *(*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, double*> packed_float64_array_operator_index_const;

        [NativeTypeName("int32_t *(*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, int*> packed_int32_array_operator_index;

        [NativeTypeName("int32_t *(*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, int*> packed_int32_array_operator_index_const;

        [NativeTypeName("int64_t *(*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, nint*> packed_int64_array_operator_index;

        [NativeTypeName("int64_t *(*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, nint*> packed_int64_array_operator_index_const;

        [NativeTypeName("GDNativeStringPtr (*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> packed_string_array_operator_index;

        [NativeTypeName("GDNativeStringPtr (*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> packed_string_array_operator_index_const;

        [NativeTypeName("GDNativeTypePtr (*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> packed_vector2_array_operator_index;

        [NativeTypeName("GDNativeTypePtr (*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> packed_vector2_array_operator_index_const;

        [NativeTypeName("GDNativeTypePtr (*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> packed_vector3_array_operator_index;

        [NativeTypeName("GDNativeTypePtr (*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> packed_vector3_array_operator_index_const;

        [NativeTypeName("GDNativeVariantPtr (*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> array_operator_index;

        [NativeTypeName("GDNativeVariantPtr (*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> array_operator_index_const;

        [NativeTypeName("GDNativeVariantPtr (*)(GDNativeTypePtr, GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*> dictionary_operator_index;

        [NativeTypeName("GDNativeVariantPtr (*)(GDNativeTypePtr, GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*> dictionary_operator_index_const;

        [NativeTypeName("void (*)(GDNativeMethodBindPtr, GDNativeObjectPtr, GDNativeVariantPtr *, GDNativeInt, GDNativeVariantPtr, GDNativeCallError *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void**, nint, void*, GDNativeCallError*, void> object_method_bind_call;

        [NativeTypeName("void (*)(GDNativeMethodBindPtr, GDNativeObjectPtr, GDNativeTypePtr *, GDNativeTypePtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void**, void*, void> object_method_bind_ptrcall;

        [NativeTypeName("void (*)(GDNativeObjectPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void> object_destroy;

        [NativeTypeName("GDNativeObjectPtr (*)(char *)")]
        public delegate* unmanaged[Cdecl]<sbyte*, void*> global_get_singleton;

        [NativeTypeName("void *(*)(GDNativeObjectPtr, void *, GDNativeInstanceBindingCallbacks *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, GDNativeInstanceBindingCallbacks*, void*> object_get_instance_binding;

        [NativeTypeName("void (*)(GDNativeObjectPtr, void *, void *, GDNativeInstanceBindingCallbacks *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, GDNativeInstanceBindingCallbacks*, void> object_set_instance_binding;

        [NativeTypeName("void (*)(GDNativeObjectPtr, char *, GDExtensionClassInstancePtr)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, void*, void> object_set_instance;

        [NativeTypeName("GDNativeObjectPtr (*)(GDNativeObjectPtr, void *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*> object_cast_to;

        [NativeTypeName("GDNativeObjectPtr (*)(GDObjectInstanceID)")]
        public delegate* unmanaged[Cdecl]<nuint, void*> object_get_instance_from_id;

        [NativeTypeName("GDObjectInstanceID (*)(GDNativeObjectPtr)")]
        public delegate* unmanaged[Cdecl]<void*, nuint> object_get_instance_id;

        [NativeTypeName("GDNativeScriptInstancePtr (*)(GDNativeExtensionScriptInstanceInfo *, GDNativeExtensionScriptInstanceDataPtr)")]
        public delegate* unmanaged[Cdecl]<GDNativeExtensionScriptInstanceInfo*, void*, void*> script_instance_create;

        [NativeTypeName("GDNativeObjectPtr (*)(char *)")]
        public delegate* unmanaged[Cdecl]<sbyte*, void*> classdb_construct_object;

        [NativeTypeName("GDNativeMethodBindPtr (*)(char *, char *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<sbyte*, sbyte*, nint, void*> classdb_get_method_bind;

        [NativeTypeName("void *(*)(char *)")]
        public delegate* unmanaged[Cdecl]<sbyte*, void*> classdb_get_class_tag;

        [NativeTypeName("void (*)(GDNativeExtensionClassLibraryPtr, char *, char *, GDNativeExtensionClassCreationInfo *)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, sbyte*, GDNativeExtensionClassCreationInfo*, void> classdb_register_extension_class;

        [NativeTypeName("void (*)(GDNativeExtensionClassLibraryPtr, char *, GDNativeExtensionClassMethodInfo *)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, GDNativeExtensionClassMethodInfo*, void> classdb_register_extension_class_method;

        [NativeTypeName("void (*)(GDNativeExtensionClassLibraryPtr, char *, char *, char *, GDNativeInt, GDNativeBool)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, sbyte*, sbyte*, nint, byte, void> classdb_register_extension_class_integer_constant;

        [NativeTypeName("void (*)(GDNativeExtensionClassLibraryPtr, char *, GDNativePropertyInfo *, char *, char *)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, GDNativePropertyInfo*, sbyte*, sbyte*, void> classdb_register_extension_class_property;

        [NativeTypeName("void (*)(GDNativeExtensionClassLibraryPtr, char *, char *, char *)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, sbyte*, sbyte*, void> classdb_register_extension_class_property_group;

        [NativeTypeName("void (*)(GDNativeExtensionClassLibraryPtr, char *, char *, char *)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, sbyte*, sbyte*, void> classdb_register_extension_class_property_subgroup;

        [NativeTypeName("void (*)(GDNativeExtensionClassLibraryPtr, char *, char *, GDNativePropertyInfo *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, sbyte*, GDNativePropertyInfo*, nint, void> classdb_register_extension_class_signal;

        [NativeTypeName("void (*)(GDNativeExtensionClassLibraryPtr, char *)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, void> classdb_unregister_extension_class;

        [NativeTypeName("void (*)(GDNativeExtensionClassLibraryPtr, GDNativeStringPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void> get_library_path;
    }

    [NativeTypeName("unsigned int")]
    public enum GDNativeInitializationLevel : uint
    {
        GDNATIVE_INITIALIZATION_CORE,
        GDNATIVE_INITIALIZATION_SERVERS,
        GDNATIVE_INITIALIZATION_SCENE,
        GDNATIVE_INITIALIZATION_EDITOR,
        GDNATIVE_MAX_INITIALIZATION_LEVEL,
    }

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

public class NativeTypeNameAttribute : Attribute
{
    public NativeTypeNameAttribute(string name) {}
}