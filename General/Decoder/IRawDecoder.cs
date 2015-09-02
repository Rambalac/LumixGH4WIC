using System.IO;
using com.azi.Image;
using com.azi.tiff;

namespace com.azi.Decoder
{
    public interface IRawDecoder
    {
        bool IsSupported(Stream stream);
        Exif DecodeExif(Stream stream);
        RawMap DecodeMap(Stream stream, Exif exif);
    }
}