using System;

namespace NumberManager
{
    public interface INumberManager
    {
        int GetNumber();
        void ReleaseNumber(int number);
    }
}
