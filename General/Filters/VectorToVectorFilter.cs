using System.Runtime.CompilerServices;
using com.azi.Image;
using System.Numerics;
using System;

namespace com.azi.Filters
{
    public abstract class VectorToVectorFilter : IFilter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract void ProcessVector(ref Vector3 input, ref Vector3 output);

        public void ProcessVector(Vector3[] input, int inputOffset, Vector3[] output, int outputOffset)
        {
            ProcessVector(ref input[inputOffset], ref output[outputOffset]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ProcessColorInCurve(int index, Vector3[] input, Vector3[] output)
        {
            ProcessVector(ref input[index], ref output[index]);
        }

        public void ProcessVector(Vector3[] input, int inputOffset, VectorPixel output)
        {
            ProcessVector(input, inputOffset, output.Map, output.Offset);
        }

        public void ProcessVector(Vector3 input, VectorPixel output)
        {
            ProcessVector(ref input, ref output.Map[output.Offset]);
        }

        public void ProcessVector(VectorPixel input, VectorPixel output)
        {
            ProcessVector(input.Map, input.Offset, output.Map, output.Offset);
        }
    }

    public abstract class VectorToColorFilter<T> : IFilter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract void ProcessVector(ref Vector3 input, ref T outR, ref T outG, ref T outB);

        public void ProcessVector(Vector3[] input, int inputOffset, T[] output, int outputOffset)
        {
            ProcessVector(ref input[inputOffset], ref output[outputOffset + 0], ref output[outputOffset + 1], ref output[outputOffset + 2]);
        }

        public void ProcessColorInCurve(int index, Vector3[] input, T[][] output)
        {
            ProcessVector(ref input[index], ref output[0][index], ref output[1][index], ref output[2][index]);
        }

        public void ProcessVector(Vector3[] input, int inputOffset, ColorPixel<T> output)
        {
            ProcessVector(input, inputOffset, output.Map, output.Offset);
        }

        public void ProcessVector(Vector3 input, ColorPixel<T> output)
        {
            ProcessVector(ref input, ref output.Map[output.Offset + 0], ref output.Map[output.Offset + 1], ref output.Map[output.Offset + 2]);
        }

        public void ProcessVector(VectorPixel input, ColorPixel<T> output)
        {
            ProcessVector(input.Map, input.Offset, output.Map, output.Offset);
        }
    }

}