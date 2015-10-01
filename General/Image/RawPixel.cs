using System.Runtime.CompilerServices;

namespace com.azi.Image
{
    public class RawPixel
    {
        readonly int _limit;
        readonly RawMap _map;
        readonly ushort[] line;
        int _index;


        public RawPixel(RawMap map, int x, int y, int limit)
        {
            _map = map;
            _limit = limit;
            line = map.Raw[y];

            _index = x;
        }

        public RawPixel(RawMap map)
            : this(map, 0, 0, map.Width)
        {
        }

        public RawPixel(RawMap map, int x, int y)
            : this(map, x, y, map.Width)
        {
        }

        public ushort Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get { return line[_index]; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set { line[_index] = value; }
        }

        public int MaxValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get { return _map.MaxValue; }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MoveNext()
        {
            _index ++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MoveNext(int step)
        {
            _index += step;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetAndMoveNext(ushort val)
        {
            line[_index] = val;
            MoveNext();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetRel(int x)
        {
            return line[_index + x];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort SetRel(int x, ushort val)
        {
            return line[_index + x] = val;
        }
    }
}