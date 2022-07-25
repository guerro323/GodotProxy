using System;
using System.Diagnostics;
using GDNative;
using GodotCLR.HighLevel;

namespace GodotCLR
{
    public unsafe partial class Native
    {
        public static void Load(GDNativeInterface* interfacePtr, void* library)
        {
            Debug.Assert(interfacePtr != null, "interfacePtr != null");
            
            _interface = interfacePtr;
            _library = library;
            
            UtilityFunctions.Load();
            GD.Load();
        }
    }
}