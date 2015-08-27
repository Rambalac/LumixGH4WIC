using System;
using System.Runtime.InteropServices;
using WIC;


namespace LumixGH4WIC
{

    [Guid("DD48659C-F21F-4C15-AE70-6879ED43B84C")]
    public class WICBitmapDecoder : IWICBitmapDecoder
    {

        public void CopyPalette(IWICPalette pIPalette)
        {
            throw new NotImplementedException();
        }

        public void GetColorContexts(uint cCount, ref IWICColorContext ppIColorContexts, out uint pcActualCount)
        {
            throw new NotImplementedException();
        }

        public void GetContainerFormat(out Guid pguidContainerFormat)
        {
            throw new NotImplementedException();
        }

        public void GetDecoderInfo(out IWICBitmapDecoderInfo ppIDecoderInfo)
        {
            throw new NotImplementedException();
        }

        public void GetFrame(uint index, out IWICBitmapFrameDecode ppIBitmapFrame)
        {
            throw new NotImplementedException();
        }

        public void GetFrameCount(out uint pCount)
        {
            throw new NotImplementedException();
        }

        public void GetMetadataQueryReader(out IWICMetadataQueryReader ppIMetadataQueryReader)
        {
            throw new NotImplementedException();
        }

        public void GetPreview(out IWICBitmapSource ppIBitmapSource)
        {
            throw new NotImplementedException();
        }

        public void GetThumbnail(out IWICBitmapSource ppIThumbnail)
        {
            throw new NotImplementedException();
        }

        public void Initialize(IStream pIStream, WICDecodeOptions cacheOptions)
        {
            throw new NotImplementedException();
        }

        public void QueryCapability(IStream pIStream, out uint pdwCapability)
        {
            throw new NotImplementedException();
        }
    }
}
