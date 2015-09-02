using System;

namespace WIC
{
	public enum WICBitmapTransformOptions
	{
		WICBitmapTransformRotate0,
		WICBitmapTransformRotate90,
		WICBitmapTransformRotate180,
		WICBitmapTransformRotate270,
		WICBitmapTransformFlipHorizontal = 8,
		WICBitmapTransformFlipVertical = 16,
		WICBITMAPTRANSFORMOPTIONS_FORCE_DWORD = 2147483647
	}
}
