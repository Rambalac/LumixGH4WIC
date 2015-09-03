using System;
using System.Collections.Generic;
using System.Numerics;

namespace com.azi.Image
{
    public class VectorMap : IColorMap
    {
        public delegate void VectorMapProcessor(int x, int y, VectorPixel input, VectorPixel output);

        public delegate Vector3 CurveProcessor(int index, Vector3 input);

        public readonly Vector3[] Rgb;
        private readonly int _height;
        private readonly int _width;

        protected virtual void Dispose(Boolean notnative)
        {
            ArraysReuseManager.Release(Rgb);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public VectorMap(int w, int h)
            : this(w, h, ArraysReuseManager.ReuseOrGetNew<Vector3>(w * h))
        {
        }

        public VectorMap(int w, int h, Vector3[] rgb)
        {
            _width = w;
            _height = h;
            Rgb = rgb;
        }

        public VectorMap(IColorMap m, Vector3[] rgb)
        {
            _width = m.Width;
            _height = m.Height;
            Rgb = rgb;
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public VectorPixel GetPixel(int x = 0, int y = 0)
        {
            return new VectorPixel(this, x, y, Width * Height);
        }

        public VectorPixel GetRow(int y)
        {
            return new VectorPixel(this, 0, y, Width);
        }

        public VectorMap CopyAndUpdateColors(int newMaxBits, VectorMapProcessor processor)
        {
            var result = new VectorMap(_width, _height);
            var input = GetPixel();
            var output = result.GetPixel();
            for (var y = 0; y < _height; y++)
                for (var x = 0; x < _width; x++)
                {
                    processor(x, y, input, output);
                    input.MoveNext();
                    output.MoveNext();
                }
            return result;
        }

        public void ForEachPixel(Action action)
        {
            var pix = GetPixel();
            do
            {
                action();
            } while (pix.MoveNextAndCheck());
        }

        public void ForEachPixel(Action<VectorPixel> action)
        {
            var pix = GetPixel();
            do
            {
                action(pix);
            } while (pix.MoveNextAndCheck());
        }

        public Histogram GetHistogram(int maxIndex)
        {
            var result = new Histogram(maxIndex);

            ForEachPixel(b =>
            {
                result.AddValue(0, (int)(maxIndex * b.Value.X));
                result.AddValue(1, (int)(maxIndex * b.Value.Y));
                result.AddValue(2, (int)(maxIndex * b.Value.Z));
            });
            return result;
        }

    }
}