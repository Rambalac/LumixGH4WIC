using com.azi.Filters.VectorMapFilters;
using com.azi.Image;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace com.azi.Filters
{
    public interface IIIFilterAutoAdjuster
    {
        void AutoAdjust(IFilter filter, IColorMap map);
    }

    public interface IIFilterAutoAdjuster<in M> : IIIFilterAutoAdjuster where M : IColorMap
    {
    }

    public interface IFilterAutoAdjuster<in M, in F> : IIFilterAutoAdjuster<M> where M : IColorMap where F : IFilter
    {
        void AutoAdjust(F filter, M map);
    }

    public abstract class AFilterAutoAdjuster<M, F> : IFilterAutoAdjuster<M, F> where M : IColorMap where F : IFilter
    {
        public void AutoAdjust(IFilter filter, IColorMap map)
        {
            AutoAdjust((F)filter, (M)map);
        }

        public abstract void AutoAdjust(F filter, M map);
    }

    public interface IAutoAdjustableFilter
    {
        bool IsAdjusted { get; }
    }

    static class AutoAdjustersFactory
    {
        static Dictionary<Type, HashSet<Type>> filterToAutoadjusters = new Dictionary<Type, HashSet<Type>>();

        static public void AddAutoAdjuster<A, F>() where A : IIIFilterAutoAdjuster, new() where F : IFilter => AddAutoAdjuster(typeof(A), typeof(F));

        static void AddAutoAdjuster(Type adjuster, Type filter)
        {
            var list = GetAutoAdjusterTypes(filter);
            list.Add(adjuster);
        }

        static public Type GetAutoAdjusterType(Type filter) => GetAutoAdjusterTypes(filter).FirstOrDefault();

        static public IIIFilterAutoAdjuster GetNewAutoAdjuster(Type filter)
        {
            var t = GetAutoAdjusterType(filter);
            if (t == null) return null;
            return (IIIFilterAutoAdjuster)Activator.CreateInstance(t);
        }

        static public IIIFilterAutoAdjuster GetNewAutoAdjuster(IFilter filter) => GetNewAutoAdjuster(filter.GetType());

        static public ICollection<Type> GetAutoAdjusterTypes(Type filter)
        {
            HashSet<Type> result = null;
            if (!filterToAutoadjusters.TryGetValue(filter, out result))
            {
                result = new HashSet<Type>();
                filterToAutoadjusters.Add(filter, result);
            }
            return result;
        }

        static AutoAdjustersFactory()
        {
            AddAutoAdjuster<LightFilterAutoAdjuster, LightFilter>();
            AddAutoAdjuster<WhiteBalanceFilterAutoAdjuster, WhiteBalanceFilter>();
        }

    }
}