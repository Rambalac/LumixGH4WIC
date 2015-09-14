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
                    block.variant = ImageWidth;
                    break;
                case IfdTag.ImageLength:
                    ImageHeight = block.GetUInt16();
                    block.variant = ImageHeight;
                    break;
                case IfdTag.Make:
                    Maker = block.GetString();
                    block.variant = Maker;
                    break;
                case IfdTag.Model:
                    Model = block.GetString();
                    block.variant = Model;
                    break;
                case IfdTag.StripOffsets:
                    StripOffset = (int)block.GetUInt32();
                    block.variant = StripOffset;
                    break;
                case IfdTag.Orientation:
                    Orientation = (int)block.GetUInt32();
                    block.variant = Orientation;
                    break;
                case IfdTag.RowsPerStrip:
                    StripByteCounts = (int)block.GetUInt32();
                    block.variant = StripByteCounts;
                    break;
                case IfdTag.ExifIFD:
                    reader.BaseStream.Seek(block.GetUInt32(), SeekOrigin.Begin);
                    ParseIfd(reader);
                    break;
                case IfdTag.ExposureTime:
                    Shutter = block.GetFraction();
                    block.variant = Shutter.ToFloat();
                    break;
                case IfdTag.FNumber:
                    Aperture = block.GetFraction();
                    block.variant = Aperture.ToFloat();
                    break;
                case IfdTag.ExposureProgram:
                    ExposureProgram = (int)block.GetUInt32();
                    block.variant = ExposureProgram;
                    break;
                case IfdTag.ExifVersion:
                    ExifVersion = block.GetString();
                    block.variant = ExifVersion;
                    break;
                case IfdTag.DateTimeOriginal:
                    DateTimeOriginal = block.GetString();
                    block.variant = DateTimeOriginal;
                    break;
                case IfdTag.DateTimeDigitized:
                    DateTimeDigitized = block.GetString();
                    block.variant = DateTimeDigitized;
                    break;
                case IfdTag.ExposureBiasValue:
                    ExposureBiasValue = block.GetFraction();
                    block.variant = ExposureBiasValue.ToFloat();
                    break;
                case IfdTag.MaxApertureValue:
                    MaxApertureValue = block.GetFraction();
                    block.variant = MaxApertureValue.ToFloat();
                    break;
                case IfdTag.MeteringMode:
                    MeteringMode = (int)block.GetUInt32();
                    block.variant = MeteringMode;
                    break;
                case IfdTag.Flash:
                    Flash = (int)block.GetUInt32();
                    block.variant = Flash;
                    break;
                case IfdTag.FocalLength:
                    FocalLength = block.GetFraction();
                    block.variant = FocalLength.ToFloat();
                    break;
                case IfdTag.SubsecTimeOriginal:
                    SubsecTimeOriginal = block.GetString();
                    block.variant = SubsecTimeOriginal;
                    break;
                case IfdTag.SubsecTimeDigitized:
                    SubsecTimeDigitized = block.GetString();
                    block.variant = SubsecTimeDigitized;
                    break;
                case IfdTag.FileSource:
                    FileSource = (int)block.GetUInt32();
                    block.variant = FileSource;
                    break;
                default:
                    if (block.length <= 4)
                        block.variant = block.GetUInt32();
                    else if (block.type == IfdType.UInt32Fraction)
                        block.variant = block.GetFraction().ToString();
                    else block.variant = string.Join(", ", block.rawdata);
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