using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WIC
{
	[Guid("0000000B-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IStorage
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateStream([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName, [In] uint grfMode, [In] uint reserved1, [In] uint reserved2, [MarshalAs(UnmanagedType.Interface)] out IStream ppstm);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteOpenStream([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName, [In] uint cbReserved1, [In] ref byte reserved1, [In] uint grfMode, [In] uint reserved2, [MarshalAs(UnmanagedType.Interface)] out IStream ppstm);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateStorage([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName, [In] uint grfMode, [In] uint reserved1, [In] uint reserved2, [MarshalAs(UnmanagedType.Interface)] out IStorage ppstg);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void OpenStorage([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName, [MarshalAs(UnmanagedType.Interface)] [In] IStorage pstgPriority, [In] uint grfMode, [ComAliasName("WIC.wireSNB")] [In] ref tagRemSNB snbExclude, [In] uint reserved, [MarshalAs(UnmanagedType.Interface)] out IStorage ppstg);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteCopyTo([In] uint ciidExclude, [In] ref Guid rgiidExclude, [ComAliasName("WIC.wireSNB")] [In] ref tagRemSNB snbExclude, [MarshalAs(UnmanagedType.Interface)] [In] IStorage pstgDest);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveElementTo([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName, [MarshalAs(UnmanagedType.Interface)] [In] IStorage pstgDest, [MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsNewName, [In] uint grfFlags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Commit([In] uint grfCommitFlags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Revert();

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoteEnumElements([In] uint reserved1, [In] uint cbReserved2, [In] ref byte reserved2, [In] uint reserved3, [MarshalAs(UnmanagedType.Interface)] out IEnumSTATSTG ppenum);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void DestroyElement([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RenameElement([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsOldName, [MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsNewName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetElementTimes([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName, [In] ref _FILETIME pctime, [In] ref _FILETIME patime, [In] ref _FILETIME pmtime);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetClass([In] ref Guid clsid);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetStateBits([In] uint grfStateBits, [In] uint grfMask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Stat(out tagSTATSTG pstatstg, [In] uint grfStatFlag);
	}
}
