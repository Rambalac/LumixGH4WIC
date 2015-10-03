using System.IO;
using System;
using com.azi.Decoder;
using com.azi.tiff;

namespace com.azi.Image
{
    public class ImageFile<T> : ImageFile
    {
        Stream _stream;
        IRawDecoder<T> _decoder;
        Exif exif;

        public ColorMap<T> Raw
        {
            get
            {
                return _decoder.DecodeMap(_stream, Exif);
            }
        }

        public override Exif Exif
        {
            get
            {
                if (exif == null)
                    exif = _decoder.DecodeExif(_stream);
                return exif;
            }
        }

        //public RawImageFile(RawMap raw)
        //{
        //    Raw = raw;
        //}

        public ImageFile(Stream stream, IRawDecoder<T> decoder)
        {
            if (!stream.CanSeek) throw new ArgumentException("Stream should be seekable");
            _stream = stream;
            _decoder = decoder;
        }

        public override int Width
        {
            get { return Raw.Width; }
        }

        public override int Height
        {
            get { return Raw.Height; }
        }
    }
}