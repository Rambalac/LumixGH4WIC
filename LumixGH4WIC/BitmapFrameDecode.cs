﻿using com.azi.Decoder.Panasonic;
using com.azi.Decoder.Panasonic.Rw2;
using com.azi.Filters;
using com.azi.Filters.Converters;
using com.azi.Filters.Converters.Demosaic;
using com.azi.Filters.VectorMapFilters;
using com.azi.Image;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using WIC;

namespace LumixGH4WIC
{
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    [Guid("1253D0FF-6836-4DE9-A141-FEF5B4C55DAA")]
    class BitmapFrameDecode : IWICBitmapFrameDecode, IWICBitmapSourceTransform, IWICMetadataBlockReader
    {
        static readonly Guid GUID_WICPixelFormat16bppBGR565 = new Guid("6FDDC324-4E03-4BFE-B185-3D77768DC90A");
        static readonly Guid GUID_WICPixelFormat16bppBGRA5551 = new Guid("05EC7C2B-F1E6-4961-AD46-E1CC810A87D2");

        static readonly Guid GUID_WICPixelFormat24bppBGR = new Guid("6FDDC324-4E03-4BFE-B185-3D77768DC90C");
        static readonly Guid GUID_WICPixelFormat24bppRGB = new Guid("6FDDC324-4E03-4BFE-B185-3D77768DC90D");

        static readonly Guid GUID_WICPixelFormat32bppBGR = new Guid("6FDDC324-4E03-4BFE-B185-3D77768DC90E");
        static readonly Guid GUID_WICPixelFormat32bppBGRA = new Guid("6FDDC324-4E03-4BFE-B185-3D77768DC90F");
        static readonly Guid GUID_WICPixelFormat32bppRGB = new Guid("D98C6B95-3EFE-47D6-BB25-EB1748AB0CF1");
        static readonly Guid GUID_WICPixelFormat32bppRGBA = new Guid("F5C7AD2D-6A8D-43DD-A7A8-A29935261AE9");

        PanasonicExif exif;
        Stream stream;
        ColorMap<BGRA8> rgbmap;

        public BitmapFrameDecode(Stream _stream, PanasonicExif _exif)
        {
            exif = _exif;
            stream = _stream;
        }

        ColorMap<ushort> ReadRaw()
        {
            var position = stream.Position;
            try
            {
                return new PanasonicRW2Decoder().DecodeMap(stream, exif);
            }
            catch (Exception e)
            {
                throw new COMException("RW2 Decoding failed", e);
            }
            finally
            {
                stream.Position = position;
                stream.Dispose();
            }
        }

        public void CopyPalette(IWICPalette pIPalette)
        {
            Log.Trace("CopyPalette called");
            throw new COMException("No Palette", (int)WinCodecErrors.WINCODEC_ERR_PALETTEUNAVAILABLE);
        }

        unsafe void CopyBGRA(ref WICRect prc, uint cbStride, uint cbBufferSize, byte[] pbBuffer)
        {
            BuildRGB();

            fixed (byte* p = pbBuffer)
            {
                var dest = p;
                for (int y = prc.Y; y < prc.Y + prc.Height; y++)
                {
                    fixed (BGRA8* line = rgbmap.Values[y])
                    {
                        var src = (byte*)(line + prc.X);
                        int length = prc.Width * rgbmap.BytesPerPixel;
                        for (; length > 0; length--, dest++, src++) *dest = *src;
                    }
                }
            }
        }

        List<IFilter> PrepareFilters()
        {
            var debayer = new AverageBGGRDemosaic();

            var white = new WhiteBalanceFilter();
            white.WhiteColor = exif.WhiteColor.ToVector3();

            var gamma = new GammaFilter();
            var light = new LightFilter();

            var satur = new SaturationFilter(3f);
            var colorMatrix = new ColorMatrixFilter
            {
                Matrix = new[,]
                    {
                        {1.87f, -0.81f, -0.06f},
                        {-0.16f, 1.55f, -0.39f},
                        {0.05f, -0.47f, 1.42f}
                    }.ToMatrix4x4()
            };
            var compressor = new VectorBGRACompressorFilter();

            var filters = new List<IFilter>
            {
                    debayer,
                    white,
                    gamma,
                    //new RGB2YUVFilter(),
                    light,
                    satur,
                    //new YUV2RGBFilter(),
                    //colorMatrix,
                    compressor
            };
            return filters;
        }

        void BuildRGB()
        {
            Log.Trace($"BuildRGB called");
            lock (this)
            {
                if (rgbmap == null)
                {
                    var map = ReadRaw();

                    var filters = PrepareFilters();

                    var processor = new ImageProcessor(map, filters);
                    rgbmap = (ColorMap<BGRA8>)processor.Invoke();
                    map.Dispose();
                }
            }
            Log.Trace("BuildRGB finished");
        }

        public void GetColorContexts(uint cCount, ref IWICColorContext ppIColorContexts, out uint pcActualCount)
        {
            Log.Trace("GetColorContexts called");
            pcActualCount = 0;
        }

        public void GetMetadataQueryReader(out IWICMetadataQueryReader ppIMetadataQueryReader)
        {
            Log.Trace("GetMetadataQueryReader called");
            ppIMetadataQueryReader = new MetadataEnumerator(exif);
            Log.Trace("GetMetadataQueryReader finished");
        }

