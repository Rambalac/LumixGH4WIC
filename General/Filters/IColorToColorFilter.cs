using System;
using com.azi.Image;
using com.azi.Filters;

namespace com.azi.Filters
{
    public interface IPixelFilter : IFilter
    {
    }

    public interface IPixelToPixelFilter<TA, TB> : IPixelFilter
    {
        void ProcessPixel(ref TA input, ref TB output);
    }

    public abstract class IndependentComponentPixelToPixelFilter<TA, TB> : PixelToPixelFilter<TA, TB>, IIndependentComponentFilter
    {
        public object CreateOutputCurve(int length)
        {
            return new TB[length];
        }

        public object CreateInputCurve(int length)
        {
            return new TA[length];
        }

        public void ProcessCurve(object incurve, object outcurve)
        {
            var inc = (TA[])incurve;
            var outc = (TB[])outcurve;
            for (int i = 0; i < inc.Length; i++)
                ProcessPixel(ref inc[i], ref outc[i]);
        }
    }

    public abstract class PixelToPixelFilter<TA, TB> : Filter<TA, TB>, IPixelToPixelFilter<TA, TB>
    {
        public abstract void ProcessPixel(ref TA input, ref TB output);
        public override void ProcessMap(ColorMap<TA> inmap, ColorMap<TB> outmap)
        {
            var maplines = inmap.GetRows().GetEnumerator();
            var reslines = outmap.GetRows().GetEnumerator();
            while (maplines.MoveNext() && reslines.MoveNext())
            {
                var mapline = maplines.Current;
                var resline = reslines.Current;
                do
                {
                    ProcessPixel(ref mapline.line[mapline.index], ref resline.line[resline.index]);
                } while (mapline.MoveNextAndCheck() && resline.MoveNextAndCheck());
            }
        }
    }
}