using com.azi.Decoder.Panasonic;
using com.azi.Decoder.Panasonic.Rw2;
using com.azi.tiff;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using WIC;
using IStream = System.Runtime.InteropServices.ComTypes.IStream;

namespace LumixGH4WIC
{
    [ComVisible(true)]
    [Guid("DD48659C-F21F-4C15-AE70-6879ED43B84C")]
    public class RW2BitmapDecoder : IWICBitmapDecoder, IDisposable//, IWICMetadataBlockReader
    {
        internal static IWICImagingFactory ImagingFactory;
        internal static IWICImagingFactory GetImagingFactory()
        {
            if (ImagingFactory == null)
                try
                {
                    ImagingFactory = (IWICImagingFactory)new WICImagingFactory1();
                }
                catch
                {
                    try
                    {
                        ImagingFactory = (IWICImagingFactory)new WICImagingFactory2();
                    }
                    catch
                    {
                        ImagingFactory = (IWICImagingFactory)new WICImagingFactory3();
                    }
                }
            return ImagingFactory;
        }

        PanasonicExif _exif;
        WICReadOnlyStreamWrapper stream;

        private PanasonicExif Exif
        {
            get
            {
                if (_exif == null)
                    _exif = (PanasonicExif)new PanasonicRW2Decoder().DecodeExif(stream);
                return _exif;
            }
        }

        [ComRegisterFunction]
        public static void OnRegistry(Type type)
        {
            NativeMethods.SHChangeNotify(HChangeNotifyEventID.SHCNE_ASSOCCHANGED, HChangeNotifyFlags.SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);

            Log.Trace("GH4 RW2 WIC Decoder registered. " + type);
        }

        [ComUnregisterFunction]
        public static void OnUnregistry(Type type)
        {
            Log.Trace("GH4 RW2 WIC Decoder unregistered. " + type);
        }

        public void CopyPalette(IWICPalette pIPalette)
        {
            Log.Trace("CopyPalette called");

            throw new NotImplementedException();
        }

        public void GetColorContexts(uint cCount, ref IWICColorContext ppIColorContexts, out uint pcActualCount)
        {
            Log.Trace("GetColorContexts called");

            throw new NotImplementedException();
        }

        public void GetContainerFormat(out Guid pguidContainerFormat)
        {
            Log.Trace("Main GetContainerFormat called");
            try {
                pguidContainerFormat = new Guid("163bcc30-e2e9-4f0b-961d-a3e9fdb788a3");
                Log.Trace("Main GetContainerFormat finished");
            }catch(Exception e)
            {
                Log.Error("Main GetContainerFormat failed: " + e);
                throw;
            }
        }

        public void GetDecoderInfo(out IWICBitmapDecoderInfo ppIDecoderInfo)
        {
            Log.Trace("GetDecoderInfo called");

            var imagingFactory = GetImagingFactory();
            IWICComponentInfo componentInfo;
            var guid = GetType().GUID;
            imagingFactory.CreateComponentInfo(ref guid, out componentInfo);
            ppIDecoderInfo = (IWICBitmapDecoderInfo)componentInfo;

            Log.Trace("GetDecoderInfo finished");
        }

        public void GetFrame(uint index, out IWICBitmapFrameDecode ppIBitmapFrame)
        {
            Log.Trace($"GetFrame called: {index}");
            try
            {
                if (index != 0) throw new COMException("Only 0 Frame available");
                ppIBitmapFrame = new BitmapFrameDecode(stream, Exif);
                Log.Trace("GetFrame finished");
            }
            catch (Exception e)
            {
                Log.Error("GetFrame failed: " + e);
                throw;
            }
        }

        public void GetFrameCount(out uint pCount)
        {
            pCount = 1;
        }

        public void GetMetadataQueryReader(out IWICMetadataQueryReader ppIMetadataQueryReader)
        {
            Log.Trace("GetMetadataQueryReader called");

            ppIMetadataQueryReader = new MetadataEnumerator(Exif);
        }

        public void GetPreview(out IWICBitmapSource ppIBitmapSource)
        {
            GetThumbnail(out ppIBitmapSource);
        }

        public void GetThumbnail(out IWICBitmapSource ppIThumbnail)
        {
            Log.Trace("GetThumbnail called");

            try
            {
                var guid = Guid.Empty;

                var pPreviewDecoder = GetImagingFactory().CreateDecoderFromStream(
                                    new StreamComWrapper(new MemoryStream(Exif.Thumbnail)), ref guid,
                                    WICDecodeOptions.WICDecodeMetadataCacheOnDemand);
                IWICBitmapFrameDecode pPreviewFrame;
                pPreviewDecoder.GetFrame(0, out pPreviewFrame);
                ppIThumbnail = pPreviewFrame;
                Log.Trace("GetThumbnail finished");
            }
            catch (Exception e)
            {
                Log.Error("GetThumbnail failed: " + e);
                throw;
            }
        }

        public void Initialize(IStream pIStream, WICDecodeOptions cacheOptions)
        {
            Log.Trace("Initialize called");

            stream = new WICReadOnlyStreamWrapper(pIStream);

            if (cacheOptions == WICDecodeOptions.WICDecodeMetadataCacheOnLoad)
            {
                var position = stream.Position;
                _exif = (PanasonicExif)new PanasonicRW2Decoder().DecodeExif(new WICReadOnlyStreamWrapper(pIStream));
                stream.Position = position;
            }
            Log.Trace("Initialize finished");
        }

        public void QueryCapability(IStream pIStream, out uint pdwCapability)
        {
            Log.Trace("QueryCapability called");

            stream = new WICReadOnlyStreamWrapper(pIStream);
            var position = stream.Position;
            try
            {
                pdwCapability = (new PanasonicRW2Decoder().IsSupported(stream))
                    ? (uint)(WICBitmapDecoderCapabilities.WICBitmapDecoderCapabilityCanDecodeThumbnail
                    | WICBitmapDecoderCapabilities.WICBitmapDecoderCapabilityCanDecodeAllImages
                    | WICBitmapDecoderCapabilities.WICBitmapDecoderCapabilityCanEnumerateMetadata
                    | WICBitmapDecoderCapabilities.WICBitmapDecoderCapabilityCanDecodeThumbnail)
                    : 0;
            }
            catch (Exception)
            {
                pdwCapability = 0;
            }
            finally { stream.Position = position; }

        }

        public void GetCount(out uint pcCount)
        {
            Log.Trace("IWICMetadataBlockReader.GetCount called");
            pcCount = (uint)Exif.RawIfd.Count;
            Log.Trace("IWICMetadataBlockReader.GetCount finished");
        }

        public void GetReaderByIndex(uint nIndex, out IWICMetadataReader ppIMetadataReader)
        {
            Log.Trace($"IWICMetadataBlockReader.GetReaderByIndex called: {nIndex}");
            throw new NotImplementedException();
            //Log.Trace("IWICMetadataBlockReader.GetReaderByIndex finished");
        }

        public void GetEnumerator(out IEnumUnknown ppIEnumMetadata)
        {
            Log.Trace("IWICMetadataBlockReader.GetEnumerator called");
            ppIEnumMetadata = new MetadataEnumerator(Exif);
            Log.Trace("IWICMetadataBlockReader.GetEnumerator finished");
        }

        protected virtual void Dispose(bool notnative)
        {
            stream.Dispose();
        }

        public void Dispose()
        {
            Dispose(false);
        }
    }
}
