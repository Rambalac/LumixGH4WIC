using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("5CACDB4C-407E-41B3-B936-D0F010CD6732"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICDdsEncoder
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetParameters([In] ref WICDdsParameters pParameters);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetParameters(out WICDdsParameters pParameters);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateNewFrame([MarshalAs(UnmanagedType.Interface)] out IWICBitmapFrameEncode ppIFrameEncode, out uint pArrayIndex, out uint pMipLevel, out uint pSliceIndex);
	}
}
