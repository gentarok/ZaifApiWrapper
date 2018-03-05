using System;
using System.Collections.Generic;
using Xunit;

namespace ZaifApiWrapper.Test
{
    public class DictionaryExtentionTest
    {
        [Fact]
        public void ThrowIfNotContainsKey_throw_ArgumentException_if_not_contains_key()
        {
            //arrange
            var obj = new Dictionary<string, string>();

            //act
            var actual = Record.Exception(() => obj.ThrowIfNotContainsKey("test", "test"));

            //assert
            Assert.IsType<ArgumentException>(actual);
        }
    }
}
