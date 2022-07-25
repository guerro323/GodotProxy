using char32_t = System.UInt32; // if windows UInt32
// ReSharper disable All

namespace GDNative
{
    public unsafe partial struct GDNativeInterface
    {
        [NativeTypeName("uint32_t")]
        public uint version_major;

        [NativeTypeName("uint32_t")]
        public uint version_minor;

        [NativeTypeName("uint32_t")]
        public uint version_patch;

        [NativeTypeName("const char *")]
        public sbyte* version_string;

        [NativeTypeName("void *(*)(size_t)")]
        public delegate* unmanaged[Cdecl]<nuint, void*> mem_alloc;

        [NativeTypeName("void *(*)(void *, size_t)")]
        public delegate* unmanaged[Cdecl]<void*, nuint, void*> mem_realloc;

        [NativeTypeName("void (*)(void *)")]
        public delegate* unmanaged[Cdecl]<void*, void> mem_free;

        [NativeTypeName("void (*)(const char *, const char *, const char *, int32_t)")]
        public delegate* unmanaged[Cdecl]<sbyte*, sbyte*, sbyte*, int, void> print_error;

        [NativeTypeName("void (*)(const char *, const char *, const char *, int32_t)")]
        public delegate* unmanaged[Cdecl]<sbyte*, sbyte*, sbyte*, int, void> print_warning;

        [NativeTypeName("void (*)(const char *, const char *, const char *, int32_t)")]
        public delegate* unmanaged[Cdecl]<sbyte*, sbyte*, sbyte*, int, void> print_script_error;

        [NativeTypeName("uint64_t (*)(const char *)")]
        public delegate* unmanaged[Cdecl]<sbyte*, nuint> get_native_struct_size;

