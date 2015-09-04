using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace WIC
{
    [Guid("ABA958BF-C672-44D1-8D61-CE6DF2E682C2"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IWICMetadataHandlerInfo : IWICComponentInfo
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetComponentType(out WICComponentType pType);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetCLSID(out Guid pclsid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetSigningStatus(out uint pStatus);


        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetVendorGUID(out Guid pguidVendor);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetSpecVersion([In] uint cchSpecVersion, [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 0)] StringBuilder wzSpecVersion, out uint pcchActual);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetMetadataFormat(out Guid pguidMetadataFormat);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetContainerFormats([In] uint cContainerFormats, [In] [Out] ref Guid pguidContainerFormats, out uint pcchActual);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetDeviceManufacturer([In] uint cchDeviceManufacturer, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder wzDeviceManufacturer, out uint pcchActual);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetDeviceModels([In] uint cchDeviceModels, [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 0)] StringBuilder wzDeviceModels, out uint pcchActual);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void DoesRequireFullStream(out int pfRequiresFullStream);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void DoesSupportPadding(out int pfSupportsPadding);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void DoesRequireFixedSize(out int pfFixedSize);
    }
}
