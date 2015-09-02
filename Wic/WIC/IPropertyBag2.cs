using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("22F55882-280B-11D0-A8A9-00A0C90C2004"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IPropertyBag2
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Read([In] uint cProperties, [In] ref tagPROPBAG2 pPropBag, [MarshalAs(UnmanagedType.Interface)] [In] IErrorLog pErrLog, [MarshalAs(UnmanagedType.Struct)] out object pvarValue, [MarshalAs(UnmanagedType.Error)] [In] [Out] ref int phrError);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Write([In] uint cProperties, [In] ref tagPROPBAG2 pPropBag, [MarshalAs(UnmanagedType.Struct)] [In] ref object pvarValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CountProperties(out uint pcProperties);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetPropertyInfo([In] uint iProperty, [In] uint cProperties, out tagPROPBAG2 pPropBag, out uint pcProperties);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void LoadObject([MarshalAs(UnmanagedType.LPWStr)] [In] string pstrName, [In] uint dwHint, [MarshalAs(UnmanagedType.IUnknown)] [In] object pUnkObject, [MarshalAs(UnmanagedType.Interface)] [In] IErrorLog pErrLog);
	}
}
