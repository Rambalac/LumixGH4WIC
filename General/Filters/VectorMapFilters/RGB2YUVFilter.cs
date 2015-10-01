using System.Numerics;

namespace com.azi.Filters.VectorMapFilters
{
    public class RGB2YUVFilter : ColorMatrixFilter
    {
        override public Matrix4x4 Matrix
        {
            get
            {
                return Matrix4x4.Transpose(new Matrix4x4(0.299f, 0.587f, 0.114f, 0,
                                       -0.14713f, -0.28886f, 0.436f, 0,
                                        0.615f, -0.51499f, -0.10001f, 0,
                                        0, 0, 0, 1));
            }
        }
    }
    public class YUV2RGBFilter : ColorMatrixFilter
    {
        override public Matrix4x4 Matrix
        {
            get
            {
                return Matrix4x4.Transpose(new Matrix4x4(1f, 0f, 1.13983f, 0,   // 0.299R+0.587G+0.114B+1.13983*(0.615R-0.51499G-0.10001B)
                                        1f, -0.39456f, 0.58060f, 0,             //0.299R+0.587G+0.114B+
                                        1f, 2.03211f, 0, 0,                     //0.299R+0.587G+0.114B+2.03211*(-0.14713R+0.28886G+0.436B)
                                        0, 0, 0, 1));
            }
        }
    }
}
