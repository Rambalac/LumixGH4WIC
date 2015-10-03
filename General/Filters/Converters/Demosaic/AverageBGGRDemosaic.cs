using System;
using System.Threading.Tasks;
using com.azi.Image;
using System.Numerics;

namespace com.azi.Filters.Converters.Demosaic
{
    public class AverageBGGRDemosaic : Filter<ushort, Vector3>
    {

        // B G B G
        // G R G R
        // B G B G
        // G R G R
        public override ColorMap<Vector3> CreateResultMap(IColorMap map)
        {
            return new ColorMap<Vector3>(map.Width, map.Height, map.Bits + 1);
        }

        public override void ProcessMap(ColorMap<ushort> inmap, ColorMap<Vector3> outmap)
        {
            if (inmap.Width % 2 != 0 || inmap.Height % 2 != 0) throw new ArgumentException("Width and Height should be even");
            float maxValue = (1 << (inmap.Bits + 1)) - 1;

            ProcessTopLine(inmap, outmap, maxValue);

            ProcessMiddleRows(inmap, outmap, maxValue);

            ProcessBottomLine(inmap, outmap, maxValue);
        }

        static void ProcessMiddleRows(ColorMap<ushort> map, ColorMap<Vector3> res, float maxValue)
        {
            // Middle Rows
            Parallel.For(0, (res.Height - 2) / 2, yy =>
            {
                var y = yy * 2 + 1;
                ProcessMiddleOddRows(map.GetRow(y), map.GetRow(y - 1), map.GetRow(y + 1), res.Width, res.GetRow(y), maxValue);

                y++;

                ProcessMiddleEvenRows(map.GetRow(y), map.GetRow(y - 1), map.GetRow(y + 1), res.Width, res.GetRow(y), maxValue);
            });
        }

        static void ProcessMiddleEvenRows(Pixel<ushort> raw0, Pixel<ushort> rawU, Pixel<ushort> rawD, int Width, Pixel<Vector3> pix, float maxValue)
        {
            // Second left pixel
            pix.SetAndMoveNext(
                ((rawU.GetRel(1) + rawD.GetRel(1))),
                ((rawU.GetRel(0) + rawD.GetRel(0) + (raw0.GetRel(1) << 1)) >> 1),
                (raw0.Value << 1),
                maxValue);
            raw0.MoveNext();
            rawU.MoveNext();
            rawD.MoveNext();

            var lastX = Width - 1;
            for (var x = 1; x < lastX; x += 2)
            {
                var xy = raw0.Value;
                var x1y = raw0.GetRel(+1);
                var xy12 = rawU.GetRel(0) + rawD.GetRel(0);

                pix.SetAndMoveNext(
                    (xy12),
                    (xy << 1),
                    ((raw0.GetRel(-1) + x1y)),
                maxValue);

                pix.SetAndMoveNext(
                    ((xy12 + rawU.GetRel(+2) + rawD.GetRel(+2)) >> 1),

                        ((xy + raw0.GetRel(+2) + rawU.GetRel(+1) + rawD.GetRel(+1)) >> 1),
                    (x1y << 1),
                maxValue);
                raw0.MoveNext(2);
                rawU.MoveNext(2);
                rawD.MoveNext(2);
            }

            // Second right pixel
            pix.SetAndMoveNext(
                ((rawU.GetRel(0) + rawD.GetRel(0))),
                (raw0.Value << 1),
                (raw0.GetRel(-1) << 1),
                maxValue);
        }

