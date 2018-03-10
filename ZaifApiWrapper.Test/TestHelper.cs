using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZaifApiWrapper.Test
{
    // テストヘルパー
    internal static class TestHelper
    {
        // 正しい形式のAPI key, API secret
        internal const string VALID_CREDENTIAL = "00000000-0000-0000-0000-000000000000";

        // ApiClientOptionのファクトリ
        internal static ApiClientOption CreateApiClientOption(IHttpClientAccessor accessor)
            => CreateApiClientOption(VALID_CREDENTIAL, VALID_CREDENTIAL, accessor);

        // ApiClientOptionのファクトリ
        internal static ApiClientOption CreateApiClientOption(string apiKey, string apiSecret, IHttpClientAccessor accessor)
            => new ApiClientOption(accessor)
            {
                ApiKey = apiKey,
                ApiSecret = apiSecret,
                HttpErrorRetryInterval = 0,
                ApiErrorRetryInterval = 0,
                PostRequestInterval = 0, //テストで無駄に待たないように
            };

        // 指定したHttpResponceMessageを返すIApiClientのファクトリ（正しい形式のAPI key, API secretを使用）
        internal static IApiClient CreateApiClientWithMockHttpAccessor(HttpResponseMessage httpResponseMessage = null)
        {
            var accessor = CreateHttpClientAccessor(httpResponseMessage);
            var option = CreateApiClientOption(VALID_CREDENTIAL, VALID_CREDENTIAL, accessor);
            return CreateApiClientWithMockHttpAccessor(option);
        }

        // IApiClientのファクトリ（ApiClientOptionを指定可）
        internal static IApiClient CreateApiClientWithMockHttpAccessor(ApiClientOption option)
            => new ApiClient("http://localhost", option);

        // 指定したHttpResponceMessageを返すIHttpClientAccessorのファクトリ
        internal static IHttpClientAccessor CreateHttpClientAccessor(HttpResponseMessage httpResponseMessage)
        {
            var handler = new Mock<DelegatingHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(() => Task.FromResult(httpResponseMessage));

            var accessor = new Mock<IHttpClientAccessor>();
            accessor.SetupGet((x) => x.Client).Returns(new HttpClient(handler.Object));

            return accessor.Object;
        }

        // 指定したJSONを返すHttpResponseMessageのファクトリ（HttpStatusCode.OK）
        internal static HttpResponseMessage CreateJsonResponse(string jsonString)
            => CreateJsonResponse(HttpStatusCode.OK, jsonString);

        // 指定したJSONを返すHttpResponseMessageのファクトリ（HttpStatusCode指定可能）
        internal static HttpResponseMessage CreateJsonResponse(HttpStatusCode httpStatusCode, string jsonString)
        {
            return new HttpResponseMessage
            {
                StatusCode = httpStatusCode,
                Content = new StringContent(jsonString, Encoding.UTF8, "application/json"),
            };
        }
    }
}
