using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[ComConversionLoss]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct tagBLOB
	{
		public uint cbSize;

		[ComConversionLoss]
		public IntPtr pBlobData;
	}
}
