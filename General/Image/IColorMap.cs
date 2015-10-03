using System;
using System.Collections.Generic;

namespace com.azi.Image
{
    public interface IColorMap : IDisposable
    {
        int Height { get; }
        int Width { get; }
        int Stride { get; }
        int Bits { get; }
    }
}