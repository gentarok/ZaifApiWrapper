using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        private const string CREDENTIAL_PATTERN = "^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$";

        private static readonly Regex CredentialMatcher = new Regex(CREDENTIAL_PATTERN);
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private static long _nonce = DateTime.Now.ToUnixTimeStamp();

        private readonly string _endpoint;
        private readonly ApiClientOption _option;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="endpoint">APIのエンドポイント</param>
        /// <param name="option"><see cref="ApiClientOption"/>オブジェクト。</param>
        internal ApiClient(string endpoint, ApiClientOption option)
        {
            _endpoint = endpoint;
            _option = option;
        }

        /// <summary>
        /// HTTP Getメソッドでデータを取得します
        /// </summary>
        /// <typeparam name="T">JSONで取得したデータをマップする型</typeparam>
        /// <param name="method">APIメソッド名</param>
        /// <param name="arguments">APIメソッド引数の配列</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <param name="progress"><see cref="IProgress{T}"/>オブジェクト。</param>
        /// <returns>APIで取得したデータ</returns>
        /// <exception cref="RetryCountOverException">最大試行回数を超えました。</exception>
        /// <exception cref="ZaifApiException">error.</exception>
        public async Task<T> GetAsync<T>(string method, string[] arguments, CancellationToken token, IProgress<RetryReport> progress)
        {
            var args = string.Join("/", arguments);
            var uri = new Uri($"{_endpoint.TrimEnd('/')}/{method}/{args}");
            Debug.WriteLine($"Uri:{uri}");

            int? interval = null;
            int count = 0;

            while (true)
            {
                token.ThrowIfCancellationRequested();

                if (count >= _option.MaxRetry)
                    throw new RetryCountOverException("最大試行回数を超えました。");

                if (interval.HasValue)
                    await Task.Delay(interval.Value, token).ConfigureAwait(false);

                var res = await _option.HttpClientAcessor.Client.GetAsync(uri, token).ConfigureAwait(false);

                Debug.WriteLine($"StatusCode:{res.StatusCode}");
                if (!res.IsSuccessStatusCode && _option.HttpStatusCodesToRetry.Contains(res.StatusCode))
                {
                    interval = _option.HttpErrorRetryInterval;
                    count++;
                    Debug.WriteLine($"Retry(HttpError):{count}");

                    progress?.Report(new RetryReport(count));
                    continue;
                }

                // 上記のケース以外でステータス異常なら例外とする
                res.EnsureSuccessStatusCode();

                var jsonString = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
                Debug.WriteLine($"jsonString:{jsonString}");

                // 成功・失敗で型が変わるためチェックしてから処理を行う
                var tmp = JsonConvert.DeserializeObject(jsonString, SerializerSettings);

                if (tmp is JObject obj)
                {
                    // API処理結果の確認
                    // 失敗の場合は {"error": "(message)"} の形で返ってくる
                    //（APIリファレンスには書いてないので注意）
                    if (obj["error"] != null)
                    {
                        var error = obj["error"].ToString();
                        if (Regex.IsMatch(error, _option.ApiErrorMessagePatternToRetry))
                        {
                            interval = _option.ApiErrorRetryInterval;
                            count++;
                            Debug.WriteLine($"Retry(ApiError):{count}");

                            progress?.Report(new RetryReport(count));
                            continue;
                        }

                        throw new ZaifApiException(error);
                    }
                    
                    return obj.ToObject<T>();
                }

                return ((JArray)tmp).ToObject<T>();
            }
        }

        /// <summary>
        /// HTTP Postメソッドでデータを取得します
        /// </summary>
        /// <typeparam name="T">JSONで取得したデータをマップする型</typeparam>
        /// <param name="method">APIメソッド名</param>
        /// <param name="parameters">APIパラメータのディクショナリ</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <param name="progress"><see cref="IProgress{T}"/>オブジェクト。</param>
        /// <returns>APIで取得したデータ</returns>
        /// <exception cref="CredentialFormatException">
        /// API Keyの形式が正しくありません。
        /// or
        /// API Secretの形式が正しくありません。
        /// </exception>
        /// <exception cref="RetryCountOverException">最大試行回数を超えました。</exception>
        /// <exception cref="ZaifApiException">error.</exception>
        public async Task<T> PostAsync<T>(string method, IDictionary<string, string> parameters, CancellationToken token, IProgress<RetryReport> progress)
        {
            if (!CredentialMatcher.IsMatch(_option.ApiKey))
                throw new CredentialFormatException("API Keyの形式が正しくありません。");

            if (!CredentialMatcher.IsMatch(_option.ApiSecret))
                throw new CredentialFormatException("API Secretの形式が正しくありません。");

            parameters = parameters ?? new Dictionary<string, string>();
            parameters.Add("method", method);

            var uri = new Uri(_endpoint);
            Debug.WriteLine($"Uri:{uri}");

            int? interval = null;
            int count = 0;

            while (true)
            {
                token.ThrowIfCancellationRequested();

                if (count >= _option.MaxRetry)
                    throw new RetryCountOverException("最大試行回数を超えました。");

                if (interval.HasValue)
                    await Task.Delay(interval.Value, token).ConfigureAwait(false);

                // マルチスレッドで利用されてもnonceが重複しないようにする
                Interlocked.Increment(ref _nonce);
                Debug.WriteLine($"nonce:{_nonce}");

                var content = new FormUrlEncodedContent(
                    new Dictionary<string, string>(parameters)
                    {
                        { "nonce", _nonce.ToString() }
                    });

                var sign = await GenerateSignature(content).ConfigureAwait(false);

                content.Headers.Add("key", _option.ApiKey);
                content.Headers.Add("sign", sign);

                //NOTE:「nonce not incremented」対策(issue:#19)
                //１．同一APIキーからのPOSTリクエスト時、APIサーバー側の処理終了前に次のリクエストを投げると、
                //　　「nonce not incremented」が返される。そのため、リクエストはスレッド排他的な処理とする必要がある。
                //２．上記処理を行ってもリクエストの間隔が極端に短いと、前の処理が正常終了しているか否かに関わらず
                //　　「nonce not incremented」になるため、強制的に500ms(0.5秒)待つ。
                //　　これにより全てのリクエスト発行後に0.5秒遅延が発生する。
                //　　理想は、複数スレッドで呼ばれてもいずれかのスレッドの処理が完了した直後の次の処理開始前だけ待つ、
                //　　というものだが、良い実装方法が思いつかない。（Timer使えば可能だろうが無駄に複雑になりそう）
                HttpResponseMessage res;
                await _semaphore.WaitAsync(token).ConfigureAwait(false);
                try
                {
                    await Task.Delay(_option.PostRequestInterval, token).ConfigureAwait(false);
                    res = await _option.HttpClientAcessor.Client.PostAsync(uri, content, token).ConfigureAwait(false);
                }
                finally
                {
                    _semaphore.Release();
                }

                Debug.WriteLine($"StatusCode:{res.StatusCode}");
                if (!res.IsSuccessStatusCode && _option.HttpStatusCodesToRetry.Contains(res.StatusCode))
                {
                    interval = _option.HttpErrorRetryInterval;
                    count++;
                    Debug.WriteLine($"Retry(HttpError):{count}");

                    progress?.Report(new RetryReport(count));
                    continue;
                }

                // 上記のケース以外でステータス異常なら例外とする
                res.EnsureSuccessStatusCode();

                var jsonString = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
                Debug.WriteLine($"jsonString:{jsonString}");

                // 成功・失敗で型が変わるためチェックしてから処理を行う
                var obj = JsonConvert.DeserializeObject<JObject>(jsonString, SerializerSettings);

                // API処理結果の確認
                if (obj["success"] != null && obj["success"].ToString() != "1")
                {
                    // 失敗の場合は"return"ではなく"error"に値が入る
                    //（APIリファレンスには嘘が書いてあるので注意）
                    var error = obj["error"].ToString();
                    if (Regex.IsMatch(error, _option.ApiErrorMessagePatternToRetry))
                    {
                        interval = _option.ApiErrorRetryInterval;
                        count++;
                        Debug.WriteLine($"Retry(ApiError):{count}");

                        progress?.Report(new RetryReport(count));
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
            var key = Encoding.UTF8.GetBytes(_option.ApiSecret);
            var hash = new HMACSHA512(key).ComputeHash(buffer);
            return BitConverter.ToString(hash).ToLower().Replace("-", "");
        }
    }
}
