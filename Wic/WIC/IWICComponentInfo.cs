using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace WIC
{
    [Guid("23BC3F0A-698B-4357-886B-F24D50671334"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IWICComponentInfo
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetComponentType(out WICComponentType pType);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetCLSID(out Guid pclsid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetSigningStatus(out uint pStatus);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetAuthor([In] uint cchAuthor, [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 0)] StringBuilder wzAuthor, out uint pcchActual);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetVendorGUID(out Guid pguidVendor);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetVersion([In] uint cchVersion, [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 0)] StringBuilder wzVersion, out uint pcchActual);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetSpecVersion([In] uint cchSpecVersion, [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 0)] StringBuilder wzSpecVersion, out uint pcchActual);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetFriendlyName([In] uint cchFriendlyName, [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 0)] StringBuilder wzFriendlyName, out uint pcchActual);
    }
}
