using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct _userCLIPFORMAT
	{
		public int fContext;

		public __MIDL_IWinTypes_0001 u;
	}
}
