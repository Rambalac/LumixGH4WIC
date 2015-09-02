using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("08FB9676-B444-41E8-8DBE-6A53A542BFF1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICMetadataBlockWriter : IWICMetadataBlockReader
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetContainerFormat(out Guid pguidContainerFormat);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCount(out uint pcCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetReaderByIndex([In] uint nIndex, [MarshalAs(UnmanagedType.Interface)] out IWICMetadataReader ppIMetadataReader);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetEnumerator([MarshalAs(UnmanagedType.Interface)] out IEnumUnknown ppIEnumMetadata);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void InitializeFromBlockReader([MarshalAs(UnmanagedType.Interface)] [In] IWICMetadataBlockReader pIMDBlockReader);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetWriterByIndex([In] uint nIndex, [MarshalAs(UnmanagedType.Interface)] out IWICMetadataWriter ppIMetadataWriter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddWriter([MarshalAs(UnmanagedType.Interface)] [In] IWICMetadataWriter pIMetadataWriter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetWriterByIndex([In] uint nIndex, [MarshalAs(UnmanagedType.Interface)] [In] IWICMetadataWriter pIMetadataWriter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoveWriterByIndex([In] uint nIndex);
	}
}
