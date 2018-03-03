using System;
using System.Net;
using System.Net.Http;
using Xunit;

namespace ZaifApiWrapper.Test
{
    public class SingleHttpClientAccessorTest
    {
        [Fact]
        public void Client_getter_should_return_same_object()
        {
            //arrange
            var obj1 = new SingleHttpClientAccessor();
            var obj2 = new SingleHttpClientAccessor();
            var uri = new Uri(ApiUrl.Base);

            //act
            var actual1 = obj1.Client;
            var actual2 = obj2.Client;

            //Assert
            Assert.NotNull(actual1);
            Assert.NotNull(actual2);
            Assert.IsType<HttpClient>(actual1);
            Assert.IsType<HttpClient>(actual2);
            Assert.Same(actual1, actual2);

            Assert.Equal(60000, ServicePointManager.FindServicePoint(uri).ConnectionLeaseTimeout);
        }
    }
}
