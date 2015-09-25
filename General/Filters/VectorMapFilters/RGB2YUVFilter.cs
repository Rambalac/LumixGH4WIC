using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace com.azi.Filters.VectorMapFilters
{
    class RGB2YUVFilter : ColorMatrixFilter
    {
        override public Matrix4x4 Matrix
        {
            get
            {
                return new Matrix4x4(   0.299f,   0.587f,    0.114f, 0,
                                       -0.14713f, 0.28886f,  0.436f, 0,
                                        0.615f,  -0.51499f, -0.10001f, 0,
                                        0, 0, 0, 1);
            }
        }
    }
    class YUV2RGBFilter : ColorMatrixFilter
    {
        override public Matrix4x4 Matrix
        {
            get
            {
                return new Matrix4x4(   1f,  0f,        1.13983f, 0,
                                        1f, -0.39456f,  0.58060f, 0,
                                        1f,  2.03211f,  0, 0,
                                        0, 0, 0, 1);
            }
        }
    }
}
