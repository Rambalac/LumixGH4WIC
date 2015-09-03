using com.azi.Decoder.Panasonic;
using com.azi.tiff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WIC;

namespace LumixGH4WIC
{
    [ComVisible(true)]
    class MetadataEnumerator : IEnumUnknown, IWICMetadataQueryReader, IWICMetadataReader
    {
        private PanasonicExif exif;
        private List<IfdBlock>.Enumerator enumerator;

        public MetadataEnumerator(PanasonicExif exif)
        {
            this.exif = exif;
            enumerator = exif.RawIfd.GetEnumerator();
        }

        public void Clone(out IEnumUnknown ppenum)
        {
            ppenum = new MetadataEnumerator(exif);
        }

        public void RemoteNext(uint celt, out object rgelt, out uint pceltFetched)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            enumerator = exif.RawIfd.GetEnumerator();
        }

        public void Skip(uint celt)
        {
            throw new NotImplementedException();
        }

        public void GetContainerFormat(out Guid pguidContainerFormat)
        {
            pguidContainerFormat = typeof(RW2BitmapDecoder).GUID;
        }

        public void GetLocation(uint cchMaxLength, ref ushort wzNamespace, out uint pcchActualLength)
        {
            throw new NotImplementedException();
        }

        public void GetMetadataByName(string wzName, ref tag_inner_PROPVARIANT pvarValue)
        {
            throw new NotImplementedException();
        }

        public void GetEnumerator(out IEnumString ppIEnumString)
        {
            throw new NotImplementedException();
        }

        public void GetMetadataFormat(out Guid pguidMetadataFormat)
        {
            throw new NotImplementedException();
        }

        public void GetMetadataHandlerInfo(out IWICMetadataHandlerInfo ppIHandler)
        {
            throw new NotImplementedException();
        }

        public void GetCount(out uint pcCount)
        {
            throw new NotImplementedException();
        }

        public void GetValueByIndex(uint nIndex, ref tag_inner_PROPVARIANT pvarSchema, ref tag_inner_PROPVARIANT pvarId, ref tag_inner_PROPVARIANT pvarValue)
        {
            throw new NotImplementedException();
        }

        public void GetValue(ref tag_inner_PROPVARIANT pvarSchema, ref tag_inner_PROPVARIANT pvarId, ref tag_inner_PROPVARIANT pvarValue)
        {
            throw new NotImplementedException();
        }

        public void GetEnumerator(out IWICEnumMetadataItem ppIEnumMetadata)
        {
            throw new NotImplementedException();
        }
    }

}
