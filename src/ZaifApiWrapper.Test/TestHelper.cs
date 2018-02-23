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

        // 指定したHttpResponceMessageを返すIApiClientのファクトリ（正しい形式のAPI key, API secretを使用）
        internal static IApiClient CreateApiClientWithMockHttpAccessor(HttpResponseMessage httpRequestMessage = null)
            => CreateApiClientWithMockHttpAccessor(VALID_CREDENTIAL, VALID_CREDENTIAL, httpRequestMessage);

        // 指定したHttpResponceMessageを返すIApiClientのファクトリ（API key, API secretを指定可能）
        internal static IApiClient CreateApiClientWithMockHttpAccessor(string apiKey, string apiSecret, HttpResponseMessage httpRequestMessage = null)
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
                ApiErrorRetryInterval = 0,
            };

            return new ApiClient("http://localhost", option);
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
