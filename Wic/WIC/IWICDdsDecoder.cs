using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("409CD537-8532-40CB-9774-E2FEB2DF4E9C"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICDdsDecoder
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetParameters(out WICDdsParameters pParameters);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IWICBitmapFrameDecode GetFrame([In] uint arrayIndex, [In] uint mipLevel, [In] uint sliceIndex);
	}
}
