using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZaifApiWrapper.LeverageData;

namespace ZaifApiWrapper
{
    /// <summary>
    /// レバレッジ取引API
    /// </summary>
    /// <remarks>
    /// <see href="http://techbureau-api-document.readthedocs.io/ja/latest/trade_leverage/index.html"/>
    /// </remarks>
    public class LeverageApi
    {
        private readonly IApiClient _client;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="apiKey">APIキー</param>
        /// <param name="apiSecret">APIシークレット</param>
        public LeverageApi(string apiKey, string apiSecret)
            : this(new ApiClientOption(apiKey, apiSecret)) { }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="option"><see cref="ApiClientOption"/>オブジェクト。</param>
        public LeverageApi(ApiClientOption option)
            : this(new ApiClient(ApiUrl.Leverage, option)) { }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="client"><see cref="IApiClient"/>具象オブジェクト。</param>
        internal LeverageApi(IApiClient client) => _client = client;

        /// <summary>
        /// レバレッジ取引のユーザー自身の取引履歴を取得します。
        /// </summary>
        /// <param name="type">type</param>
        /// <param name="groupId">group_id</param>
        /// <param name="from">from</param>
        /// <param name="count">count</param>
        /// <param name="fromId">from_id</param>
        /// <param name="endId">endId</param>
        /// <param name="order">order</param>
        /// <param name="since">since</param>
        /// <param name="end">end</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="GetPositionsResponse"/>のディクショナリ（キーはレバレッジ注文id）</returns>
        /// <exception cref="ArgumentException">'type' が 'futures'の場合、パラメータ 'group_id' は必須です。 - groupId</exception>
        public Task<IDictionary<int, GetPositionsResponse>> GetPositionsAsync(
            string type, int? groupId = null, int? from = null, int? count = null, int? fromId = null, int? endId = null,
            string order = null, long? since = null, long? end = null, string currencyPair = null, CancellationToken token = default)
        {
            type.ThrowArgumentExcepitonIfNotContains(Definitions.LeverageTypes, nameof(type));
            if (order != null)
                order.ThrowArgumentExcepitonIfNotContains(Definitions.Orders, nameof(order));

            if (type == "futures" && groupId == null)
                throw new ArgumentException("'type' が 'futures'の場合、パラメータ 'group_id' は必須です。", nameof(groupId));

            var parameters = new Dictionary<string, string>
            {
                { nameof(type).ToSnakeCase(), type },
            };

            if (groupId.HasValue) parameters.Add(nameof(groupId).ToSnakeCase(), groupId.ToString());
            if (from.HasValue) parameters.Add(nameof(from).ToSnakeCase(), from.ToString());
            if (count.HasValue) parameters.Add(nameof(count).ToSnakeCase(), count.ToString());
            if (fromId.HasValue) parameters.Add(nameof(fromId).ToSnakeCase(), fromId.ToString());
            if (endId.HasValue) parameters.Add(nameof(endId).ToSnakeCase(), endId.ToString());
            if (order != null) parameters.Add(nameof(order).ToSnakeCase(), order);
            if (since.HasValue) parameters.Add(nameof(since).ToSnakeCase(), since.ToString());
            if (end.HasValue) parameters.Add(nameof(end).ToSnakeCase(), end.ToString());
            if (currencyPair != null) parameters.Add(nameof(currencyPair).ToSnakeCase(), currencyPair);

            return GetPositionsAsync(parameters, token);
        }

        /// <summary>
        /// レバレッジ取引のユーザー自身の取引履歴を取得します。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="GetPositionsResponse"/>のディクショナリ（キーはレバレッジ注文id）</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="ArgumentException">
        /// 'type' が 'futures'の場合、パラメータ 'group_id' は必須です。 - parameters
        /// </exception>
        public Task<IDictionary<int, GetPositionsResponse>> GetPositionsAsync(IDictionary<string, string> parameters, CancellationToken token = default)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            parameters.ThrowIfNotContainsKey("type", nameof(parameters));
            parameters["type"].ThrowArgumentExcepitonIfNotContains(Definitions.LeverageTypes, nameof(parameters), "type");

            if (parameters["type"] == "futures" && !parameters.ContainsKey("group_id"))
                throw new ArgumentException("'type' が 'futures'の場合、パラメータ 'group_id' は必須です。", nameof(parameters));
            if (parameters.ContainsKey("order"))
                parameters["order"].ThrowArgumentExcepitonIfNotContains(Definitions.Orders, nameof(parameters), "order");

