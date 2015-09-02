using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[ComConversionLoss]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct WICBitmapPattern
	{
		public _ULARGE_INTEGER Position;

		public uint Length;

		[ComConversionLoss]
		public IntPtr Pattern;

		[ComConversionLoss]
		public IntPtr Mask;

		public int EndOfStream;
	}
}
