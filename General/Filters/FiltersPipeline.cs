using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.azi.Filters.Converters;
using com.azi.Image;
using System.Numerics;
using Azi.Helpers;

namespace com.azi.Filters
{
    public class FiltersPipeline
    {
        readonly List<IFilter> _filters;

        public FiltersPipeline(IEnumerable<IFilter> filters)
        {
            _filters = new List<IFilter>(filters);
        }

        public IColorMap ProcessFilters(IColorMap map) => ProcessFilters(map, _filters);

        public static IColorMap ProcessFilters(IColorMap map, IFilter filter)
        {
            return ProcessFilters(map, new[] { filter });
        }

        public static IColorMap ProcessFilters(IColorMap map, IEnumerable<IFilter> filters)
        {
            IColorMap currentMap = map;
            foreach (var filter in filters)
            {
                if (filter == null)
                    throw new ArgumentNullException(nameof(filter));

                var newmap = filter.CreateResultMap(currentMap);
                filter.ProcessMap(currentMap, newmap);
                if (newmap != currentMap && currentMap != map) currentMap.Dispose();
                currentMap = newmap;
            }
            return currentMap;
        }
    }
}