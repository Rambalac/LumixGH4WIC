using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[ComConversionLoss]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct WICBitmapPlane
	{
		[ComAliasName("WIC.WICPixelFormatGUID")]
		public WICPixelFormatGUID Format;

		[ComConversionLoss]
		public IntPtr pbBuffer;

		public uint cbStride;

		public uint cbBufferSize;
	}
}
