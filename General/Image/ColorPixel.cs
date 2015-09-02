using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace com.azi.Image
{
    public class ColorPixel<T>
    {
        public readonly T[] Map;
        protected readonly int _limit;
        public int Offset;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ColorPixel(T[] map, int offset, int limit)
        {
            Map = map;
            Offset = offset;
            _limit = limit;
        }

        public T R
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get { return Map[Offset + 0]; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set { Map[Offset + 0] = value; }
        }

        public T G
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get { return Map[Offset + 1]; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set { Map[Offset + 1] = value; }
        }

        public T B
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get { return Map[Offset + 2]; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set { Map[Offset + 2] = value; }
        }

        public T this[int i]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                //if (i > 2) throw new ArgumentException("Should be less than 3");
                return Map[Offset + i];
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                //if (i > 2) throw new ArgumentException("Should be less than 3");
                Map[Offset + i] = value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] GetCopy()
        {
            return new[] { R, G, B };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MoveNext()
        {
            Offset += 3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNextAndCheck()
        {
            Offset += 3;
            return Offset < _limit;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetAndMoveNext(T r, T g, T b)
        {
            Map[Offset + 0] = r;
            Map[Offset + 1] = g;
            Map[Offset + 2] = b;
            MoveNext();
        }
    }

    public static class ColorExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort MaxComponent(this ColorPixel<ushort> c)
        {
            return (c.R > c.G)
                ? (c.R > c.B) ? c.R : c.B
                : (c.G > c.B) ? c.G : c.B;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Brightness(this ColorPixel<ushort> c)
        {
            return c.R * c.R + c.G * c.G + c.B * c.B;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BrightnessSqrt(this ColorPixel<ushort> c)
        {
            return Math.Sqrt(c.Brightness());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Brightness(this ushort[] c)
        {
            return c[0] * c[0] + c[1] * c[1] + c[2] * c[2];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BrightnessSqrt(this ushort[] c)
        {
            return Math.Sqrt(c.Brightness());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Brightness(this double[] c)
        {
            return c[0] * c[0] + c[1] * c[1] + c[2] * c[2];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BrightnessSqrt(this double[] c)
        {
            return Math.Sqrt(c.Brightness());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float[] ToFloat(this ushort[] c, int bits)
        {
            var maxValue = (float)((1 << bits) - 1);
            return c.Select(v => v / maxValue).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort[] Normalize(this ushort[] color, int maxBits)
        {
            var colorNorma = color.BrightnessSqrt();

            var d = (double)((1 << maxBits) - 1);
            return color.Select(v => (ushort)(v * d / colorNorma)).ToArray();
        }
    }
}