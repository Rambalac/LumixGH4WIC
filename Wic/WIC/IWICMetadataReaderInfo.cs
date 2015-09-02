using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WIC
{
	[ComConversionLoss, Guid("EEBF1F5B-07C1-4447-A3AB-22ACAF78A804"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICMetadataReaderInfo : IWICMetadataHandlerInfo
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
		void GetMetadataFormat(out Guid pguidMetadataFormat);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetContainerFormats([In] uint cContainerFormats, [In] [Out] ref Guid pguidContainerFormats, out uint pcchActual);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetDeviceManufacturer([In] uint cchDeviceManufacturer, [In] [Out] ref ushort wzDeviceManufacturer, out uint pcchActual);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetDeviceModels([In] uint cchDeviceModels, [In] [Out] ref ushort wzDeviceModels, out uint pcchActual);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void DoesRequireFullStream(out int pfRequiresFullStream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void DoesSupportPadding(out int pfSupportsPadding);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void DoesRequireFixedSize(out int pfFixedSize);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Remote_GetPatterns([In] ref Guid guidContainerFormat, [Out] IntPtr ppPatterns, out uint pcPatterns);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void MatchesPattern([In] ref Guid guidContainerFormat, [MarshalAs(UnmanagedType.Interface)] [In] IStream pIStream, out int pfMatches);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateInstance([MarshalAs(UnmanagedType.Interface)] out IWICMetadataReader ppIReader);
	}
}
