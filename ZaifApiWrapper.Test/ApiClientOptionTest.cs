using System;
using Xunit;

namespace ZaifApiWrapper.Test
{
    public class ApiClientOptionTest
    {
        [Fact]
        public void HttpStatusCodesToRetry_setter_should_throw_ArgumentNullException_if_value_is_null()
        {
            //arrange
            var obj = new ApiClientOption();

            //act
            var actual = Record.Exception(() => obj.HttpStatusCodesToRetry = null);

            //assert
            Assert.IsType<ArgumentNullException>(actual);
        }
    }
}
