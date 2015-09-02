using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[ComConversionLoss, Guid("00020401-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ITypeInfo
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteGetTypeAttr([Out] IntPtr ppTypeAttr, [ComAliasName("WIC.DWORD")] out uint pDummy);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetTypeComp([MarshalAs(UnmanagedType.Interface)] out ITypeComp ppTComp);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteGetFuncDesc([In] uint index, [Out] IntPtr ppFuncDesc, [ComAliasName("WIC.DWORD")] out uint pDummy);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteGetVarDesc([In] uint index, [Out] IntPtr ppVarDesc, [ComAliasName("WIC.DWORD")] out uint pDummy);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteGetNames([In] int memid, [MarshalAs(UnmanagedType.BStr)] out string rgBstrNames, [In] uint cMaxNames, out uint pcNames);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetRefTypeOfImplType([In] uint index, out uint pRefType);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetImplTypeFlags([In] uint index, out int pImplTypeFlags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void LocalGetIDsOfNames();

		[MethodImpl(MethodImplOptions.InternalCall)]
		void LocalInvoke();

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteGetDocumentation([In] int memid, [In] uint refPtrFlags, [MarshalAs(UnmanagedType.BStr)] out string pbstrName, [MarshalAs(UnmanagedType.BStr)] out string pBstrDocString, out uint pdwHelpContext, [MarshalAs(UnmanagedType.BStr)] out string pBstrHelpFile);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteGetDllEntry([In] int memid, [In] tagINVOKEKIND invkind, [In] uint refPtrFlags, [MarshalAs(UnmanagedType.BStr)] out string pBstrDllName, [MarshalAs(UnmanagedType.BStr)] out string pbstrName, out ushort pwOrdinal);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetRefTypeInfo([In] uint hreftype, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.TypeToTypeInfoMarshaler")] out Type ppTInfo);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void LocalAddressOfMember();

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteCreateInstance([In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetMops([In] int memid, [MarshalAs(UnmanagedType.BStr)] out string pBstrMops);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteGetContainingTypeLib([MarshalAs(UnmanagedType.Interface)] out ITypeLib ppTLib, out uint pIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void LocalReleaseTypeAttr();

		[MethodImpl(MethodImplOptions.InternalCall)]
		void LocalReleaseFuncDesc();

		[MethodImpl(MethodImplOptions.InternalCall)]
		void LocalReleaseVarDesc();
	}
}
