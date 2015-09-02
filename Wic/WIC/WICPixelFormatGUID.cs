using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct WICPixelFormatGUID
	{
        [MarshalAs(UnmanagedType.U4)]
        public int Data1;

        [MarshalAs(UnmanagedType.U2)]
        public short Data2;

        [MarshalAs(UnmanagedType.U2)]
        public short Data3;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] Data4;
	}
}
