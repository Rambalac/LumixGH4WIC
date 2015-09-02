using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("00000301-A8F2-4877-BA0A-FD2B6645FB94"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICFormatConverter : IWICBitmapSource
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
		void Initialize([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pISource, [ComAliasName("WIC.REFWICPixelFormatGUID")] [In] ref Guid dstFormat, [In] WICBitmapDitherType dither, [MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette, [In] double alphaThresholdPercent, [In] WICBitmapPaletteType paletteTranslate);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CanConvert([ComAliasName("WIC.REFWICPixelFormatGUID")] [In] ref Guid srcPixelFormat, [ComAliasName("WIC.REFWICPixelFormatGUID")] [In] ref Guid dstPixelFormat, out int pfCanConvert);
	}
}
