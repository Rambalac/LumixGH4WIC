using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[ComConversionLoss]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct _BYTE_SIZEDARR
	{
		public uint clSize;

		[ComConversionLoss]
		public IntPtr pData;
	}
}
