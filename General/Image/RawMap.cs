using Azi.Helpers;
using System;

namespace com.azi.Image
{
    public class RawBGGRMap : RawMap
    {
        public RawBGGRMap(int w, int h, int maxBits)
            : base(w, h, maxBits)
        {
        }
    }

    public class RawMap : IColorMap
    {
        public readonly int MaxBits;
        public readonly ushort[][] Raw;
        readonly int _height;
        readonly int _width;

        public RawMap(int w, int h, int maxBits)
        {
            _width = w;
            _height = h;
            MaxBits = maxBits;
            Raw = Arrays.CreateRepeat(h, () => new ushort[w]);
        }

        public int MaxValue
        {
            get { return (1 << MaxBits) - 1; }
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public RawPixel GetPixel(int x, int y)
        {
            return new RawPixel(this, x, y);
        }

        public RawPixel GetRow(int y)
        {
            return new RawPixel(this, 0, y, Width);
        }

        protected virtual void Dispose(Boolean notnative)
        {
            Raw.Release();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}