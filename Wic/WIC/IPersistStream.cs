using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WIC
{
	[Guid("00000109-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IPersistStream : IPersist
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
	}
}