        static void ProcessMiddleOddRows(Pixel<ushort> raw0, Pixel<ushort> rawU, Pixel<ushort> rawD, int Width, Pixel<Vector3> pix, float maxValue)
        {
            // First left pixel
            pix.SetAndMoveNext(
                (raw0.GetRel(1) << 1),
                (raw0.Value << 1),
                ((rawU.GetRel(0) + rawD.GetRel(0))),
                maxValue);
            raw0.MoveNext();
            rawU.MoveNext();
            rawD.MoveNext();

            var lastX = Width - 1;
            for (var x = 1; x < lastX; x += 2)
            {
                var xy = raw0.Value;
                var x1y = raw0.GetRel(+1);
                var x11y12 = rawU.GetRel(+1) + rawD.GetRel(+1);

                pix.SetAndMoveNext(
                    (xy << 1),
                    ((raw0.GetRel(-1) + x1y + rawU.GetRel(0) + rawD.GetRel(0)) >> 1),
                    ((rawU.GetRel(-1) + x11y12 + rawD.GetRel(-1)) >> 1),
                maxValue);

                pix.SetAndMoveNext(
                    ((xy + raw0.GetRel(+2))),
                    (x1y << 1),
                    (x11y12),
                maxValue);
                raw0.MoveNext(2);
                rawU.MoveNext(2);
                rawD.MoveNext(2);
            }

            // First right pixel
            pix.SetAndMoveNext(
                (raw0.Value << 1),
                ((rawU.GetRel(0) + rawD.GetRel(0) + (raw0.GetRel(-1) << 1)) >> 1),
                ((rawU.GetRel(-1) + rawD.GetRel(-1))),
                maxValue);
        }

        static void ProcessTopLine(ColorMap<ushort> map, ColorMap<Vector3> res, float maxValue)
        {
            var pix = res.GetRow(0);
            var raw0 = map.GetRow(0);
            var rawD = map.GetRow(1);
            // Top Left pixel

            pix.SetAndMoveNext(
                (rawD.GetRel(1) << 1),
                ((raw0.GetRel(1) + rawD.GetRel(0))),
                (raw0.Value << 1),
                maxValue);
            raw0.MoveNext();
            rawD.MoveNext();

            // Top row
            for (var x = 1; x < res.Width - 1; x += 2)
            {
                pix.SetAndMoveNext(
                    (rawD.GetRel(0) << 1),
                    (raw0.Value << 1),
                    ((raw0.GetRel(-1) + raw0.GetRel(+1))),
                maxValue);

                pix.SetAndMoveNext(
                    ((rawD.GetRel(0) + rawD.GetRel(+2))),
                    ((raw0.Value + raw0.GetRel(+2) + (rawD.GetRel(+1) << 1)) >> 1),
                    (raw0.GetRel(+1) << 1),
                maxValue);
                raw0.MoveNext(2);
                rawD.MoveNext(2);
            }

            // Top right pixel
            pix.SetAndMoveNext(
                (rawD.GetRel(-1) << 1),
                (raw0.GetRel(-1) << 1),
                (raw0.GetRel(-2) << 1),
                maxValue);
        }

        // B G B G
        // G R G R
        // B G B G
        // G R G R
        static void ProcessBottomLine(ColorMap<ushort> map, ColorMap<Vector3> res, float maxValue)
        {
            var pix = res.GetRow(res.Height - 1);
            var raw0 = map.GetRow(res.Height - 1);
            var rawU = map.GetRow(res.Height - 2);

            // Bottom Left pixel
            var lastY = res.Height - 1;

            pix.SetAndMoveNext(
                (raw0.GetRel(1) << 1),
                (raw0.Value << 1),
                (rawU.GetRel(0) << 1),
                maxValue);
            raw0.MoveNext();
            rawU.MoveNext();

            // Bottom row
            for (var x = 1; x < res.Width - 1; x += 2)
            {
                pix.SetAndMoveNext(
                    (raw0.Value << 1),
                    ((raw0.GetRel(-1) + raw0.GetRel(+1) + rawU.GetRel(0) << 1) >> 1),
                    ((raw0.GetRel(-1) + raw0.GetRel(+1))),
                maxValue);

                pix.SetAndMoveNext(
                    ((raw0.Value + raw0.GetRel(+2))),
                    (raw0.GetRel(+1) << 1),
                    (rawU.GetRel(+1) << 1),
                maxValue);
                raw0.MoveNext(2);
                rawU.MoveNext(2);
            }

            // Bottom right pixel
            pix.SetAndMoveNext(
                (raw0.GetRel(-1) << 1),
                ((rawU.GetRel(-1) + raw0.GetRel(-2))),
                (rawU.GetRel(-2) << 1),
                maxValue);
        }

    }
}