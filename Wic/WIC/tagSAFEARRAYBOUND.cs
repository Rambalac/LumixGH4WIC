using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct tagSAFEARRAYBOUND
	{
		public uint cElements;

		public int lLbound;
	}
}
