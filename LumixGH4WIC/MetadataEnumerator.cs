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

        public void GetAuthor([In] uint cchAuthor, StringBuilder wzAuthor, out uint pcchActual)
        {
            Log.Trace("MetadataHandlerInfo.GetAuthor called");
            throw new NotImplementedException();
        }

        public void GetCLSID(out Guid pclsid)
        {
            Log.Trace("MetadataHandlerInfo.GetCLSID called");
            throw new NotImplementedException();
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

        string Manufacturer = "Panasonic";

        public void GetDeviceManufacturer([In] uint cchDeviceManufacturer, StringBuilder wzDeviceManufacturer, out uint pcchActual)
        {
            if (cchDeviceManufacturer == 0) { pcchActual = (uint)Manufacturer.Length; return; }

            Log.Trace("MetadataHandlerInfo.GetDeviceManufacturer called");
            wzDeviceManufacturer.Append(Manufacturer);
            pcchActual = (uint)wzDeviceManufacturer.Length;
            Log.Trace("MetadataHandlerInfo.GetDeviceManufacturer finished");
        }

        public void GetDeviceModels([In] uint cchDeviceModels, StringBuilder wzDeviceModels, out uint pcchActual)
        {
            Log.Trace("MetadataHandlerInfo.GetDeviceModels called");
            throw new NotImplementedException();
        }

        string FriendlyName = "Rambalac Lumix GH4 WIC Decoder";

        public void GetFriendlyName([In] uint cchFriendlyName, StringBuilder wzFriendlyName, out uint pcchActual)
        {
            if (cchFriendlyName == 0) { pcchActual = (uint)FriendlyName.Length; return; }

            Log.Trace("MetadataHandlerInfo.GetFriendlyName called");
            wzFriendlyName.Append(FriendlyName);
            pcchActual = (uint)wzFriendlyName.Length;
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
            throw new NotImplementedException();
        }

        public void GetSpecVersion([In] uint cchSpecVersion, StringBuilder wzSpecVersion, out uint pcchActual)
        {
            Log.Trace("MetadataHandlerInfo.GetSpecVersion called");
            throw new NotImplementedException();
        }

        public void GetVendorGUID(out Guid pguidVendor)
        {
            Log.Trace("MetadataHandlerInfo.GetVendorGUID called");
            throw new NotImplementedException();
        }

        public void GetVersion([In] uint cchVersion, StringBuilder wzVersion, out uint pcchActual)
        {
            Log.Trace("MetadataHandlerInfo.GetVersion called");
            throw new NotImplementedException();
        }

    }

    [ComVisible(true)]
    class MetadataReader : IWICMetadataReader
    {
        private PanasonicExif exif;
        static MetadataHandlerInfo MetadataHandlerInfo = new MetadataHandlerInfo();

        public void GetMetadataFormat(out Guid pguidMetadataFormat)
        {
            Log.Trace("MetadataReader.GetMetadataFormat called");
            pguidMetadataFormat = new Guid("{8FD3DFC3-F951-492B-817F-69C2E6D9A5B0}");
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
            Log.Trace("MetadataReader.GetCount finished");
        }

        public void GetValueByIndex(uint nIndex, ref tag_inner_PROPVARIANT pvarSchema, ref tag_inner_PROPVARIANT pvarId, ref tag_inner_PROPVARIANT pvarValue)
        {
            Log.Trace("MetadataReader.GetValueByIndex called");
            throw new NotImplementedException();
        }

        public void GetValue(ref tag_inner_PROPVARIANT pvarSchema, ref tag_inner_PROPVARIANT pvarId, ref tag_inner_PROPVARIANT pvarValue)
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
