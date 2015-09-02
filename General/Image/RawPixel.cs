using System.Runtime.CompilerServices;

namespace com.azi.Image
{
    public class RawPixel
    {
        private readonly int _limit;
        private readonly RawMap _map;
        private int _index;


        public RawPixel(RawMap map, int x, int y, int limit)
        {
            _map = map;
            _limit = limit;
            _index = y * map.Width + x;
        }

        public RawPixel(RawMap map)
            : this(map, 0, 0, map.Width * map.Height)
        {
        }

        public RawPixel(RawMap map, int x, int y)
            : this(map, x, y, map.Width * map.Height)
        {
        }

        public ushort Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get { return _map.Raw[_index]; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set { _map.Raw[_index] = value; }
        }

        public int MaxValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get { return _map.MaxValue; }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetRel(int x, int y)
        {
            return _map.Raw[_index + x + y * _map.Width];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort SetRel(int x, int y, ushort val)
        {
            return _map.Raw[_index + x + y * _map.Width] = val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MoveNext()
        {
            _index += 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetAndMoveNext(ushort val)
        {
            _map.Raw[_index] = val;
            MoveNext();
        }
    }
}