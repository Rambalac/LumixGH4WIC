using Azi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace com.azi.Image
{
    [StructLayout(LayoutKind.Explicit)]
    public struct BGRA8
    {
        [FieldOffset(0)]
        public byte B;

        [FieldOffset(1)]
        public byte G;

        [FieldOffset(2)]
        public byte R;

        [FieldOffset(3)]
        public byte A;

        [FieldOffset(0)]
        public uint RGBA;

        public BGRA8(byte B, byte G, byte R) : this(B, G, R, 255)
        {
        }

        public BGRA8(byte B, byte G, byte R, byte A)
        {
            RGBA = 0;
            this.B = B;
            this.G = G;
            this.R = R;
            this.A = A;
        }
    }

    public class ColorMap<T> : IColorMap
    {
        public readonly T[][] Values;
        public readonly int stride;
        readonly int height;
        readonly int width;
        readonly int bits;

        public ColorMap(int width, int height, int bits = 0, int? stride = null)
        {
            this.bits = bits;
            this.stride = stride ?? width;
            this.height = height;
            this.width = width;
            Values = Arrays.CreateRepeat(height, () => ArraysReuseManager.ReuseOrGetNew<T>(Stride));
        }

        public int BytesPerPixel => Marshal.SizeOf(typeof(T));

        public int Width => width;

        public int Height => height;

        public int Stride => stride;

        public int Bits => bits;

        public Pixel<T> GetRow(int y) => new Pixel<T>(this, 0, y);

        public IEnumerable<Pixel<T>> GetRows()
        {
            for (int y = 0; y < height; y++)
                yield return new Pixel<T>(this, 0, y);
        }

        public void ForEachPixel(Action<Pixel<T>> action)
        {

            foreach (var pix in GetRows())
                do
                {
                    action(pix);
                } while (pix.MoveNextAndCheck());
        }

        public void SetPixel(int x, int y, T val)
        {
            Values[y][x] = val;
        }

        public void Dispose()
        {
            for (int y = 0; y < height; y++) Values[y].Release();
        }
    }

    public static class ColorMapExtensions
    {
        public static Histogram GetHistogram(this ColorMap<Vector3> map, int maxIndex)
        {
            var result = new Histogram(maxIndex);

            map.ForEachPixel(b =>
            {
                result.AddValue((int)(maxIndex * b.Value.X), 0);
                result.AddValue((int)(maxIndex * b.Value.Y), 1);
                result.AddValue((int)(maxIndex * b.Value.Z), 2);
            });
            return result;
        }
    }
}
