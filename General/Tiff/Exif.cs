using System;
using System.Collections.Generic;
using System.IO;

namespace com.azi.tiff
{
    public class Exif
    {
        private readonly List<IfdBlock> _ifdBlocks = new List<IfdBlock>();

        public List<IfdBlock> RawIfd => _ifdBlocks;

        public Fraction Aperture;
        public float[,] ColorMatrix;
        public string DateTimeDigitized;
        public string DateTimeOriginal;
        public string ExifVersion;
        public Fraction ExposureBiasValue;
        public int ExposureProgram;
        public int FileSource;
        public int Flash;
        public Fraction FocalLength;
        public int ImageHeight;
        public int ImageWidth;
        public string Maker;
        public Fraction MaxApertureValue;
        public int MeteringMode;
        public string Model;
        public float? Multiplier;
        public int Orientation;
        public Fraction Shutter;
        public int StripByteCounts;
        public int StripOffset;
        public string SubsecTimeDigitized;
        public string SubsecTimeOriginal;
        public byte[] Thumbnail;
        public float[] WhiteColor;
        public int RealHeight;
        public int RealWidth;


        protected void InternalParse(Stream stream)
        {
            var reader = new BinaryReader(stream);
            var order = reader.ReadUInt16();
            if (order != 0x4949 && order != 0x4d4d) throw new ArgumentException("Wrong file");
            reader.ReadUInt16();

            while (reader.PeekChar() != -1)
            {
                var offset = reader.ReadUInt32();
                if (offset == 0) return;
                reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                ParseIfd(reader);
            }
        }

        public static Exif Parse(Stream stream)
        {
            var result = new Exif();
            result.InternalParse(stream);
            return result;
        }

        protected virtual void ParseIfdBlock(IfdBlock block, BinaryReader reader)
        {
            switch (block.tag)
            {
                case IfdTag.ImageWidth:
                    ImageWidth = block.GetUInt16();
                    break;
                case IfdTag.ImageLength:
                    ImageHeight = block.GetUInt16();
                    break;
                case IfdTag.Make:
                    Maker = block.GetString();
                    break;
                case IfdTag.Model:
                    Model = block.GetString();
                    break;
                case IfdTag.StripOffsets:
                    StripOffset = (int) block.GetUInt32();
                    break;
                case IfdTag.Orientation:
                    Orientation = (int) block.GetUInt32();
                    break;
                case IfdTag.RowsPerStrip:
                    StripByteCounts = (int) block.GetUInt32();
                    break;
                case IfdTag.ExifIFD:
                    reader.BaseStream.Seek(block.GetUInt32(), SeekOrigin.Begin);
                    ParseIfd(reader);
                    break;
                case IfdTag.ExposureTime:
                    Shutter = block.GetFraction();
                    break;
                case IfdTag.FNumber:
                    Aperture = block.GetFraction();
                    break;
                case IfdTag.ExposureProgram:
                    ExposureProgram = (int) block.GetUInt32();
                    break;
                case IfdTag.ExifVersion:
                    ExifVersion = block.GetString();
                    break;
                case IfdTag.DateTimeOriginal:
                    DateTimeOriginal = block.GetString();
                    break;
                case IfdTag.DateTimeDigitized:
                    DateTimeDigitized = block.GetString();
                    break;
                case IfdTag.ExposureBiasValue:
                    ExposureBiasValue = block.GetFraction();
                    break;
                case IfdTag.MaxApertureValue:
                    MaxApertureValue = block.GetFraction();
                    break;
                case IfdTag.MeteringMode:
                    MeteringMode = (int) block.GetUInt32();
                    break;
                case IfdTag.Flash:
                    Flash = (int) block.GetUInt32();
                    break;
                case IfdTag.FocalLength:
                    FocalLength = block.GetFraction();
                    break;
                case IfdTag.SubsecTimeOriginal:
                    SubsecTimeOriginal = block.GetString();
                    break;
                case IfdTag.SubsecTimeDigitized:
                    SubsecTimeDigitized = block.GetString();
                    break;
                case IfdTag.FileSource:
                    FileSource = (int) block.GetUInt32();
                    break;
            }
        }

        private void ParseIfd(BinaryReader reader)
        {
            var blocksnumber = reader.ReadUInt16();
            if (blocksnumber > 512) throw new ArgumentException("Too many items in ifd");
            while (blocksnumber-- > 0)
            {
                var block = IfdBlock.parse(reader);
                _ifdBlocks.Add(block);
                ParseIfdBlock(block, reader);
                block.moveNext(reader);
            }
        }
    }
}