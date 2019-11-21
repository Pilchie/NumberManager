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
    }
}
