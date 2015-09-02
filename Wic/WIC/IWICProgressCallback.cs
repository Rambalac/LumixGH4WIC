using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("4776F9CD-9517-45FA-BF24-E89C5EC5C60C"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICProgressCallback
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Notify([In] uint uFrameNum, [In] WICProgressOperation operation, [In] double dblProgress);
	}
}
