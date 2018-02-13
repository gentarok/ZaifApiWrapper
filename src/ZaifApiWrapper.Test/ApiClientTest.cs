using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ZaifApiWrapper.Test.TestDouble;

namespace ZaifApiWrapper.Test
{
    public class ApiClientTest
    {

        class Test
        {
            public string Name { get; set; }
        }

        [Fact]
        public async void GetAsync_should_return_specific_object()
        {
            // arrange
            var jsonString = @"{ ""Name"": ""test"" }";

            var handler = new FakeResponseHandler();
            handler.AddFakeResponse(new Uri("http://localhost/test/1"),
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonString, Encoding.UTF8, "application/json"),
                });

            var option = new ApiClientOption(new FakeHttpClientAccessor(handler));
            var client = new ApiClient("http://localhost/", option);

            // act 
            var obj = await client.GetAsync<Test>("test", new[] { "1" }, CancellationToken.None);

            // assert
            Assert.NotNull(obj);
            Assert.IsType<Test>(obj);
            Assert.Equal("test", obj.Name);
        }

        [Fact]
        public void PostAsync_should_throw_exception_if_api_error()
        {
            // arrange
            var jsonString = @"{ ""success"": 0 , ""error"": ""api errer raised."" }";

            var handler = new FakeResponseHandler();
            handler.AddFakeResponse(new Uri("http://localhost"),
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonString, Encoding.UTF8, "application/json"),
                });

            var option = new ApiClientOption(new FakeHttpClientAccessor(handler));
            var client = new ApiClient("http://localhost/", option);

            // act 
            var func = new Func<Task>(async () => await client.PostAsync<Test>("test", null, CancellationToken.None));

            // assert
            Assert.ThrowsAsync<ZaifApiException>(func);
        }

        [Fact]
        public void PostAsync_should_throw_exception_if_http_error_without_timeout()
        {
            // arrange
            var jsonString = @"{ ""Name"": ""test"" }";

            var handler = new FakeResponseHandler();
            handler.AddFakeResponse(new Uri("http://localhost"),
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(jsonString, Encoding.UTF8, "application/json"),
                });

            var option = new ApiClientOption(new FakeHttpClientAccessor(handler));
            var client = new ApiClient("http://localhost/", option);

            // act 
            var func = new Func<Task>(async () => await client.PostAsync<Test>("test", null, CancellationToken.None));

            // assert
            Assert.ThrowsAsync<HttpRequestException>(func);
        }
    }
}
