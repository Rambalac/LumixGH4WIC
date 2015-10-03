using com.azi.Image;
using System.Numerics;

namespace com.azi.Filters.Converters
{
    public class VectorBGRACompressorFilter : IndependentComponentPixelToPixelFilter<Vector3, BGRA8>
    {
        public override void ProcessPixel(ref Vector3 input, ref BGRA8 output)
        {
            var v = Vector3.Clamp(input, Vector3.Zero, Vector3.One);
            output.B = (byte)(v.Z * 255);
            output.G = (byte)(v.Y * 255);
            output.R = (byte)(v.X * 255);
            output.A = 255;
        }
    }
}