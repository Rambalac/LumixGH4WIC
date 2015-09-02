using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct WICDdsFormatInfo
	{
		public DXGI_FORMAT DxgiFormat;

		public uint BytesPerBlock;

		public uint BlockWidth;

		public uint BlockHeight;
	}
}
