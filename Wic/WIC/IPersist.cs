using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("0000010C-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IPersist
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetClassID(out Guid pClassID);
	}
}
