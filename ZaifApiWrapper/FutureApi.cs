using System;
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
        //TODO: last_priceのドキュメントには'all'または'active'を指定するとgroup_id付きのデータのコレクションが取得できると書いてあるが、実際にやってみるとエラーになったため保留。

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
        /// 先物取引の情報を取得します。(all/active)
        /// </summary>
        /// <param name="group">all or active</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <param name="progress"><see cref="IProgress{T}"/>オブジェクト。</param>
        /// <returns>
        /// <see cref="GroupsResponse"/>のコレクション
        /// </returns>
        public Task<IEnumerable<GroupsResponse>> GroupsAsync(string group = "all",
            CancellationToken token = default, IProgress<RetryReport> progress = null)
        {
            group.ThrowIfValueInvalid(Definitions.LeverageGroups, nameof(group));

            return _client.GetAsync<IEnumerable<GroupsResponse>>(
                nameof(GroupsAsync).ToApiMethodName(), new[] { group }, token, progress);
        }

        /// <summary>
        /// 先物取引の情報を取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <param name="progress"><see cref="IProgress{T}"/>オブジェクト。</param>
        /// <returns>
        /// <see cref="GroupsResponse"/>のコレクション
        /// </returns>
        public Task<IEnumerable<GroupsResponse>> GroupsAsync(int groupId, 
            CancellationToken token = default, IProgress<RetryReport> progress = null) =>
            _client.GetAsync<IEnumerable<GroupsResponse>>(
                nameof(GroupsAsync).ToApiMethodName(), new[] { groupId.ToString() }, token, progress);

        /// <summary>
        /// 現在の終値を取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <param name="progress"><see cref="IProgress{T}"/>オブジェクト。</param>
        /// <returns>
        /// <see cref="LastPriceResponse"/>のコレクション
        /// </returns>
        public Task<LastPriceResponse> LastPriceAsync(int groupId, string currencyPair,
            CancellationToken token = default, IProgress<RetryReport> progress = null)
        {
            currencyPair.ThrowIfIsNullOrWhiteSpace(nameof(currencyPair));

            return _client.GetAsync<LastPriceResponse>(
                nameof(LastPriceAsync).ToApiMethodName(), new[] { groupId.ToString(), currencyPair }, token, progress);
        }

        /// <summary>
        /// ティッカーを取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <param name="progress"><see cref="IProgress{T}"/>オブジェクト。</param>
        /// <returns><see cref="TickerResponse"/>オブジェクト。</returns>
        public Task<TickerResponse> TickerAsync(int groupId, string currencyPair, 
            CancellationToken token = default, IProgress<RetryReport> progress = null)
        {
            currencyPair.ThrowIfIsNullOrWhiteSpace(nameof(currencyPair));

            return _client.GetAsync<TickerResponse>(
                nameof(TickerAsync).ToApiMethodName(), new[] { groupId.ToString(), currencyPair }, token, progress);
        }

        /// <summary>
        /// 全ての取引履歴を取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <param name="progress"><see cref="IProgress{T}"/>オブジェクト。</param>
        /// <returns>
        /// <see cref="TradesResponse"/>のコレクション
        /// </returns>
        public Task<IEnumerable<TradesResponse>> TradesAsync(int groupId, string currencyPair, 
            CancellationToken token = default, IProgress<RetryReport> progress = null)
        {
            currencyPair.ThrowIfIsNullOrWhiteSpace(nameof(currencyPair));

            return _client.GetAsync<IEnumerable<TradesResponse>>(
                nameof(TradesAsync).ToApiMethodName(), new[] { groupId.ToString(), currencyPair }, token, progress);
        }

        /// <summary>
        /// 板情報を取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <param name="progress"><see cref="IProgress{T}"/>オブジェクト。</param>
        /// <returns><see cref="DepthResponse"/>オブジェクト。</returns>
        public Task<DepthResponse> DepthAsync(int groupId, string currencyPair, 
            CancellationToken token = default, IProgress<RetryReport> progress = null)
        {
            currencyPair.ThrowIfIsNullOrWhiteSpace(nameof(currencyPair));

            return _client.GetAsync<DepthResponse>(
                nameof(DepthAsync).ToApiMethodName(), new[] { groupId.ToString(), currencyPair }, token, progress);
        }

        /// <summary>
        /// 確定したスワップポイントの履歴を取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="page">1～100までの値。</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <param name="progress"><see cref="IProgress{T}"/>オブジェクト。</param>
        /// <returns><see cref="SwapHistoryResponse"/>オブジェクト。</returns>
        public Task<IEnumerable<SwapHistoryResponse>> SwapHistoryAsync(int groupId, string currencyPair, int? page,
            CancellationToken token = default, IProgress<RetryReport> progress = null)
        {
            currencyPair.ThrowIfIsNullOrWhiteSpace(nameof(currencyPair));

            return _client.GetAsync<IEnumerable<SwapHistoryResponse>>(
                nameof(SwapHistoryAsync).ToApiMethodName(), new[] { groupId.ToString(), currencyPair, page.ToString() }, token, progress);
        }
    }
}
