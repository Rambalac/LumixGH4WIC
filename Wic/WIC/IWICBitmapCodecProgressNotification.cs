using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("64C1024E-C3CF-4462-8078-88C2B11C46D9"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICBitmapCodecProgressNotification
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Remote_RegisterProgressNotification([MarshalAs(UnmanagedType.Interface)] [In] IWICProgressCallback pICallback, [In] uint dwProgressFlags);
	}
}
