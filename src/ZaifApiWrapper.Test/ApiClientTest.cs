using Moq;
using Moq.Protected;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ZaifApiWrapper.Test
{
    public class ApiClientTest
    {
        #region Test helper

        // Test target factory
        private ApiClient Create(HttpResponseMessage httpRequestMessage = null, string apiKey = null, string apiSecret = null)
        {
            var handler = new Mock<DelegatingHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(() => Task.FromResult(httpRequestMessage));

            var accessor = new Mock<IHttpClientAccessor>();
            accessor.SetupGet((x) => x.Client).Returns(new HttpClient(handler.Object));

            var option = new ApiClientOption(accessor.Object)
            {
                ApiKey = apiKey,
                ApiSecret = apiSecret,
                HttpErrorRetryInterval = 0,
                ApiTimeoutRetryInterval = 0,
            };
            return new ApiClient("http://localhost", option);
        }

        public static object[][] GetAsyncSuccessDana = new object[][]
        {
            new object[] { @"{ ""key"": ""value"" }", },
            new object[] { @"[ { ""key"": 1 }, { ""key"": 2 }, ]", },
        };

        // GetAsync, PostAsyncがリトライになるケースのデータ
        public static object[][] RetryData = new object[][]
        {
           new object[]
           {
               HttpStatusCode.BadGateway, string.Empty,
           },
           new object[]
           {
               HttpStatusCode.ServiceUnavailable, string.Empty,
           },
           new object[]
           {
               HttpStatusCode.GatewayTimeout, string.Empty,
           },
           new object[]
           {
               HttpStatusCode.OK, @"{ ""success"": 0, ""error"": ""time wait restriction, please try later."" }",
           },
        };

        // 正しい形式のAPI key, API secret
        private const string VALID_CREDENTIAL = "00000000-0000-0000-0000-000000000000";

        // GetAsync, PostAsyncがリトライになるケースのデータ
        public static object[][] InvalidCredentialData = new object[][]
        {
           new object[]
           {
               "00000000-0000-0000-0000-0000000000000", //桁が多い
               VALID_CREDENTIAL,
           },
           new object[]
           {
               VALID_CREDENTIAL,
               "AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA", //大文字
           },
           new object[]
           {
               "00000000-0000-0000-0000-00000000000g", //範囲外
               VALID_CREDENTIAL,
           },
           new object[]
           {
               VALID_CREDENTIAL,
               "000000000-000-0000-0000-000000000000", //形式異常
           },
        };

        #endregion

        [Fact]
        public void Costructor_should_return_instance()
        {
            //arrange

            //act
            var obj = Create();

            //assert
            Assert.NotNull(obj);
            Assert.IsType<ApiClient>(obj);
        }

        [Theory]
        [MemberData(nameof(GetAsyncSuccessDana))]
        public async void GetAsync_should_success(string jsonString)
        {
            //arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonString, Encoding.UTF8, "application/json"),
            };

            var obj = Create(response);

            //act
            var actual = await obj.GetAsync<object>("_", new[] { "_" }, CancellationToken.None);

            //assert
            Assert.NotNull(actual);
        }

        [Theory]
        [MemberData(nameof(RetryData))]
        public void GetAsync_should_throw_RetryCountOverException(HttpStatusCode statusCode, string jsonString)
        {
            //arrange
            var response = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(jsonString, Encoding.UTF8, "application/json"),
            };

            var obj = Create(response);

            //act
            var actual = Record.ExceptionAsync(async () => await obj.GetAsync<object>("_", new[] { "_" }, CancellationToken.None));

            //assert
            Assert.IsType<RetryCountOverException>(actual.Result);
        }

        [Fact]
        public void GetAsync_should_throw_ZaifApiException()
        {
            //arrange
            var jsonString = @"{ ""error"": ""api errer raised."" }";

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonString, Encoding.UTF8, "application/json"),
            };

            var obj = Create(response);

            //act
            var actual = Record.ExceptionAsync(async () => await obj.GetAsync<object>("_", new[] { "_" }, CancellationToken.None));

            //assert
            Assert.IsType<ZaifApiException>(actual.Result);
        }

        [Fact]
        public async void PostAsync_should_success()
        {
            //arrange
            var jsonString = @"{ ""success"": 1, ""return"": { ""key"": ""value"" } }";

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonString, Encoding.UTF8, "application/json"),
            };

            var obj = Create(response, VALID_CREDENTIAL, VALID_CREDENTIAL);

            //act
            var actual = await obj.PostAsync<object>("_", null, CancellationToken.None);

            //assert
            Assert.NotNull(actual);
        }

        [Theory]
        [MemberData(nameof(InvalidCredentialData))]
        public void PostAsync_should_throw_CredentialFormatException(string apiKey, string apiSecret)
        {
            // arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
            };

            var obj = Create(response, apiKey, apiSecret);

            //act
            var actual = Record.ExceptionAsync(async () => await obj.PostAsync<object>("_", null, CancellationToken.None));

            //assert
            Assert.IsType<CredentialFormatException>(actual.Result);
        }

        [Theory]
        [MemberData(nameof(RetryData))]
        public void PostAsync_should_throw_RetryCountOverException(HttpStatusCode statusCode, string jsonString)
        {
            //arrange
            var response = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(jsonString, Encoding.UTF8, "application/json"),
            };

            var obj = Create(response, VALID_CREDENTIAL, VALID_CREDENTIAL);

            //act
            var actual = Record.ExceptionAsync(async () => await obj.PostAsync<object>("_", null, CancellationToken.None));

            //assert
            Assert.IsType<RetryCountOverException>(actual.Result);
        }

        [Fact]
        public void PostAsync_should_throw_ZaifApiException()
        {
            //arrange
            var jsonString = @"{ ""success"": 0 , ""error"": ""api errer raised."" }";

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonString, Encoding.UTF8, "application/json"),
            };

            var obj = Create(response, VALID_CREDENTIAL, VALID_CREDENTIAL);

            //act
            var actual = Record.ExceptionAsync(async () => await obj.PostAsync<object>("_", null, CancellationToken.None));

            //assert
            Assert.IsType<ZaifApiException>(actual.Result);
        }

        [Fact]
        public void PostAsync_should_throw_HttpRequestException()
        {
            // arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
            };

            var obj = Create(response, VALID_CREDENTIAL, VALID_CREDENTIAL);

            //act
            var actual = Record.ExceptionAsync(async () => await obj.PostAsync<object>("_", null, CancellationToken.None));

            //assert
            Assert.IsType<HttpRequestException>(actual.Result);
        }
    }
}
