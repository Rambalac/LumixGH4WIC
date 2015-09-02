using System.Collections.Generic;
using System.IO;
using System.Linq;
using com.azi.tiff;

namespace com.azi.Decoder.Panasonic
{
    public enum PanasoncIfdTag
    {
        CropLeft,
        CropWidth,
        CropTop,
        CropHeight,
        Filters,
        Iso,
        Black,
        CamMul,
        Thumb,
        RawOffset,
    }

    public class PanasonicExif : Exif
    {
        public const int MaxBits = 12;

        private static readonly Dictionary<int, PanasoncIfdTag> TagMap = new Dictionary<int, PanasoncIfdTag>
        {
            {5, PanasoncIfdTag.CropLeft},
            {6, PanasoncIfdTag.CropTop},
            {7, PanasoncIfdTag.CropWidth},
            {8, PanasoncIfdTag.CropHeight},
            {9, PanasoncIfdTag.Filters},
            {23, PanasoncIfdTag.Iso},
            {28, PanasoncIfdTag.Black},
            {29, PanasoncIfdTag.Black},
            {30, PanasoncIfdTag.Black},
            {36, PanasoncIfdTag.CamMul},
            {37, PanasoncIfdTag.CamMul},
            {38, PanasoncIfdTag.CamMul},
            {46, PanasoncIfdTag.Thumb},
            {280, PanasoncIfdTag.RawOffset},
        };

        public ushort[] Black = new ushort[4];
        public float[] CamMul;
        public int CropBottom;

        public int CropLeft;
        public int CropRight;
        public int CropTop;
        public int Filters;
        public int Iso;
        public int RawOffset;

        public new static PanasonicExif Parse(Stream stream)
        {
            stream.Position = 0;
            var result = new PanasonicExif();
            result.InternalParse(stream);

            result.ColorMatrix = new[,]
            {
                {1.87f, -0.81f, -0.06f},
                {-0.16f, 1.55f, -0.39f},
                {0.05f, -0.47f, 1.42f}
            };

            if (result.CamMul == null) return result;

            var max = result.CamMul.Max();
            result.WhiteColor = result.CamMul.Select(v => max/v).Reverse().ToArray();
            result.Multiplier = max;

            return result;
        }

        protected override void ParseIfdBlock(IfdBlock block, BinaryReader reader)
        {
            PanasoncIfdTag tag;
            if (!TagMap.TryGetValue(block.rawtag, out tag))
            {
                base.ParseIfdBlock(block, reader);
                return;
            }

            switch (tag)
            {
                case PanasoncIfdTag.CropLeft:
                    CropLeft = (int) block.GetUInt32();
                    CropRight += CropLeft;
                    break;
                case PanasoncIfdTag.CropTop:
                    CropTop = (int) block.GetUInt32();
                    CropBottom += CropTop;
                    break;
                case PanasoncIfdTag.CropWidth:
                    CropRight += (int) block.GetUInt32();
                    break;
                case PanasoncIfdTag.CropHeight:
                    CropBottom += (int) block.GetUInt32();
                    break;
                case PanasoncIfdTag.Filters:
                    Filters = (int) block.GetUInt32();
                    break;
                case PanasoncIfdTag.Iso:
                    Iso = (int) block.GetUInt32();
                    break;
                case PanasoncIfdTag.Black:
                    Black[block.rawtag - 28] = block.GetUInt16();
                    Black[3] = Black[1];
                    break;
                case PanasoncIfdTag.CamMul:
                    if (CamMul == null) CamMul = new float[3];
                    CamMul[block.rawtag - 36] = block.GetUInt16();
                    break;
                case PanasoncIfdTag.Thumb:
                    Thumbnail = block.rawdata;
                    break;
                case PanasoncIfdTag.RawOffset:
                    RawOffset = (int) block.GetUInt32();
                    break;
            }
        }
    }
}