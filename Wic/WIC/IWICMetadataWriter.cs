using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("F7836E16-3BE0-470B-86BB-160D0AECD7DE"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICMetadataWriter : IWICMetadataReader
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

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetValue([In] ref tag_inner_PROPVARIANT pvarSchema, [In] ref tag_inner_PROPVARIANT pvarId, [In] ref tag_inner_PROPVARIANT pvarValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetValueByIndex([In] uint nIndex, [In] ref tag_inner_PROPVARIANT pvarSchema, [In] ref tag_inner_PROPVARIANT pvarId, [In] ref tag_inner_PROPVARIANT pvarValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoveValue([In] ref tag_inner_PROPVARIANT pvarSchema, [In] ref tag_inner_PROPVARIANT pvarId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoveValueByIndex([In] uint nIndex);
	}
}
