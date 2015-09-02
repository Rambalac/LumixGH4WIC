using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace com.azi.tiff
{
    public class IfdBlock
    {
        private static readonly Dictionary<int, IfdTag> TagMap = new Dictionary<int, IfdTag>
        {
            {254, IfdTag.NewSubfileType},
            {255, IfdTag.SubfileType},
            {2, IfdTag.ImageWidth},
            {256, IfdTag.ImageWidth},
            {257, IfdTag.ImageLength},
            {3, IfdTag.ImageLength},
            {258, IfdTag.BitsPerSample},
            {259, IfdTag.Compression},
            {262, IfdTag.PhotometricInterpretation},
            {263, IfdTag.Threshholding},
            {264, IfdTag.CellWidth},
            {265, IfdTag.CellLength},
            {266, IfdTag.FillOrder},
            {269, IfdTag.DocumentName},
            {270, IfdTag.ImageDescription},
            {271, IfdTag.Make},
            {272, IfdTag.Model},
            {273, IfdTag.StripOffsets},
            {274, IfdTag.Orientation},
            {277, IfdTag.SamplesPerPixel},
            {278, IfdTag.RowsPerStrip},
            {279, IfdTag.StripByteCounts},
            {280, IfdTag.MinSampleValue},
            {281, IfdTag.MaxSampleValue},
            {282, IfdTag.XResolution},
            {283, IfdTag.YResolution},
            {284, IfdTag.PlanarConfiguration},
            {285, IfdTag.PageName},
            {286, IfdTag.XPosition},
            {287, IfdTag.YPosition},
            {288, IfdTag.FreeOffsets},
            {289, IfdTag.FreeByteCounts},
            {290, IfdTag.GrayResponseUnit},
            {291, IfdTag.GrayResponseCurve},
            {292, IfdTag.T4Options},
            {293, IfdTag.T6Options},
            {296, IfdTag.ResolutionUnit},
            {297, IfdTag.PageNumber},
            {301, IfdTag.TransferFunction},
            {305, IfdTag.Software},
            {306, IfdTag.DateTime},
            {315, IfdTag.Artist},
            {316, IfdTag.HostComputer},
            {317, IfdTag.Predictor},
            {318, IfdTag.WhitePoint},
            {319, IfdTag.PrimaryChromaticities},
            {320, IfdTag.ColorMap},
            {321, IfdTag.HalftoneHints},
            {322, IfdTag.TileWidth},
            {323, IfdTag.TileLength},
            {324, IfdTag.TileOffsets},
            {325, IfdTag.TileByteCounts},
            {326, IfdTag.BadFaxLines},
            {327, IfdTag.CleanFaxData},
            {328, IfdTag.ConsecutiveBadFaxLines},
            {330, IfdTag.SubIFDs},
            {332, IfdTag.InkSet},
            {333, IfdTag.InkNames},
            {334, IfdTag.NumberOfInks},
            {336, IfdTag.DotRange},
            {337, IfdTag.TargetPrinter},
            {338, IfdTag.ExtraSamples},
            {339, IfdTag.SampleFormat},
            {340, IfdTag.SMinSampleValue},
            {341, IfdTag.SMaxSampleValue},
            {342, IfdTag.TransferRange},
            {343, IfdTag.ClipPath},
            {344, IfdTag.XClipPathUnits},
            {345, IfdTag.YClipPathUnits},
            {346, IfdTag.Indexed},
            {347, IfdTag.JPEGTables},
            {351, IfdTag.OPIProxy},
            {400, IfdTag.GlobalParametersIFD},
            {401, IfdTag.ProfileType},
            {402, IfdTag.FaxProfile},
            {403, IfdTag.CodingMethods},
            {404, IfdTag.VersionYear},
            {405, IfdTag.ModeNumber},
            {433, IfdTag.Decode},
            {434, IfdTag.DefaultImageColor},
            {512, IfdTag.JPEGProc},
            {513, IfdTag.JPEGInterchangeFormat},
            {514, IfdTag.JPEGInterchangeFormatLength},
            {515, IfdTag.JPEGRestartInterval},
            {517, IfdTag.JPEGLosslessPredictors},
            {518, IfdTag.JPEGPointTransforms},
            {519, IfdTag.JPEGQTables},
            {520, IfdTag.JPEGDCTables},
            {521, IfdTag.JPEGACTables},
            {529, IfdTag.YCbCrCoefficients},
            {530, IfdTag.YCbCrSubSampling},
            {531, IfdTag.YCbCrPositioning},
            {532, IfdTag.ReferenceBlackWhite},
            {559, IfdTag.StripRowCounts},
            {700, IfdTag.XMP},
            {18246, IfdTag.ImageRating},
            {18249, IfdTag.ImageRatingPercent},
            {32781, IfdTag.ImageID},
            {32932, IfdTag.WangAnnotation},
            {33421, IfdTag.CFARepeatPatternDim},
            {33422, IfdTag.CFAPattern},
            {33423, IfdTag.BatteryLevel},
            {33432, IfdTag.Copyright},
            {33434, IfdTag.ExposureTime},
            {33437, IfdTag.FNumber},
            {33445, IfdTag.MDFileTag},
            {33446, IfdTag.MDScalePixel},
            {33447, IfdTag.MDColorTable},
            {33448, IfdTag.MDLabName},
            {33449, IfdTag.MDSampleInfo},
            {33450, IfdTag.MDPrepDate},
            {33451, IfdTag.MDPrepTime},
            {33452, IfdTag.MDFileUnits},
            {33550, IfdTag.ModelPixelScaleTag},
            {33723, IfdTag.IPTCNAA},
            {33918, IfdTag.INGRPacketDataTag},
            {33919, IfdTag.INGRFlagRegisters},
            {33920, IfdTag.IrasBTransformationMatrix},
            {33922, IfdTag.ModelTiepointTag},
            {34016, IfdTag.Site},
            {34017, IfdTag.ColorSequence},
            {34018, IfdTag.IT8Header},
            {34019, IfdTag.RasterPadding},
            {34020, IfdTag.BitsPerRunLength},
            {34021, IfdTag.BitsPerExtendedRunLength},
            {34022, IfdTag.ColorTable},
            {34023, IfdTag.ImageColorIndicator},
            {34024, IfdTag.BackgroundColorIndicator},
            {34025, IfdTag.ImageColorValue},
            {34026, IfdTag.BackgroundColorValue},
            {34027, IfdTag.PixelIntensityRange},
            {34028, IfdTag.TransparencyIndicator},
            {34029, IfdTag.ColorCharacterization},
            {34030, IfdTag.HCUsage},
            {34031, IfdTag.TrapIndicator},
            {34032, IfdTag.CMYKEquivalent},
            {34033, IfdTag.Reserved},
            {34034, IfdTag.Reserved},
            {34035, IfdTag.Reserved},
            {34264, IfdTag.ModelTransformationTag},
            {34377, IfdTag.Photoshop},
            {34665, IfdTag.ExifIFD},
            {34675, IfdTag.InterColorProfile},
            {34732, IfdTag.ImageLayer},
            {34735, IfdTag.GeoKeyDirectoryTag},
            {34736, IfdTag.GeoDoubleParamsTag},
            {34737, IfdTag.GeoAsciiParamsTag},
            {34850, IfdTag.ExposureProgram},
            {34852, IfdTag.SpectralSensitivity},
            {34853, IfdTag.GPSInfo},
            {34855, IfdTag.ISOSpeedRatings},
            {34856, IfdTag.OECF},
            {34857, IfdTag.Interlace},
            {34858, IfdTag.TimeZoneOffset},
            {34859, IfdTag.SelfTimeMode},
            {34864, IfdTag.SensitivityType},
            {34865, IfdTag.StandardOutputSensitivity},
            {34866, IfdTag.RecommendedExposureIndex},
            {34867, IfdTag.ISOSpeed},
            {34868, IfdTag.ISOSpeedLatitudeyyy},
            {34869, IfdTag.ISOSpeedLatitudezzz},
            {34908, IfdTag.HylaFAXFaxRecvParams},
            {34909, IfdTag.HylaFAXFaxSubAddress},
            {34910, IfdTag.HylaFAXFaxRecvTime},
            {36864, IfdTag.ExifVersion},
            {36867, IfdTag.DateTimeOriginal},
            {36868, IfdTag.DateTimeDigitized},
            {37121, IfdTag.ComponentsConfiguration},
            {37122, IfdTag.CompressedBitsPerPixel},
            {37377, IfdTag.ShutterSpeedValue},
            {37378, IfdTag.ApertureValue},
            {37379, IfdTag.BrightnessValue},
            {37380, IfdTag.ExposureBiasValue},
            {37381, IfdTag.MaxApertureValue},
            {37382, IfdTag.SubjectDistance},
            {37383, IfdTag.MeteringMode},
            {37384, IfdTag.LightSource},
            {37385, IfdTag.Flash},
            {37386, IfdTag.FocalLength},
            {37387, IfdTag.FlashEnergy},
            {37388, IfdTag.SpatialFrequencyResponse},
            {37389, IfdTag.Noise},
            {37390, IfdTag.FocalPlaneXResolution},
            {37391, IfdTag.FocalPlaneYResolution},
            {37392, IfdTag.FocalPlaneResolutionUnit},
            {37393, IfdTag.ImageNumber},
            {37394, IfdTag.SecurityClassification},
            {37395, IfdTag.ImageHistory},
            {37396, IfdTag.SubjectLocation},
            {37397, IfdTag.ExposureIndex},
            {37398, IfdTag.TIFFEPStandardID},
            {37399, IfdTag.SensingMethod},
            {37500, IfdTag.MakerNote},
            {37510, IfdTag.UserComment},
            {37520, IfdTag.SubsecTime},
            {37521, IfdTag.SubsecTimeOriginal},
            {37522, IfdTag.SubsecTimeDigitized},
            {37724, IfdTag.ImageSourceData},
            {40091, IfdTag.XPTitle},
            {40092, IfdTag.XPComment},
            {40093, IfdTag.XPAuthor},
            {40094, IfdTag.XPKeywords},
            {40095, IfdTag.XPSubject},
            {40960, IfdTag.FlashpixVersion},
            {40961, IfdTag.ColorSpace},
            {40962, IfdTag.PixelXDimension},
            {40963, IfdTag.PixelYDimension},
            {40964, IfdTag.RelatedSoundFile},
            {40965, IfdTag.InteroperabilityIFD},
            {41483, IfdTag.FlashEnergy},
            {41484, IfdTag.SpatialFrequencyResponse},
            {41486, IfdTag.FocalPlaneXResolution},
            {41487, IfdTag.FocalPlaneYResolution},
            {41488, IfdTag.FocalPlaneResolutionUnit},
            {41492, IfdTag.SubjectLocation},
            {41493, IfdTag.ExposureIndex},
            {41495, IfdTag.SensingMethod},
            {41728, IfdTag.FileSource},
            {41729, IfdTag.SceneType},
            {41730, IfdTag.CFAPattern},
            {41985, IfdTag.CustomRendered},
            {41986, IfdTag.ExposureMode},
            {41987, IfdTag.WhiteBalance},
            {41988, IfdTag.DigitalZoomRatio},
            {41989, IfdTag.FocalLengthIn35mmFilm},
            {41990, IfdTag.SceneCaptureType},
            {41991, IfdTag.GainControl},
            {41992, IfdTag.Contrast},
            {41993, IfdTag.Saturation},
            {41994, IfdTag.Sharpness},
            {41995, IfdTag.DeviceSettingDescription},
            {41996, IfdTag.SubjectDistanceRange},
            {42016, IfdTag.ImageUniqueID},
            {42032, IfdTag.CameraOwnerName},
            {42033, IfdTag.BodySerialNumber},
            {42034, IfdTag.LensSpecification},
            {42035, IfdTag.LensMake},
            {42036, IfdTag.LensModel},
            {42037, IfdTag.LensSerialNumber},
            {42112, IfdTag.GDAL_METADATA},
            {42113, IfdTag.GDAL_NODATA},
            {48129, IfdTag.PixelFormat},
            {48130, IfdTag.Transformation},
            {48131, IfdTag.Uncompressed},
            {48256, IfdTag.ImageWidth},
            {48257, IfdTag.ImageHeight},
            {48258, IfdTag.WidthResolution},
            {48259, IfdTag.HeightResolution},
            {48320, IfdTag.ImageOffset},
            {48321, IfdTag.ImageByteCount},
            {48322, IfdTag.AlphaOffset},
            {48323, IfdTag.AlphaByteCount},
            {48324, IfdTag.ImageDataDiscard},
            {48325, IfdTag.AlphaDataDiscard},
            {48132, IfdTag.ImageType},
            {50215, IfdTag.OceScanjobDescription},
            {50216, IfdTag.OceApplicationSelector},
            {50217, IfdTag.OceIdentificationNumber},
            {50218, IfdTag.OceImageLogicCharacteristics},
            {50341, IfdTag.PrintImageMatching},
            {50706, IfdTag.DNGVersion},
            {50707, IfdTag.DNGBackwardVersion},
            {50708, IfdTag.UniqueCameraModel},
            {50709, IfdTag.LocalizedCameraModel},
            {50710, IfdTag.CFAPlaneColor},
            {50711, IfdTag.CFALayout},
            {50712, IfdTag.LinearizationTable},
            {50713, IfdTag.BlackLevelRepeatDim},
            {50714, IfdTag.BlackLevel},
            {50715, IfdTag.BlackLevelDeltaH},
            {50716, IfdTag.BlackLevelDeltaV},
            {50717, IfdTag.WhiteLevel},
            {50718, IfdTag.DefaultScale},
            {50719, IfdTag.DefaultCropOrigin},
            {50720, IfdTag.DefaultCropSize},
            {50721, IfdTag.ColorMatrix1},
            {50722, IfdTag.ColorMatrix2},
            {50723, IfdTag.CameraCalibration1},
            {50724, IfdTag.CameraCalibration2},
            {50725, IfdTag.ReductionMatrix1},
            {50726, IfdTag.ReductionMatrix2},
            {50727, IfdTag.AnalogBalance},
            {50728, IfdTag.AsShotNeutral},
            {50729, IfdTag.AsShotWhiteXY},
            {50730, IfdTag.BaselineExposure},
            {50731, IfdTag.BaselineNoise},
            {50732, IfdTag.BaselineSharpness},
            {50733, IfdTag.BayerGreenSplit},
            {50734, IfdTag.LinearResponseLimit},
            {50735, IfdTag.CameraSerialNumber},
            {50736, IfdTag.LensInfo},
            {50737, IfdTag.ChromaBlurRadius},
            {50738, IfdTag.AntiAliasStrength},
            {50739, IfdTag.ShadowScale},
            {50740, IfdTag.DNGPrivateData},
            {50741, IfdTag.MakerNoteSafety},
            {50778, IfdTag.CalibrationIlluminant1},
            {50779, IfdTag.CalibrationIlluminant2},
            {50780, IfdTag.BestQualityScale},
            {50781, IfdTag.RawDataUniqueID},
            {50784, IfdTag.AliasLayerMetadata},
            {50827, IfdTag.OriginalRawFileName},
            {50828, IfdTag.OriginalRawFileData},
            {50829, IfdTag.ActiveArea},
            {50830, IfdTag.MaskedAreas},
            {50831, IfdTag.AsShotICCProfile},
            {50832, IfdTag.AsShotPreProfileMatrix},
            {50833, IfdTag.CurrentICCProfile},
            {50834, IfdTag.CurrentPreProfileMatrix},
            {50879, IfdTag.ColorimetricReference},
            {50931, IfdTag.CameraCalibrationSignature},
            {50932, IfdTag.ProfileCalibrationSignature},
            {50933, IfdTag.ExtraCameraProfiles},
            {50934, IfdTag.AsShotProfileName},
            {50935, IfdTag.NoiseReductionApplied},
            {50936, IfdTag.ProfileName},
            {50937, IfdTag.ProfileHueSatMapDims},
            {50938, IfdTag.ProfileHueSatMapData1},
            {50939, IfdTag.ProfileHueSatMapData2},
            {50940, IfdTag.ProfileToneCurve},
            {50941, IfdTag.ProfileEmbedPolicy},
            {50942, IfdTag.ProfileCopyright},
            {50964, IfdTag.ForwardMatrix1},
            {50965, IfdTag.ForwardMatrix2},
            {50966, IfdTag.PreviewApplicationName},
            {50967, IfdTag.PreviewApplicationVersion},
            {50968, IfdTag.PreviewSettingsName},
            {50969, IfdTag.PreviewSettingsDigest},
            {50970, IfdTag.PreviewColorSpace},
            {50971, IfdTag.PreviewDateTime},
            {50972, IfdTag.RawImageDigest},
            {50973, IfdTag.OriginalRawFileDigest},
            {50974, IfdTag.SubTileBlockSize},
            {50975, IfdTag.RowInterleaveFactor},
            {50981, IfdTag.ProfileLookTableDims},
            {50982, IfdTag.ProfileLookTableData},
            {51008, IfdTag.OpcodeList1},
            {51009, IfdTag.OpcodeList2},
            {51022, IfdTag.OpcodeList3},
            {51041, IfdTag.NoiseProfile},
            {51089, IfdTag.OriginalDefaultFinalSize},
            {51090, IfdTag.OriginalBestQualityFinalSize},
            {51091, IfdTag.OriginalDefaultCropSize},
            {51107, IfdTag.ProfileHueSatMapEncoding},
            {51108, IfdTag.ProfileLookTableEncoding},
            {51109, IfdTag.BaselineExposureOffset},
            {51110, IfdTag.DefaultBlackRender},
            {51111, IfdTag.NewRawImageDigest},
            {51112, IfdTag.RawToPreviewGain},
            {51125, IfdTag.DefaultUserCrop}
        };

        private static readonly Dictionary<int, IfdType> IfdTypes = new Dictionary<int, IfdType>
        {
            // 11124811248484
            {1, new IfdType {BytesLength = 1}},
            {2, new IfdType {BytesLength = 1}},
            {3, IfdType.UInt16},
            {4, IfdType.UInt32},
            {5, IfdType.UInt32Fraction},
            {6, new IfdType {BytesLength = 1}},
            {7, IfdType.Byte},
            {10, IfdType.UInt32Fraction},
        };

        public uint length;
        public uint nextOffset;
        public byte[] rawdata;
        public ushort rawtag;
        public ushort rawtype;

        public IfdTag? tag;
        public IfdType type;


        public static IfdBlock parse(BinaryReader reader)
        {
            var result = new IfdBlock();
            result.rawtag = reader.ReadUInt16();
            IfdTag tag;
            result.tag = (TagMap.TryGetValue(result.rawtag, out tag)) ? (IfdTag?)tag : null;

            result.rawtype = reader.ReadUInt16();
            result.type = IfdTypes[result.rawtype];

            var length = reader.ReadUInt32();
            result.length = length;
            result.nextOffset = ((uint)reader.BaseStream.Position) + 4;
            var datalength = length * result.type.BytesLength;
            if (datalength > 4)
                reader.BaseStream.Seek(reader.ReadUInt32(), SeekOrigin.Begin);

            result.rawdata = reader.ReadBytes((int)datalength);
            if (datalength < 4) reader.ReadBytes(4 - (int)datalength);
            return result;
        }

        public uint moveNext(BinaryReader reader)
        {
            reader.BaseStream.Seek(nextOffset, SeekOrigin.Begin);
            return nextOffset;
        }

        public uint GetUInt32()
        {
            if (type == IfdType.Byte)
                return rawdata[0];
            if (type == IfdType.UInt16)
                return BitConverter.ToUInt16(rawdata, 0);
            if (type == IfdType.UInt32)
                return BitConverter.ToUInt32(rawdata, 0);
            throw new ArgumentException("rawdata", "GetUInt32 failed on wrong type: " + rawtype);
        }

        public ushort GetUInt16()
        {
            var res = GetUInt32();
            if (res > ushort.MaxValue)
                throw new ArgumentException("rawdata", "GetUInt16 failed on value bigger than UInt16: " + res);
            return (ushort)res;
        }

        public string GetString()
        {
            return Encoding.UTF8.GetString(rawdata, 0, rawdata.Length).Trim(new[] { ' ', '\t', '\0' });
        }

        public Fraction GetFraction()
        {
            if (type == IfdType.UInt32Fraction)
                return new Fraction((int)BitConverter.ToUInt32(rawdata, 0), (int)BitConverter.ToUInt32(rawdata, 4));
            throw new ArgumentException("rawdata", "readFraction failed on wrong type: " + rawtype);
        }
    }
}