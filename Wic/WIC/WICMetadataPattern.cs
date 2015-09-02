using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[ComConversionLoss]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct WICMetadataPattern
	{
		public _ULARGE_INTEGER Position;

		public uint Length;

		[ComConversionLoss]
		public IntPtr Pattern;

		[ComConversionLoss]
		public IntPtr Mask;

		public _ULARGE_INTEGER DataOffset;
	}
}