        public void GetPixelFormat(out Guid pPixelFormat)
        {
            Log.Trace("GetPixelFormat called");
            pPixelFormat = GUID_WICPixelFormat32bppBGRA;
            //pPixelFormat = GUID_WICPixelFormat24bppRGB;
            Log.Trace("GetPixelFormat finished");
        }

        public void GetResolution(out double pDpiX, out double pDpiY)
        {
            Log.Trace("GetResolution called");
            pDpiX = 96;
            pDpiY = 96;
            Log.Trace("GetResolution finished");
        }

        public void GetSize(out uint puiWidth, out uint puiHeight)
        {
            Log.Trace("GetSize called");
            puiWidth = (uint)exif.RealWidth;
            puiHeight = (uint)exif.RealHeight;
            Log.Trace($"GetSize finished: {puiWidth}x{puiHeight}");
        }

        public void GetThumbnail(out IWICBitmapSource ppIThumbnail)
        {
            Log.Trace("GetThumbnail called");
            var guid = Guid.Empty;
            var pPreviewDecoder = RW2BitmapDecoder.GetImagingFactory().CreateDecoderFromStream(
                                new StreamComWrapper(new MemoryStream(exif.Thumbnail)), ref guid,
                                WICDecodeOptions.WICDecodeMetadataCacheOnDemand);
            IWICBitmapFrameDecode pPreviewFrame;
            pPreviewDecoder.GetFrame(0, out pPreviewFrame);
            ppIThumbnail = pPreviewFrame;
            Log.Trace("GetThumbnail finished");
        }

        public void GetContainerFormat(out Guid pguidContainerFormat)
        {
            Log.Trace("Frame GetContainerFormat called");
            try
            {
                pguidContainerFormat = RW2BitmapDecoder.FormatGuid;
                Log.Trace("Frame  GetContainerFormat finished");
            }
            catch (Exception e)
            {
                Log.Error("Frame  GetContainerFormat failed: " + e);
                throw;
            }

        }

        ////////// Metadata
        public void GetCount(out uint pcCount)
        {
            Log.Trace("Frame IWICMetadataBlockReader.GetCount called");
            pcCount = 0;
            Log.Trace("Frame IWICMetadataBlockReader.GetCount finished");
        }

        public void GetReaderByIndex(uint nIndex, out IWICMetadataReader ppIMetadataReader)
        {
            Log.Error($"Frame IWICMetadataBlockReader.GetReaderByIndex called: {nIndex}");
            throw new NotImplementedException();
        }

        public void GetEnumerator(out IEnumUnknown ppIEnumMetadata)
        {
            Log.Error("Frame IWICMetadataBlockReader.GetEnumerator called");
            throw new NotImplementedException();
        }

        ////////// Transform

        public void GetClosestSize([In, Out] ref uint puiWidth, [In, Out] ref uint puiHeight)
        {
            Log.Trace($"Trans GetClosestSize called: {puiWidth} {puiHeight}");
            //throw new NotImplementedException();
        }

        public void GetClosestPixelFormat([In, Out] ref Guid dstFormat)
        {
            Log.Trace($"Trans GetClosestPixelFormat called: {dstFormat}");
            if (dstFormat == GUID_WICPixelFormat32bppBGRA) return;

            Log.Error($"Trans GetClosestPixelFormat not supported: {dstFormat}");
            throw new NotImplementedException();
        }

        public void DoesSupportTransform([In] WICBitmapTransformOptions dstTransform, out int pfIsSupported)
        {
            Log.Trace($"Trans DoesSupportTransform called");
            pfIsSupported = 0;
        }

        public void CopyPixels([In] ref WICRect prc, [In] uint uiWidth, [In] uint uiHeight, Guid dstFormat, [In] WICBitmapTransformOptions dstTransform, [In] uint nStride, [In] uint cbBufferSize, byte[] pbBuffer)
        {
            try
            {
                Log.Trace($"Trans CopyPixels called");
                Log.Trace($"Trans CopyPixels called: {uiWidth}, {uiHeight} {dstFormat}");
                if (dstFormat == GUID_WICPixelFormat32bppBGRA)
                    CopyBGRA(ref prc, nStride, cbBufferSize, pbBuffer);
                if (prc.Y + prc.Height == rgbmap.Height) rgbmap.Dispose();
                Log.Trace("CopyPixels finished");
            }
            catch (Exception e)
            {
                Log.Error("CopyPixels failed: " + e);
                throw;
            }
        }

        public void CopyPixels(ref WICRect prc, uint cbStride, uint cbBufferSize, byte[] pbBuffer)
        {
            //Log.Trace($"CopyPixels called: ({prc.X} {prc.Y} {prc.Width} {prc.Height}) Stride: {cbStride}, Size: {cbBufferSize}");

            try
            {
                //Log.Trace($"CopyPixels rgbmap {rgbmap.Width}, {rgbmap.Height}");
                CopyBGRA(ref prc, cbStride, cbBufferSize, pbBuffer);
                if (prc.Y + prc.Height == rgbmap.Height) rgbmap.Dispose();
                //Log.Trace("CopyPixels finished");
            }
            catch (Exception e)
            {
                Log.Error("CopyPixels failed: " + e);
                throw;
            }

        }

    }
}
