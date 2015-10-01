using System.Numerics;
using com.azi.Image;
using System.Runtime.CompilerServices;

namespace com.azi.Filters.VectorMapFilters
{
    public class SaturationFilter : VectorToVectorFilter
    {
        float _saturation = 1;

        public SaturationFilter(float sat)
        {
            _saturation = sat;
        }

        public float Saturation
        {
            set
            {
                _saturation = value;
            }
            get { return _saturation; }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void ProcessVector(ref Vector3 input, ref Vector3 output)
        {
            var chroma = input.Intensity();
            output = chroma + ((input - chroma) * _saturation);
        }
    }
}