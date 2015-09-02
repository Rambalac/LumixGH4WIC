using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct WICBitmapPlaneDescription
	{
		[ComAliasName("WIC.WICPixelFormatGUID")]
		public WICPixelFormatGUID Format;

		public uint Width;

		public uint Height;
	}
}
