using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.azi.Filters.Converters;
using com.azi.Image;
using System.Numerics;

namespace com.azi.Filters
{
    public class FiltersPipeline
    {
        private readonly List<IFilter> _filters;

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
            var indColorFilter = new List<IIndependentComponentFilter>();
            IColorMap currentMap = map;
            foreach (var filter in filters)
            {
                if (filter == null)
                    throw new ArgumentNullException(nameof(filter));

                if (currentMap is UshortColorMap && filter is IIndependentComponentFilter)
                {
                    indColorFilter.Add((IIndependentComponentFilter)filter);
                }
                else
                {
                    if (indColorFilter.Any())
                    {
                        currentMap = ApplyIndependentColorFilters(currentMap, indColorFilter);
                        indColorFilter.Clear();
                    }
                    var newmap = ApplySingleFilter(currentMap, filter);
                    if (newmap != currentMap && currentMap != map) currentMap.Dispose();
                    currentMap = newmap;

                }
            }
            if (indColorFilter.Any())
            {
                var newmap = ApplyIndependentColorFilters(currentMap, indColorFilter);
                if (newmap!=currentMap&&currentMap != map) currentMap.Dispose();
                currentMap = newmap;
            }
            return currentMap;
        }

        public static IColorMap ApplySingleFilter(IColorMap map, IFilter filter)
        {
            if (map is RawBGGRMap)
            {
                if (filter is IRawToColorMap16Filter<RawBGGRMap>)
                    return ((IRawToColorMap16Filter<RawBGGRMap>)filter).Process((RawBGGRMap)map);
                else
                    throw new NotSupportedException($"Not supported Filter: {filter.GetType()} for Map: {map.GetType()}");
            }
            else if (map is VectorMap)
            {
                if (filter is VectorToVectorFilter)
                {
                    return ApplySingleFilter((VectorMap)map, (VectorToVectorFilter)filter);
                }
                else if (filter is VectorToColorFilter<byte>)
                {
                    return ConvertToRGB((VectorMap)map, (VectorToColorFilter<byte>)filter);
                }
                else
                    throw new NotSupportedException("Not supported Filter: " + filter.GetType() + " for Map: " +
                                                    map.GetType());
            }
            else
            {
                throw new NotSupportedException("Not supported Map: " + map.GetType());
            }
        }

        private static VectorMap ApplySingleFilter(VectorMap map, VectorToVectorFilter filter)
        {
            var result = new VectorMap(map.Width, map.Height);
            Parallel.For(0, map.Height, y =>
            {
                var inpix = map.GetRow(y);
                var outpix = result.GetRow(y);
                for (var x = 0; x < map.Width; x++)
                {
                    filter.ProcessVector(inpix, outpix);
                    inpix.MoveNext();
                    outpix.MoveNext();
                }
            });
            return result;
        }

        private static RGB8Map ConvertToRGB(VectorMap map, VectorToColorFilter<byte> filter)
        {
            var result = new RGB8Map(map.Width, map.Height);
            Parallel.For(0, map.Height, y =>
            {
                var input = map.GetRow(y);
                var output = result.GetRow(y);
                for (var x = 0; x < map.Width; x++)
                {
                    filter.ProcessVector(input, output);
                    input.MoveNext();
                    output.MoveNext();
                }
            });
            return result;
        }
        private static Vector3[] InitVector3Array(int maxIndex)
        {
            var curve = ArraysReuseManager.ReuseOrGetNew<Vector3>(maxIndex + 1);

            Parallel.For(0, maxIndex + 1, i =>
            {
                var fi = i / (float)maxIndex;
                curve[i] = new Vector3(fi);
            });
            return curve;
        }

        private static ushort[][] InitUshortArray(int maxIndex)
        {
            var curve = new[] { ArraysReuseManager.ReuseOrGetNew<ushort>(maxIndex + 1), ArraysReuseManager.ReuseOrGetNew<ushort>(maxIndex + 1), ArraysReuseManager.ReuseOrGetNew<ushort>(maxIndex + 1) }; ;

            Parallel.For(0, maxIndex + 1, i =>
            {
                curve[0][i] = curve[1][i] = curve[2][i] = (ushort)i;
            });
            return curve;
        }

        private static byte[][] InitByteArray(int maxIndex)
        {
            var curve = new[] { ArraysReuseManager.ReuseOrGetNew<byte>(maxIndex + 1), ArraysReuseManager.ReuseOrGetNew<byte>(maxIndex + 1), ArraysReuseManager.ReuseOrGetNew<byte>(maxIndex + 1) }; ;

            Parallel.For(0, maxIndex + 1, i =>
            {
                curve[0][i] = curve[1][i] = curve[2][i] = (byte)(i * 255 / maxIndex);
            });
            return curve;
        }

