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
        static class _ArraysReuseManager<T>
        {
            private static ConcurrentDictionary<int, HashSet<WeakReference<T[]>>> reuse = new ConcurrentDictionary<int, HashSet<WeakReference<T[]>>>(5, 100);

            public static void Release(T[] arr)
            {
                var set = reuse.GetOrAdd(arr.Length, (s) => new HashSet<WeakReference<T[]>>());
                lock (set)
                {
                    set.Add(new WeakReference<T[]>(arr));
                }
            }

            public static T[] ReuseOrGetNew(int size)
            {
                HashSet<WeakReference<T[]>> set;
                if (reuse.TryGetValue(size, out set))
                {
                    LinkedList<WeakReference<T[]>> delete = new LinkedList<WeakReference<T[]>>();
                    lock (set)
                    {
                        try
                        {

                            foreach (var item in set)
                            {
                                T[] ar;
                                if (item.TryGetTarget(out ar))
                                {
                                    delete.AddLast(item);
                                    return ar;
                                }
                                else delete.AddLast(item);
                            }
                            return new T[size];
                        }
                        finally
                        {
                            foreach (var item in delete)
                                set.Remove(item);
                            if (set.Count == 0) reuse.TryRemove(size, out set);
                        }
                    }
                }
                else
                    return new T[size];
            }
        }

        public static R[] ReuseOrGetNew<R>(int size)
        {
            return _ArraysReuseManager<R>.ReuseOrGetNew(size);
        }
        public static void Release<R>(R[] arr)
        {
            _ArraysReuseManager<R>.Release(arr);
        }
    }
}
