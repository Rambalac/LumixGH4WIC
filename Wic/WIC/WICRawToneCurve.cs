using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct WICRawToneCurve
	{
		public uint cPoints;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public WICRawToneCurvePoint[] aPoints;
	}
}
