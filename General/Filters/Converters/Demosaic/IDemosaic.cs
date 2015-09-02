using com.azi.Image;

namespace com.azi.Filters.Converters.Demosaic
{
    public interface IDemosaic<T> : IRawToColorMap16Filter<T>
        where T : RawMap
    {
    }

    public interface IBGGRDemosaic : IDemosaic<RawBGGRMap>
    {
    }
}