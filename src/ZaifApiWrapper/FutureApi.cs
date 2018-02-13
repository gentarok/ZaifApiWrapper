using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZaifApiWrapper.FutureData;

namespace ZaifApiWrapper
{
    /// <summary>
    /// 先物取引API
    /// </summary>
    /// <remarks>
    /// <see href="http://techbureau-api-document.readthedocs.io/ja/latest/public_futures/index.html"/>
    /// </remarks>
    public class FutureApi
    {
        private readonly IApiClient _client;

        /// <summary>
        /// 初期化
        /// </summary>
        public FutureApi() 
            : this(new ApiClientOption()) { }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="option"><see cref="ApiClientOption"/>オブジェクト。</param>
        public FutureApi(ApiClientOption option) 
            : this(new ApiClient(ApiUrl.Future, option)) { }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="client"><see cref="IApiClient"/>具象オブジェクト。</param>
        internal FutureApi(IApiClient client) => _client = client;

        /// <summary>
        /// 先物取引の情報を取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <returns>
        /// <see cref="GroupsResponse"/>のコレクション
        /// </returns>
        public Task<IEnumerable<GroupsResponse>> GroupsAsync(string groupId = "all") =>
            GroupsAsync(groupId, CancellationToken.None);

        /// <summary>
        /// 先物取引の情報を取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns>
        /// <see cref="GroupsResponse"/>のコレクション
        /// </returns>
        public Task<IEnumerable<GroupsResponse>> GroupsAsync(string groupId, CancellationToken token) =>
            _client.GetAsync<IEnumerable<GroupsResponse>>(
                nameof(GroupsAsync).ToApiMethodName(), new[] { groupId }, token);

        /// <summary>
        /// 現在の終値を取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <returns>
        /// <see cref="LastPriceResponse"/>のコレクション
        /// </returns>
        public Task<IEnumerable<LastPriceResponse>> LastPriceAsync(string groupId = "all", string currencyPair = "") =>
            LastPriceAsync(groupId, currencyPair, CancellationToken.None);

        /// <summary>
        /// 現在の終値を取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns>
        /// <see cref="LastPriceResponse"/>のコレクション
        /// </returns>
        public Task<IEnumerable<LastPriceResponse>> LastPriceAsync(string groupId, string currencyPair, CancellationToken token) =>
            _client.GetAsync<IEnumerable<LastPriceResponse>>(
                nameof(LastPriceAsync).ToApiMethodName(), new[] { groupId, currencyPair }, token);

        /// <summary>
        /// ティッカーを取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <returns><see cref="TickerResponse"/>オブジェクト。</returns>
        public Task<TickerResponse> TickerAsync(string groupId, string currencyPair) =>
            TickerAsync(groupId, currencyPair, CancellationToken.None);
            
        /// <summary>
        /// ティッカーを取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="TickerResponse"/>オブジェクト。</returns>
        public Task<TickerResponse> TickerAsync(string groupId, string currencyPair, CancellationToken token) =>
            _client.GetAsync<TickerResponse>(
                nameof(TickerAsync).ToApiMethodName(), new[] { groupId, currencyPair }, token);

        /// <summary>
        /// 全ての取引履歴を取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <returns>
        /// <see cref="TradesResponse"/>のコレクション
        /// </returns>
        public Task<IEnumerable<TradesResponse>> TradesAsync(string groupId, string currencyPair) =>
             TradesAsync(groupId, currencyPair, CancellationToken.None);

        /// <summary>
        /// 全ての取引履歴を取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns>
        /// <see cref="TradesResponse"/>のコレクション
        /// </returns>
        public Task<IEnumerable<TradesResponse>> TradesAsync(string groupId, string currencyPair, CancellationToken token) =>
             _client.GetAsync<IEnumerable<TradesResponse>>(
                nameof(TradesAsync).ToApiMethodName(), new[] { groupId, currencyPair }, token);

        /// <summary>
        /// 板情報を取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <returns><see cref="DepthResponse"/>オブジェクト。</returns>
        public Task<DepthResponse> DepthAsync(string groupId, string currencyPair) =>
            DepthAsync(groupId, currencyPair, CancellationToken.None);

        /// <summary>
        /// 板情報を取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="DepthResponse"/>オブジェクト。</returns>
        public Task<DepthResponse> DepthAsync(string groupId, string currencyPair, CancellationToken token) =>
            _client.GetAsync<DepthResponse>(
                nameof(DepthAsync).ToApiMethodName(), new[] { groupId, currencyPair }, token);
    }
}
