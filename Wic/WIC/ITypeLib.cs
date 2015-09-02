using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[ComConversionLoss, Guid("00020402-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ITypeLib
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteGetTypeInfoCount(out uint pcTInfo);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetTypeInfo([In] uint index, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.TypeToTypeInfoMarshaler")] out Type ppTInfo);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetTypeInfoType([In] uint index, out tagTYPEKIND pTKind);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetTypeInfoOfGuid([In] ref Guid guid, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.TypeToTypeInfoMarshaler")] out Type ppTInfo);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteGetLibAttr([Out] IntPtr ppTLibAttr, [ComAliasName("WIC.DWORD")] out uint pDummy);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetTypeComp([MarshalAs(UnmanagedType.Interface)] out ITypeComp ppTComp);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteGetDocumentation([In] int index, [In] uint refPtrFlags, [MarshalAs(UnmanagedType.BStr)] out string pbstrName, [MarshalAs(UnmanagedType.BStr)] out string pBstrDocString, out uint pdwHelpContext, [MarshalAs(UnmanagedType.BStr)] out string pBstrHelpFile);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteIsName([MarshalAs(UnmanagedType.LPWStr)] [In] string szNameBuf, [In] uint lHashVal, out int pfName, [MarshalAs(UnmanagedType.BStr)] out string pBstrLibName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteFindName([MarshalAs(UnmanagedType.LPWStr)] [In] string szNameBuf, [In] uint lHashVal, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.TypeToTypeInfoMarshaler")] out Type ppTInfo, out int rgMemId, [In] [Out] ref ushort pcFound, [MarshalAs(UnmanagedType.BStr)] out string pBstrLibName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void LocalReleaseTLibAttr();
	}
}
