using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[ComConversionLoss]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct tagPARAMDESC
	{
		[ComConversionLoss]
		public IntPtr pparamdescex;

		public ushort wParamFlags;
	}
}
