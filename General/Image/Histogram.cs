using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace com.azi.Image
{
    public class Histogram
    {
        public delegate int HistogramTransformFunc(int index, int value, int comp);

        public readonly int[] MaxIndex;

        public readonly int[] MaxValues;
        public readonly int[] MinIndex;
        public readonly int[][] Values;
        readonly int _maxIndex;
        public int TotalPixels;

        public Histogram(int maxIndex)
        {
            _maxIndex = maxIndex;
            MaxValues = new int[3];
            Values = new[] { new int[maxIndex + 1], new int[maxIndex + 1], new int[maxIndex + 1] };
            MinIndex = new[] { maxIndex, maxIndex, maxIndex };
            MaxIndex = new[] { 0, 0, 0 };
        }

        public RGB8Map MakeRGB8Map(int width, int height)
        {
            var result = new RGB8Map(width, height);
            var max = MaxValues.Average();
            for (int x = 0; x < width; x++)
            {
                var ind = x * width / (_maxIndex + 1);
                var val = (Values[0][ind] + Values[1][ind] + Values[2][ind]) / 3;
                for (int y = (int)(height - height * val / max); y < height; y++)
                    result.SetPixel(x, y, 255, 255, 255);
            }
            return result;
        }

        public void AddValue(int comp, int index)
        {
            if (comp == 0) TotalPixels++;
            if (index > _maxIndex) index = _maxIndex;
            MinIndex[comp] = Math.Min(MinIndex[comp], index);
            MaxIndex[comp] = Math.Max(MaxIndex[comp], index);
            var val = Values[comp][index]++;
            MaxValues[comp] = Math.Max(MaxValues[comp], val);
        }

        public ushort[] FindWeightCenter(ushort[] min, ushort[] max)
        {
            var result = FindWeightCenter(min.Cast<int>().ToArray(), max.Cast<int>().ToArray());
            return result.Cast<ushort>().ToArray();
        }

        public Vector3 FindWeightCenter(Vector3? min = null, Vector3? max = null)
        {
            var result = FindWeightCenter(FromVector(min ?? new Vector3(0f, 0f, 0f)), FromVector(max ?? new Vector3(1f, 1f, 1f)));
            return ToVector(result);
        }

        int[] FromVector(Vector3 a)
        {
            return new[] { (int)(a.X * _maxIndex), (int)(a.Y * _maxIndex), (int)(a.Z * _maxIndex) };
        }

        Vector3 ToVector(int[] a)
        {
            return new Vector3(a[0] / (float)_maxIndex, a[1] / (float)_maxIndex, a[2] / (float)_maxIndex);
        }

        int[] FromFloat(IEnumerable<float> a)
        {
            return a.Select(v => (int)(v * _maxIndex)).ToArray();
        }

        float[] ToFloat(IEnumerable<int> a)
        {
            return a.Select(v => v / (float)_maxIndex).ToArray();
        }

        public void Transform(HistogramTransformFunc func)
        {
            var newval = new[] { new int[_maxIndex + 1], new int[_maxIndex + 1], new int[_maxIndex + 1] };
            for (var c = 0; c < 3; c++)
                for (var i = 0; i <= _maxIndex; i++)
                    newval[c][func(i, Values[c][i], c)] += Values[c][i];
        }

        public int[] FindWeightCenter(int[] min, int[] max)
        {
            var result = new int[3];
            for (var c = 0; c < 3; c++)
            {
                var vals = Values[c];
                var minsum = 0;
                var maxsum = 0;
                var mini = min[c];
                var maxi = max[c];

                do
                {
                    while (minsum <= maxsum && mini < maxi) minsum += vals[mini++];
                    while (maxsum < minsum && maxi > mini) maxsum += vals[maxi--];
                } while (mini < maxi);

                result[c] = mini;
            }
            return result;
        }

        public void FindMinMax(out ushort[] minout, out ushort[] maxout, float e1 = 0.005f, float e2 = 0.005f)
        {
            int[] min, max;
            FindMinMax(out min, out max, e1, e2);
            minout = min.Cast<ushort>().ToArray();
            maxout = max.Cast<ushort>().ToArray();
        }

        public void FindMinMax(out float[] minout, out float[] maxout, float e1 = 0.005f, float e2 = 0.005f)
        {
            int[] min, max;
            FindMinMax(out min, out max, e1, e2);
            minout = ToFloat(min);
            maxout = ToFloat(max);
        }

        public void FindMinMax(out Vector3 minout, out Vector3 maxout, float e1 = 0.005f, float e2 = 0.005f)
        {
            int[] min, max;
            FindMinMax(out min, out max, e1, e2);
            minout = ToVector(min);
            maxout = ToVector(max);
        }

        public void FindMinMax(out int[] min, out int[] max, float e1 = 0.005f, float e2 = 0.005f)
        {
            max = new[] { _maxIndex, _maxIndex, _maxIndex };
            min = new[] { 0, 0, 0 };

            var amount1 = (int)(e1 * TotalPixels);
            var amount2 = (int)(e2 * TotalPixels);
            for (var c = 0; c < 3; c++)
            {
                var vals = Values[c];
                var minsum = 0;
                var maxsum = 0;
                var start = Math.Min(1, Math.Min(MinIndex[c], _maxIndex - MaxIndex[c]));

                bool cont = true;
                for (var i = start; i < _maxIndex && cont; i++)
                {
                    cont = false;
                    minsum += vals[i];
                    if (minsum < amount1)
                    {
                        min[c] = (ushort)i;
                        cont = true;
                    }

                    maxsum += vals[_maxIndex - i];
                    if (maxsum < amount2)
                    {
                        max[c] = (ushort)(_maxIndex - i);
                        cont = true;
                    }
                }
            }
        }
    }
}