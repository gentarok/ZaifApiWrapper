using System;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace ZaifApiWrapper
{
    /// <summary>
    /// HttpClientのシングルトンへのアクセサ
    /// </summary>
    /// <seealso cref="IHttpClientAccessor" />
    internal class SingleHttpClientAccessor : IHttpClientAccessor
    {
        // HttpClientは生成の都度新しいソケットをopenする。
        // オブジェクトをDisposeしてもソケットはすぐには開放されず、リクエストの回数が多と大量のソケットを消費する。
        // そのため、同一ホストに接続する場合はひとつのインスタンスを使いまわす事が推奨されている。
        // <see href="https://qiita.com/nskhara/items/b7c31d60531ffbe29537"/>
        private static readonly Lazy<HttpClient> _httpClient = new Lazy<HttpClient>(
            CreateHttpClient, LazyThreadSafetyMode.ExecutionAndPublication);

        private static HttpClient CreateHttpClient()
        {
            // staticに保持することでDNSの変更が反映されない可能性があるため、
            // コネクションのリースタイムアウト設定と併せて行う
            var uri = new Uri(ApiUrl.Base);
            var timeout = 60 * 1000;
            ServicePointManager.FindServicePoint(uri).ConnectionLeaseTimeout = timeout;
            return new HttpClient();
        }

        /// <summary>
        /// <see cref="HttpClient"/>オブジェクト
        /// </summary>
        public HttpClient Client => _httpClient.Value;
    }
}
