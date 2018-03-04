using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZaifApiWrapper.TradeData;

namespace ZaifApiWrapper
{
    /// <summary>
    /// 現物取引API
    /// </summary>
    /// <remarks>
    /// <see href="http://techbureau-api-document.readthedocs.io/ja/latest/trade/index.html"/>
    /// </remarks>
    public class TradeApi
    {
        private readonly IApiClient _client;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="apiKey">APIキー</param>
        /// <param name="apiSecret">APIシークレット</param>
        public TradeApi(string apiKey, string apiSecret)
            : this(new ApiClientOption(apiKey, apiSecret)) { }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="option"><see cref="ApiClientOption"/>オブジェクト。</param>
        public TradeApi(ApiClientOption option)
            : this(new ApiClient(ApiUrl.Trade, option)) { }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="client"><see cref="IApiClient"/>具象オブジェクト。</param>
        internal TradeApi(IApiClient client) => _client = client;

        /// <summary>
        /// 現在の残高（余力および残高・トークン）、APIキーの権限、過去のトレード数、アクティブな注文数、サーバーのタイムスタンプを取得します。
        /// </summary>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="GetInfoResponse"/>オブジェクト。</returns>
        public Task<GetInfoResponse> GetInfoAsync(CancellationToken token = default) =>
            _client.PostAsync<GetInfoResponse>(nameof(GetInfoAsync).ToApiMethodName(), null, token);

        /// <summary>
        /// <see cref="GetInfoAsync(CancellationToken)"/>の軽量版で、過去のトレード数を除く項目を返します。
        /// </summary>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="GetInfo2Response"/>オブジェクト。</returns>
        public Task<GetInfo2Response> GetInfo2Async(CancellationToken token = default) =>
            _client.PostAsync<GetInfo2Response>(nameof(GetInfo2Async).ToApiMethodName(), null, token);

        /// <summary>
        /// チャットに使用されるニックネームと画像のパスを返します。
        /// </summary>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="GetPersonalInfoResponse"/>オブジェクト。</returns>
        public Task<GetPersonalInfoResponse> GetPersonalInfoAsync(CancellationToken token = default) =>
            _client.PostAsync<GetPersonalInfoResponse>(nameof(GetPersonalInfoAsync).ToApiMethodName(), null, token);

        /// <summary>
        /// ユーザーIDやメールアドレスといった個人情報を取得します
        /// </summary>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="GetIdInfoResponse"/>オブジェクト。</returns>
        public Task<GetIdInfoResponse> GetIdInfoAsync(CancellationToken token = default) =>
            _client.PostAsync<GetIdInfoResponse>(nameof(GetIdInfoAsync).ToApiMethodName(), null, token);

        /// <summary>
        /// ユーザー自身の取引履歴を取得します。
        /// </summary>
        /// <param name="from">from</param>
        /// <param name="count">count</param>
        /// <param name="fromId">from_id</param>
        /// <param name="endId">end_id</param>
        /// <param name="order">order</param>
        /// <param name="since">since</param>
        /// <param name="end">end</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="isToken">is_token</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="TradeHistoryResponse"/>のディクショナリ（キーは注文id）</returns>
        public Task<IDictionary<int, TradeHistoryResponse>> TradeHistoryAsync(
            int? from = null, int? count = null, int? fromId = null, int? endId = null, string order = null,
            long? since = null, long? end = null, string currencyPair = null, bool? isToken = null, CancellationToken token = default)
        {
            if (order != null)
                order.ThrowIfValueInvalid(Definitions.Orders, nameof(order));

            var parameters = new Dictionary<string, string>();

            if (from.HasValue) parameters.Add(nameof(from).ToSnakeCase(), from.ToString());
            if (count.HasValue) parameters.Add(nameof(count).ToSnakeCase(), count.ToString());
            if (fromId.HasValue) parameters.Add(nameof(fromId).ToSnakeCase(), fromId.ToString());
            if (endId.HasValue) parameters.Add(nameof(endId).ToSnakeCase(), endId.ToString());
            if (order != null) parameters.Add(nameof(order).ToSnakeCase(), order);
            if (since.HasValue) parameters.Add(nameof(since).ToSnakeCase(), since.ToString());
            if (end.HasValue) parameters.Add(nameof(end).ToSnakeCase(), end.ToString());
            if (currencyPair != null) parameters.Add(nameof(currencyPair).ToSnakeCase(), currencyPair);
            if (isToken.HasValue) parameters.Add(nameof(isToken).ToSnakeCase(), isToken.ToString());

            return TradeHistoryAsync(parameters, token);
        }

