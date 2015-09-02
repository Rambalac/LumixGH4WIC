using System;

namespace WIC
{
	public enum WICPersistOptions
	{
		WICPersistOptionDefault,
		WICPersistOptionLittleEndian = 0,
		WICPersistOptionBigEndian,
		WICPersistOptionStrictFormat,
		WICPersistOptionNoCacheStream = 4,
		WICPersistOptionPreferUTF8 = 8,
		WICPersistOptionMask = 65535
	}
}
