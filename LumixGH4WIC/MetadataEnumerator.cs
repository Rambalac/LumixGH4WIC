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
            Log.Trace("MetadataEnumerator.Clone called");
            ppenum = new MetadataEnumerator(exif);
        }

        public void RemoteNext(uint celt, out object rgelt, out uint pceltFetched)
        {
            Log.Trace("MetadataEnumerator.RemoteNext called");
            throw new NotImplementedException();
        }

        public void Reset()
        {
            Log.Trace("MetadataEnumerator.Reset called");
            enumerator = exif.RawIfd.GetEnumerator();
        }

        public void Skip(uint celt)
        {
            Log.Trace("MetadataEnumerator.Skip called");
            throw new NotImplementedException();
        }

        public void GetContainerFormat(out Guid pguidContainerFormat)
        {
            Log.Trace("MetadataEnumerator.GetContainerFormatGetContainerFormat called");
            pguidContainerFormat = typeof(RW2BitmapDecoder).GUID;
        }

        public void GetLocation(uint cchMaxLength, ref ushort wzNamespace, out uint pcchActualLength)
        {
            Log.Trace("MetadataEnumerator.GetLocation called");
            throw new NotImplementedException();
        }

        public void GetMetadataByName(string wzName, ref tag_inner_PROPVARIANT pvarValue)
        {
            Log.Trace("MetadataEnumerator.GetMetadataByName called");
            throw new NotImplementedException();
        }

        public void GetEnumerator(out IEnumString ppIEnumString)
        {
            Log.Trace("MetadataEnumerator.GetEnumerator called");
            throw new NotImplementedException();
        }

        public void GetMetadataFormat(out Guid pguidMetadataFormat)
        {
            Log.Trace("MetadataEnumerator.GetMetadataFormat called");
            throw new NotImplementedException();
        }

        public void GetMetadataHandlerInfo(out IWICMetadataHandlerInfo ppIHandler)
        {
            Log.Trace("MetadataEnumerator.GetMetadataHandlerInfo called");
            throw new NotImplementedException();
        }

        public void GetCount(out uint pcCount)
        {
            Log.Trace("MetadataEnumerator.GetCount called");
            throw new NotImplementedException();
        }

        public void GetValueByIndex(uint nIndex, ref tag_inner_PROPVARIANT pvarSchema, ref tag_inner_PROPVARIANT pvarId, ref tag_inner_PROPVARIANT pvarValue)
        {
            Log.Trace("MetadataEnumerator.GetValueByIndex called");
            throw new NotImplementedException();
        }

        public void GetValue(ref tag_inner_PROPVARIANT pvarSchema, ref tag_inner_PROPVARIANT pvarId, ref tag_inner_PROPVARIANT pvarValue)
        {
            Log.Trace("MetadataEnumerator.GetValue called");
            throw new NotImplementedException();
        }

        public void GetEnumerator(out IWICEnumMetadataItem ppIEnumMetadata)
        {
            Log.Trace("MetadataEnumerator.GetEnumerator called");
            throw new NotImplementedException();
        }
    }

}
