using System;
using System.Numerics;
using static com.azi.Image.Vector3Extensions;

namespace com.azi.Filters.VectorMapFilters
{
    public class GammaFilter : VectorToVectorFilter, IIndependentComponentFilter
    {
        private Vector3 _gamma = new Vector3(2.2f, 2.2f, 2.2f);
        private Vector3 _gamma1 = new Vector3(1 / 2.2f, 1 / 2.2f, 1 / 2.2f);

        public GammaFilter()
        {
        }

        public GammaFilter(float gamma)
        {
            Gamma = new Vector3(gamma, gamma, gamma);
        }

        public Vector3 Gamma
        {
            get { return _gamma; }
            set
            {
                _gamma = value;
                _gamma1 = Vector3.Divide(Vector3.One, _gamma);
            }
        }

        public override void ProcessVector(ref Vector3 input, ref Vector3 output)
        {
            output = Pow(input, _gamma1);
        }
    }
}