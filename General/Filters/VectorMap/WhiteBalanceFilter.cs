using System;
using System.Linq;
using System.Runtime.CompilerServices;
using com.azi.Image;
using System.Numerics;

namespace com.azi.Filters.VectorMapFilters
{
    public class WhiteBalanceFilterAutoAdjuster : AFilterAutoAdjuster<VectorMap, WhiteBalanceFilter>
    {
        public override void AutoAdjust(WhiteBalanceFilter f, VectorMap map)
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
            f.WhiteColor = whiteColor / maxComp;
        }

    }

    public class WhiteBalanceFilter : VectorToVectorFilter, IIndependentComponentFilter, IAutoAdjustableFilter
    {
        private Vector3 _whiteColor = Vector3.One;
        private Vector3 _whiteColor1 = Vector3.One;
        private bool isAdjusted = false;

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
        public override void ProcessVector(ref Vector3 input, ref Vector3 output)
        {
            output = input * _whiteColor1;
        }
    }
}