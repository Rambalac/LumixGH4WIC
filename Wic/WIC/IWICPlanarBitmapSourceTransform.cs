using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("3AFF9CCE-BE95-4303-B927-E7D16FF4A613"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICPlanarBitmapSourceTransform
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void DoesSupportTransform([In] [Out] ref uint puiWidth, [In] [Out] ref uint puiHeight, WICBitmapTransformOptions dstTransform, WICPlanarOptions dstPlanarOptions, [ComAliasName("WIC.WICPixelFormatGUID")] [In] ref WICPixelFormatGUID pguidDstFormats, out WICBitmapPlaneDescription pPlaneDescriptions, uint cPlanes, out int pfIsSupported);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CopyPixels([In] ref WICRect prcSource, uint uiWidth, uint uiHeight, WICBitmapTransformOptions dstTransform, WICPlanarOptions dstPlanarOptions, [In] ref WICBitmapPlane pDstPlanes, uint cPlanes);
	}
}
