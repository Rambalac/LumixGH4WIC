using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using com.azi.Image;
using System.Numerics;
using static com.azi.Image.Vector3Extensions;

namespace com.azi.Filters.VectorMapFilters
{
    public class LightFilterAutoAdjuster : AFilterAutoAdjuster<VectorMap, LightFilter>
    {
        public override void AutoAdjust(LightFilter f, VectorMap map)
        {
            const int maxValue = 1023;
            var h = map.GetHistogram(maxValue);

            var wcenter = h.FindWeightCenter(Vector3.Zero, Vector3.One);
            var wcenterf = (wcenter - f.MinIn) / (f.MaxIn - f.MinIn);
            var contrast = Log(new Vector3(0.55f), wcenterf);

            //f.Contrast = f.Contrast.Average();

            //            h.Transform((index, value, comp) => (int)(1023 * Math.Pow(index / 1023f, _contrast[comp])));

            Vector3 max;
            Vector3 min;
            h.FindMinMax(out min, out max, 0.005f, 0.001f);

            //min = new Vector3(min.MinComponent());
            //max = new Vector3(max.MaxComponent());

            f.Set(min, max, Vector3.Zero, Vector3.One, contrast);
        }
    }
    public class LightFilter : VectorToVectorFilter, IIndependentComponentFilter
    {
        private Vector3 _contrast = Vector3.One;
        private Vector3 _inoutLen = Vector3.One;
        private Vector3 _maxIn = Vector3.One;
        private Vector3 _maxOut = Vector3.One;
        private Vector3 _minIn = Vector3.Zero;
        private Vector3 _minOut = Vector3.Zero;

        public Vector3 Contrast
        {
            get { return _contrast; }
            set
            {
                _contrast = value;
            }
        }

        public Vector3 MinIn
        {
            get { return _minIn; }
            set
            {
                _minIn = value;
                Recalculate();
            }
        }

        public Vector3 MaxIn
        {
            get { return _maxIn; }
            set
            {
                _maxIn = value;
                Recalculate();
            }
        }

        public Vector3 MinOut
        {
            get { return _minOut; }
            set
            {
                _minOut = value;
                Recalculate();
            }
        }

        public Vector3 MaxOut
        {
            get { return _maxOut; }
            set
            {
                _maxOut = value;
                Recalculate();
            }
        }

        public void SetContrast(float value)
        {
            _contrast = new Vector3(value, value, value);
        }

        private void Recalculate()
        {
            _inoutLen = (_maxOut - _minOut) / (_maxIn - _minIn);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void ProcessVector(ref Vector3 input, ref Vector3 output)
        {
            var f = Vector3.Max(Vector3.Zero, ((input - _minIn) * _inoutLen + _minOut));
            output = Pow(f, _contrast);
        }

        internal void Set(Vector3 minIn, Vector3 maxIn, Vector3 minOut, Vector3 maxOut, Vector3 contrast)
        {
            _minIn = minIn;
            _maxIn = maxIn;
            _minOut = minOut;
            _maxOut = maxOut;
            _contrast = contrast;
            Recalculate();
        }
    }
}