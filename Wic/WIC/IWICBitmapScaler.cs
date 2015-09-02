using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("00000302-A8F2-4877-BA0A-FD2B6645FB94"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICBitmapScaler : IWICBitmapSource
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
		void Initialize([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pISource, [In] uint uiWidth, [In] uint uiHeight, [In] WICBitmapInterpolationMode mode);
	}
}
