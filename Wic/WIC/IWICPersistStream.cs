using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WIC
{
	[Guid("00675040-6908-45F8-86A3-49C7DFD6D9AD"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICPersistStream : IPersistStream
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetClassID(out Guid pClassID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsDirty();

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Load([MarshalAs(UnmanagedType.Interface)] [In] IStream pstm);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Save([MarshalAs(UnmanagedType.Interface)] [In] IStream pstm, [In] int fClearDirty);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetSizeMax(out _ULARGE_INTEGER pcbSize);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void LoadEx([MarshalAs(UnmanagedType.Interface)] [In] IStream pIStream, [In] ref Guid pguidPreferredVendor, [In] uint dwPersistOptions);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SaveEx([MarshalAs(UnmanagedType.Interface)] [In] IStream pIStream, [In] uint dwPersistOptions, [In] int fClearDirty);
	}
}
