using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("B66F034F-D0E2-40AB-B436-6DE39E321A94"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICColorTransform : IWICBitmapSource
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetSize(out uint puiWidth, out uint puiHeight);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetPixelFormat([ComAliasName("WIC.WICPixelFormatGUID")] out WICPixelFormatGUID pPixelFormat);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetResolution(out double pDpiX, out double pDpiY);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CopyPalette([MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CopyPixels([In] ref WICRect prc, [In] uint cbStride, [In] uint cbBufferSize, out byte pbBuffer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Initialize([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pIBitmapSource, [MarshalAs(UnmanagedType.Interface)] [In] IWICColorContext pIContextSource, [MarshalAs(UnmanagedType.Interface)] [In] IWICColorContext pIContextDest, [ComAliasName("WIC.REFWICPixelFormatGUID")] [In] ref Guid pixelFmtDest);
	}
}
