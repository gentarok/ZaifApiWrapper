using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ZaifApiWrapper
{
    /// <summary>
    /// APIを利用するためのHttpClientのラッパー
    /// </summary>
    internal class ApiClient : IApiClient
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            // JObjectにデシリアライズする場合に必要（既定ではJSONのfloatが.NETのdoubleに変換される）
            // DeserializeObject<T>で直接decimal型を持つクラスを指定する場合はなくても可？
            FloatParseHandling = FloatParseHandling.Decimal,
        };

        // HttpClientはテスト時に差し替えられるようにアクセサ経由で使う
        private readonly IHttpClientAccessor _accessor;

        private readonly string _apiKey;
        private readonly string _apiSecret;
        private readonly string _endpoint;
        private readonly int _maxRetry;
        private readonly int _httpErrorRetryInterval;
        private readonly int _apiTimeoutRetryInterval;

        private static long _nonce = DateTime.Now.ToUnixTimeStamp();

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="endpoint">APIのエンドポイント</param>
        /// <param name="option"><see cref="ApiClientOption"/>オブジェクト。</param>
        internal ApiClient(string endpoint, ApiClientOption option)
        {
            _endpoint = endpoint;

            _apiKey = option.ApiKey;
            _apiSecret = option.ApiSecret;
            _accessor = option.HttpClientAcessor;
            _maxRetry = option.MaxRetry;
            _httpErrorRetryInterval = option.HttpErrorRetryInterval;
            _apiTimeoutRetryInterval = option.ApiTimeoutRetryInterval;
        }

        // 再試行対象のHTTPステータスコード
        private static readonly HttpStatusCode[] StatusToRetry = new[] {
            HttpStatusCode.BadGateway,
            HttpStatusCode.ServiceUnavailable,
            HttpStatusCode.GatewayTimeout,
        };

        /// <summary>
        /// HTTP Getメソッドでデータを取得します
        /// </summary>
        /// <typeparam name="T">JSONで取得したデータをマップする型</typeparam>
        /// <param name="method">APIメソッド名</param>
        /// <param name="arguments">APIメソッド引数の配列</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns>APIで取得したデータ</returns>
        public async Task<T> GetAsync<T>(string method, string[] arguments, CancellationToken token)
        {
            var args = string.Join("/", arguments);
            var uri = new Uri($"{_endpoint.TrimEnd('/')}/{method}/{args}");
            Debug.WriteLine($"Uri:{uri}");


            int? interval = null;
            int count = 0;

            while (true)
            {
                token.ThrowIfCancellationRequested();

                if (count >= _maxRetry) throw new ZaifApiException("最大試行回数を超えました。");
                if (interval.HasValue) await Task.Delay(interval.Value, token).ConfigureAwait(false);

                string jsonString;

                using (var res = await _accessor.Client.GetAsync(uri, token).ConfigureAwait(false))
                {
                    Debug.WriteLine($"StatusCode:{res.StatusCode}");
                    if (!res.IsSuccessStatusCode && StatusToRetry.Contains(res.StatusCode))
                    {
                        interval = _httpErrorRetryInterval;
                        count++;
                        Debug.WriteLine($"Retry:{count}");
                        continue;
                    }
                    // 上記のケース以外でステータス異常なら例外とする
                    res.EnsureSuccessStatusCode();

                    jsonString = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
                    Debug.WriteLine($"jsonString:{ jsonString}");
                }

                return JsonConvert.DeserializeObject<T>(jsonString, SerializerSettings);
            }
        }

        /// <summary>
        /// HTTP Postメソッドでデータを取得します
        /// </summary>
        /// <typeparam name="T">JSONで取得したデータをマップする型</typeparam>
        /// <param name="method">APIメソッド名</param>
        /// <param name="parameters">APIパラメータのディクショナリ</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns>APIで取得したデータ</returns>
        public async Task<T> PostAsync<T>(string method, IDictionary<string, string> parameters, CancellationToken token)
        {
            if (_apiKey == null) throw new ZaifApiException("API Keyがセットされていません。");
            if (_apiSecret == null) throw new ZaifApiException("API Secretがセットされていません。");

            parameters = parameters ?? new Dictionary<string, string>();
            parameters.Add("method", method);

            var uri = new Uri(_endpoint);
            Debug.WriteLine($"Uri:{uri}");

            int? interval = null;
            int count = 0;

            while (true)
            {
                token.ThrowIfCancellationRequested();

                if (count >= _maxRetry) throw new ZaifApiException("最大試行回数を超えました。");
                if (interval.HasValue) await Task.Delay(interval.Value, token).ConfigureAwait(false);

                // マルチスレッドで利用されてもnonceが重複しないようにする
                Interlocked.Increment(ref _nonce);
                Debug.WriteLine($"nonce:{_nonce}");

                var content = new FormUrlEncodedContent(
                    new Dictionary<string, string>(parameters)
                    {
                        { "nonce", _nonce.ToString() }
                    });

                var sign = await GenerateSignature(content).ConfigureAwait(false);

                content.Headers.Add("key", _apiKey);
                content.Headers.Add("sign", sign);

                string jsonString;

                using (var res = await _accessor.Client.PostAsync(uri, content, token).ConfigureAwait(false))
                {
                    Debug.WriteLine($"StatusCode:{res.StatusCode}");
                    if (!res.IsSuccessStatusCode && StatusToRetry.Contains(res.StatusCode))
                    {
                        interval = _httpErrorRetryInterval;
                        count++;
                        Debug.WriteLine($"Retry(HttpError):{count}");
                        continue;
                    }
                    // 上記のケース以外でステータス異常なら例外とする
                    res.EnsureSuccessStatusCode();

                    jsonString = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
                    Debug.WriteLine($"jsonString:{ jsonString}");
                }

                // 成功・失敗で型が変わるためチェックしてから処理を行う
                var obj = JsonConvert.DeserializeObject<JObject>(jsonString, SerializerSettings);

                // API処理結果の確認
                if (obj["success"] != null && obj["success"].ToString() != "1")
                {
                    // 失敗の場合は"return"ではなく"error"に値が入る
                    //（APIリファレンスには嘘が書いてあるので注意）
                    var error = obj["error"].ToString();
                    if (Regex.IsMatch(error, "please try later", RegexOptions.IgnoreCase))
                    {
                        interval = _apiTimeoutRetryInterval;
                        count++;
                        Debug.WriteLine($"Retry(ApiTimeout):{count}");
                        continue;
                    }
                    throw new ZaifApiException(error);
                }

                return obj["return"].ToObject<T>();
            }
        }

        private async Task<string> GenerateSignature(FormUrlEncodedContent content)
        {
            var req = await content.ReadAsStringAsync().ConfigureAwait(false);
            var buffer = Encoding.UTF8.GetBytes(req);
            var key = Encoding.UTF8.GetBytes(_apiSecret);
            var hash = new HMACSHA512(key).ComputeHash(buffer);
            return BitConverter.ToString(hash).ToLower().Replace("-", "");
        }
    }
}