        [NativeTypeName("void (*)(GDNativeVariantPtr, const GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void> variant_new_copy;

        [NativeTypeName("void (*)(GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void> variant_new_nil;

        [NativeTypeName("void (*)(GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void> variant_destroy;

        [NativeTypeName("void (*)(GDNativeVariantPtr, const GDNativeStringNamePtr, const GDNativeVariantPtr *, const GDNativeInt, GDNativeVariantPtr, GDNativeCallError *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void**, nint, void*, GDNativeCallError*, void> variant_call;

        [NativeTypeName("void (*)(GDNativeVariantType, const GDNativeStringNamePtr, const GDNativeVariantPtr *, const GDNativeInt, GDNativeVariantPtr, GDNativeCallError *)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, void*, void**, nint, void*, GDNativeCallError*, void> variant_call_static;

        [NativeTypeName("void (*)(GDNativeVariantOperator, const GDNativeVariantPtr, const GDNativeVariantPtr, GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantOperator, void*, void*, void*, byte*, void> variant_evaluate;

        [NativeTypeName("void (*)(GDNativeVariantPtr, const GDNativeVariantPtr, const GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte*, void> variant_set;

        [NativeTypeName("void (*)(GDNativeVariantPtr, const GDNativeStringNamePtr, const GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte*, void> variant_set_named;

        [NativeTypeName("void (*)(GDNativeVariantPtr, const GDNativeVariantPtr, const GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte*, void> variant_set_keyed;

        [NativeTypeName("void (*)(GDNativeVariantPtr, GDNativeInt, const GDNativeVariantPtr, GDNativeBool *, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*, byte*, byte*, void> variant_set_indexed;

        [NativeTypeName("void (*)(const GDNativeVariantPtr, const GDNativeVariantPtr, GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte*, void> variant_get;

        [NativeTypeName("void (*)(const GDNativeVariantPtr, const GDNativeStringNamePtr, GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte*, void> variant_get_named;

        [NativeTypeName("void (*)(const GDNativeVariantPtr, const GDNativeVariantPtr, GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte*, void> variant_get_keyed;

        [NativeTypeName("void (*)(const GDNativeVariantPtr, GDNativeInt, GDNativeVariantPtr, GDNativeBool *, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*, byte*, byte*, void> variant_get_indexed;

        [NativeTypeName("GDNativeBool (*)(const GDNativeVariantPtr, GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, byte*, byte> variant_iter_init;

        [NativeTypeName("GDNativeBool (*)(const GDNativeVariantPtr, GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, byte*, byte> variant_iter_next;

        [NativeTypeName("void (*)(const GDNativeVariantPtr, GDNativeVariantPtr, GDNativeVariantPtr, GDNativeBool *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, byte*, void> variant_iter_get;

        [NativeTypeName("GDNativeInt (*)(const GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, nint> variant_hash;

        [NativeTypeName("GDNativeInt (*)(const GDNativeVariantPtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, nint> variant_recursive_hash;

        [NativeTypeName("GDNativeBool (*)(const GDNativeVariantPtr, const GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, byte> variant_hash_compare;

        [NativeTypeName("GDNativeBool (*)(const GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, byte> variant_booleanize;

        [NativeTypeName("void (*)(const GDNativeVariantPtr, const GDNativeVariantPtr, GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, void> variant_sub;

        [NativeTypeName("void (*)(const GDNativeVariantPtr, const GDNativeVariantPtr, float, GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, float, void*, void> variant_blend;

        [NativeTypeName("void (*)(const GDNativeVariantPtr, const GDNativeVariantPtr, float, GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, float, void*, void> variant_interpolate;

        [NativeTypeName("void (*)(const GDNativeVariantPtr, GDNativeVariantPtr, GDNativeBool)")]
        public delegate* unmanaged[Cdecl]<void*, void*, byte, void> variant_duplicate;

        [NativeTypeName("void (*)(const GDNativeVariantPtr, GDNativeStringPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void> variant_stringify;

        [NativeTypeName("GDNativeVariantType (*)(const GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, GDNativeVariantType> variant_get_type;

        [NativeTypeName("GDNativeBool (*)(const GDNativeVariantPtr, const GDNativeStringNamePtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, byte> variant_has_method;

        [NativeTypeName("GDNativeBool (*)(GDNativeVariantType, const GDNativeStringNamePtr)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, void*, byte> variant_has_member;

        [NativeTypeName("GDNativeBool (*)(const GDNativeVariantPtr, const GDNativeVariantPtr, GDNativeBool *)")]
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

        [NativeTypeName("GDNativePtrBuiltInMethod (*)(GDNativeVariantType, const char *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, sbyte*, nint, delegate* unmanaged[Cdecl]<void*, void**, void*, int, void>> variant_get_ptr_builtin_method;

        [NativeTypeName("GDNativePtrConstructor (*)(GDNativeVariantType, int32_t)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, int, delegate* unmanaged[Cdecl]<void*, void**, void>> variant_get_ptr_constructor;

        [NativeTypeName("GDNativePtrDestructor (*)(GDNativeVariantType)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, delegate* unmanaged[Cdecl]<void*, void>> variant_get_ptr_destructor;

        [NativeTypeName("void (*)(GDNativeVariantType, GDNativeVariantPtr, const GDNativeVariantPtr *, int32_t, GDNativeCallError *)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, void*, void**, int, GDNativeCallError*, void> variant_construct;

        [NativeTypeName("GDNativePtrSetter (*)(GDNativeVariantType, const char *)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, sbyte*, delegate* unmanaged[Cdecl]<void*, void*, void>> variant_get_ptr_setter;

        [NativeTypeName("GDNativePtrGetter (*)(GDNativeVariantType, const char *)")]
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

        [NativeTypeName("void (*)(GDNativeVariantType, const char *, GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<GDNativeVariantType, sbyte*, void*, void> variant_get_constant_value;

        [NativeTypeName("GDNativePtrUtilityFunction (*)(const char *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<sbyte*, long, delegate* unmanaged[Cdecl]<void*, void**, int, void>> variant_get_ptr_utility_function;

        [NativeTypeName("void (*)(GDNativeStringPtr, const char *)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, void> string_new_with_latin1_chars;

        [NativeTypeName("void (*)(GDNativeStringPtr, const char *)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, void> string_new_with_utf8_chars;

        [NativeTypeName("void (*)(GDNativeStringPtr, const char16_t *)")]
        public delegate* unmanaged[Cdecl]<void*, ushort*, void> string_new_with_utf16_chars;

        [NativeTypeName("void (*)(GDNativeStringPtr, const char32_t *)")]
        public delegate* unmanaged[Cdecl]<void*, char32_t*, void> string_new_with_utf32_chars;

        [NativeTypeName("void (*)(GDNativeStringPtr, const wchar_t *)")]
        public delegate* unmanaged[Cdecl]<void*, uint*, void> string_new_with_wide_chars;

        [NativeTypeName("void (*)(GDNativeStringPtr, const char *, const GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, nint, void> string_new_with_latin1_chars_and_len;

        [NativeTypeName("void (*)(GDNativeStringPtr, const char *, const GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, nint, void> string_new_with_utf8_chars_and_len;

        [NativeTypeName("void (*)(GDNativeStringPtr, const char16_t *, const GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, ushort*, nint, void> string_new_with_utf16_chars_and_len;

        [NativeTypeName("void (*)(GDNativeStringPtr, const char32_t *, const GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, char32_t*, nint, void> string_new_with_utf32_chars_and_len;

        [NativeTypeName("void (*)(GDNativeStringPtr, const wchar_t *, const GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, uint*, nint, void> string_new_with_wide_chars_and_len;

        [NativeTypeName("GDNativeInt (*)(const GDNativeStringPtr, char *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, nint, nint> string_to_latin1_chars;

        [NativeTypeName("GDNativeInt (*)(const GDNativeStringPtr, char *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, nint, nint> string_to_utf8_chars;

        [NativeTypeName("GDNativeInt (*)(const GDNativeStringPtr, char16_t *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, ushort*, nint, nint> string_to_utf16_chars;

        [NativeTypeName("GDNativeInt (*)(const GDNativeStringPtr, char32_t *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, char32_t*, nint, nint> string_to_utf32_chars;

        [NativeTypeName("GDNativeInt (*)(const GDNativeStringPtr, wchar_t *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, uint*, nint, nint> string_to_wide_chars;

        [NativeTypeName("char32_t *(*)(GDNativeStringPtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, char32_t*> string_operator_index;

        [NativeTypeName("const char32_t *(*)(const GDNativeStringPtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, char32_t*> string_operator_index_const;

        [NativeTypeName("uint8_t *(*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, byte*> packed_byte_array_operator_index;

        [NativeTypeName("const uint8_t *(*)(const GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, byte*> packed_byte_array_operator_index_const;

        [NativeTypeName("GDNativeTypePtr (*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> packed_color_array_operator_index;

        [NativeTypeName("GDNativeTypePtr (*)(const GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> packed_color_array_operator_index_const;

        [NativeTypeName("float *(*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, float*> packed_float32_array_operator_index;

        [NativeTypeName("const float *(*)(const GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, float*> packed_float32_array_operator_index_const;

        [NativeTypeName("double *(*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, double*> packed_float64_array_operator_index;

        [NativeTypeName("const double *(*)(const GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, double*> packed_float64_array_operator_index_const;

        [NativeTypeName("int32_t *(*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, int*> packed_int32_array_operator_index;

        [NativeTypeName("const int32_t *(*)(const GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, int*> packed_int32_array_operator_index_const;

        [NativeTypeName("int64_t *(*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, nint*> packed_int64_array_operator_index;

        [NativeTypeName("const int64_t *(*)(const GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, nint*> packed_int64_array_operator_index_const;

        [NativeTypeName("GDNativeStringPtr (*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> packed_string_array_operator_index;

        [NativeTypeName("GDNativeStringPtr (*)(const GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> packed_string_array_operator_index_const;

        [NativeTypeName("GDNativeTypePtr (*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> packed_vector2_array_operator_index;

        [NativeTypeName("GDNativeTypePtr (*)(const GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> packed_vector2_array_operator_index_const;

        [NativeTypeName("GDNativeTypePtr (*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> packed_vector3_array_operator_index;

        [NativeTypeName("GDNativeTypePtr (*)(const GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> packed_vector3_array_operator_index_const;

        [NativeTypeName("GDNativeVariantPtr (*)(GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> array_operator_index;

        [NativeTypeName("GDNativeVariantPtr (*)(const GDNativeTypePtr, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, nint, void*> array_operator_index_const;

        [NativeTypeName("GDNativeVariantPtr (*)(GDNativeTypePtr, const GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*> dictionary_operator_index;

        [NativeTypeName("GDNativeVariantPtr (*)(const GDNativeTypePtr, const GDNativeVariantPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*> dictionary_operator_index_const;

        [NativeTypeName("void (*)(const GDNativeMethodBindPtr, GDNativeObjectPtr, const GDNativeVariantPtr *, GDNativeInt, GDNativeVariantPtr, GDNativeCallError *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void**, nint, void*, GDNativeCallError*, void> object_method_bind_call;

        [NativeTypeName("void (*)(const GDNativeMethodBindPtr, GDNativeObjectPtr, const GDNativeTypePtr *, GDNativeTypePtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void**, void*, void> object_method_bind_ptrcall;

        [NativeTypeName("void (*)(GDNativeObjectPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void> object_destroy;

        [NativeTypeName("GDNativeObjectPtr (*)(const char *)")]
        public delegate* unmanaged[Cdecl]<sbyte*, void*> global_get_singleton;

        [NativeTypeName("void *(*)(GDNativeObjectPtr, void *, const GDNativeInstanceBindingCallbacks *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, GDNativeInstanceBindingCallbacks*, void*> object_get_instance_binding;

        [NativeTypeName("void (*)(GDNativeObjectPtr, void *, void *, const GDNativeInstanceBindingCallbacks *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*, GDNativeInstanceBindingCallbacks*, void> object_set_instance_binding;

        [NativeTypeName("void (*)(GDNativeObjectPtr, const char *, GDExtensionClassInstancePtr)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, void*, void> object_set_instance;

        [NativeTypeName("GDNativeObjectPtr (*)(const GDNativeObjectPtr, void *)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void*> object_cast_to;

        [NativeTypeName("GDNativeObjectPtr (*)(GDObjectInstanceID)")]
        public delegate* unmanaged[Cdecl]<nuint, void*> object_get_instance_from_id;

        [NativeTypeName("GDObjectInstanceID (*)(const GDNativeObjectPtr)")]
        public delegate* unmanaged[Cdecl]<void*, nuint> object_get_instance_id;

        [NativeTypeName("GDNativeScriptInstancePtr (*)(const GDNativeExtensionScriptInstanceInfo *, GDNativeExtensionScriptInstanceDataPtr)")]
        public delegate* unmanaged[Cdecl]<GDNativeExtensionScriptInstanceInfo*, void*, void*> script_instance_create;

        [NativeTypeName("GDNativeObjectPtr (*)(const char *)")]
        public delegate* unmanaged[Cdecl]<sbyte*, void*> classdb_construct_object;

        [NativeTypeName("GDNativeMethodBindPtr (*)(const char *, const char *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<sbyte*, sbyte*, nint, void*> classdb_get_method_bind;

        [NativeTypeName("void *(*)(const char *)")]
        public delegate* unmanaged[Cdecl]<sbyte*, void*> classdb_get_class_tag;

        [NativeTypeName("void (*)(const GDNativeExtensionClassLibraryPtr, const char *, const char *, const GDNativeExtensionClassCreationInfo *)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, sbyte*, GDNativeExtensionClassCreationInfo*, void> classdb_register_extension_class;

        [NativeTypeName("void (*)(const GDNativeExtensionClassLibraryPtr, const char *, const GDNativeExtensionClassMethodInfo *)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, GDNativeExtensionClassMethodInfo*, void> classdb_register_extension_class_method;

        [NativeTypeName("void (*)(const GDNativeExtensionClassLibraryPtr, const char *, const char *, const char *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, sbyte*, sbyte*, nint, void> classdb_register_extension_class_integer_constant;

        [NativeTypeName("void (*)(const GDNativeExtensionClassLibraryPtr, const char *, const GDNativePropertyInfo *, const char *, const char *)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, GDNativePropertyInfo*, sbyte*, sbyte*, void> classdb_register_extension_class_property;

        [NativeTypeName("void (*)(const GDNativeExtensionClassLibraryPtr, const char *, const char *, const char *)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, sbyte*, sbyte*, void> classdb_register_extension_class_property_group;

        [NativeTypeName("void (*)(const GDNativeExtensionClassLibraryPtr, const char *, const char *, const char *)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, sbyte*, sbyte*, void> classdb_register_extension_class_property_subgroup;

        [NativeTypeName("void (*)(const GDNativeExtensionClassLibraryPtr, const char *, const char *, const GDNativePropertyInfo *, GDNativeInt)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, sbyte*, GDNativePropertyInfo*, nint, void> classdb_register_extension_class_signal;

        [NativeTypeName("void (*)(const GDNativeExtensionClassLibraryPtr, const char *)")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, void> classdb_unregister_extension_class;

        [NativeTypeName("void (*)(const GDNativeExtensionClassLibraryPtr, GDNativeStringPtr)")]
        public delegate* unmanaged[Cdecl]<void*, void*, void> get_library_path;
    }
}
