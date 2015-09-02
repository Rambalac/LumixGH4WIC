using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct WICDdsParameters
	{
		public uint Width;

		public uint Height;

		public uint Depth;

		public uint MipLevels;

		public uint ArraySize;

		public DXGI_FORMAT DxgiFormat;

		public WICDdsDimension Dimension;

		public WICDdsAlphaMode AlphaMode;
	}
}
