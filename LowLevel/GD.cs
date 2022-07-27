using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using GDNative;
using GodotCLR.HighLevel;
using GodotCLR.NativeStructs;

namespace GodotCLR;

public static unsafe partial class GD
{
    public interface IHasPointer
    {
        IntPtr Pointer { get; }
    }

    internal static void Load()
    {
        // Singletons
        Engine.GdLoad();
        SceneTree.GdLoad();
        ResourceLoader.GdLoad();
        // Object
        Object.GdLoad();
        Ref.GdLoad();
        // Resource<>
        Resource.GdLoad();
        PackedScene.GdLoad();
        // Nodes
        Node.GdLoad();
        Node3D.GdLoad();
    }

    internal static Variant InvokeVariant(void* methodPtr, void* instance, Span<Variant> variants,
        out GDNativeCallError error)
    {
        if (methodPtr == null)
            throw new NullReferenceException(nameof(methodPtr));

        Unsafe.SkipInit(out error);
        Unsafe.SkipInit(out Variant result);

        Span<IntPtr> args = stackalloc IntPtr[variants.Length];
        for (var i = 0; i < variants.Length; i++)
            args[i] = (IntPtr) Unsafe.AsPointer(ref variants[i]);

        Native.Interface.object_method_bind_call(
            methodPtr,
            instance,
            (void**) Unsafe.AsPointer(ref MemoryMarshal.GetReference(args)),
            variants.Length,
            &result,
            null
        );

        return result;
    }

    internal static T InvokePtr<T>(void* methodPtr, void* instance, ReadOnlySpan<nuint> args)
    {
        if (methodPtr == null)
            throw new NullReferenceException(nameof(methodPtr));
        if (instance == null)
            throw new NullReferenceException(nameof(instance));

        Unsafe.SkipInit(out T ret);

        Native.Interface.object_method_bind_ptrcall(
            methodPtr,
            instance,
            (void**) Unsafe.AsPointer(ref MemoryMarshal.GetReference(args)),
            Unsafe.AsPointer(ref ret)
        );

        return ret;
    }

    internal static void* GetMethodBind(string className, string methodName, long hashCode)
    {
        if (hashCode == 0)
            throw new InvalidOperationException(nameof(hashCode));

        using var classNameUtf8 = new Utf8Array(className);
        using var methodNameUtf8 = new Utf8Array(methodName);
        var method = Native.Interface.classdb_get_method_bind(
            (sbyte*) classNameUtf8.Pointer,
            (sbyte*) methodNameUtf8.Pointer,
            (nint) hashCode
        );

        if (method == null)
            throw new InvalidOperationException(
                $"Couldn't get method '{methodName}' from '{className}' with hash {hashCode}"
            );

        return method;
    }

    [UnmanagedCallersOnly(CallConvs = new[] {typeof(CallConvCdecl)})]
    static void* class_vtable_create(void* userData)
    {
        if (userData == null)
        {
            Console.WriteLine("received null creation data");
            return null;
        }

        ref readonly var classData = ref *(ClassData*) userData;
        
        var alloc = NativeMemory.Alloc(classData.Size);
        var owner = Native.Interface.classdb_construct_object(classData.ParentName);
        Native.Interface.object_set_instance(owner, classData.Name, alloc);
        Native.Interface.object_set_instance_binding(
            owner,
            Native.Library.ToPointer(),
            alloc,
            (GDNativeInstanceBindingCallbacks*) Unsafe.AsPointer(ref class_binding_callbacks)
        );

        return owner;
    }

    [UnmanagedCallersOnly(CallConvs = new[] {typeof(CallConvCdecl)})]
    static void class_vtable_free(void* userData, void* instance)
    {
        if (instance == null)
        {
            Console.WriteLine("Received a null instance");
            return;
        }

        NativeMemory.Free(instance);
    }

    [UnmanagedCallersOnly(CallConvs = new[] {typeof(CallConvCdecl)})]
    static delegate* unmanaged[Cdecl]<void*, void**, void*, void> class_vtable_get_virtual_func(void* userData,
        sbyte* name)
    {
        //Console.WriteLine($"method: {new string(name)}");
        return null;
    }

    [UnmanagedCallersOnly(CallConvs = new[] {typeof(CallConvCdecl)})]
    static void* class_binding_create_callback(void* token, void* instance)
    {
        return null;
    }

    [UnmanagedCallersOnly(CallConvs = new[] {typeof(CallConvCdecl)})]
    static void class_binding_free_callback(void* token, void* instance, void* binding)
    {
    }

    [UnmanagedCallersOnly(CallConvs = new[] {typeof(CallConvCdecl)})]
    static byte class_binding_reference_callback(void* token, void* instance, byte reference)
    {
        return 1;
    }

    private static GDNativeInstanceBindingCallbacks class_binding_callbacks = new()
    {
        create_callback = &class_binding_create_callback,
        free_callback = &class_binding_free_callback,
        reference_callback = &class_binding_reference_callback,
    };

