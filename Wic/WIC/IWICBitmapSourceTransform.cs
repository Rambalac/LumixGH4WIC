using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("3B16811B-6A43-4EC9-B713-3D5A0C13B940"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICBitmapSourceTransform
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void CopyPixels([In] ref WICRect prc, [In] uint uiWidth, [In] uint uiHeight, [ComAliasName("WIC.WICPixelFormatGUID")] [In] ref WICPixelFormatGUID pguidDstFormat, [In] WICBitmapTransformOptions dstTransform, [In] uint nStride, [In] uint cbBufferSize, out byte pbBuffer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetClosestSize([In] [Out] ref uint puiWidth, [In] [Out] ref uint puiHeight);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetClosestPixelFormat([ComAliasName("WIC.WICPixelFormatGUID")] [In] [Out] ref WICPixelFormatGUID pguidDstFormat);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void DoesSupportTransform([In] WICBitmapTransformOptions dstTransform, out int pfIsSupported);
	}
}
