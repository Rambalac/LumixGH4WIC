using System;
using System.Threading.Tasks;

namespace com.azi.Image
{
    public class UshortColorMap : IColorMap
    {
        public delegate void ColorMapProcessor(int x, int y, ColorPixel<ushort> input, ColorPixel<ushort> output);

        public delegate ushort CurveProcessor(int component, int index, ushort input);

        public readonly ushort[] Rgb;
        private readonly int _height;
        private readonly int _width;

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public ColorPixel<ushort> GetPixel(int x = 0, int y = 0)
        {
            return new ColorPixel<ushort>(Rgb, (y * Width + x) * 3, Width * Height * 3);
        }

        public ColorPixel<ushort> GetRow(int y)
        {
            return new ColorPixel<ushort>(Rgb, y * Width * 3, Width * 3);
        }

        public UshortColorMap CopyAndUpdateColors(int newMaxBits, ColorMapProcessor processor)
        {
            var result = new UshortColorMap(_width, _height, newMaxBits, new ushort[_width * _height * 3]);
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

        public void ForEachPixel(Action<ColorPixel<ushort>> action)
        {
            var pix = GetPixel();
            do
            {
                action(pix);
            } while (pix.MoveNextAndCheck());
        }

        public void ForEachPixel(Action<int, ushort> action)
        {
            var pix = GetPixel();
            do
            {
                action(0, pix[0]);
                action(1, pix[1]);
                action(2, pix[2]);
            } while (pix.MoveNextAndCheck());
        }

        public readonly int MaxBits;

        public UshortColorMap(int w, int h, int maxBits)
            : this(w, h, maxBits, ArraysReuseManager.ReuseOrGetNew<ushort>(w * h * 3))
        {

        }

        public UshortColorMap(int w, int h, int maxBits, ushort[] rgb)
        {
            _width = w;
            _height = h;
            Rgb = rgb;

            MaxBits = maxBits;
        }

        public UshortColorMap(UshortColorMap m, ushort[] rgb)
            : this(m.Width, m.Height, m.MaxBits, rgb)
        {
        }

        public int MaxValue
        {
            get { return (1 << MaxBits) - 1; }
        }

        public Histogram GetHistogram()
        {
            var result = new Histogram(MaxValue);

            ForEachPixel((comp, b) => result.AddValue(comp, b));
            return result;
        }

        protected virtual void Dispose(Boolean notnative)
        {
            ArraysReuseManager.Release(Rgb);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static explicit operator VectorMap(UshortColorMap map)
        {
            var result = new VectorMap(map.Width, map.Height);
            var mult = (float)map.MaxValue;
            Parallel.For(0, map.Height, y =>
            {
                var input = map.GetRow(y);
                var output = result.GetRow(y);
                for (var x = 0; x < map.Width; x++)
                {
                    output.SetAndMoveNext(input.R / mult, input.G / mult, input.B / mult);
                    input.MoveNext();
                }
            });
            return result;
        }
    }
}