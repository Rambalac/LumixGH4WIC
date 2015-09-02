using System;

namespace WIC
{
	public enum WICProgressOperation
	{
		WICProgressOperationCopyPixels = 1,
		WICProgressOperationWritePixels,
		WICProgressOperationAll = 65535,
		WICPROGRESSOPERATION_FORCE_DWORD = 2147483647
	}
}
