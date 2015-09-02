using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("BEBEE9CB-83B0-4DCC-8132-B0AAA55EAC96"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICPlanarFormatConverter : IWICBitmapSource
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
		void Initialize([MarshalAs(UnmanagedType.Interface)] [In] ref IWICBitmapSource ppPlanes, uint cPlanes, [ComAliasName("WIC.REFWICPixelFormatGUID")] [In] ref Guid dstFormat, [In] WICBitmapDitherType dither, [MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette, [In] double alphaThresholdPercent, [In] WICBitmapPaletteType paletteTranslate);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CanConvert([ComAliasName("WIC.WICPixelFormatGUID")] [In] ref WICPixelFormatGUID pSrcPixelFormats, uint cSrcPlanes, [ComAliasName("WIC.REFWICPixelFormatGUID")] [In] ref Guid dstPixelFormat, out int pfCanConvert);
	}
}
