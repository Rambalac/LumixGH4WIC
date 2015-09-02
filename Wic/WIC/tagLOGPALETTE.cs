using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[ComConversionLoss]
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	public struct tagLOGPALETTE
	{
		public ushort palVersion;

		public ushort palNumEntries;

		[ComConversionLoss]
		public IntPtr palPalEntry;
	}
}
