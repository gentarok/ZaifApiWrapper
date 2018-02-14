﻿using System;
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
        /// <returns>
        /// <see cref="GroupsResponse"/>のコレクション
        /// </returns>
        public Task<IEnumerable<GroupsResponse>> GroupsAsync(string group = "all") =>
            GroupsAsync(group, CancellationToken.None);

        /// <summary>
        /// 先物取引の情報を取得します。(all/active)
        /// </summary>
        /// <param name="group">all or active</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns>
        /// <see cref="GroupsResponse"/>のコレクション
        /// </returns>
        /// <exception cref="ArgumentException">'group'は'all'または'active'を指定してください。</exception>
        public Task<IEnumerable<GroupsResponse>> GroupsAsync(string group, CancellationToken token)
        {
            if (group != "all" || group != "active") throw new ArgumentException("'group'は'all'または'active'を指定してください。", nameof(group));

            return _client.GetAsync<IEnumerable<GroupsResponse>>(
                nameof(GroupsAsync).ToApiMethodName(), new[] { group }, token);
        }

        /// <summary>
        /// 先物取引の情報を取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <returns>
        /// <see cref="GroupsResponse"/>のコレクション
        /// </returns>
        public Task<IEnumerable<GroupsResponse>> GroupsAsync(int groupId) =>
            GroupsAsync(groupId, CancellationToken.None);

        /// <summary>
        /// 先物取引の情報を取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns>
        /// <see cref="GroupsResponse"/>のコレクション
        /// </returns>
        public Task<IEnumerable<GroupsResponse>> GroupsAsync(int groupId, CancellationToken token) =>
            _client.GetAsync<IEnumerable<GroupsResponse>>(
                nameof(GroupsAsync).ToApiMethodName(), new[] { groupId.ToString() }, token);

        /// <summary>
        /// 現在の終値を取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <returns>
        /// <see cref="LastPriceResponse"/>のコレクション
        /// </returns>
        public Task<LastPriceResponse> LastPriceAsync(int groupId, string currencyPair) =>
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
        public Task<LastPriceResponse> LastPriceAsync(int groupId, string currencyPair, CancellationToken token) =>
            _client.GetAsync<LastPriceResponse>(
                nameof(LastPriceAsync).ToApiMethodName(), new[] { groupId.ToString(), currencyPair }, token);

        /// <summary>
        /// ティッカーを取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <returns><see cref="TickerResponse"/>オブジェクト。</returns>
        public Task<TickerResponse> TickerAsync(int groupId, string currencyPair) =>
            TickerAsync(groupId, currencyPair, CancellationToken.None);
            
        /// <summary>
        /// ティッカーを取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="TickerResponse"/>オブジェクト。</returns>
        public Task<TickerResponse> TickerAsync(int groupId, string currencyPair, CancellationToken token) =>
            _client.GetAsync<TickerResponse>(
                nameof(TickerAsync).ToApiMethodName(), new[] { groupId.ToString(), currencyPair }, token);

        /// <summary>
        /// 全ての取引履歴を取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <returns>
        /// <see cref="TradesResponse"/>のコレクション
        /// </returns>
        public Task<IEnumerable<TradesResponse>> TradesAsync(int groupId, string currencyPair) =>
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
        public Task<IEnumerable<TradesResponse>> TradesAsync(int groupId, string currencyPair, CancellationToken token) =>
             _client.GetAsync<IEnumerable<TradesResponse>>(
                nameof(TradesAsync).ToApiMethodName(), new[] { groupId.ToString(), currencyPair }, token);

        /// <summary>
        /// 板情報を取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <returns><see cref="DepthResponse"/>オブジェクト。</returns>
        public Task<DepthResponse> DepthAsync(int groupId, string currencyPair) =>
            DepthAsync(groupId, currencyPair, CancellationToken.None);

        /// <summary>
        /// 板情報を取得します。
        /// </summary>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="DepthResponse"/>オブジェクト。</returns>
        public Task<DepthResponse> DepthAsync(int groupId, string currencyPair, CancellationToken token) =>
            _client.GetAsync<DepthResponse>(
                nameof(DepthAsync).ToApiMethodName(), new[] { groupId.ToString(), currencyPair }, token);
    }
}
