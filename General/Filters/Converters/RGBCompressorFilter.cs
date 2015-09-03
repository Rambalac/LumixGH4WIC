using System;
using System.Numerics;

namespace com.azi.Filters.Converters
{
    public class VectorRGBCompressorFilter : VectorToColorFilter<byte>, IIndependentComponentFilter
    {
        public override void ProcessVector(ref Vector3 input, ref byte outR, ref byte outG, ref byte outB)
        {
            var v = Vector3.Clamp(input, Vector3.Zero, Vector3.One);
            outR = (byte)(v.X * 255);
            outG = (byte)(v.Y * 255);
            outB = (byte)(v.Z * 255);
        }
    }
    public class VectorBGRCompressorFilter : VectorToColorFilter<byte>, IIndependentComponentFilter
    {
        public override void ProcessVector(ref Vector3 input, ref byte outR, ref byte outG, ref byte outB)
        {
            var v = Vector3.Clamp(input, Vector3.Zero, Vector3.One);
            outB = (byte)(v.X * 255);
            outG = (byte)(v.Y * 255);
            outR = (byte)(v.Z * 255);
        }
    }
}