using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("DC2BB46D-3F07-481E-8625-220C4AEDBB33"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICEnumMetadataItem
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Next([In] uint celt, [In] [Out] ref tag_inner_PROPVARIANT rgeltSchema, [In] [Out] ref tag_inner_PROPVARIANT rgeltId, [In] [Out] ref tag_inner_PROPVARIANT rgeltValue, out uint pceltFetched);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Skip([In] uint celt);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Reset();

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Clone([MarshalAs(UnmanagedType.Interface)] out IWICEnumMetadataItem ppIEnumMetadataItem);
	}
}
