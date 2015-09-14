using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WIC;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace LumixGH4WIC.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [Guid("CACAF262-9370-4615-A13B-9F5539DA4C0A")]
        [ComImport]
        class WICImagingFactory1 { }

        [Guid("7B816B45-1996-4476-B132-DE9E247C8AF0")]
        [ComImport]
        class WICImagingFactory2 { }

        [Guid("EC5EC8A9-C395-4314-9C77-54D7A935FF70")]
        [ComImport]
        class WICImagingFactory3 { }
        internal static IWICImagingFactory GetImagingFactory()
        {
            try
            {
                return (IWICImagingFactory)new WICImagingFactory1();
            }
            catch
            {
                try
                {
                    return (IWICImagingFactory)new WICImagingFactory2();
                }
                catch
                {
                    return (IWICImagingFactory)new WICImagingFactory3();
                }
            }
        }

        [TestMethod]
        public void TestCreateDecoder()
        {
            var factory = GetImagingFactory();
            using (Stream sourceStream = File.Open(@"..\..\..\PanasonicRW2.Tests\P1350577.RW2", FileMode.Open, FileAccess.Read))
            {
                Guid nul = Guid.Empty;
                IStream stream = new StreamComWrapper(sourceStream);
                IWICBitmapDecoder decoder = factory.CreateDecoderFromStream(stream, nul, WICDecodeOptions.WICDecodeMetadataCacheOnDemand);
                decoder.Initialize(stream, WICDecodeOptions.WICDecodeMetadataCacheOnLoad);
                IWICBitmapFrameDecode frame;
                decoder.GetFrame(0, out frame);
                uint w, h;
                frame.GetSize(out w, out h);
                var buf = new byte[w * h * 3];
                frame.CopyPixels(new WICRect { Height = (int)h, Width = (int)w }, w * 3, (uint)buf.Length, buf);
            }
            Marshal.ReleaseComObject(factory);
        }
        [TestMethod]
        public void TestCreateDecoderMultithread()
        {
            var factory = GetImagingFactory();
            using (Stream sourceStream = File.Open(@"..\..\..\PanasonicRW2.Tests\P1350577.RW2", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Guid nul = Guid.Empty;
                IStream stream = new StreamComWrapper(sourceStream);
                IWICBitmapDecoder decoder = factory.CreateDecoderFromStream(stream, nul, WICDecodeOptions.WICDecodeMetadataCacheOnDemand);
                decoder.Initialize(stream, WICDecodeOptions.WICDecodeMetadataCacheOnLoad);
                Parallel.For(0, 8, new ParallelOptions { MaxDegreeOfParallelism = 8 }, (i) =>
                 {
                     IWICBitmapFrameDecode frame;
                     decoder.GetFrame(0, out frame);
                     uint w, h;
                     frame.GetSize(out w, out h);
                     var buf = new byte[w * h * 3];
                     frame.CopyPixels(new WICRect { Height = (int)h, Width = (int)w }, w * 3, (uint)buf.Length, buf);
                 });
            }
            Marshal.ReleaseComObject(factory);
        }
    }
}
