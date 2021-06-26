using System;
using Xunit;

namespace NumberManager.Tests
{
    public abstract class UnitTests
    {
        readonly INumberManager _manager;

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

        [Fact]
        public void Get0Get1Get2Release0Release1Get0Get1Get3()
        {
            Assert.Equal(0, _manager.GetNumber());
            Assert.Equal(1, _manager.GetNumber());
            Assert.Equal(2, _manager.GetNumber());
            _manager.ReleaseNumber(0);
            _manager.ReleaseNumber(1);
            Assert.Equal(0, _manager.GetNumber());
            Assert.Equal(1, _manager.GetNumber());
            Assert.Equal(3, _manager.GetNumber());
        }

        [Fact]
        public void Get0Get1Get2Release1Release0Get0Get1Get3()
        {
            Assert.Equal(0, _manager.GetNumber());
            Assert.Equal(1, _manager.GetNumber());
            Assert.Equal(2, _manager.GetNumber());
            _manager.ReleaseNumber(1);
            _manager.ReleaseNumber(0);
            Assert.Equal(0, _manager.GetNumber());
            Assert.Equal(1, _manager.GetNumber());
            Assert.Equal(3, _manager.GetNumber());
        }

        [Fact]
        public void Get0Get1Get2Get3Get4Release1Release3Release2Get1Get2Get3Get5()
        {
            Assert.Equal(0, _manager.GetNumber());
            Assert.Equal(1, _manager.GetNumber());
            Assert.Equal(2, _manager.GetNumber());
            Assert.Equal(3, _manager.GetNumber());
            Assert.Equal(4, _manager.GetNumber());
            _manager.ReleaseNumber(1);
            _manager.ReleaseNumber(3);
            _manager.ReleaseNumber(2);
            Assert.Equal(1, _manager.GetNumber());
            Assert.Equal(2, _manager.GetNumber());
            Assert.Equal(3, _manager.GetNumber());
            Assert.Equal(5, _manager.GetNumber());
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

    public class SortedSetOfRangeNumberManagerTests : UnitTests
    {
        public SortedSetOfRangeNumberManagerTests()
            : base(new SortedSetOfRangeNumberManager())
        {
        }
    }
}
