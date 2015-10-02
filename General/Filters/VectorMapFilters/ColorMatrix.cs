using System.Numerics;

namespace com.azi.Filters.VectorMapFilters
{
    public class ColorMatrixFilter : PixelToPixelFilter<Vector3, Vector3>
    {
        Matrix4x4 _matrix = Matrix4x4.Identity;

        virtual public Matrix4x4 Matrix
        {
            get { return _matrix; }
            set
            {
                _matrix = value;
            }
        }

        public override void ProcessPixel(ref Vector3 input, ref Vector3 output)
        {
            output = Vector3.Transform(input, Matrix);
        }
    }
}