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
        void CopyPixels(
            [In] ref WICRect prc, //0
            [In] uint uiWidth, //1
            [In] uint uiHeight, //2
            Guid pguidDstFormat, //3
            [In] WICBitmapTransformOptions dstTransform,  //4
            [In] uint nStride,  //5
            [In] uint cbBufferSize, //6
            [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 6)]byte[] pbBuffer);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetClosestSize([In] [Out] ref uint puiWidth, [In] [Out] ref uint puiHeight);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetClosestPixelFormat(ref Guid pguidDstFormat);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void DoesSupportTransform([In] WICBitmapTransformOptions dstTransform, out int pfIsSupported);
    }
}
