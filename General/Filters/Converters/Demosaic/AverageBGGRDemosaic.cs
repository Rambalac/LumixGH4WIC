using System;
using System.Threading.Tasks;
using com.azi.Image;

namespace com.azi.Filters.Converters.Demosaic
{
    public class AverageBGGRDemosaic : IBGGRDemosaic
    {

        // B G B G
        // G R G R
        // B G B G
        // G R G R
        public UshortColorMap Process(RawBGGRMap file)
        {
            if (file.Width % 2 != 0 || file.Height % 2 != 0) throw new ArgumentException("Width and Height should be even");
            var res = new UshortColorMap(file.Width, file.Height, file.MaxBits + 1);

            ProcessTopLine(file, res);

            ProcessMiddleRows(file, res);

            ProcessBottomLine(file, res);

            return res;
        }

        static void ProcessMiddleRows(RawBGGRMap file, UshortColorMap res)
        {
            // Middle Rows
            Parallel.For(0, (res.Height - 2) / 2, yy =>
            {
                var y = yy * 2 + 1;
                ProcessMiddleOddRows(file.GetRow(y), res.Width, res.GetRow(y));

                y++;

                ProcessMiddleEvenRows(file.GetRow(y), res.Width, res.GetRow(y));
            });
        }

        static void ProcessMiddleEvenRows(RawPixel raw, int Width, ColorPixel<ushort> pix)
        {
            // Second left pixel
            pix.SetAndMoveNext(
                (ushort)((raw.GetRel(1, -1) + raw.GetRel(1, +1))),
                (ushort)((raw.GetRel(0, -1) + raw.GetRel(0, +1) + (raw.GetRel(1, 0) << 1)) >> 1),
                (ushort)(raw.Value << 1));
            raw.MoveNext();

            var lastX = Width - 1;
            for (var x = 1; x < lastX; x += 2)
            {
                var xy = raw.Value;
                var x1y = raw.GetRel(+1, 0);
                var xy12 = raw.GetRel(0, -1) + raw.GetRel(0, +1);

                pix.SetAndMoveNext(
                    (ushort)(xy12),
                    (ushort)(xy << 1),
                    (ushort)((raw.GetRel(-1, 0) + x1y)));

                pix.SetAndMoveNext(
                    (ushort)((xy12 + raw.GetRel(+2, -1) + raw.GetRel(+2, +1)) >> 1),
                    (ushort)
                        ((xy + raw.GetRel(+2, 0) + raw.GetRel(+1, -1) + raw.GetRel(+1, +1)) >> 1),
                    (ushort)(x1y << 1));
                raw.MoveNext();
                raw.MoveNext();
            }

            // Second right pixel
            pix.SetAndMoveNext(
                (ushort)((raw.GetRel(0, -1) + raw.GetRel(0, +1))),
                (ushort)(raw.Value << 1),
                (ushort)(raw.GetRel(-1, 0) << 1));
            raw.MoveNext();
        }

        static void ProcessMiddleOddRows(RawPixel raw, int Width, ColorPixel<ushort> pix)
        {
            // First left pixel
            pix.SetAndMoveNext(
                (ushort)(raw.GetRel(1, 0) << 1),
                (ushort)(raw.Value << 1),
                (ushort)((raw.GetRel(0, -1) + raw.GetRel(0, +1))));
            raw.MoveNext();

            var lastX = Width - 1;
            for (var x = 1; x < lastX; x += 2)
            {
                var xy = raw.Value;
                var x1y = raw.GetRel(+1, 0);
                var x11y12 = raw.GetRel(+1, -1) + raw.GetRel(+1, +1);

                pix.SetAndMoveNext(
                    (ushort)(xy << 1),
                    (ushort)((raw.GetRel(-1, 0) + x1y + raw.GetRel(0, -1) + raw.GetRel(0, +1)) >> 1),
                    (ushort)((raw.GetRel(-1, -1) + x11y12 + raw.GetRel(-1, +1)) >> 1));

                pix.SetAndMoveNext(
                    (ushort)((xy + raw.GetRel(+2, 0))),
                    (ushort)(x1y << 1),
                    (ushort)(x11y12));
                raw.MoveNext();
                raw.MoveNext();
            }

            // First right pixel
            pix.SetAndMoveNext(
                (ushort)(raw.Value << 1),
                (ushort)((raw.GetRel(0, -1) + raw.GetRel(0, +1) + (raw.GetRel(-1, 0) << 1)) >> 1),
                (ushort)((raw.GetRel(-1, -1) + raw.GetRel(-1, +1))));
            raw.MoveNext();
        }

