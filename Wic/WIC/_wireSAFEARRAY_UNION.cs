using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct _wireSAFEARRAY_UNION
	{
		public uint sfType;

		public __MIDL_IOleAutomationTypes_0001 u;
	}
}
