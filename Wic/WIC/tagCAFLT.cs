using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[ComConversionLoss]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct tagCAFLT
	{
		public uint cElems;

		[ComConversionLoss]
		public IntPtr pElems;
	}
}
