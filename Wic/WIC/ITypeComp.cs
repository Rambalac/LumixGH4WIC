using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[ComConversionLoss, Guid("00020403-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ITypeComp
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteBind([MarshalAs(UnmanagedType.LPWStr)] [In] string szName, [In] uint lHashVal, [In] ushort wFlags, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.TypeToTypeInfoMarshaler")] out Type ppTInfo, out tagDESCKIND pDescKind, [Out] IntPtr ppFuncDesc, [Out] IntPtr ppVarDesc, [MarshalAs(UnmanagedType.Interface)] out ITypeComp ppTypeComp, [ComAliasName("WIC.DWORD")] out uint pDummy);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteBindType([MarshalAs(UnmanagedType.LPWStr)] [In] string szName, [In] uint lHashVal, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.TypeToTypeInfoMarshaler")] out Type ppTInfo);
	}
}
