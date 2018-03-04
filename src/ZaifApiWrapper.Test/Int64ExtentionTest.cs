using System;
using System.Linq;
using Xunit;

namespace ZaifApiWrapper.Test
{
    public class Int64ExtentionTest
    {
        [Fact]
        public void ToDateTime_should_return_UNIX_time()
        {
            //arrange
            //act
            var unixEpoch = 0L.ToDateTime();
            var actual1 = 100000000L.ToDateTime();
            var actual2 = 1000000000L.ToDateTime();
            var actual3 = 1234567890L.ToDateTime();
            var actual4 = 2147483647L.ToDateTime();

            //assert
            Assert.Equal(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), unixEpoch);
            Assert.Equal(new DateTime(1973, 3, 3, 9, 46, 40, DateTimeKind.Utc), actual1);
            Assert.Equal(new DateTime(2001, 9, 9, 1, 46, 40, DateTimeKind.Utc), actual2);
            Assert.Equal(new DateTime(2009, 2, 13, 23, 31, 30, DateTimeKind.Utc), actual3);
            Assert.Equal(new DateTime(2038, 1, 19, 3, 14, 7, DateTimeKind.Utc), actual4);
        }
    }
}
