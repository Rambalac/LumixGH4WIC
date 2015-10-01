using System.IO;
using com.azi.Decoder.Panasonic.Rw2;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Tests
{
    [TestClass]
    public class Test
    {

        [TestMethod]
        public void TestDecodeDoesNotFail()
        {
            var decoder = new PanasonicRW2Decoder();

            var file = new FileStream(@"..\..\P1350577.RW2", FileMode.Open, FileAccess.Read);
            var exif=decoder.DecodeExif(file);
            decoder.DecodeMap(file, exif);
        }
    }
}