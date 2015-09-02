using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[ComConversionLoss]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct tagCAUH
	{
		public uint cElems;

		[ComConversionLoss]
		public IntPtr pElems;
	}
}