        /// <summary>
        /// ユーザー自身の取引履歴を取得します。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="TradeHistoryResponse"/>のディクショナリ（キーは注文id）</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        public Task<IDictionary<int, TradeHistoryResponse>> TradeHistoryAsync(IDictionary<string, string> parameters,
            CancellationToken token = default)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            if (parameters.ContainsKey("order"))
                parameters["order"].ThrowIfValueInvalid(Definitions.Orders, nameof(parameters), "order");

            return _client.PostAsync<IDictionary<int, TradeHistoryResponse>>(
                nameof(TradeHistoryAsync).ToApiMethodName(), parameters, token);
        }

        /// <summary>
        /// 現在有効な注文一覧を取得します（未約定注文一覧）。
        /// </summary>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="isToken">is_token</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="ActiveOrdersResponse"/>のディクショナリ（キーは注文id）</returns>
        public Task<IDictionary<int, ActiveOrdersResponse>> ActiveOrdersAsync(
            string currencyPair = null, bool? isToken = null, CancellationToken token = default)
        {
            var parameters = new Dictionary<string, string>();

            if (currencyPair != null) parameters.Add(nameof(currencyPair).ToSnakeCase(), currencyPair);
            if (isToken.HasValue) parameters.Add(nameof(isToken).ToSnakeCase(), isToken.ToString());

            return ActiveOrdersAsync(parameters, token);
        }        

        /// <summary>
        /// 現在有効な注文一覧を取得します（未約定注文一覧）。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="ActiveOrdersResponse"/>のディクショナリ（キーは注文id）</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="NotSupportedException">is_token_bothパラメータはサポートしていません。</exception>
        public Task<IDictionary<int, ActiveOrdersResponse>> ActiveOrdersAsync(IDictionary<string, string> parameters,
            CancellationToken token = default)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            // is_token_bothは削除予定、かつ指定された場合に戻り値の型が変わってしまうためサポートしない
            if (parameters != null && parameters.ContainsKey("is_token_both"))
                throw new NotSupportedException("'is_token_both' パラメータはサポートしていません。");

            return _client.PostAsync<IDictionary<int, ActiveOrdersResponse>>(
                nameof(ActiveOrdersAsync).ToApiMethodName(), parameters, token);
        }

        /// <summary>
        /// 取引注文を行います。
        /// </summary>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="action">action</param>
        /// <param name="price">price</param>
        /// <param name="amount">amount</param>
        /// <param name="limit">limit</param>
        /// <param name="comment">comment</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="TradeResponse"/>オブジェクト。</returns>
        public Task<TradeResponse> TradeAsync(
            string currencyPair, string action, decimal price, decimal amount, decimal? limit = null, 
            string comment = null, CancellationToken token = default)
        {
            currencyPair.ThrowIfIsNullOrWhiteSpace(nameof(currencyPair));
            action.ThrowIfValueInvalid(Definitions.Actions, nameof(action));

            var parameters = new Dictionary<string, string>
            {
                { nameof(currencyPair).ToSnakeCase(), currencyPair },
                { nameof(action).ToSnakeCase(), action },
                { nameof(price).ToSnakeCase(), price.ToString() },
                { nameof(amount).ToSnakeCase(), amount.ToString() },
            };

            if (limit.HasValue) parameters.Add(nameof(limit).ToSnakeCase(), limit.ToString());
            if (comment != null) parameters.Add(nameof(comment).ToSnakeCase(), comment);

            return TradeAsync(parameters, token);
        }

        /// <summary>
        /// 取引注文を行います。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="TradeResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        public Task<TradeResponse> TradeAsync(IDictionary<string, string> parameters, CancellationToken token = default)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            parameters.ThrowIfNotContainsKey("currency_pair", nameof(parameters));
            parameters["currency_pair"].ThrowIfIsNullOrWhiteSpace(nameof(parameters), "currency_pair");

            parameters.ThrowIfNotContainsKey("action", nameof(parameters));
            parameters["action"].ThrowIfValueInvalid(Definitions.Actions, nameof(parameters), "action");

            parameters.ThrowIfNotContainsKey("price", nameof(parameters));
            parameters.ThrowIfNotContainsKey("amount", nameof(parameters));

            return _client.PostAsync<TradeResponse>(
                nameof(TradeAsync).ToApiMethodName(), parameters, token);
        }        

        /// <summary>
        /// 注文の取消しを行います。
        /// </summary>
        /// <param name="orderId">order_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="isToken">is_token</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="CancelOrderResponse"/>オブジェクト。</returns>
        public Task<CancelOrderResponse> CancelOrderAsync(
            int orderId, string currencyPair = null, bool? isToken = null, CancellationToken token = default)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(orderId).ToSnakeCase(), orderId.ToString() },
            };

            if (currencyPair != null) parameters.Add(nameof(currencyPair).ToSnakeCase(), currencyPair);
            if (isToken.HasValue) parameters.Add(nameof(isToken).ToSnakeCase(), isToken.ToString());

            return CancelOrderAsync(parameters, token);
        }

        /// <summary>
        /// 注文の取消しを行います。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="CancelOrderResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        public Task<CancelOrderResponse> CancelOrderAsync(IDictionary<string, string> parameters, CancellationToken token = default)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            parameters.ThrowIfNotContainsKey("order_id", nameof(parameters));

            return _client.PostAsync<CancelOrderResponse>(
                nameof(CancelOrderAsync).ToApiMethodName(), parameters, token);
        }        

        private static readonly string[] OptFeeAcceptableCurrencies = { "btc", "mona" };

        /// <summary>
        /// 資金の引き出しリクエストを送信します。
        /// </summary>
        /// <param name="currency">currency</param>
        /// <param name="address">address</param>
        /// <param name="amount">amount</param>
        /// <param name="message">message</param>
        /// <param name="optFee">opt_fee</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="WithdrawResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentException">'currency' が 'btc', 'mona' 以外の場合は 'opt_fee' は指定できません。 - optFee</exception>
        public Task<WithdrawResponse> WithdrawAsync(
            string currency, string address, decimal amount, string message = null, decimal? optFee = null, CancellationToken token = default)
        {
            currency.ThrowIfIsNullOrWhiteSpace(nameof(currency));
            address.ThrowIfIsNullOrWhiteSpace(nameof(address));

            if (!OptFeeAcceptableCurrencies.Contains(currency) && optFee != null)
                throw new ArgumentException("'currency' が 'btc', 'mona' 以外の場合は 'opt_fee' は指定できません。", nameof(optFee));

            var parameters = new Dictionary<string, string>
            {
                { nameof(currency).ToSnakeCase(), currency },
                { nameof(address).ToSnakeCase(), address },
                { nameof(amount).ToSnakeCase(), amount.ToString() },
            };

            if (message != null) parameters.Add(nameof(message).ToSnakeCase(), message);
            if (optFee.HasValue) parameters.Add(nameof(optFee).ToSnakeCase(), optFee.ToString());

            return WithdrawAsync(parameters, token);
        }

        /// <summary>
        /// 資金の引き出しリクエストを送信します。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="WithdrawResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="ArgumentException">
        /// 'currency' が 'btc', 'mona' 以外の場合は 'opt_fee' は指定できません。 - parameters
        /// </exception>
        public Task<WithdrawResponse> WithdrawAsync(IDictionary<string, string> parameters, CancellationToken token = default)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            parameters.ThrowIfNotContainsKey("currency", nameof(parameters));
            parameters["currency"].ThrowIfIsNullOrWhiteSpace(nameof(parameters), "currency");

            parameters.ThrowIfNotContainsKey("address", nameof(parameters));
            parameters["address"].ThrowIfIsNullOrWhiteSpace(nameof(parameters), "address");

            parameters.ThrowIfNotContainsKey("amount", nameof(parameters));
            parameters["amount"].ThrowIfIsNullOrWhiteSpace(nameof(parameters), "amount");

            if (!OptFeeAcceptableCurrencies.Contains(parameters["currency"]) && parameters.ContainsKey("opt_fee"))
                throw new ArgumentException("'currency' が 'btc', 'mona' 以外の場合は 'opt_fee' は指定できません。", nameof(parameters));

            return _client.PostAsync<WithdrawResponse>(
                nameof(WithdrawAsync).ToApiMethodName(), parameters, token);
        }

        /// <summary>
        /// 入金履歴を取得します。
        /// </summary>
        /// <param name="currency">currency</param>
        /// <param name="from">from</param>
        /// <param name="count">count</param>
        /// <param name="fromId">from_id</param>
        /// <param name="endId">end_id</param>
        /// <param name="order">order</param>
        /// <param name="since">since</param>
        /// <param name="end">end</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="DepositHistoryResponse"/>のディクショナリ（キーは入金履歴id?ドキュメントに記載なし）</returns>
        public Task<IDictionary<int, DepositHistoryResponse>> DepositHistoryAsync(
            string currency, int? from = null, int? count = null, int? fromId = null, int? endId = null,
            string order = null, long? since = null, long? end = null, CancellationToken token = default)
        {
            currency.ThrowIfIsNullOrWhiteSpace(nameof(currency));
            if (order != null)
                order.ThrowIfValueInvalid(Definitions.Orders, nameof(order));

            var parameters = new Dictionary<string, string>
            {
                { nameof(currency).ToSnakeCase(), currency },
            };

            if (from.HasValue) parameters.Add(nameof(from).ToSnakeCase(), from.ToString());
            if (count.HasValue) parameters.Add(nameof(count).ToSnakeCase(), count.ToString());
            if (fromId.HasValue) parameters.Add(nameof(fromId).ToSnakeCase(), fromId.ToString());
            if (endId.HasValue) parameters.Add(nameof(endId).ToSnakeCase(), endId.ToString());
            if (order != null) parameters.Add(nameof(order).ToSnakeCase(), order);
            if (since.HasValue) parameters.Add(nameof(since).ToSnakeCase(), since.ToString());
            if (end.HasValue) parameters.Add(nameof(end).ToSnakeCase(), end.ToString());

            return DepositHistoryAsync(parameters, token);
        }

        /// <summary>
        /// 入金履歴を取得します。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="DepositHistoryResponse"/>のディクショナリ（キーは入金履歴id?ドキュメントに記載なし）</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        public Task<IDictionary<int, DepositHistoryResponse>> DepositHistoryAsync(
            IDictionary<string, string> parameters, CancellationToken token = default)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            parameters.ThrowIfNotContainsKey("currency", nameof(parameters));
            if (parameters.ContainsKey("order"))
                parameters["order"].ThrowIfValueInvalid(Definitions.Orders, nameof(parameters), "order");

            return _client.PostAsync<IDictionary<int, DepositHistoryResponse>>(
                nameof(DepositHistoryAsync).ToApiMethodName(), parameters, token);
        }

        /// <summary>
        /// 出金履歴を取得します。
        /// </summary>
        /// <param name="currency">currency</param>
        /// <param name="from">from</param>
        /// <param name="count">count</param>
        /// <param name="fromId">from_id</param>
        /// <param name="endId">end_id</param>
        /// <param name="order">order</param>
        /// <param name="since">since</param>
        /// <param name="end">end</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="WithdrawHistoryResponse"/>のディクショナリ（キーは出金履歴id?ドキュメントに記載なし）</returns>
        public Task<IDictionary<int, WithdrawHistoryResponse>> WithdrawHistoryAsync(
            string currency, int? from = null, int? count = null, int? fromId = null, int? endId = null,
            string order = null, long? since = null, long? end = null, CancellationToken token = default)
        {
            currency.ThrowIfIsNullOrWhiteSpace(nameof(currency));
            if (order != null)
                order.ThrowIfValueInvalid(Definitions.Orders, nameof(order));

            var parameters = new Dictionary<string, string>
            {
                { nameof(currency).ToSnakeCase(), currency },
            };

            if (from.HasValue) parameters.Add(nameof(from).ToSnakeCase(), from.ToString());
            if (count.HasValue) parameters.Add(nameof(count).ToSnakeCase(), count.ToString());
            if (fromId.HasValue) parameters.Add(nameof(fromId).ToSnakeCase(), fromId.ToString());
            if (endId.HasValue) parameters.Add(nameof(endId).ToSnakeCase(), endId.ToString());
            if (order != null) parameters.Add(nameof(order).ToSnakeCase(), order);
            if (since.HasValue) parameters.Add(nameof(since).ToSnakeCase(), since.ToString());
            if (end.HasValue) parameters.Add(nameof(end).ToSnakeCase(), end.ToString());

            return WithdrawHistoryAsync(parameters, token);
        }

        /// <summary>
        /// 出金履歴を取得します。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>構造体。</param>
        /// <returns><see cref="WithdrawHistoryResponse"/>のディクショナリ（キーは出金履歴id?ドキュメントに記載なし）</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        public Task<IDictionary<int, WithdrawHistoryResponse>> WithdrawHistoryAsync(
            IDictionary<string, string> parameters, CancellationToken token = default)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            parameters.ThrowIfNotContainsKey("currency", nameof(parameters));
            parameters["currency"].ThrowIfIsNullOrWhiteSpace(nameof(parameters), "currency");

            if (parameters.ContainsKey("order"))
                parameters["order"].ThrowIfValueInvalid(Definitions.Orders, nameof(parameters), "order");

            return _client.PostAsync<IDictionary<int, WithdrawHistoryResponse>>(
                nameof(WithdrawHistoryAsync).ToApiMethodName(), parameters, token);
        }
    }
}
