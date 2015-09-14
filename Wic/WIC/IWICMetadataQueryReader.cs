using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("30989668-E1C9-4597-B395-458EEDB808DF"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICMetadataQueryReader
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetContainerFormat(out Guid pguidContainerFormat);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetLocation([In] uint cchMaxLength, [In] [Out] ref ushort wzNamespace, out uint pcchActualLength);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetMetadataByName([MarshalAs(UnmanagedType.LPWStr)] [In] string wzName, IntPtr pvarValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetEnumerator([MarshalAs(UnmanagedType.Interface)] out IEnumString ppIEnumString);
	}
}
