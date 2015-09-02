using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("B22E3FBA-3925-4323-B5C1-9EBFC430F236"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICMetadataWriterInfo : IWICMetadataHandlerInfo
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
		void Remote_GetHeader([In] ref Guid guidContainerFormat, out WICMetadataHeader pHeader);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateInstance([MarshalAs(UnmanagedType.Interface)] out IWICMetadataWriter ppIWriter);
	}
}