        private static object ConvertToCurve(
            ICollection<IIndependentComponentFilter> indFilters, int maxIndex)
        {
            object curvein = null;
            if (indFilters.First() is VectorToVectorFilter || indFilters.First() is VectorToColorFilter<byte>)
                curvein = InitVector3Array(maxIndex);
            else throw new NotSupportedException($"Initial filter is not supported: {indFilters.First().GetType()}");

            foreach (var f in indFilters)
            {
                var filter = f;
                if (curvein is Vector3[])
                {
                    if (filter is VectorToVectorFilter)
                        Parallel.For(0, maxIndex + 1, i => ((VectorToVectorFilter)filter).ProcessColorInCurve(i, (Vector3[])curvein, (Vector3[])curvein));
                    else if (filter is VectorToColorFilter<byte>)
                    {
                        var curveout = InitByteArray(maxIndex);
                        Parallel.For(0, maxIndex + 1, i => ((VectorToColorFilter<byte>)filter).ProcessColorInCurve(i, (Vector3[])curvein, curveout));
                        ArraysReuseManager.Release((Vector3[])curvein);
                        curvein = curveout;
                    }
                    else throw new NotSupportedException($"Filter is not supported: {filter.GetType()}");
                }
                else throw new NotSupportedException($"Curve is not supported: {curvein.GetType()}");
            }
            return curvein;
        }

        private static IColorMap ApplyIndependentColorFilters(IColorMap map,
            ICollection<IIndependentComponentFilter> indColorFilters)
        {
            if (map is UshortColorMap)
            {
                var m = (UshortColorMap)map;
                var maxValue = m.MaxValue;
                var curvef = ConvertToCurve(indColorFilters, maxValue);

                return ApplyCurve(m, curvef);
            }
            if (map is VectorMap)
            {
                var m = (VectorMap)map;
                foreach (IIndependentComponentFilter f in indColorFilters)
                {
                    if (f is VectorToVectorFilter)
                        m = ApplySingleFilter(m, (VectorToVectorFilter)f);
                    else
                        throw new NotSupportedException($"Filter is not supported: {f.GetType()}");
                }
                return m;
            }
            throw new NotSupportedException($"Map is not supported: {map.GetType()}");
        }

        private static IColorMap ApplyCurve(UshortColorMap map, object curve)
        {
            //GC.Collect(2, GCCollectionMode.Forced, true, true);

            if (curve is ushort[][])
                return ApplyCurve(map, (ushort[][])curve);
            if (curve is byte[][])
                return ApplyCurve(map, (byte[][])curve);
            if (curve is Vector3[])
                return ApplyCurve(map, (Vector3[])curve);
            throw new NotSupportedException($"Curve not supported: {curve.GetType()}");
        }

        private static UshortColorMap ApplyCurve(UshortColorMap map, ushort[][] curve)
        {
            var result = new UshortColorMap(map.Width, map.Height, map.MaxBits);
            Parallel.For(0, result.Height, y =>
            {
                var input = map.GetRow(y);
                var output = result.GetRow(y);
                for (var x = 0; x < result.Width; x++)
                {
                    output.SetAndMoveNext(curve[0][input.R], curve[1][input.G], curve[2][input.B]);
                    input.MoveNext();
                }
            });
            return result;
        }

        private static VectorMap ApplyCurve(UshortColorMap map, Vector3[] curve)
        {
            var result = new VectorMap(map.Width, map.Height);
            Parallel.For(0, result.Height, y =>
            {
                var input = map.GetRow(y);
                var output = result.GetRow(y);
                for (var x = 0; x < result.Width; x++)
                {
                    output.SetAndMoveNext(curve[input.R].X, curve[input.G].Y, curve[input.B].Z);
                    input.MoveNext();
                }
            });
            return result;
        }

        private static RGB8Map ApplyCurve(UshortColorMap map, byte[][] curve)
        {
            var result = new RGB8Map(map.Width, map.Height);

            Parallel.For(0, result.Height, y =>
            {
                var input = map.GetRow(y);
                var output = result.GetRow(y);
                for (var x = 0; x < result.Width; x++)
                {
                    output.SetAndMoveNext(curve[0][input.R], curve[1][input.G], curve[2][input.B]);
                    input.MoveNext();
                }
            });
            return result;
        }
    }
}