using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("00000123-A8F2-4877-BA0A-FD2B6645FB94"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICBitmapLock
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetSize(out uint puiWidth, out uint puiHeight);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetStride(out uint pcbStride);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetDataPointer(out uint pcbBufferSize, [ComAliasName("WIC.int")] out int ppbData);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetPixelFormat([ComAliasName("WIC.WICPixelFormatGUID")] out WICPixelFormatGUID pPixelFormat);
	}
}
