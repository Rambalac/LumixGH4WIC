using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WIC
{
	[Guid("135FF860-22B7-4DDF-B0F6-218F4F299A43"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICStream : IStream
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteRead(out byte pv, [In] uint cb, out uint pcbRead);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteWrite([In] ref byte pv, [In] uint cb, out uint pcbWritten);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteSeek([In] _LARGE_INTEGER dlibMove, [In] uint dwOrigin, out _ULARGE_INTEGER plibNewPosition);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetSize([In] _ULARGE_INTEGER libNewSize);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteCopyTo([MarshalAs(UnmanagedType.Interface)] [In] IStream pstm, [In] _ULARGE_INTEGER cb, out _ULARGE_INTEGER pcbRead, out _ULARGE_INTEGER pcbWritten);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Commit([In] uint grfCommitFlags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Revert();

		[MethodImpl(MethodImplOptions.InternalCall)]
		void LockRegion([In] _ULARGE_INTEGER libOffset, [In] _ULARGE_INTEGER cb, [In] uint dwLockType);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void UnlockRegion([In] _ULARGE_INTEGER libOffset, [In] _ULARGE_INTEGER cb, [In] uint dwLockType);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Stat(out tagSTATSTG pstatstg, [In] uint grfStatFlag);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Clone([MarshalAs(UnmanagedType.Interface)] out IStream ppstm);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void InitializeFromIStream([MarshalAs(UnmanagedType.Interface)] [In] IStream pIStream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void InitializeFromFilename([MarshalAs(UnmanagedType.LPWStr)] [In] string wzFilename, [In] uint dwDesiredAccess);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void InitializeFromMemory([ComAliasName("WIC.int")] [In] int pbBuffer, [In] uint cbBufferSize);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void InitializeFromIStreamRegion([MarshalAs(UnmanagedType.Interface)] [In] IStream pIStream, [In] _ULARGE_INTEGER ulOffset, [In] _ULARGE_INTEGER ulMaxSize);
	}
}
