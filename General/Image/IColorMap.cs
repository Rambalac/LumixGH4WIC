using System;

namespace com.azi.Image
{
    public interface IColorMap : IDisposable
    {
        int Width { get; }
        int Height { get; }
    }
}