using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WIC
{
	[Guid("9EDDE9E7-8DEE-47EA-99DF-E6FAF2ED44BF"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICBitmapDecoder
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void QueryCapability([MarshalAs(UnmanagedType.Interface)] [In] IStream pIStream, out uint pdwCapability);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Initialize([MarshalAs(UnmanagedType.Interface)] [In] IStream pIStream, [In] WICDecodeOptions cacheOptions);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetContainerFormat(out Guid pguidContainerFormat);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetDecoderInfo([MarshalAs(UnmanagedType.Interface)] out IWICBitmapDecoderInfo ppIDecoderInfo);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CopyPalette([MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetMetadataQueryReader([MarshalAs(UnmanagedType.Interface)] out IWICMetadataQueryReader ppIMetadataQueryReader);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetPreview([MarshalAs(UnmanagedType.Interface)] out IWICBitmapSource ppIBitmapSource);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetColorContexts([In] uint cCount, [MarshalAs(UnmanagedType.Interface)] [In] [Out] ref IWICColorContext ppIColorContexts, out uint pcActualCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetThumbnail([MarshalAs(UnmanagedType.Interface)] out IWICBitmapSource ppIThumbnail);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetFrameCount(out uint pCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetFrame([In] uint index, [MarshalAs(UnmanagedType.Interface)] out IWICBitmapFrameDecode ppIBitmapFrame);
	}
}
