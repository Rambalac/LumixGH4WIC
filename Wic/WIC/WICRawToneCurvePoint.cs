using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct WICRawToneCurvePoint
	{
		public double Input;

		public double Output;
	}
}
