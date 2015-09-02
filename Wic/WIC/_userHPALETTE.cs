using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct _userHPALETTE
	{
		public int fContext;

		public __MIDL_IWinTypes_0008 u;
	}
}
