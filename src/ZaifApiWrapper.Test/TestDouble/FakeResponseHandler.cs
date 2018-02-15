using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ZaifApiWrapper.Test.TestDouble
{
    class FakeResponseHandler : DelegatingHandler
    {
        private readonly Dictionary<Uri, HttpResponseMessage> FakeResponses = new Dictionary<Uri, HttpResponseMessage>();

        public void AddFakeResponse(Uri uri, HttpResponseMessage responseMessage) =>
            FakeResponses.Add(uri, responseMessage);

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            SendCount++;

            if (FakeResponses.ContainsKey(request.RequestUri))
                return Task.FromResult(FakeResponses[request.RequestUri]);

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound) { RequestMessage = request });
        }

        public int SendCount { get; private set; } = 0;
    }
}
