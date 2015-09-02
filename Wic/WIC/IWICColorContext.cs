using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("3C613A02-34B2-44EA-9A7C-45AEA9C6FD6D"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICColorContext
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void InitializeFromFilename([MarshalAs(UnmanagedType.LPWStr)] [In] string wzFilename);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void InitializeFromMemory([In] ref byte pbBuffer, [In] uint cbBufferSize);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void InitializeFromExifColorSpace([In] uint value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetType(out WICColorContextType pType);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetProfileBytes([In] uint cbBuffer, [In] [Out] ref byte pbBuffer, out uint pcbActual);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetExifColorSpace(out uint pValue);
	}
}
