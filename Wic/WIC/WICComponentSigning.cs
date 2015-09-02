using System;

namespace WIC
{
	public enum WICComponentSigning
	{
		WICComponentSigned = 1,
		WICComponentUnsigned,
		WICComponentSafe = 4,
		WICComponentDisabled = -2147483648,
		WICCOMPONENTSIGNING_FORCE_DWORD = 2147483647
	}
}
