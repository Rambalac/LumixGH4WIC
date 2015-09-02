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
            private static HashSet<WeakReference<T[]>> reuse = new HashSet<WeakReference<T[]>>();

            public static void Release(T[] arr)
            {
                reuse.Add(new WeakReference<T[]>(arr));
            }

            public static T[] ReuseOrGetNew(int size)
            {
                HashSet<WeakReference<T[]>> delete = new HashSet<WeakReference<T[]>>();
                try
                {
                    foreach (var item in reuse)
                    {
                        T[] ar;
                        if (item.TryGetTarget(out ar))
                        {
                            if (ar.Length == size)
                            {
                                delete.Add(item);
                                return ar;
                            }
                        }
                        else delete.Add(item);

                    }
                    return new T[size];
                }
                finally
                {
                    foreach (var item in delete)
                        reuse.Remove(item);
                }
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
