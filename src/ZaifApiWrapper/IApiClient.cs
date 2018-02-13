using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ZaifApiWrapper
{
    /// <summary>
    /// APIを利用するためのHttpClientのラッパーのインターフェイス
    /// </summary>
    internal interface IApiClient
    {
        /// <summary>
        /// HTTP Getメソッドでデータを取得します
        /// </summary>
        /// <typeparam name="T">JSONで取得したデータをマップする型</typeparam>
        /// <param name="method">APIメソッド名</param>
        /// <param name="arguments">APIメソッド引数の配列</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns>APIで取得したデータ</returns>
        Task<T> GetAsync<T>(string method, string[] arguments, CancellationToken token);

        /// <summary>
        /// HTTP Postメソッドでデータを取得します
        /// </summary>
        /// <typeparam name="T">JSONで取得したデータをマップする型</typeparam>
        /// <param name="method">APIメソッド名</param>
        /// <param name="parameters">APIパラメータのディクショナリ</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns>APIで取得したデータ</returns>
        Task<T> PostAsync<T>(string method, IDictionary<string, string> parameters, CancellationToken token);
    }
}
