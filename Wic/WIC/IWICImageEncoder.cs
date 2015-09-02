using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[ComConversionLoss, Guid("04C75BF8-3CE1-473B-ACC5-3CC4F5E94999"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICImageEncoder
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void WriteFrame([In] IntPtr pImage, [MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapFrameEncode pFrameEncode, [In] ref WICImageParameters pImageParameters);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void WriteFrameThumbnail([In] IntPtr pImage, [MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapFrameEncode pFrameEncode, [In] ref WICImageParameters pImageParameters);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void WriteThumbnail([In] IntPtr pImage, [MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapEncoder pEncoder, [In] ref WICImageParameters pImageParameters);
	}
}
