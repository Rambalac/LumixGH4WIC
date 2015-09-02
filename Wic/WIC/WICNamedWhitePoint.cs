using System;

namespace WIC
{
	public enum WICNamedWhitePoint
	{
		WICWhitePointDefault = 1,
		WICWhitePointDaylight,
		WICWhitePointCloudy = 4,
		WICWhitePointShade = 8,
		WICWhitePointTungsten = 16,
		WICWhitePointFluorescent = 32,
		WICWhitePointFlash = 64,
		WICWhitePointUnderwater = 128,
		WICWhitePointCustom = 256,
		WICWhitePointAutoWhiteBalance = 512,
		WICWhitePointAsShot = 1,
		WICNAMEDWHITEPOINT_FORCE_DWORD = 2147483647
	}
}
