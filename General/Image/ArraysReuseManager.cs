using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.azi.Image
{
    public static class ArraysReuseManager
    {
        const int MinSizeLimit = 1 << 20;
        static class _ArraysReuseManager<T>
        {
            static readonly ConcurrentDictionary<int, ConcurrentBag<WeakReference<T[]>>> reuse = new ConcurrentDictionary<int, ConcurrentBag<WeakReference<T[]>>>(5, 100);

            public static void Release(T[] arr)
            {
                var set = reuse.GetOrAdd(arr.Length, (s) => new ConcurrentBag<WeakReference<T[]>>());
                set.Add(new WeakReference<T[]>(arr));
            }

            public static T[] ReuseOrGetNew(int size)
            {
                ConcurrentBag<WeakReference<T[]>> set;
                WeakReference<T[]> item;
                T[] ar;
                if (reuse.TryGetValue(size, out set))
                {
                    while (set.TryTake(out item))
                    {
                        if (item.TryGetTarget(out ar))
                            return ar;
                    }
                    return new T[size];
                }
                else
                    return new T[size];
            }
        }

        public static R[] ReuseOrGetNew<R>(int size)
        {
            if (size < MinSizeLimit) return new R[size];
            return _ArraysReuseManager<R>.ReuseOrGetNew(size);
        }
        public static void Release<R>(R[] arr)
        {
            if (arr.Length < MinSizeLimit) return;
            _ArraysReuseManager<R>.Release(arr);
        }
    }
}
