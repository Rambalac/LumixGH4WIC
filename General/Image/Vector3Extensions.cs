using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace com.azi.Image
{
    public static class Vector3Extensions
    {
        static double PowA(double a, double b)
        {
            var tmp = (int)(BitConverter.DoubleToInt64Bits(a) >> 32);
            var tmp2 = (int)(b * (tmp - 1072632447) + 1072632447);
            return BitConverter.Int64BitsToDouble(((long)tmp2) << 32);
        }
        public static Vector3 Pow(Vector3 a, Vector3 b) => new Vector3((float)Math.Pow(a.X, b.X),
                                                                       (float)Math.Pow(a.Y, b.Y),
                                                                       (float)Math.Pow(a.Z, b.Z));

        public static Vector3 Log(Vector3 a, Vector3 b) => new Vector3((float)Math.Log(a.X, b.X),
                                                                       (float)Math.Log(a.Y, b.Y),
                                                                       (float)Math.Log(a.Z, b.Z));

        public static Vector3 Average(this Vector3 a) => new Vector3((a.X + a.Y + a.Z) / 3);

        public static float MaxComponent(this Vector3 a) => Math.Max(a.X, Math.Max(a.Y, a.Z));
        public static float MinComponent(this Vector3 a) => Math.Min(a.X, Math.Min(a.Y, a.Z));

        public static Vector3 ToVector3(this float[] color) => new Vector3(color[0], color[1], color[2]);

        public static float[,] Shift(this float[,] arr, int row, int index, float shift)
        {
            var othershift = -shift / (arr.GetUpperBound(1));
            for (int i = 0; i < arr.GetUpperBound(1) + 1; i++) arr[row, i] += ((i == index) ? shift : othershift);
            return arr;
        }

        public static Matrix4x4 ToMatrix4x4(this float[,] matrix) => new Matrix4x4(
            matrix[0, 0], matrix[1, 0], matrix[2, 0], 0,
            matrix[0, 1], matrix[1, 1], matrix[2, 1], 0,
            matrix[0, 2], matrix[1, 2], matrix[2, 2], 0,
            0, 0, 0, 0);

    }
}
