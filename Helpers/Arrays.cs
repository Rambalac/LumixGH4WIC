using System;

namespace Azi.Helpers
{
    public static class Arrays
    {
        public static T[] CreateRepeat_<T>(int length, T value)
        {
            var result = new T[length];
            for (int i = 0; i < length; i++) result[i] = value;
            return result;
        }

        public static T[] CreateRepeat<T>(int length, Func<T> valueFunc)
        {
            var result = new T[length];
            for (int i = 0; i < length; i++) result[i] = valueFunc();
            return result;
        }
    }
}
