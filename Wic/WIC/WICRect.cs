using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct WICRect
	{
		public int X;

		public int Y;

		public int Width;

		public int Height;
	}
}
