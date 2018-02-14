using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZaifApiWrapper.PublicData;

namespace ZaifApiWrapper
{
    /// <summary>
    /// 現物公開API
    /// </summary>
    /// <remarks>
    /// <see href="http://techbureau-api-document.readthedocs.io/ja/latest/public/index.html"/>
    /// </remarks>
    public class PublicApi
    {
        private readonly IApiClient _client;

        /// <summary>
        /// 初期化
        /// </summary>
        public PublicApi()
            : this(new ApiClientOption()) { }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="option"><see cref="ApiClientOption"/>オブジェクト。</param>
        public PublicApi(ApiClientOption option)
            : this(new ApiClient(ApiUrl.Public, option)) { }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="client"><see cref="IApiClient"/>具象オブジェクト。</param>
        internal PublicApi(IApiClient client) => _client = client;

        /// <summary>
        /// 通貨情報を取得します。
        /// </summary>
        /// <param name="currency">currency</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns>
        /// <see cref="CurrenciesResponse"/>のコレクション
        /// </returns>
        public Task<IEnumerable<CurrenciesResponse>> CurrenciesAsync(string currency = "all", CancellationToken token = default)
        {
            currency.ThrowArgumentExceptionIfNullOrWhiteSpace(nameof(currency));

            return _client.GetAsync<IEnumerable<CurrenciesResponse>>(
                nameof(CurrenciesAsync).ToApiMethodName(), new[] { currency }, token);
        }

        /// <summary>
        /// 通貨ペア情報を取得します。
        /// </summary>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns>
        /// <see cref="CurrencyPairsResponse"/>のコレクション
        /// </returns>
        public Task<IEnumerable<CurrencyPairsResponse>> CurrencyPairsAsync(string currencyPair = "all", CancellationToken token = default)
        {
            currencyPair.ThrowArgumentExceptionIfNullOrWhiteSpace(nameof(currencyPair));

            return _client.GetAsync<IEnumerable<CurrencyPairsResponse>>(
                nameof(CurrencyPairsAsync).ToApiMethodName(), new[] { currencyPair }, token);
        }
      
        /// <summary>
        /// 現在の終値を取得します
        /// </summary>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="LastPriceResponse"/>オブジェクト。</returns>
        public Task<LastPriceResponse> LastPriceAsync(string currencyPair, CancellationToken token = default)
        {
            currencyPair.ThrowArgumentExceptionIfNullOrWhiteSpace(nameof(currencyPair));

            return _client.GetAsync<LastPriceResponse>(
                nameof(LastPriceAsync).ToApiMethodName(), new[] { currencyPair }, token);
        }
        
        /// <summary>
        /// ティッカーを取得します。
        /// </summary>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="TickerResponse"/>オブジェクト。</returns>
        public Task<TickerResponse> TickerAsync(string currencyPair, CancellationToken token = default)
        {
            currencyPair.ThrowArgumentExceptionIfNullOrWhiteSpace(nameof(currencyPair));

            return _client.GetAsync<TickerResponse>(
                nameof(TickerAsync).ToApiMethodName(), new[] { currencyPair }, token);
        }

        /// <summary>
        /// 全ての取引履歴を取得します。
        /// </summary>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns>
        /// <see cref="TradesResponse"/>のコレクション
        /// </returns>
        public Task<IEnumerable<TradesResponse>> TradesAsync(string currencyPair, CancellationToken token = default)
        {
            currencyPair.ThrowArgumentExceptionIfNullOrWhiteSpace(nameof(currencyPair));

            return _client.GetAsync<IEnumerable<TradesResponse>>(
                nameof(TradesAsync).ToApiMethodName(), new[] { currencyPair }, token);
        }
        
        /// <summary>
        /// 板情報を取得します。
        /// </summary>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="DepthResponse"/>オブジェクト。</returns>
        public Task<DepthResponse> DepthAsync(string currencyPair, CancellationToken token = default)
        {
            currencyPair.ThrowArgumentExceptionIfNullOrWhiteSpace(nameof(currencyPair));

            return _client.GetAsync<DepthResponse>(
                nameof(DepthAsync).ToApiMethodName(), new[] { currencyPair }, token);
        }
    }
}
