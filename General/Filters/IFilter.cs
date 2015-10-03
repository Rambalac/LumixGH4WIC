using System;
using com.azi.Image;

namespace com.azi.Filters
{
    public interface IFilter
    {
        void ProcessMap(IColorMap inmap, IColorMap outmap);
        IColorMap CreateResultMap(IColorMap likemap);
    }

    public abstract class Filter<A, B> : IFilter
    {
        public virtual ColorMap<B> CreateResultMap(IColorMap map)
        {
            return new ColorMap<B>(map.Width, map.Height, map.Bits);
        }
        public abstract void ProcessMap(ColorMap<A> inmap, ColorMap<B> outmap);

        IColorMap IFilter.CreateResultMap(IColorMap likemap)
        {
            return CreateResultMap(likemap);
        }

        void IFilter.ProcessMap(IColorMap inmap, IColorMap outmap)
        {
            ProcessMap((ColorMap<A>)inmap, (ColorMap<B>)outmap);
        }
    }
}