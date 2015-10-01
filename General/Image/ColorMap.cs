using Azi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace com.azi.Image
{
    [StructLayout(LayoutKind.Explicit)]
    struct BGRA8
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
    }

    [StructLayout(LayoutKind.Explicit)]
    struct BGRA16
    {
        [FieldOffset(0)]
        public ushort B;

        [FieldOffset(2)]
        public ushort G;

        [FieldOffset(4)]
        public ushort R;

        [FieldOffset(6)]
        public ushort A;

        [FieldOffset(0)]
        public ulong RGBA;
    }

    class ColorMap<T> : IColorMap
    {
        int BytesPerPixel => Marshal.SizeOf(typeof(T));

        public readonly T[][] Values;
        public readonly int Stride;
        readonly int height;
        readonly int width;

        public ColorMap(int width, int height, int? stride = null)
        {
            Stride = stride ?? width;
            Values = Arrays.CreateRepeat(height, () => ArraysReuseManager.ReuseOrGetNew<T>(Stride));
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public void Dispose()
        {
            for (int y = 0; y < height; y++) Values[y].Release();
        }
    }
}
