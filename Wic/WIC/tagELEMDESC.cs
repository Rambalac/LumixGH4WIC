using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct tagELEMDESC
	{
		public tagTYPEDESC tdesc;

		public tagPARAMDESC paramdesc;
	}
}
