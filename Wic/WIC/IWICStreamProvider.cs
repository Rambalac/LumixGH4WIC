using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WIC
{
	[Guid("449494BC-B468-4927-96D7-BA90D31AB505"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICStreamProvider
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetStream([MarshalAs(UnmanagedType.Interface)] out IStream ppIStream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetPersistOptions(out uint pdwPersistOptions);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetPreferredVendorGUID(out Guid pguidPreferredVendor);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RefreshStream();
	}
}
