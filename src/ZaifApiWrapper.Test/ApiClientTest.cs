using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
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
        public async void PostAsync_should_throw_exception_if_api_error()
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

            var option = new ApiClientOption(new FakeHttpClientAccessor(handler)) {
                ApiKey = "00000000-0000-0000-0000-000000000000",
                ApiSecret = "00000000-0000-0000-0000-000000000000",
                MaxRetry = 5,
                ApiTimeoutRetryInterval = 0 };

            var client = new ApiClient("http://localhost/", option);

            // act 
            // assert
            await Assert.ThrowsAsync<ZaifApiException>(
                async () => await client.PostAsync<Test>("test", null, CancellationToken.None));
 
            Assert.Equal(1, handler.SendCount);
        }

        [Fact]
        public async void PostAsync_should_throw_exception_if_api_error_after_retry()
        {
            // arrange
            var jsonString = @"{ ""success"": 0 , ""error"": ""please try later."" }";

            var handler = new FakeResponseHandler();
            handler.AddFakeResponse(new Uri("http://localhost"),
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonString, Encoding.UTF8, "application/json"),
                });

            var option = new ApiClientOption(new FakeHttpClientAccessor(handler))
            {
                ApiKey = "00000000-0000-0000-0000-000000000000",
                ApiSecret = "00000000-0000-0000-0000-000000000000",
                MaxRetry = 5,
                ApiTimeoutRetryInterval = 0
            };

            var client = new ApiClient("http://localhost/", option);

            // act 
            // assert
            await Assert.ThrowsAsync<ZaifApiException>(
                async () => await client.PostAsync<Test>("test", null, CancellationToken.None));

            Assert.Equal(5, handler.SendCount);
        }


        [Fact]
        public async void PostAsync_should_throw_exception_if_http_error_except_timeout()
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

            var option = new ApiClientOption(new FakeHttpClientAccessor(handler))
            {
                ApiKey = "00000000-0000-0000-0000-000000000000",
                ApiSecret = "00000000-0000-0000-0000-000000000000",
                MaxRetry = 5,
                HttpErrorRetryInterval = 0
            };
            var client = new ApiClient("http://localhost/", option);

            // act 
            // assert
            await Assert.ThrowsAsync<HttpRequestException>(
                async () => await client.PostAsync<Test>("test", null, CancellationToken.None));

            Assert.Equal(1, handler.SendCount);
            
        }

        [Fact]
        public async void PostAsync_should_throw_exception_if_http_timeout_after_retry()
        {
            // arrange
            var jsonString = @"{ ""Name"": ""test"" }";

            var handler = new FakeResponseHandler();
            handler.AddFakeResponse(new Uri("http://localhost"),
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadGateway,
                    Content = new StringContent(jsonString, Encoding.UTF8, "application/json"),
                });

            var option = new ApiClientOption(new FakeHttpClientAccessor(handler))
            {
                ApiKey = "00000000-0000-0000-0000-000000000000",
                ApiSecret = "00000000-0000-0000-0000-000000000000",
                MaxRetry = 10,
                HttpErrorRetryInterval = 0
            };

            var client = new ApiClient("http://localhost/", option);

            // act
            // assert
            await Assert.ThrowsAsync<ZaifApiException>(
                async () => await client.PostAsync<Test>("test", null, CancellationToken.None));

            Assert.Equal(10, handler.SendCount);
        }
    }
}
