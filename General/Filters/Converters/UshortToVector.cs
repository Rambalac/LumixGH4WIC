using com.azi.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace com.azi.Filters.Converters
{
    public class UshortToVector : IFilter
    {
        public VectorMap Process(UshortColorMap map)
        {
            var result = new VectorMap(map.Width, map.Height);
            var respixel = result.GetPixel();
            var mappixel = map.GetPixel();
            do
            {
                var v = new Vector3(mappixel.R / (float)map.MaxValue, mappixel.G / (float)map.MaxValue, mappixel.B / (float)map.MaxValue);
                respixel.SetAndMoveNext(ref v);
            } while (mappixel.MoveNextAndCheck());
            return result;
        }
    }
}
