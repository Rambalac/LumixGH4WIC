using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WIC;
using System.IO;
using System.Runtime.InteropServices;

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
            using (Stream sourceStream = File.Open(@"..\..\..\PanasonicRW2.Tests\P1350577.RW2", FileMode.Open, FileAccess.Read))
            {
                Guid nul = Guid.Empty;
                IWICBitmapDecoder decoder = GetImagingFactory().CreateDecoderFromStream(new StreamComWrapper(sourceStream), nul, WICDecodeOptions.WICDecodeMetadataCacheOnDemand);
            }

        }
    }
}
