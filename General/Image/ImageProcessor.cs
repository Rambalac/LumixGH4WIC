using com.azi.Filters;
using com.azi.Filters.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.azi.Image
{
    public static class LinqExtensions
    {
        public enum SplitPlace
        {
            First, Last
        }

        public static IEnumerable<ICollection<T>> Split<T>(this IEnumerable<T> elements, Func<T, bool> predicate, SplitPlace place)
        {
            List<T> result = new List<T>();
            foreach (T element in elements)
            {
                if (predicate(element))
                {
                    if (place == SplitPlace.First)
                    {
                        if (result.Any()) yield return result;
                        result = new List<T>();
                        result.Add(element);
                    }
                    else
                    if (place == SplitPlace.Last)
                    {
                        result.Add(element);
                        if (result.Any()) yield return result;
                        result = new List<T>();
                    }
                }
                else
                    result.Add(element);
            }
            if (result.Any()) yield return result;
        }
    }
    public class ImageProcessor
    {
        class Stage
        {
            public IFilter Filter { get; set; }
            public IIIFilterAutoAdjuster AutoAdjuster { get; set; }
        }
        RawMap raw;
        List<Stage> stages;

        public ImageProcessor(RawMap raw, IEnumerable<IFilter> filters)
        {
            this.raw = raw;
            stages = filters.Select(f =>
            {
                IIIFilterAutoAdjuster adjuster = null;
                if (!(f is IAutoAdjustableFilter) || !((IAutoAdjustableFilter)f).IsAdjusted) adjuster = AutoAdjustersFactory.GetNewAutoAdjuster(f);
                var stage = new Stage
                {
                    Filter = f,
                    AutoAdjuster = adjuster
                };
                f.Changed += OnFilterChange;
                return stage;
            }).ToList();
        }

        private void OnFilterChange(IFilter obj)
        {
        }

        public IColorMap Invoke()
        {
            IColorMap image = raw;
            IColorMap newimage;

            foreach (List<Stage> stagesGroup in stages.Split(s => s.AutoAdjuster != null, LinqExtensions.SplitPlace.Last))
            {
                var last = stagesGroup.Last();
                if (last.AutoAdjuster != null)
                {
                    if (stagesGroup.Count > 1)
                    {
                        newimage = FiltersPipeline.ProcessFilters(image, stagesGroup.Take(stagesGroup.Count - 1).Select(s => s.Filter));
                        image.Dispose();
                        image = newimage;
                    }
                    IColorMap imagetoadjust = image;
                    if (image is UshortColorMap && last.AutoAdjuster is IIFilterAutoAdjuster<VectorMap>) imagetoadjust = new UshortToVector().Process((UshortColorMap)image);
                    last.AutoAdjuster.AutoAdjust(last.Filter, imagetoadjust);
                    if (image != imagetoadjust) imagetoadjust.Dispose();
                    last.AutoAdjuster = null;

                    newimage = FiltersPipeline.ProcessFilters(image, new[] { last.Filter });
                    image.Dispose();
                    image = newimage;
                }
                else
                {
                    newimage = FiltersPipeline.ProcessFilters(image, stagesGroup.Select(s => s.Filter));
                    image.Dispose();
                    image = newimage;
                }
            }
            return image;
        }

    }
}
