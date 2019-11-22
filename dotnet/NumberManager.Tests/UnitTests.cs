using System;
using Xunit;

namespace NumberManager.Tests
{
    public abstract class UnitTests
    {
        INumberManager _manager;

        protected UnitTests(INumberManager manager)
        {
            _manager = manager;
        }

        [Fact]
        public void GetReturnsZero()
        {
            Assert.Equal(0, _manager.GetNumber());
        }

        [Fact]
        public void Get0Get1Get2Get3Release1Get1Get4()
        {
            Assert.Equal(0, _manager.GetNumber());
            Assert.Equal(1, _manager.GetNumber());
            Assert.Equal(2, _manager.GetNumber());
            Assert.Equal(3, _manager.GetNumber());
            _manager.ReleaseNumber(1);
            Assert.Equal(1, _manager.GetNumber());
            Assert.Equal(4, _manager.GetNumber());
        }
    }

    public class BitArrayNumberManagerTests : UnitTests
    {
        public BitArrayNumberManagerTests()
            : base(new BitArrayNumberManager())
        {
        }
    }

    public class BitVector32NumberManagerTests : UnitTests
    {
        public BitVector32NumberManagerTests()
            : base(new BitVector32NumberManager())
        {
        }
    }

    public class HashSetNumberManagerTests : UnitTests
    {
        public HashSetNumberManagerTests()
            : base(new HashSetNumberManager())
        {
        }
    }

    public class SortedSetNumberManagerTests : UnitTests
    {
        public SortedSetNumberManagerTests()
            : base(new SortedSetNumberManager())
        {
        }
    }
}
