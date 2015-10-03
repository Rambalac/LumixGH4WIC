using System.Runtime.CompilerServices;
using com.azi.Image;
using System.Numerics;

namespace com.azi.Filters.VectorMapFilters
{
    public class WhiteBalanceFilterAutoAdjuster : AFilterAutoAdjuster<ColorMap<Vector3>, WhiteBalanceFilter>
    {
        public override void AutoAdjust(WhiteBalanceFilter filter, ColorMap<Vector3> map)
        {
            double maxbright = 0;
            Vector3 whiteColor = Vector3.One;
            map.ForEachPixel(color =>
            {
                var bright = color.Value.LengthSquared();
                if (bright < maxbright || color.Value.MaxComponent() >= 1f) return;

                maxbright = bright;
                whiteColor = color.Value;
            });
            var maxComp = whiteColor.MaxComponent();
            filter.WhiteColor = whiteColor / maxComp;
        }

    }

    public class WhiteBalanceFilter : IndependentComponentPixelToPixelFilter<Vector3, Vector3>, IAutoAdjustableFilter
    {
        Vector3 _whiteColor = Vector3.One;
        Vector3 _whiteColor1 = Vector3.One;
        bool isAdjusted = false;

        public bool IsAdjusted
        {
            get { return isAdjusted; }
        }

        public Vector3 WhiteColor
        {
            get { return _whiteColor; }
            set
            {
                _whiteColor = value;
                _whiteColor1 = Vector3.One / _whiteColor;
                isAdjusted = true;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void ProcessPixel(ref Vector3 input, ref Vector3 output)
        {
            output = input * _whiteColor1;
        }
    }
}