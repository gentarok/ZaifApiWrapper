using System.Net.Http;

namespace ZaifApiWrapper
{
    /// <summary>
    /// <see cref="HttpClient"/>オブジェクトのアクセサ用のインターフェイス
    /// </summary>
    internal interface IHttpClientAccessor
    {
        /// <summary>
        /// <see cref="HttpClient"/>オブジェクト
        /// </summary>
        HttpClient Client { get; }
    }
}