    public static void RegisterClass<T>(string name, string parentName)
        where T : unmanaged
    {
        using var nameUtf8 = new Utf8Array(name);
        using var parentNameUtf8 = new Utf8Array(parentName);

        var data = (ClassData*) NativeMemory.Alloc((nuint) sizeof(ClassData));
        data->Size = (nuint) sizeof(T);
        data->ParentName = (sbyte*) NativeMemory.Alloc((nuint) parentNameUtf8.ByteSpanWithNull.Length);
        {
            parentNameUtf8.ByteSpanWithNull.CopyTo(new Span<byte>(data->ParentName, parentNameUtf8.ByteSpanWithNull.Length));
        }
        data->Name = (sbyte*) NativeMemory.Alloc((nuint) nameUtf8.ByteSpanWithNull.Length);
        {
            nameUtf8.ByteSpanWithNull.CopyTo(new Span<byte>(data->Name, nameUtf8.ByteSpanWithNull.Length));
        }

        var classInfo = new GDNativeExtensionClassCreationInfo
        {
            create_instance_func = &class_vtable_create,
            free_instance_func = &class_vtable_free,
            get_virtual_func = &class_vtable_get_virtual_func,
            class_userdata = data
        };

        Native.Interface.classdb_register_extension_class(
            Native.Library.ToPointer(),
            (sbyte*) nameUtf8.Pointer,
            (sbyte*) parentNameUtf8.Pointer,
            &classInfo
        );
    }

    public delegate Variant OnMethodCall<T>(ref byte methodData, ref T instance, VariantMethodArgs args);

    [UnmanagedCallersOnly(CallConvs = new[] {typeof(CallConvCdecl)})]
    static void class_method_generic_call_func(void* methodData, void* instance, void** args, nint argsCount,
        void* ret, GDNativeCallError* error)
    {
        var handle = Unsafe.AsRef<GCHandle>(methodData);
        if (!handle.IsAllocated)
            throw new InvalidOperationException("freed handle");

        var method = (OnMethodCall<byte>) handle.Target!;
        *(Variant*) ret = method(
            ref Unsafe.AsRef<byte>(methodData),
            ref Unsafe.AsRef<byte>(instance),
            new VariantMethodArgs
            {
                Native = (Variant**) args,
                Length = (int) argsCount
            }
        );
    }

    public static void AddMethod<T>(string className, string methodName, OnMethodCall<T> action,
        ReadOnlySpan<(Variant.EType type, string name)> args, Variant.EType retType,
        GDNativeExtensionClassMethodFlags flags)
        where T : unmanaged
    {
        var nonGeneric = new OnMethodCall<byte>((ref byte methodData, ref byte instance, VariantMethodArgs args) =>
        {
            return action(ref methodData, ref Unsafe.As<byte, T>(ref instance), args);
        });
        var handle = GCHandle.Alloc(nonGeneric, GCHandleType.Normal);
        var data = NativeMemory.Alloc((nuint) sizeof(GCHandle));
        *(GCHandle*) data = handle;
        
        AddMethod(className, methodName, data, &class_method_generic_call_func, args, retType, flags);
    }

    public static void AddMethod(
        string className,
        string methodName,
        void* methodData,
        delegate*unmanaged[Cdecl]<void*, void*, void**, nint, void*, GDNativeCallError*, void> action,
        ReadOnlySpan<(Variant.EType type, string name)> args, Variant.EType retType,
        GDNativeExtensionClassMethodFlags flags
    )
    {
        var argsArray = args.ToArray();
        _ = GCHandle.Alloc(argsArray, GCHandleType.Normal);
        
        using var classNameUtf8 = new Utf8Array(className);
        using var methodNameUtf8 = new Utf8Array(methodName);
        var methodInfo = new GDNativeExtensionClassMethodInfo
        {
            name = (sbyte*) methodNameUtf8.Pointer,
            method_userdata = methodData,
            call_func = action,
            method_flags = (uint) flags,
            argument_count = (uint) args.Length,
            has_return_value = (byte) Variant.EType.NIL,
            get_argument_type_func = (delegate*unmanaged[Cdecl]<void*, int, GDNativeVariantType>)
                Marshal.GetFunctionPointerForDelegate((void* _, int index) =>
                {
                    if (index == -1)
                    {
                        return (GDNativeVariantType) retType;
                    }

                    return (GDNativeVariantType) (argsArray[index].type - 1);
                }),
            get_argument_info_func = (delegate*unmanaged[Cdecl]<void*, int, GDNativePropertyInfo*, void>)
                Marshal.GetFunctionPointerForDelegate((void* _, int index, GDNativePropertyInfo* info) =>
                {
                    var name = index == -1 ? "ret" : argsArray[index].name;
                    using var nameUtf8 = new Utf8Array(name);
                    
                    info[0] = new GDNativePropertyInfo
                    {
                        type = (uint) retType,
                        name = (sbyte*) nameUtf8.Pointer, 
                        usage = (uint) GDNativeExtensionClassMethodFlags.GDNATIVE_EXTENSION_METHOD_FLAG_NORMAL
                    };
                }),
            get_argument_metadata_func = (delegate*unmanaged[Cdecl]<void*, int, GDNativeExtensionClassMethodArgumentMetadata>)
                Marshal.GetFunctionPointerForDelegate((void* _, int index) =>
                {
                    return GDNativeExtensionClassMethodArgumentMetadata
                        .GDNATIVE_EXTENSION_METHOD_ARGUMENT_METADATA_NONE;
                }),
        };

        Native.Interface.classdb_register_extension_class_method(
            Native.Library.ToPointer(),
            (sbyte*) classNameUtf8.Pointer,
            &methodInfo
        );
    }

    private struct ClassData
    {
        public nuint Size;
        public sbyte* ParentName;
        public sbyte* Name;
    }

    public struct Method
    {

    }
}