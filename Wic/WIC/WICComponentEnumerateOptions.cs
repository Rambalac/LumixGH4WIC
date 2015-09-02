using System;

namespace WIC
{
	public enum WICComponentEnumerateOptions
	{
		WICComponentEnumerateDefault,
		WICComponentEnumerateRefresh,
		WICComponentEnumerateDisabled = -2147483648,
		WICComponentEnumerateUnsigned = 1073741824,
		WICComponentEnumerateBuiltInOnly = 536870912,
		WICCOMPONENTENUMERATEOPTIONS_FORCE_DWORD = 2147483647
	}
}
