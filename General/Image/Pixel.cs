using System.Numerics;
using System.Runtime.CompilerServices;

namespace com.azi.Image
{
    public class Pixel<T>
    {
        internal readonly ColorMap<T> map;
        internal readonly T[] line;
        internal int index;


        public Pixel(ColorMap<T> map, int x, int y)
        {
            this.map = map;
            line = map.Values[y];

            Index = x;
        }

        public Pixel(ColorMap<T> map) : this(map, 0, 0)
        {
        }

        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get { return line[Index]; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set { line[Index] = value; }
        }

        internal ColorMap<T> Map
        {
            get
            {
                return map;
            }
        }

        internal int Index
        {
            get
            {
                return index;
            }

            set
            {
                index = value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MoveNext()
        {
            Index++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MoveNext(int step)
        {
            Index += step;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNextAndCheck()
        {
            Index++;
            return Index < line.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetAndMoveNext(T val)
        {
            line[Index] = val;
            MoveNext();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetRel(int x)
        {
            return line[Index + x];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetRel(int x, T val)
        {
            line[Index + x] = val;
        }
    }

    public static class PixelExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAndMoveNext(this Pixel<Vector3> pix, int R, int G, int B, float MaxValue)
        {
            pix.line[pix.Index].X = R / MaxValue;
            pix.line[pix.Index].Y = G / MaxValue;
            pix.line[pix.Index].Z = B / MaxValue;
            pix.Index++;
        }
        public static void SetAndMoveNext(this Pixel<BGRA8> pix, byte R, byte G, byte B)
        {
            pix.line[pix.Index].B = B;
            pix.line[pix.Index].G = G;
            pix.line[pix.Index].R = R;
            pix.line[pix.Index].A = 255;
            pix.Index++;
        }

    }
}