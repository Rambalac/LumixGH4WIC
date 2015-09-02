using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[ComConversionLoss]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct WICMetadataHeader
	{
		public _ULARGE_INTEGER Position;

		public uint Length;

		[ComConversionLoss]
		public IntPtr Header;

		public _ULARGE_INTEGER DataOffset;
	}
}
