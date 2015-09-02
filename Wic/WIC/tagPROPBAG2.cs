using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct tagPROPBAG2
	{
		public uint dwType;

		public ushort vt;

		[ComAliasName("WIC.wireCLIPFORMAT")]
		public IntPtr cfType;

		public uint dwHint;

		[MarshalAs(UnmanagedType.LPWStr)]
		public string pstrName;

		public Guid clsid;
	}
}
