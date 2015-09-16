using System;

namespace com.azi.Image
{
    public class RGB8Map : IColorMap
    {
        const int BytesPerPixel = 3;

        public readonly byte[] Rgb;
        public readonly int Stride;
        readonly int _height;
        readonly int _width;

        public RGB8Map(int w, int h)
            : this(w, h, (4 * (w * 3 + 3) / 4), null)
        {
            Rgb = ArraysReuseManager.ReuseOrGetNew<byte>(Stride * h);
            //for (int i = 0; i < Rgb.Length; i++) Rgb[i] = 255;
        }

        RGB8Map(int width, int height, int stride, byte[] rgb)
        {
            _width = width;
            _height = height;
            Stride = stride;
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

        public ColorPixel<byte> GetRow(int y)
        {
            return new ColorPixel<byte>(Rgb, y * Stride, y * Stride + _width * 3);
        }

        public void ForEachPixel(Action<ColorPixel<byte>> action)
        {
            for (var y = 0; y < _height; y++)
            {
                var pix = GetRow(y);

                do
                {
                    action(pix);
                } while (pix.MoveNextAndCheck());
            }
        }

        internal void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            var ind = Stride * y + x * 3;
            Rgb[ind + 0] = r;
            Rgb[ind + 1] = g;
            Rgb[ind + 2] = b;
        }

        public void ForEachPixel(Action<int, byte> action)
        {
            for (var y = 0; y < _height; y++)
            {
                var pix = GetRow(y);

                do
                {
                    action(0, pix[0]);
                    action(1, pix[1]);
                    action(2, pix[2]);
                } while (pix.MoveNextAndCheck());
            }
        }

        public Histogram GetHistogram()
        {
            var result = new Histogram(255);

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

        public ColorPixel<byte> GetPixel(int x, int y)
        {
            return new ColorPixel<byte>(Rgb, y * Stride + x * 3, y * Stride + (_width - x) * 3);
        }
    }
}