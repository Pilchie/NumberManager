using System.Collections;

namespace NumberManager
{
    public class BitArrayNumberManager : INumberManager
    {
        readonly BitArray _bits = new(0);

        public int GetNumber()
        {
            var i = 0;
            for (; i < _bits.Length; i++)
            {
                if (!_bits[i])
                {
                    _bits[i] = true;
                    return i;
                }
            }

            _bits.Length = i+1;
            _bits[i] = true;
            return i;
        }

        public void ReleaseNumber(int number)
        {
            _bits[number] = false;
        }
    }
}