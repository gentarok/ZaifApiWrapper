using System.Net.Http;

namespace ZaifApiWrapper.Test.TestDouble
{
    class FakeHttpClientAccessor : IHttpClientAccessor
    {
        private HttpClient _client;

        public FakeHttpClientAccessor(FakeResponseHandler handler) =>
            _client = new HttpClient(handler);

        public HttpClient Client => _client;
    }
}
