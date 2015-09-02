using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct tagPALETTEENTRY
	{
		public byte peRed;

		public byte peGreen;

		public byte peBlue;

		public byte peFlags;
	}
}
