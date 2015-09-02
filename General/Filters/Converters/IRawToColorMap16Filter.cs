using com.azi.Image;

namespace com.azi.Filters.Converters
{
    public interface IRawToColorMap16Filter<T> : IFilter where T: RawMap
    {
        UshortColorMap Process(T raw);
    }
}