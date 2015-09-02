using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct tagIDLDESC
	{
		[ComAliasName("WIC.ULONG_PTR")]
		public ulong dwReserved;

		public ushort wIDLFlags;
	}
}
