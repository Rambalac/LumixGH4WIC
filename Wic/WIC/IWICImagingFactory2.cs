using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WIC
{
	[ComConversionLoss, Guid("7B816B45-1996-4476-B132-DE9E247C8AF0"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICImagingFactory2 : IWICImagingFactory
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IWICBitmapDecoder CreateDecoderFromFilename([MarshalAs(UnmanagedType.LPWStr)] [In] string wzFilename, [In] ref Guid pguidVendor, [In] uint dwDesiredAccess, [In] WICDecodeOptions metadataOptions);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IWICBitmapDecoder CreateDecoderFromStream([MarshalAs(UnmanagedType.Interface)] [In] IStream pIStream, [In] ref Guid pguidVendor, [In] WICDecodeOptions metadataOptions);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IWICBitmapDecoder CreateDecoderFromFileHandle([ComAliasName("WIC.ULONG_PTR")] [In] ulong hFile, [In] ref Guid pguidVendor, [In] WICDecodeOptions metadataOptions);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateComponentInfo([In] ref Guid clsidComponent, [MarshalAs(UnmanagedType.Interface)] out IWICComponentInfo ppIInfo);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IWICBitmapDecoder CreateDecoder([In] ref Guid guidContainerFormat, [In] ref Guid pguidVendor);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IWICBitmapEncoder CreateEncoder([In] ref Guid guidContainerFormat, [In] ref Guid pguidVendor);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreatePalette([MarshalAs(UnmanagedType.Interface)] out IWICPalette ppIPalette);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateFormatConverter([MarshalAs(UnmanagedType.Interface)] out IWICFormatConverter ppIFormatConverter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateBitmapScaler([MarshalAs(UnmanagedType.Interface)] out IWICBitmapScaler ppIBitmapScaler);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateBitmapClipper([MarshalAs(UnmanagedType.Interface)] out IWICBitmapClipper ppIBitmapClipper);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateBitmapFlipRotator([MarshalAs(UnmanagedType.Interface)] out IWICBitmapFlipRotator ppIBitmapFlipRotator);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateStream([MarshalAs(UnmanagedType.Interface)] out IWICStream ppIWICStream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateColorContext([MarshalAs(UnmanagedType.Interface)] out IWICColorContext ppIWICColorContext);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateColorTransformer([MarshalAs(UnmanagedType.Interface)] out IWICColorTransform ppIWICColorTransform);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateBitmap([In] uint uiWidth, [In] uint uiHeight, [ComAliasName("WIC.REFWICPixelFormatGUID")] [In] ref Guid pixelFormat, [In] WICBitmapCreateCacheOption option, [MarshalAs(UnmanagedType.Interface)] out IWICBitmap ppIBitmap);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateBitmapFromSource([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pIBitmapSource, [In] WICBitmapCreateCacheOption option, [MarshalAs(UnmanagedType.Interface)] out IWICBitmap ppIBitmap);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateBitmapFromSourceRect([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pIBitmapSource, [In] uint X, [In] uint Y, [In] uint Width, [In] uint Height, [MarshalAs(UnmanagedType.Interface)] out IWICBitmap ppIBitmap);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateBitmapFromMemory([In] uint uiWidth, [In] uint uiHeight, [ComAliasName("WIC.REFWICPixelFormatGUID")] [In] ref Guid pixelFormat, [In] uint cbStride, [In] uint cbBufferSize, [In] ref byte pbBuffer, [MarshalAs(UnmanagedType.Interface)] out IWICBitmap ppIBitmap);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateBitmapFromHBITMAP([ComAliasName("WIC.wireHBITMAP")] [In] ref _userHBITMAP hBitmap, [ComAliasName("WIC.wireHPALETTE")] [In] ref _userHPALETTE hPalette, [In] WICBitmapAlphaChannelOption options, [MarshalAs(UnmanagedType.Interface)] out IWICBitmap ppIBitmap);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateBitmapFromHICON([ComAliasName("WIC.wireHICON")] [In] ref _RemotableHandle hIcon, [MarshalAs(UnmanagedType.Interface)] out IWICBitmap ppIBitmap);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateComponentEnumerator([In] uint componentTypes, [In] uint options, [MarshalAs(UnmanagedType.Interface)] out IEnumUnknown ppIEnumUnknown);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateFastMetadataEncoderFromDecoder([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapDecoder pIDecoder, [MarshalAs(UnmanagedType.Interface)] out IWICFastMetadataEncoder ppIFastEncoder);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateFastMetadataEncoderFromFrameDecode([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapFrameDecode pIFrameDecoder, [MarshalAs(UnmanagedType.Interface)] out IWICFastMetadataEncoder ppIFastEncoder);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateQueryWriter([In] ref Guid guidMetadataFormat, [In] ref Guid pguidVendor, [MarshalAs(UnmanagedType.Interface)] out IWICMetadataQueryWriter ppIQueryWriter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateQueryWriterFromReader([MarshalAs(UnmanagedType.Interface)] [In] IWICMetadataQueryReader pIQueryReader, [In] ref Guid pguidVendor, [MarshalAs(UnmanagedType.Interface)] out IWICMetadataQueryWriter ppIQueryWriter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateImageEncoder([In] IntPtr pD2DDevice, [MarshalAs(UnmanagedType.Interface)] out IWICImageEncoder ppWICImageEncoder);
	}
}
