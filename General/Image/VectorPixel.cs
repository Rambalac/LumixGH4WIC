using System.Numerics;
using System.Runtime.CompilerServices;

namespace com.azi.Image
{
    public class VectorPixel
    {
        private readonly int _limit;
        private readonly Vector3[] _map;
        private int _index;

        public int Offset { get { return _index; } }

        public Vector3[] Map { get { return _map; } }

        public VectorPixel(VectorMap map, int x, int y, int limit)
        {
            _map = map.Rgb;
            _limit = limit;
            _index = y * map.Width + x;
        }

        public VectorPixel(VectorMap map)
            : this(map, 0, 0, map.Width * map.Height)
        {
        }

        public VectorPixel(VectorMap map, int x, int y)
            : this(map, x, y, map.Width * map.Height)
        {
        }

        public Vector3 Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get { return _map[_index]; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set { _map[_index] = value; }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MoveNext()
        {
            _index++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetAndMoveNext(ref Vector3 val)
        {
            _map[_index] = val;
            MoveNext();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNextAndCheck()
        {
            _index++;
            return _index < _limit;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetAndMoveNext(float r, float g, float b)
        {
            _map[_index].X = r;
            _map[_index].Y = g;
            _map[_index].Z = b;
            MoveNext();
        }

    }
}
