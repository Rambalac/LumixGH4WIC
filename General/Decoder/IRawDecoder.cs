using System.IO;
using com.azi.Image;
using com.azi.tiff;

namespace com.azi.Decoder
{
    public interface IRawDecoder<T>
    {
        bool IsSupported(Stream stream);
        Exif DecodeExif(Stream stream);
        ColorMap<T> DecodeMap(Stream stream, Exif exif);
    }
}