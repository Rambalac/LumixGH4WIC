using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("3D4C0C61-18A4-41E4-BD80-481A4FC9F464"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICDdsFrameDecode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetSizeInBlocks(out uint pWidthInBlocks, out uint pHeightInBlocks);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetFormatInfo(out WICDdsFormatInfo pFormatInfo);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CopyBlocks([In] ref WICRect prcBoundsInBlocks, [In] uint cbStride, [In] uint cbBufferSize, out byte pbBuffer);
	}
}
