using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("DAAC296F-7AA5-4DBF-8D15-225C5976F891"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICProgressiveLevelControl
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		uint GetLevelCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		uint GetCurrentLevel();

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetCurrentLevel([In] uint nLevel);
	}
}