            return _client.PostAsync<IDictionary<int, GetPositionsResponse>>(
                nameof(GetPositionsAsync).ToApiMethodName(), parameters, token);
        }

        /// <summary>
        /// レバレッジ取引のユーザー自身の取引履歴の明細を取得します。
        /// </summary>
        /// <param name="type">type</param>
        /// <param name="leverageId">leverage_id</param>
        /// <param name="groupId">group_id</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="PositionHistoryResponse"/>のディクショナリ（キーはレバレッジ注文id）</returns>
        /// <exception cref="ArgumentException">'type' が 'futures'の場合、パラメータ 'group_id' は必須です。 - groupId</exception>
        public Task<IDictionary<int, PositionHistoryResponse>> PositionHistoryAsync(
            string type, int leverageId, int? groupId = null, CancellationToken token = default)
        {
            type.ThrowArgumentExcepitonIfNotContains(Definitions.LeverageTypes, nameof(type));

            if (type == "futures" && groupId == null)
                throw new ArgumentException("'type' が 'futures'の場合、パラメータ 'group_id' は必須です。", nameof(groupId));

            var parameters = new Dictionary<string, string>
            {
                { nameof(type).ToSnakeCase(), type },
                { nameof(leverageId).ToSnakeCase(), leverageId.ToString() },
            };

            if (groupId.HasValue) parameters.Add(nameof(groupId).ToSnakeCase(), groupId.ToString());

            return PositionHistoryAsync(parameters, token);
        }

        /// <summary>
        /// レバレッジ取引のユーザー自身の取引履歴の明細を取得します。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="PositionHistoryResponse"/>のディクショナリ（キーはレバレッジ注文id）</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="ArgumentException">
        /// 'type' が 'futures'の場合、パラメータ 'group_id' は必須です。 - parameters
        /// </exception>
        public Task<IDictionary<int, PositionHistoryResponse>> PositionHistoryAsync(
            IDictionary<string, string> parameters, CancellationToken token = default)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            parameters.ThrowIfNotContainsKey("type", nameof(parameters));
            parameters["type"].ThrowArgumentExcepitonIfNotContains(Definitions.LeverageTypes, nameof(parameters), "type");

            parameters.ThrowIfNotContainsKey("leverage_id", nameof(parameters));

            if (parameters["type"] == "futures" && !parameters.ContainsKey("group_id"))
                throw new ArgumentException("'type' が 'futures'の場合、パラメータ 'group_id' は必須です。", nameof(parameters));

