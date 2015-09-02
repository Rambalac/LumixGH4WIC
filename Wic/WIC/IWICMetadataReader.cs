using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("9204FE99-D8FC-4FD5-A001-9536B067A899"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICMetadataReader
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetMetadataFormat(out Guid pguidMetadataFormat);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetMetadataHandlerInfo([MarshalAs(UnmanagedType.Interface)] out IWICMetadataHandlerInfo ppIHandler);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCount(out uint pcCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetValueByIndex([In] uint nIndex, [In] [Out] ref tag_inner_PROPVARIANT pvarSchema, [In] [Out] ref tag_inner_PROPVARIANT pvarId, [In] [Out] ref tag_inner_PROPVARIANT pvarValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetValue([In] ref tag_inner_PROPVARIANT pvarSchema, [In] ref tag_inner_PROPVARIANT pvarId, [In] [Out] ref tag_inner_PROPVARIANT pvarValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetEnumerator([MarshalAs(UnmanagedType.Interface)] out IWICEnumMetadataItem ppIEnumMetadata);
	}
}