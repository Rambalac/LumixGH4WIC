using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[ComConversionLoss, Guid("FBEC5E44-F7BE-4B65-B7F8-C0C81FEF026D"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICDevelopRaw : IWICBitmapFrameDecode
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
		void GetMetadataQueryReader([MarshalAs(UnmanagedType.Interface)] out IWICMetadataQueryReader ppIMetadataQueryReader);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetColorContexts([In] uint cCount, [MarshalAs(UnmanagedType.Interface)] [In] [Out] ref IWICColorContext ppIColorContexts, out uint pcActualCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetThumbnail([MarshalAs(UnmanagedType.Interface)] out IWICBitmapSource ppIThumbnail);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Remote_QueryRawCapabilitiesInfo([In] [Out] ref WICRawCapabilitiesInfo pInfo);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void LoadParameterSet([In] WICRawParameterSet ParameterSet);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCurrentParameterSet([MarshalAs(UnmanagedType.Interface)] out IPropertyBag2 ppCurrentParameterSet);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetExposureCompensation([In] double ev);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetExposureCompensation(out double pEV);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetWhitePointRGB([In] uint Red, [In] uint Green, [In] uint Blue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetWhitePointRGB(out uint pRed, out uint pGreen, out uint pBlue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetNamedWhitePoint([In] WICNamedWhitePoint WhitePoint);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetNamedWhitePoint(out WICNamedWhitePoint pWhitePoint);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetWhitePointKelvin([In] uint WhitePointKelvin);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetWhitePointKelvin(out uint pWhitePointKelvin);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetKelvinRangeInfo(out uint pMinKelvinTemp, out uint pMaxKelvinTemp, out uint pKelvinTempStepValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetContrast([In] double Contrast);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetContrast(out double pContrast);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetGamma([In] double Gamma);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetGamma(out double pGamma);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetSharpness([In] double Sharpness);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetSharpness(out double pSharpness);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetSaturation([In] double Saturation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetSaturation(out double pSaturation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetTint([In] double Tint);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetTint(out double pTint);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetNoiseReduction([In] double NoiseReduction);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetNoiseReduction(out double pNoiseReduction);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetDestinationColorContext([MarshalAs(UnmanagedType.Interface)] [In] IWICColorContext pColorContext);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Remote_SetToneCurve([In] uint cPoints, [In] ref WICRawToneCurvePoint aPoints);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Remote_GetToneCurve(out uint pcPoints, [Out] IntPtr paPoints);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetRotation([In] double Rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetRotation(out double pRotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetRenderMode([In] WICRawRenderMode RenderMode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetRenderMode(out WICRawRenderMode pRenderMode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetNotificationCallback([MarshalAs(UnmanagedType.Interface)] [In] IWICDevelopRawNotificationCallback pCallback);
	}
}
