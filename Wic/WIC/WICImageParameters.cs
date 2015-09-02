using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[ComConversionLoss]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct WICImageParameters
	{
		[ComConversionLoss]
		public IntPtr pixelFormat;

		public float dpiX;

		public float dpiY;

		public float Top;

		public float Left;

		public uint PixelWidth;

		public uint PixelHeight;
	}
}
