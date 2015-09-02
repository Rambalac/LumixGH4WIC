using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("00000105-A8F2-4877-BA0A-FD2B6645FB94"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICBitmapFrameEncode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Initialize([MarshalAs(UnmanagedType.Interface)] [In] IPropertyBag2 pIEncoderOptions);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetSize([In] uint uiWidth, [In] uint uiHeight);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetResolution([In] double dpiX, [In] double dpiY);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetPixelFormat([ComAliasName("WIC.WICPixelFormatGUID")] [In] [Out] ref WICPixelFormatGUID pPixelFormat);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetColorContexts([In] uint cCount, [MarshalAs(UnmanagedType.Interface)] [In] ref IWICColorContext ppIColorContext);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetPalette([MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetThumbnail([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pIThumbnail);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void WritePixels([In] uint lineCount, [In] uint cbStride, [In] uint cbBufferSize, [In] ref byte pbPixels);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void WriteSource([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pIBitmapSource, [In] ref WICRect prc);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Commit();

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetMetadataQueryWriter([MarshalAs(UnmanagedType.Interface)] out IWICMetadataQueryWriter ppIMetadataQueryWriter);
	}
}
