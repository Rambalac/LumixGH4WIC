using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct tag_inner_PROPVARIANT
	{
		public ushort vt;

		public byte wReserved1;

		public byte wReserved2;

		public uint wReserved3;

		public __MIDL___MIDL_itf_wic_0001_0002_0001 value;
	}
}
