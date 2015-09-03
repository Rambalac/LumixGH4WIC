using System.Numerics;
using com.azi.Image;
using System.Runtime.CompilerServices;
using System;

namespace com.azi.Filters.VectorMapFilters
{
    public class SaturationFilter : VectorToVectorFilter
    {
        private float _saturation = 1;

        public float Saturation
        {
            set { _saturation = value;
            }
            get { return _saturation; }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void ProcessVector(ref Vector3 input, ref Vector3 output)
        {
            var chroma = input.Average();
            output = chroma + (input - chroma) * _saturation;
        }
    }
}