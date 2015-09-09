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
    class MetadataHandlerInfo : IWICMetadataHandlerInfo
    {
        private void SetString(string str, uint bufsize, StringBuilder buf, out uint actual)
        {
            actual = (uint)(str.Length + 1);
            if (bufsize != 0)
                buf.Append(str);
        }


        public void DoesRequireFixedSize(out int pfFixedSize)
        {
            Log.Trace("MetadataHandlerInfo.DoesRequireFixedSize called");
            throw new NotImplementedException();
        }

        public void DoesRequireFullStream(out int pfRequiresFullStream)
        {
            Log.Trace("MetadataHandlerInfo.DoesRequireFullStream called");
            throw new NotImplementedException();
        }

        public void DoesSupportPadding(out int pfSupportsPadding)
        {
            Log.Trace("MetadataHandlerInfo.DoesSupportPadding called");
            throw new NotImplementedException();
        }

        public void GetAuthor([In] uint size, StringBuilder buf, out uint actual)
        {
            Log.Trace("MetadataHandlerInfo.GetAuthor called");
            SetString("Rambalac", size, buf, out actual);
        }

        public void GetCLSID(out Guid pclsid)
        {
            Log.Trace("MetadataHandlerInfo.GetCLSID called");
            pclsid = typeof(RW2BitmapDecoder).GUID;
            Log.Trace("MetadataHandlerInfo.GetCLSID finished");
        }

        public void GetComponentType(out WICComponentType pType)
        {
            Log.Trace("MetadataHandlerInfo.GetComponentType called");
            throw new NotImplementedException();
        }

        public void GetContainerFormats([In] uint cContainerFormats, [In, Out] ref Guid pguidContainerFormats, out uint pcchActual)
        {
            Log.Trace("MetadataHandlerInfo.GetContainerFormats called");
            throw new NotImplementedException();
        }

        public void GetDeviceManufacturer([In] uint size, StringBuilder buf, out uint actual)
        {
            Log.Trace("MetadataHandlerInfo.GetDeviceManufacturer called");
            SetString("Panasonic", size, buf, out actual);
            Log.Trace("MetadataHandlerInfo.GetDeviceManufacturer finished");
        }

        public void GetDeviceModels([In] uint size, StringBuilder buf, out uint actual)
        {
            Log.Trace("MetadataHandlerInfo.GetDeviceModels called");
            SetString("Lumix GH4", size, buf, out actual);
            Log.Trace("MetadataHandlerInfo.GetDeviceModels finished");
        }

        public void GetFriendlyName([In] uint size, StringBuilder buf, out uint actual)
        {
            Log.Trace("MetadataHandlerInfo.GetFriendlyName called");
            SetString("Rambalac Lumix GH4 WIC Decoder", size, buf, out actual);
            Log.Trace("MetadataHandlerInfo.GetFriendlyName finished");
        }

        public void GetMetadataFormat(out Guid pguidMetadataFormat)
        {
            Log.Trace("MetadataHandlerInfo.GetMetadataFormat called");
            try
            {
                pguidMetadataFormat = new Guid("{8FD3DFC3-F951-492B-817F-69C2E6D9A5B0}");
                Log.Trace("MetadataHandlerInfo.GetMetadataFormat finished");
            }
            catch (Exception e)
            {
                Log.Error("MetadataHandlerInfo.GetMetadataFormat failed: " + e);
                throw;
            }
        }

        public void GetSigningStatus(out uint pStatus)
        {
            Log.Trace("MetadataHandlerInfo.GetSigningStatus called");
            pStatus = 0;
            Log.Trace("MetadataHandlerInfo.GetSigningStatus finished");
        }

        public void GetSpecVersion([In] uint size, StringBuilder buf, out uint actual)
        {
            Log.Trace("MetadataHandlerInfo.GetSpecVersion called");
            SetString("s1.0", size, buf, out actual);
            Log.Trace("MetadataHandlerInfo.GetSpecVersion finished");
        }

        public void GetVendorGUID(out Guid pguidVendor)
        {
            Log.Trace("MetadataHandlerInfo.GetVendorGUID called");
            pguidVendor = new Guid("077A36A5-66CF-40B4-8820-027ECBD9C371");
            Log.Trace("MetadataHandlerInfo.GetVendorGUID finished");
        }

        public void GetVersion([In] uint size, StringBuilder buf, out uint actual)
        {
            Log.Trace("MetadataHandlerInfo.GetVersion called");
            SetString("v1.0", size, buf, out actual);
            Log.Trace("MetadataHandlerInfo.GetVersion finished");
        }

    }

    [ComVisible(true)]
    class MetadataReader : IWICMetadataReader//, IWICPersistStream, IWICStreamProvider
    {
        private PanasonicExif exif;
        static MetadataHandlerInfo MetadataHandlerInfo = new MetadataHandlerInfo();

        public void GetMetadataFormat(out Guid pguidMetadataFormat)
        {
            Log.Trace("MetadataReader.GetMetadataFormat called");
            //pguidMetadataFormat = new Guid("{8FD3DFC3-F951-492B-817F-69C2E6D9A5B0}");
            pguidMetadataFormat = new Guid("{163bcc30-e2e9-4f0b-961d-a3e9fdb788a3}");
            Log.Trace("MetadataReader.GetMetadataFormat finished");
        }

        public void GetMetadataHandlerInfo(out IWICMetadataHandlerInfo ppIHandler)
        {
            Log.Trace("MetadataReader.GetMetadataHandlerInfo called");
            ppIHandler = MetadataHandlerInfo;
            Log.Trace("MetadataReader.GetMetadataHandlerInfo finished");
        }

        public void GetCount(out uint pcCount)
        {
            Log.Trace("MetadataReader.GetCount called");
            pcCount = (uint)exif.RawIfd.Count;
            Log.Trace("MetadataReader.GetCount finished: " + pcCount);
        }

        public void GetValueByIndex(uint nIndex, ref object pvarSchema, ref object pvarId, ref object pvarValue)
        {
            Log.Trace("MetadataReader.GetValueByIndex called: " + nIndex);
            pvarSchema = "idf";
            pvarId = exif.RawIfd[(int)nIndex].rawtag;
            pvarValue = exif.RawIfd[(int)nIndex].variant;
            Log.Trace("MetadataReader.GetValueByIndex finished");
        }

        public void GetValue(ref object pvarSchema, ref object pvarId, ref object pvarValue)
        {
            Log.Trace("MetadataReader.GetValue called");
            throw new NotImplementedException();
        }

        public void GetEnumerator(out IWICEnumMetadataItem ppIEnumMetadata)
        {
            Log.Trace("MetadataReader.GetEnumerator called");
            throw new NotImplementedException();
        }

        public MetadataReader(PanasonicExif exif)
        {
            this.exif = exif;
        }
    }

    [ComVisible(true)]
    class MetadataEnumerator : IEnumUnknown, IWICMetadataQueryReader
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
            pguidContainerFormat = RW2BitmapDecoder.FormatGuid;
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


    }

}
