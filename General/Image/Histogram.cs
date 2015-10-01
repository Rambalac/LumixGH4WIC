using Azi.Helpers;
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
        readonly int maxIndex;
        readonly int comps;
        public int TotalPixels;



        public Histogram(int maxIndex, int comps = 3)
        {
            this.maxIndex = maxIndex;
            this.comps = comps;
            MaxValues = new int[comps];
            Values = Arrays.CreateRepeat(comps, () => new int[maxIndex + 1]);
            MinIndex = Arrays.CreateRepeat_(comps, maxIndex);
            MaxIndex = Arrays.CreateRepeat_(comps, 0);
        }

        public RGB8Map MakeRGB8Map(int width, int height)
        {
            var result = new RGB8Map(width, height);
            var max = MaxValues.Average();
            for (int x = 0; x < width; x++)
            {
                var ind = x * width / (maxIndex + 1);
                var val = 0;
                for (int i = 0; i < comps; i++) val += Values[i][ind];
                val /= comps;
                for (int y = (int)(height - height * val / max); y < height; y++)
                    result.SetPixel(x, y, 255, 255, 255);
            }
            return result;
        }

        public void AddValue(int index, int comp = 0)
        {
            if (comp == 0) TotalPixels++;
            if (index > maxIndex) index = maxIndex;
            if (index < 0) index = 0;
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

        public float[] FindWeightCenter(float[] min, float[] max)
        {
            var result = FindWeightCenter(FromFloat(min), FromFloat(max));
            return ToFloat(result);
        }

        public float FindWeightCenter(float min, float max)
        {
            var result = FindWeightCenter(FromFloat(new[] { min }), FromFloat(new[] { max }));
            return ToFloat(result)[0];
        }

        public Vector3 FindWeightCenter(Vector3? min = null, Vector3? max = null)
        {
            var result = FindWeightCenter(FromVector(min ?? new Vector3(0f, 0f, 0f)), FromVector(max ?? new Vector3(1f, 1f, 1f)));
            return ToVector(result);
        }

        int[] FromVector(Vector3 a)
        {
            return new[] { (int)(a.X * maxIndex), (int)(a.Y * maxIndex), (int)(a.Z * maxIndex) };
        }

        Vector3 ToVector(int[] a)
        {
            return new Vector3(a[0] / (float)maxIndex, a[1] / (float)maxIndex, a[2] / (float)maxIndex);
        }

        int[] FromFloat(IEnumerable<float> a)
        {
            return a.Select(v => (int)(v * maxIndex)).ToArray();
        }

        float[] ToFloat(IEnumerable<int> a)
        {
            return a.Select(v => v / (float)maxIndex).ToArray();
        }

        public void Transform(HistogramTransformFunc func)
        {
            var newval = Arrays.CreateRepeat(comps, () => new int[maxIndex + 1]);
            for (var c = 0; c < comps; c++)
                for (var i = 0; i <= maxIndex; i++)
                    newval[c][func(i, Values[c][i], c)] += Values[c][i];
        }

        public int[] FindWeightCenter(int[] min, int[] max)
        {
            var result = new int[comps];
            for (var c = 0; c < comps; c++)
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

        public void FindMinMax(out float minout, out float maxout, float e1 = 0.005f, float e2 = 0.005f)
        {
            int[] min, max;
            FindMinMax(out min, out max, e1, e2);
            minout = ToFloat(min)[0];
            maxout = ToFloat(max)[0];
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
            max = Arrays.CreateRepeat_(comps, maxIndex);
            min = Arrays.CreateRepeat_(comps, 0);

            var amount1 = (int)(e1 * TotalPixels);
            var amount2 = (int)(e2 * TotalPixels);
            for (var c = 0; c < comps; c++)
            {
                var vals = Values[c];
                var minsum = 0;
                var maxsum = 0;
                var start = Math.Min(1, Math.Min(MinIndex[c], maxIndex - MaxIndex[c]));

                bool cont = true;
                for (var i = start; i < maxIndex && cont; i++)
                {
                    cont = false;
                    minsum += vals[i];
                    if (minsum < amount1)
                    {
                        min[c] = (ushort)i;
                        cont = true;
                    }

                    maxsum += vals[maxIndex - i];
                    if (maxsum < amount2)
                    {
                        max[c] = (ushort)(maxIndex - i);
                        cont = true;
                    }
                }
            }
        }

        public float[] GetMean()
        {
            float[] ret = new float[comps];

            for (int c = 0; c < comps; c++)
            {
                int[] channelHistogram = Values[c];
                long avg = 0;
                long sum = 0;

                for (int j = 0; j < channelHistogram.Length; j++)
                {
                    avg += j * channelHistogram[j];
                    sum += channelHistogram[j];
                }
                ret[c] = (sum != 0) ? (float)avg / (float)sum : 0;
            }

            return ret;
        }
    }
}