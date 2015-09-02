using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WIC
{
	[Guid("00000103-A8F2-4877-BA0A-FD2B6645FB94"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICBitmapEncoder
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Initialize([MarshalAs(UnmanagedType.Interface)] [In] IStream pIStream, [In] WICBitmapEncoderCacheOption cacheOption);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetContainerFormat(out Guid pguidContainerFormat);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetEncoderInfo([MarshalAs(UnmanagedType.Interface)] out IWICBitmapEncoderInfo ppIEncoderInfo);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetColorContexts([In] uint cCount, [MarshalAs(UnmanagedType.Interface)] [In] ref IWICColorContext ppIColorContext);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetPalette([MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetThumbnail([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pIThumbnail);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetPreview([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pIPreview);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateNewFrame([MarshalAs(UnmanagedType.Interface)] out IWICBitmapFrameEncode ppIFrameEncode, [MarshalAs(UnmanagedType.Interface)] [In] [Out] ref IPropertyBag2 ppIEncoderOptions);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Commit();

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetMetadataQueryWriter([MarshalAs(UnmanagedType.Interface)] out IWICMetadataQueryWriter ppIMetadataQueryWriter);
	}
}
