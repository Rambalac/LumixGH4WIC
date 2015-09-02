using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[ComConversionLoss]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct _wireSAFEARR_UNKNOWN
	{
		public uint Size;

		[ComConversionLoss]
		public IntPtr apUnknown;
	}
}