        static void ProcessTopLine(RawBGGRMap map, UshortColorMap res)
        {
            var pix = res.GetPixel();
            var raw = map.GetRow(0);
            // Top Left pixel

            pix.SetAndMoveNext(
                (ushort)(raw.GetRel(1, 1) << 1),
                (ushort)((raw.GetRel(1, 0) + raw.GetRel(0, 1))),
                (ushort)(raw.Value << 1));
            raw.MoveNext();

            // Top row
            for (var x = 1; x < res.Width - 1; x += 2)
            {
                pix.SetAndMoveNext(
                    (ushort)(raw.GetRel(0, 1) << 1),
                    (ushort)(raw.Value << 1),
                    (ushort)((raw.GetRel(-1, 0) + raw.GetRel(+1, 0))));

                pix.SetAndMoveNext(
                    (ushort)((raw.GetRel(0, 1) + raw.GetRel(+2, 1))),
                    (ushort)((raw.Value + raw.GetRel(+2, 0) + (raw.GetRel(+1, 1) << 1)) >> 1),
                    (ushort)(raw.GetRel(+1, 0) << 1));
                raw.MoveNext();
                raw.MoveNext();
            }

            // Top right pixel
            pix.SetAndMoveNext(
                (ushort)(raw.GetRel(res.Width - 1, 1) << 1),
                (ushort)(raw.GetRel(res.Width - 1, 0) << 1),
                (ushort)(raw.GetRel(res.Width - 2, 0) << 1));
            raw.MoveNext();
        }

        // B G B G
        // G R G R
        // B G B G
        // G R G R
        static void ProcessBottomLine(RawBGGRMap map, UshortColorMap res)
        {
            var pix = res.GetPixel();
            var raw = map.GetRow(res.Height - 1);

            // Bottom Left pixel
            var lastY = res.Height - 1;

            pix.SetAndMoveNext(
                (ushort)(raw.GetRel(1, 0) << 1),
                (ushort)(raw.Value << 1),
                (ushort)(raw.GetRel(0, -1) << 1));
            raw.MoveNext();

            // Bottom row
            for (var x = 1; x < res.Width - 1; x += 2)
            {
                pix.SetAndMoveNext(
                    (ushort)(raw.Value << 1),
                    (ushort)((raw.GetRel(-1, 0) + raw.GetRel(+1, 0) + raw.GetRel(0, -1) << 1) >> 1),
                    (ushort)((raw.GetRel(-1, 0) + raw.GetRel(+1, 0))));

                pix.SetAndMoveNext(
                    (ushort)((raw.Value + raw.GetRel(+2, 0))),
                    (ushort)(raw.GetRel(+1, 0) << 1),
                    (ushort)(raw.GetRel(+1, 0 - 1) << 1));

                raw.MoveNext();
                raw.MoveNext();
            }

            // Bottom right pixel
            pix.SetAndMoveNext(
                (ushort)(raw.GetRel(-1, 0) << 1),
                (ushort)((raw.GetRel(-1, -1) + raw.GetRel(-2, 0))),
                (ushort)(raw.GetRel(-2, -1) << 1));
            raw.MoveNext();
        }
    }
}