using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("B84E2C09-78C9-4AC4-8BD3-524AE1663A2F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICFastMetadataEncoder
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Commit();

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetMetadataQueryWriter([MarshalAs(UnmanagedType.Interface)] out IWICMetadataQueryWriter ppIMetadataQueryWriter);
	}
}