            return _client.PostAsync<IDictionary<int, PositionHistoryResponse>>(
                nameof(PositionHistoryAsync).ToApiMethodName(), parameters, token);
        }
        
        /// <summary>
        /// レバレッジ取引の現在有効な注文一覧を取得します（未約定注文一覧）。
        /// </summary>
        /// <param name="type">type</param>
        /// <param name="groupId">group_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="ActivePositionsResponse"/>のディクショナリ（キーはレバレッジ注文id）</returns>
        /// <exception cref="ArgumentException">'type' が 'futures'の場合、パラメータ 'group_id' は必須です。 - groupId</exception>
        public Task<IDictionary<int, ActivePositionsResponse>> ActivePositionsAsync(
            string type, int? groupId = null, string currencyPair = null, CancellationToken token = default)
        {
            type.ThrowArgumentExcepitonIfNotContains(Definitions.LeverageTypes, nameof(type));

            if (type == "futures" && groupId == null)
                throw new ArgumentException("'type' が 'futures'の場合、パラメータ 'group_id' は必須です。", nameof(groupId));

            var parameters = new Dictionary<string, string>
            {
                { nameof(type).ToSnakeCase(), type },
            };

            if (groupId.HasValue) parameters.Add(nameof(groupId).ToSnakeCase(), groupId.ToString());
            if (currencyPair != null) parameters.Add(nameof(currencyPair).ToSnakeCase(), currencyPair);

            return ActivePositionsAsync(parameters, token);
        }

        /// <summary>
        /// レバレッジ取引の現在有効な注文一覧を取得します（未約定注文一覧）。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="ActivePositionsResponse"/>のディクショナリ（キーはレバレッジ注文id）</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="ArgumentException">
        /// 'type' が 'futures'の場合、パラメータ 'group_id' は必須です。 - parameters
        /// </exception>
        public Task<IDictionary<int, ActivePositionsResponse>> ActivePositionsAsync(
            IDictionary<string, string> parameters, CancellationToken token = default)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            parameters.ThrowIfNotContainsKey("type", nameof(parameters));
            parameters["type"].ThrowArgumentExcepitonIfNotContains(Definitions.LeverageTypes, nameof(parameters), "type");
            
            if (parameters["type"] == "futures" && !parameters.ContainsKey("group_id"))
                throw new ArgumentException("'type' が 'futures'の場合、パラメータ 'group_id' は必須です。", nameof(parameters));

            return _client.PostAsync<IDictionary<int, ActivePositionsResponse>>(
                nameof(ActivePositionsAsync).ToApiMethodName(), parameters, token);
        }

        /// <summary>
        /// レバレッジ取引の注文を行います。
        /// </summary>
        /// <param name="type">type</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="action">action</param>
        /// <param name="amount">amount</param>
        /// <param name="price">price</param>
        /// <param name="leverage">leverage</param>
        /// <param name="groupId">group_id</param>
        /// <param name="limit">limit</param>
        /// <param name="stop">stop</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="CreatePositionResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentException">'type' が 'futures'の場合、パラメータ 'group_id' は必須です。 - groupId</exception>
        public Task<CreatePositionResponse> CreatePositionAsync(
            string type, string currencyPair, string action, decimal amount, decimal price, decimal leverage,
            int? groupId = null, decimal? limit = null, decimal? stop = null, CancellationToken token = default)
        {
            type.ThrowArgumentExcepitonIfNotContains(Definitions.LeverageTypes, nameof(type));
            currencyPair.ThrowArgumentExceptionIfNullOrWhiteSpace(nameof(currencyPair));
            action.ThrowArgumentExcepitonIfNotContains(Definitions.Actions, nameof(action));

            if (type == "futures" && groupId == null)
                throw new ArgumentException("'type' が 'futures'の場合、パラメータ 'group_id' は必須です。", nameof(groupId));

            var parameters = new Dictionary<string, string>
            {
                { nameof(type).ToSnakeCase(), type },
                { nameof(currencyPair).ToSnakeCase(), currencyPair },
                { nameof(action).ToSnakeCase(), action },
                { nameof(amount).ToSnakeCase(), amount.ToString() },
                { nameof(price).ToSnakeCase(), price.ToString() },
                { nameof(leverage).ToSnakeCase(), leverage.ToString() },
            };

            if (groupId.HasValue) parameters.Add(nameof(groupId).ToSnakeCase(), groupId.ToString());
            if (limit.HasValue) parameters.Add(nameof(limit).ToSnakeCase(), limit.ToString());
            if (stop.HasValue) parameters.Add(nameof(stop).ToSnakeCase(), stop.ToString());

            return CreatePositionAsync(parameters, token);
        }
        
        /// <summary>
        /// レバレッジ取引の注文を行います。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="CreatePositionResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="ArgumentException">
        /// 'type' が 'futures'の場合、パラメータ 'group_id' は必須です。 - parameters
        /// </exception>
        public Task<CreatePositionResponse> CreatePositionAsync(IDictionary<string, string> parameters,
            CancellationToken token = default)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            parameters.ThrowIfNotContainsKey("type", nameof(parameters));
            parameters["type"].ThrowArgumentExcepitonIfNotContains(Definitions.LeverageTypes, nameof(parameters), "type");

            parameters.ThrowIfNotContainsKey("currency_pair", nameof(parameters));
            parameters["currency_pair"].ThrowArgumentExceptionIfNullOrWhiteSpace(nameof(parameters), "currencyPair");

            parameters.ThrowIfNotContainsKey("action", nameof(parameters));
            parameters["action"].ThrowArgumentExcepitonIfNotContains(Definitions.Actions, nameof(parameters), "action");

            parameters.ThrowIfNotContainsKey("amount", nameof(parameters));
            parameters.ThrowIfNotContainsKey("price", nameof(parameters));
            parameters.ThrowIfNotContainsKey("leverage", nameof(parameters));

            if (parameters["type"] == "futures" && !parameters.ContainsKey("group_id"))
                throw new ArgumentException("'type' が 'futures'の場合、パラメータ 'group_id' は必須です。", nameof(parameters));

            return _client.PostAsync<CreatePositionResponse>(
                nameof(CreatePositionAsync).ToApiMethodName(), parameters, token);
        }

        /// <summary>
        /// レバレッジ取引の注文の変更を行います。
        /// </summary>
        /// <param name="type">type</param>
        /// <param name="leverageId">leverage_id</param>
        /// <param name="price">price</param>
        /// <param name="groupId">group_id</param>
        /// <param name="limit">limit</param>
        /// <param name="stop">stop</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="ChangePositionResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentException">'type' が 'futures'の場合、パラメータ 'group_id' は必須です。 - groupId</exception>
        public Task<ChangePositionResponse> ChangePositionAsync(
            string type, int leverageId, decimal price, int? groupId = null, decimal? limit = null, decimal? stop = null,
            CancellationToken token = default)
        {
            type.ThrowArgumentExcepitonIfNotContains(Definitions.LeverageTypes, nameof(type));

            if (type == "futures" && groupId == null)
                throw new ArgumentException("'type' が 'futures'の場合、パラメータ 'group_id' は必須です。", nameof(groupId));

            var parameters = new Dictionary<string, string>
            {
                { nameof(type).ToSnakeCase(), type },
                { nameof(leverageId).ToSnakeCase(), leverageId.ToString() },
                { nameof(price).ToSnakeCase(), price.ToString() },
            };

            if (groupId.HasValue) parameters.Add(nameof(groupId).ToSnakeCase(), groupId.ToString());
            if (limit.HasValue) parameters.Add(nameof(limit).ToSnakeCase(), limit.ToString());
            if (stop.HasValue) parameters.Add(nameof(stop).ToSnakeCase(), stop.ToString());

            return ChangePositionAsync(parameters, token);
        }

        /// <summary>
        /// レバレッジ取引の注文の変更を行います。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="ChangePositionResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="ArgumentException">
        /// 'type' が 'futures'の場合、パラメータ 'group_id' は必須です。 - parameters
        /// </exception>
        public Task<ChangePositionResponse> ChangePositionAsync(IDictionary<string, string> parameters,
            CancellationToken token = default)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            parameters.ThrowIfNotContainsKey("type", nameof(parameters));
            parameters["type"].ThrowArgumentExcepitonIfNotContains(Definitions.LeverageTypes, nameof(parameters), "type");

            parameters.ThrowIfNotContainsKey("leverage_id", nameof(parameters));
            parameters.ThrowIfNotContainsKey("price", nameof(parameters));

            if (parameters["type"] == "futures" && !parameters.ContainsKey("group_id"))
                throw new ArgumentException("'type' が 'futures'の場合、パラメータ 'group_id' は必須です。", nameof(parameters));

            return _client.PostAsync<ChangePositionResponse>(
                nameof(ChangePositionAsync).ToApiMethodName(), parameters, token);
        }

        /// <summary>
        /// レバレッジ取引の注文の取消しを行います。
        /// </summary>
        /// <param name="type">type</param>
        /// <param name="leverageId">leverage_id</param>
        /// <param name="groupId">group_id</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="CancelPositionResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentException">'type' が 'futures'の場合、パラメータ 'group_id' は必須です。 - groupId</exception>
        public Task<CancelPositionResponse> CancelPositionAsync(
            string type, int leverageId, int? groupId = null, CancellationToken token = default)
        {
            type.ThrowArgumentExcepitonIfNotContains(Definitions.LeverageTypes, nameof(type));

            if (type == "futures" && groupId == null)
                throw new ArgumentException("'type' が 'futures'の場合、パラメータ 'group_id' は必須です。", nameof(groupId));

            var parameters = new Dictionary<string, string>
            {
                { nameof(type).ToSnakeCase(), type },
                { nameof(leverageId).ToSnakeCase(), leverageId.ToString() },
            };

            if (groupId.HasValue) parameters.Add(nameof(groupId).ToSnakeCase(), groupId.ToString());

            return CancelPositionAsync(parameters, token);
        }

        /// <summary>
        /// レバレッジ取引の注文の取消しを行います。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="CancelPositionResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="ArgumentException">
        /// 'type' が 'futures'の場合、パラメータ 'group_id' は必須です。 - parameters
        /// </exception>
        public Task<CancelPositionResponse> CancelPositionAsync(IDictionary<string, string> parameters,
            CancellationToken token = default)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            parameters.ThrowIfNotContainsKey("type", nameof(parameters));
            parameters["type"].ThrowArgumentExcepitonIfNotContains(Definitions.LeverageTypes, nameof(parameters), "type");

            parameters.ThrowIfNotContainsKey("leverage_id", nameof(parameters));

            if (parameters["type"] == "futures" && !parameters.ContainsKey("group_id"))
                throw new ArgumentException("'type' が 'futures'の場合、パラメータ 'group_id' は必須です。", nameof(parameters));

            return _client.PostAsync<CancelPositionResponse>(
                nameof(CancelPositionAsync).ToApiMethodName(), parameters, token);
        }
    }
}
