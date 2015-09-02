using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("95C75A6E-3E8C-4EC2-85A8-AEBCC551E59B"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICDevelopRawNotificationCallback
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Notify([In] uint NotificationMask);
	}
}
