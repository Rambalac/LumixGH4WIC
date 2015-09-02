using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[ComConversionLoss]
	[StructLayout(LayoutKind.Explicit, Pack = 8, Size = 8)]
	public struct __MIDL_IWinTypes_0007
	{
		[FieldOffset(0)]
		public int hInproc;

		[FieldOffset(0)]
		public long hInproc64;
	}
}
