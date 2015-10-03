using System;
using System.Diagnostics;
using System.IO;
using com.azi.Decoder.Panasonic.Rw2;
using com.azi.Filters.Converters.Demosaic;
using com.azi.Image;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace General.Tests
{
    [TestClass]
    public class AverageBGGRDebayerTests
    {
        [TestMethod]
        public void NotFail()
        {
            var decoder = new PanasonicRW2Decoder();

            var file = new FileStream(@"..\..\..\PanasonicRW2.Tests\P1350577.RW2", FileMode.Open, FileAccess.Read);
            var exif = decoder.DecodeExif(file);
            var raw = decoder.DecodeMap(file, exif);

            var debayer = new AverageBGGRDemosaic();

            var stopwatch = Stopwatch.StartNew();
            const int maxIter = 3;
            var res = debayer.CreateResultMap(raw);
            for (var iter = 0; iter < maxIter; iter++)
                debayer.ProcessMap(raw, res);
            stopwatch.Stop();

            Console.WriteLine("AverageBGGRDebayer: " + stopwatch.ElapsedMilliseconds / 3 + "ms");
        }
    }
}