using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("F928B7B8-2221-40C1-B72E-7E82F1974D1A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICPlanarBitmapFrameEncode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void WritePixels(uint lineCount, [In] ref WICBitmapPlane pPlanes, uint cPlanes);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void WriteSource([MarshalAs(UnmanagedType.Interface)] [In] ref IWICBitmapSource ppPlanes, uint cPlanes, [In] ref WICRect prcSource);
	}
}
