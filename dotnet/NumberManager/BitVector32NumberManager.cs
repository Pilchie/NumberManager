using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace NumberManager
{
    public class BitVector32NumberManager : INumberManager
    {
        readonly List<BitVector32> _bits = new();
        readonly int[] _masks = new int[32];

        public BitVector32NumberManager()
        {
            _masks[0] = BitVector32.CreateMask();
            for (var i = 1; i < 32; i++)
            {
                _masks[i] = BitVector32.CreateMask(_masks[i - 1]);
            }
        }

        public int GetNumber()
        {
            var bucket = 0;
            for (; bucket < _bits.Count; bucket++)
            {
                for (var bit = 0; bit < 32; bit++)
                {
                    if (!_bits[bucket][_masks[bit]])
                    {
                        SetBit(bucket, bit, true);
                        return 32 * bucket + bit;
                    }
                }
            }

            _bits.Add(default);
            SetBit(bucket, 0, true);
            return 32 * bucket;
        }

        public void ReleaseNumber(int number)
        {
            var bucket = number / 32;
            var bit = number % 32;

            SetBit(bucket, bit, false);

            // See if we can reclaim some space in our list.
            if (bucket + 1 == _bits.Count)
            {
                while (_bits[bucket].Data == 0)
                {
                    _bits.RemoveAt(bucket);
                    bucket--;
                }
            }
        }

        private void SetBit(int bucket, int bit, bool value)
        {
            var vec = _bits[bucket];
            vec[_masks[bit]] = value;
            _bits[bucket] = vec;
        }
    }
}