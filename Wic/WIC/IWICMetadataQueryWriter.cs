using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("A721791A-0DEF-4D06-BD91-2118BF1DB10B"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICMetadataQueryWriter : IWICMetadataQueryReader
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetContainerFormat(out Guid pguidContainerFormat);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetLocation([In] uint cchMaxLength, [In] [Out] ref ushort wzNamespace, out uint pcchActualLength);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetMetadataByName([MarshalAs(UnmanagedType.LPWStr)] [In] string wzName, [In] [Out] ref tag_inner_PROPVARIANT pvarValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetEnumerator([MarshalAs(UnmanagedType.Interface)] out IEnumString ppIEnumString);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetMetadataByName([MarshalAs(UnmanagedType.LPWStr)] [In] string wzName, [In] ref tag_inner_PROPVARIANT pvarValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoveMetadataByName([MarshalAs(UnmanagedType.LPWStr)] [In] string wzName);
	}
}
