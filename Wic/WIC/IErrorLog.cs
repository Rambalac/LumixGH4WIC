using stdole;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("3127CA40-446E-11CE-8135-00AA004BB851"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IErrorLog
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddError([MarshalAs(UnmanagedType.LPWStr)] [In] string pszPropName, [In] ref stdole.EXCEPINFO pExcepInfo);
	}
}
