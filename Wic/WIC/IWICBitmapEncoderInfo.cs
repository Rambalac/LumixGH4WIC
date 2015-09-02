using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("94C9B4EE-A09F-4F92-8A1E-4A9BCE7E76FB"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICBitmapEncoderInfo : IWICBitmapCodecInfo
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetComponentType(out WICComponentType pType);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCLSID(out Guid pclsid);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetSigningStatus(out uint pStatus);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetAuthor([In] uint cchAuthor, [In] [Out] ref ushort wzAuthor, out uint pcchActual);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetVendorGUID(out Guid pguidVendor);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetVersion([In] uint cchVersion, [In] [Out] ref ushort wzVersion, out uint pcchActual);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetSpecVersion([In] uint cchSpecVersion, [In] [Out] ref ushort wzSpecVersion, out uint pcchActual);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetFriendlyName([In] uint cchFriendlyName, [In] [Out] ref ushort wzFriendlyName, out uint pcchActual);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetContainerFormat(out Guid pguidContainerFormat);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetPixelFormats([In] uint cFormats, [In] [Out] ref Guid pguidPixelFormats, out uint pcActual);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetColorManagementVersion([In] uint cchColorManagementVersion, [In] [Out] ref ushort wzColorManagementVersion, out uint pcchActual);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetDeviceManufacturer([In] uint cchDeviceManufacturer, [In] [Out] ref ushort wzDeviceManufacturer, out uint pcchActual);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetDeviceModels([In] uint cchDeviceModels, [In] [Out] ref ushort wzDeviceModels, out uint pcchActual);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetMimeTypes([In] uint cchMimeTypes, [In] [Out] ref ushort wzMimeTypes, out uint pcchActual);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetFileExtensions([In] uint cchFileExtensions, [In] [Out] ref ushort wzFileExtensions, out uint pcchActual);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void DoesSupportAnimation(out int pfSupportAnimation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void DoesSupportChromakey(out int pfSupportChromakey);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void DoesSupportLossless(out int pfSupportLossless);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void DoesSupportMultiframe(out int pfSupportMultiframe);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void MatchesMimeType([MarshalAs(UnmanagedType.LPWStr)] [In] string wzMimeType, out int pfMatches);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateInstance([MarshalAs(UnmanagedType.Interface)] out IWICBitmapEncoder ppIBitmapEncoder);
	}
}
