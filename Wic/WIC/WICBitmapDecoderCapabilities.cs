using System;

namespace WIC
{
    [Flags]
    public enum WICBitmapDecoderCapabilities
    {
        WICBitmapDecoderCapabilitySameEncoder = 1,
        WICBitmapDecoderCapabilityCanDecodeAllImages,
        WICBitmapDecoderCapabilityCanDecodeSomeImages = 4,
        WICBitmapDecoderCapabilityCanEnumerateMetadata = 8,
        WICBitmapDecoderCapabilityCanDecodeThumbnail = 16,
        WICBITMAPDECODERCAPABILITIES_FORCE_DWORD = 2147483647
    }
}
