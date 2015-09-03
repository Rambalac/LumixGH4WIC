using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
    [ComConversionLoss]
    [TypeLibType(512)]
    public struct EXCEPINFO
    {
        public string bstrDescription;
        public string bstrHelpFile;
        public string bstrSource;
        public uint dwHelpContext;
        [ComConversionLoss]
        public IntPtr pfnDeferredFillIn;
        [ComConversionLoss]
        public IntPtr pvReserved;
        public int scode;
        public ushort wCode;
        public ushort wReserved;
    }

    [Guid("3127CA40-446E-11CE-8135-00AA004BB851"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IErrorLog
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddError([MarshalAs(UnmanagedType.LPWStr)] [In] string pszPropName, [In] ref EXCEPINFO pExcepInfo);
	}
}
