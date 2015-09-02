using System;

namespace com.azi.Filters
{
    public interface IFilter
    {
        event Action<IFilter> Changed;
    }
}