using System;

namespace WIC
{
	public enum WICProgressNotification
	{
		WICProgressNotificationBegin = 65536,
		WICProgressNotificationEnd = 131072,
		WICProgressNotificationFrequent = 262144,
		WICProgressNotificationAll = -65536,
		WICPROGRESSNOTIFICATION_FORCE_DWORD = 2147483647
	}
}
