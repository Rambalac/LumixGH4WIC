using System;
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
            private static Dictionary<int, HashSet<WeakReference<T[]>>> reuse = new Dictionary<int, HashSet<WeakReference<T[]>>>();

            private static HashSet<WeakReference<T[]>> GetReuseListOrCreate(int size)
            {
                HashSet<WeakReference<T[]>> result;
                if (!reuse.TryGetValue(size, out result))
                {
                    result = new HashSet<WeakReference<T[]>>();
                    reuse.Add(size, result);
                }
                return result;
            }

            public static void Release(T[] arr)
            {
                GetReuseListOrCreate(arr.Length).Add(new WeakReference<T[]>(arr));
            }

            public static T[] ReuseOrGetNew(int size)
            {
                HashSet<WeakReference<T[]>> set;
                if (reuse.TryGetValue(size, out set))
                {
                    LinkedList<WeakReference<T[]>> delete = new LinkedList<WeakReference<T[]>>();

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
