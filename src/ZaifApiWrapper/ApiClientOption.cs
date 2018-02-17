using System;
using System.Net;

namespace ZaifApiWrapper
{
    /// <summary>
    /// API実行時に指定できるオプション
    /// </summary>
    public class ApiClientOption
    {
        private HttpStatusCode[] _httpStatusCodesToRetry = new[] {
            HttpStatusCode.BadGateway,
            HttpStatusCode.ServiceUnavailable,
            HttpStatusCode.GatewayTimeout,
        };

        /// <summary>
        /// APIキー
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
        /// <summary>
        /// APIシークレット
        /// </summary>
        public string ApiSecret { get; set; } = string.Empty;
        /// <summary>
        /// 最大試行回数（既定値:10）
        /// </summary>
        public int MaxRetry { get; set; } = 10;
        /// <summary>
        /// HTTPエラーが発生した場合の再試行までのインターバル（ms）（既定値:1000ms）
        /// </summary>
        public int HttpErrorRetryInterval { get; set; } = 1000;
        /// <summary>
        /// 再試行対象のHTTPステータスコード（既定値:502,503,504）
        /// </summary>
        public HttpStatusCode[] HttpStatusCodesToRetry {
            get => _httpStatusCodesToRetry;
            set => _httpStatusCodesToRetry = value ?? throw new ArgumentNullException();
        }
        /// <summary>
        /// APIエラーが発生した場合な再試行までのインターバル（ms）（既定値:5000ms）
        /// </summary>
        public int ApiTimeoutRetryInterval { get; set; } = 5000;
        /// <summary>
        /// HttpClientのアクセサ
        /// </summary>
        internal IHttpClientAccessor HttpClientAcessor { get; }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="apiKey">APIキー</param>
        /// <param name="apiSecret">APIシークレット</param>
        public ApiClientOption(string apiKey, string apiSecret)
            : this()
        {
            ApiKey = apiKey;
            ApiSecret = apiSecret;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public ApiClientOption()
            : this(new SingleHttpClientAccessor()) { }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="accessor"><see cref="IHttpClientAccessor"/>の具象オブジェクト。</param>
        internal ApiClientOption(IHttpClientAccessor accessor) => HttpClientAcessor = accessor;
    }
}
