using System;

namespace WIC
{
	public enum WICTiffCompressionOption
	{
		WICTiffCompressionDontCare,
		WICTiffCompressionNone,
		WICTiffCompressionCCITT3,
		WICTiffCompressionCCITT4,
		WICTiffCompressionLZW,
		WICTiffCompressionRLE,
		WICTiffCompressionZIP,
		WICTiffCompressionLZWHDifferencing,
		WICTIFFCOMPRESSIONOPTION_FORCE_DWORD = 2147483647
	}
}
