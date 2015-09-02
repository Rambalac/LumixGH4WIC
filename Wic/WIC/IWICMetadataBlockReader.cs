using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("FEAA2A8D-B3F3-43E4-B25C-D1DE990A1AE1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICMetadataBlockReader
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetContainerFormat(out Guid pguidContainerFormat);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCount(out uint pcCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetReaderByIndex([In] uint nIndex, [MarshalAs(UnmanagedType.Interface)] out IWICMetadataReader ppIMetadataReader);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetEnumerator([MarshalAs(UnmanagedType.Interface)] out IEnumUnknown ppIEnumMetadata);
	}
}
