﻿using System;
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
        /// <returns><see cref="GetInfoResponse"/>オブジェクト。</returns>
        public Task<GetInfoResponse> GetInfoAsync() =>
             GetInfoAsync(CancellationToken.None);

        /// <summary>
        /// 現在の残高（余力および残高・トークン）、APIキーの権限、過去のトレード数、アクティブな注文数、サーバーのタイムスタンプを取得します。
        /// </summary>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="GetInfoResponse"/>オブジェクト。</returns>
        public Task<GetInfoResponse> GetInfoAsync(CancellationToken token) =>
            _client.PostAsync<GetInfoResponse>(nameof(GetInfoAsync).ToApiMethodName(), null, token);

        /// <summary>
        /// <see cref="GetIdInfoAsync()"/>の軽量版で、過去のトレード数を除く項目を返します。
        /// </summary>
        /// <returns><see cref="GetInfo2Response"/>オブジェクト。</returns>
        public Task<GetInfo2Response> GetInfo2Async() =>
            GetInfo2Async(CancellationToken.None);

        /// <summary>
        /// <see cref="GetIdInfoAsync(CancellationToken)"/>の軽量版で、過去のトレード数を除く項目を返します。
        /// </summary>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="GetInfo2Response"/>オブジェクト。</returns>
        public Task<GetInfo2Response> GetInfo2Async(CancellationToken token) =>
            _client.PostAsync<GetInfo2Response>(nameof(GetInfo2Async).ToApiMethodName(), null, token);

        /// <summary>
        /// チャットに使用されるニックネームと画像のパスを返します。
        /// </summary>
        /// <returns><see cref="GetPersonalInfoResponse"/>オブジェクト。</returns>
        public Task<GetPersonalInfoResponse> GetPersonalInfoAsync() => GetPersonalInfoAsync(CancellationToken.None);

        /// <summary>
        /// チャットに使用されるニックネームと画像のパスを返します。
        /// </summary>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="GetPersonalInfoResponse"/>オブジェクト。</returns>
        public Task<GetPersonalInfoResponse> GetPersonalInfoAsync(CancellationToken token) =>
            _client.PostAsync<GetPersonalInfoResponse>(nameof(GetPersonalInfoAsync).ToApiMethodName(), null, token);

        /// <summary>
        /// ユーザーIDやメールアドレスといった個人情報を取得します
        /// </summary>
        /// <returns><see cref="GetIdInfoResponse"/>オブジェクト。</returns>
        public Task<GetIdInfoResponse> GetIdInfoAsync() =>
            GetIdInfoAsync(CancellationToken.None);

        /// <summary>
        /// ユーザーIDやメールアドレスといった個人情報を取得します
        /// </summary>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="GetIdInfoResponse"/>オブジェクト。</returns>
        public Task<GetIdInfoResponse> GetIdInfoAsync(CancellationToken token) =>
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
        /// <returns><see cref="TradeHistoryResponse"/>オブジェクト。</returns>
        public Task<IDictionary<string, TradeHistoryResponse>> TradeHistoryAsync(
            int? from = null, int? count = null, int? fromId = null, int? endId = null, string order = null,
            long? since = null, long? end = null, string currencyPair = null, bool? isToken = null) =>
            TradeHistoryAsync(from, count, fromId, endId, order, since, end, currencyPair, isToken, CancellationToken.None);

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
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="TradeHistoryResponse"/>オブジェクト。</returns>
        public Task<IDictionary<string, TradeHistoryResponse>> TradeHistoryAsync(
            int? from, int? count, int? fromId, int? endId, string order,
            long? since, long? end, string currencyPair, bool? isToken, CancellationToken token)
        {
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
        /// <returns><see cref="TradeHistoryResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        public Task<IDictionary<string, TradeHistoryResponse>> TradeHistoryAsync(IDictionary<string, string> parameters) =>
            TradeHistoryAsync(parameters, CancellationToken.None);

        /// <summary>
        /// ユーザー自身の取引履歴を取得します。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="TradeHistoryResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        public Task<IDictionary<string, TradeHistoryResponse>> TradeHistoryAsync(IDictionary<string, string> parameters,
            CancellationToken token)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            return _client.PostAsync<IDictionary<string, TradeHistoryResponse>>(
                nameof(TradeHistoryAsync).ToApiMethodName(), parameters, token);
        }

        /// <summary>
        /// 現在有効な注文一覧を取得します（未約定注文一覧）。
        /// </summary>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="isToken">is_token</param>
        /// <returns><see cref="ActiveOrdersResponse"/>オブジェクト。</returns>
        public Task<IDictionary<string, ActiveOrdersResponse>> ActiveOrdersAsync(
            string currencyPair = null, bool? isToken = null) =>
            ActiveOrdersAsync(currencyPair, isToken, CancellationToken.None);

        /// <summary>
        /// 現在有効な注文一覧を取得します（未約定注文一覧）。
        /// </summary>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="isToken">is_token</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="ActiveOrdersResponse"/>オブジェクト。</returns>
        public Task<IDictionary<string, ActiveOrdersResponse>> ActiveOrdersAsync(
            string currencyPair, bool? isToken, CancellationToken token)
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
        /// <returns><see cref="ActiveOrdersResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="NotSupportedException">is_token_bothパラメータはサポートしていません。</exception>
        public Task<IDictionary<string, ActiveOrdersResponse>> ActiveOrdersAsync(IDictionary<string, string> parameters) =>
            ActiveOrdersAsync(parameters, CancellationToken.None);

        /// <summary>
        /// 現在有効な注文一覧を取得します（未約定注文一覧）。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="ActiveOrdersResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="NotSupportedException">is_token_bothパラメータはサポートしていません。</exception>
        public Task<IDictionary<string, ActiveOrdersResponse>> ActiveOrdersAsync(IDictionary<string, string> parameters,
            CancellationToken token)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            // is_token_bothは削除予定、かつ指定された場合に戻り値の型が変わってしまうためサポートしない
            if (parameters != null && parameters.ContainsKey("is_token_both"))
                throw new NotSupportedException("is_token_bothパラメータはサポートしていません。");

            return _client.PostAsync<IDictionary<string, ActiveOrdersResponse>>(
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
        /// <returns><see cref="TradeResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">
        /// currencyPair
        /// or
        /// action
        /// </exception>
        public Task<TradeResponse> TradeAsync(
            string currencyPair, string action, decimal price, decimal amount, decimal? limit = null, string comment = null) =>
            TradeAsync(currencyPair, action, price, amount, limit, comment, CancellationToken.None);

        /// <summary>
        /// 取引注文を行います。
        /// </summary>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="action">action</param>
        /// <param name="price">price</param>
        /// <param name="amount">amount</param>
        /// <param name="limit">limit</param>
        /// <param name="comment">comment</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="TradeResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">
        /// currencyPair
        /// or
        /// action
        /// </exception>
        public Task<TradeResponse> TradeAsync(
            string currencyPair, string action, decimal price, decimal amount, decimal? limit, string comment, CancellationToken token)
        {
            if (currencyPair == null) throw new ArgumentNullException(nameof(currencyPair));
            if (action == null) throw new ArgumentNullException(nameof(action));

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
        /// <returns><see cref="TradeResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="ArgumentException">
        /// パラメータ'currency_pair'が指定されていません。 - parameters
        /// or
        /// パラメータ'action'が指定されていません。 - parameters
        /// or
        /// パラメータ'price'が指定されていません。 - parameters
        /// or
        /// パラメータ'amount'が指定されていません。 - parameters
        /// </exception>
        public Task<TradeResponse> TradeAsync(IDictionary<string, string> parameters) =>
            TradeAsync(parameters, CancellationToken.None);

        /// <summary>
        /// 取引注文を行います。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="TradeResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="ArgumentException">
        /// パラメータ'currency_pair'が指定されていません。 - parameters
        /// or
        /// パラメータ'action'が指定されていません。 - parameters
        /// or
        /// パラメータ'price'が指定されていません。 - parameters
        /// or
        /// パラメータ'amount'が指定されていません。 - parameters
        /// </exception>
        public Task<TradeResponse> TradeAsync(IDictionary<string, string> parameters, CancellationToken token)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            if (!parameters.ContainsKey("currency_pair"))
                throw new ArgumentException("パラメータ'currency_pair'が指定されていません。", nameof(parameters));
            if (!parameters.ContainsKey("action"))
                throw new ArgumentException("パラメータ'action'が指定されていません。", nameof(parameters));
            if (!parameters.ContainsKey("price"))
                throw new ArgumentException("パラメータ'price'が指定されていません。", nameof(parameters));
            if (!parameters.ContainsKey("amount"))
                throw new ArgumentException("パラメータ'amount'が指定されていません。", nameof(parameters));

            return _client.PostAsync<TradeResponse>(
                nameof(TradeAsync).ToApiMethodName(), parameters, token);
        }

        /// <summary>
        /// 注文の取消しを行います。
        /// </summary>
        /// <param name="orderId">order_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="isToken">is_token</param>
        /// <returns><see cref="CancelOrderResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">orderId</exception>
        public Task<CancelOrderResponse> CancelOrderAsync(string orderId, string currencyPair = null, bool? isToken = null) =>
             CancelOrderAsync(orderId, currencyPair, isToken, CancellationToken.None);

        /// <summary>
        /// 注文の取消しを行います。
        /// </summary>
        /// <param name="orderId">order_id</param>
        /// <param name="currencyPair">currency_pair</param>
        /// <param name="isToken">is_token</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="CancelOrderResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">orderId</exception>
        public Task<CancelOrderResponse> CancelOrderAsync(string orderId, string currencyPair, bool? isToken, CancellationToken token)
        {
            if (orderId == null) throw new ArgumentNullException(nameof(orderId));

            var parameters = new Dictionary<string, string>
            {
                { nameof(orderId).ToSnakeCase(), orderId },
            };

            if (currencyPair != null) parameters.Add(nameof(currencyPair).ToSnakeCase(), currencyPair);
            if (isToken.HasValue) parameters.Add(nameof(isToken).ToSnakeCase(), isToken.ToString());

            return CancelOrderAsync(parameters, token);
        }

        /// <summary>
        /// 注文の取消しを行います。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <returns><see cref="CancelOrderResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="ArgumentException">パラメータ'order_id'が指定されていません。 - parameters</exception>
        public Task<CancelOrderResponse> CancelOrderAsync(IDictionary<string, string> parameters) =>
            CancelOrderAsync(parameters, CancellationToken.None);

        /// <summary>
        /// 注文の取消しを行います。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="CancelOrderResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="ArgumentException">パラメータ'order_id'が指定されていません。 - parameters</exception>
        public Task<CancelOrderResponse> CancelOrderAsync(IDictionary<string, string> parameters, CancellationToken token)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            if (!parameters.ContainsKey("order_id"))
                throw new ArgumentException("パラメータ'order_id'が指定されていません。", nameof(parameters));

            return _client.PostAsync<CancelOrderResponse>(
                nameof(CancelOrderAsync).ToApiMethodName(), parameters, token);
        }

        /// <summary>
        /// 資金の引き出しリクエストを送信します。
        /// </summary>
        /// <param name="currency">currency</param>
        /// <param name="address">address</param>
        /// <param name="amount">amount</param>
        /// <param name="message">message</param>
        /// <param name="optFee">opt_fee</param>
        /// <returns><see cref="WithdrawResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">
        /// currency
        /// or
        /// address
        /// </exception>
        /// <exception cref="ArgumentException">'currency'が'btc', 'mona'以外の場合は'opt_fee'は指定できません。 - optFee</exception>
        public Task<WithdrawResponse> WithdrawAsync(
            string currency, string address, decimal amount, string message = null, decimal? optFee = null) =>
            WithdrawAsync(currency, address, amount, message, optFee, CancellationToken.None);

        private static readonly string[] OptFeeAcceptableCurrencies = { "btc", "mona" };

        /// <summary>
        /// 資金の引き出しリクエストを送信します。
        /// </summary>
        /// <param name="currency">currency</param>
        /// <param name="address">address</param>
        /// <param name="amount">amount</param>
        /// <param name="message">message</param>
        /// <param name="optFee">opt_fee</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="WithdrawResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">
        /// currency
        /// or
        /// address
        /// </exception>
        /// <exception cref="ArgumentException">'currency'が'btc', 'mona'以外の場合は'opt_fee'は指定できません。 - optFee</exception>
        public Task<WithdrawResponse> WithdrawAsync(
            string currency, string address, decimal amount, string message, decimal? optFee, CancellationToken token)
        {
            if (currency == null) throw new ArgumentNullException(nameof(currency));
            if (address == null) throw new ArgumentNullException(nameof(address));

            if (!OptFeeAcceptableCurrencies.Contains("currency") && optFee != null)
                throw new ArgumentException("'currency'が'btc', 'mona'以外の場合は'opt_fee'は指定できません。", nameof(optFee));

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
        /// <returns><see cref="WithdrawResponse"/>オブジェクト。</returns>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="ArgumentException">
        /// パラメータ'currency'が指定されていません。 - parameters
        /// or
        /// パラメータ'address'が指定されていません。 - parameters
        /// or
        /// パラメータ'amount'が指定されていません。 - parameters
        /// or
        /// 'currency'が'btc', 'mona'以外の場合は'opt_fee'は指定できません。 - parameters
        /// </exception>
        public Task<WithdrawResponse> WithdrawAsync(IDictionary<string, string> parameters) =>
            WithdrawAsync(parameters, CancellationToken.None);

        /// <summary>
        /// 資金の引き出しリクエストを送信します。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="WithdrawResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="ArgumentException">
        /// パラメータ'currency'が指定されていません。 - parameters
        /// or
        /// パラメータ'address'が指定されていません。 - parameters
        /// or
        /// パラメータ'amount'が指定されていません。 - parameters
        /// or
        /// 'currency'が'btc', 'mona'以外の場合は'opt_fee'は指定できません。 - parameters
        /// </exception>
        public Task<WithdrawResponse> WithdrawAsync(IDictionary<string, string> parameters, CancellationToken token)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            if (!parameters.ContainsKey("currency"))
                throw new ArgumentException("パラメータ'currency'が指定されていません。", nameof(parameters));
            if (!parameters.ContainsKey("address"))
                throw new ArgumentException("パラメータ'address'が指定されていません。", nameof(parameters));
            if (!parameters.ContainsKey("amount"))
                throw new ArgumentException("パラメータ'amount'が指定されていません。", nameof(parameters));

            if (!OptFeeAcceptableCurrencies.Contains(parameters["currency"]) && parameters.ContainsKey("opt_fee"))
                throw new ArgumentException("'currency'が'btc', 'mona'以外の場合は'opt_fee'は指定できません。", nameof(parameters));

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
        /// <returns><see cref="DepositHistoryResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">currency</exception>
        public Task<IDictionary<string, DepositHistoryResponse>> DepositHistoryAsync(
            string currency, int? from = null, int? count = null, int? fromId = null, int? endId = null,
            string order = null, long? since = null, long? end = null) =>
            DepositHistoryAsync(currency, from, count, fromId, endId, order, since, end, CancellationToken.None);

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
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="DepositHistoryResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">currency</exception>
        public Task<IDictionary<string, DepositHistoryResponse>> DepositHistoryAsync(
            string currency, int? from, int? count, int? fromId, int? endId,
            string order, long? since, long? end, CancellationToken token)
        {
            if (currency == null) throw new ArgumentNullException(nameof(currency));

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
        /// <returns><see cref="DepositHistoryResponse"/>オブジェクト。</returns>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="ArgumentException">パラメータ'currency'が指定されていません。 - parameters</exception>
        public Task<IDictionary<string, DepositHistoryResponse>> DepositHistoryAsync(
            IDictionary<string, string> parameters) =>
            DepositHistoryAsync(parameters, CancellationToken.None);

        /// <summary>
        /// 入金履歴を取得します。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="DepositHistoryResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="ArgumentException">パラメータ'currency'が指定されていません。 - parameters</exception>
        public Task<IDictionary<string, DepositHistoryResponse>> DepositHistoryAsync(
            IDictionary<string, string> parameters, CancellationToken token)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            if (!parameters.ContainsKey("currency"))
                throw new ArgumentException("パラメータ'currency'が指定されていません。", nameof(parameters));

            return _client.PostAsync<IDictionary<string, DepositHistoryResponse>>(
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
        /// <returns><see cref="WithdrawHistoryResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">currency</exception>
        public Task<IDictionary<string, WithdrawHistoryResponse>> WithdrawHistoryAsync(
            string currency, int? from = null, int? count = null, int? fromId = null, int? endId = null,
            string order = null, long? since = null, long? end = null) =>
            WithdrawHistoryAsync(currency, from, count, fromId, endId, order, since, end, CancellationToken.None);

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
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="WithdrawHistoryResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">currency</exception>
        public Task<IDictionary<string, WithdrawHistoryResponse>> WithdrawHistoryAsync(
            string currency, int? from, int? count, int? fromId, int? endId,
            string order, long? since, long? end, CancellationToken token)
        {
            if (currency == null) throw new ArgumentNullException(nameof(currency));

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
        /// <returns><see cref="WithdrawHistoryResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="ArgumentException">パラメータ'currency'が指定されていません。 - parameters</exception>
        public Task<IDictionary<string, WithdrawHistoryResponse>> WithdrawHistoryAsync(IDictionary<string, string> parameters) =>
             WithdrawHistoryAsync(parameters, CancellationToken.None);

        /// <summary>
        /// 出金履歴を取得します。
        /// </summary>
        /// <param name="parameters">パラメータ</param>
        /// <param name="token"><see cref="CancellationToken"/>オブジェクト。</param>
        /// <returns><see cref="WithdrawHistoryResponse"/>オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">parameters</exception>
        /// <exception cref="ArgumentException">パラメータ'currency'が指定されていません。 - parameters</exception>
        public Task<IDictionary<string, WithdrawHistoryResponse>> WithdrawHistoryAsync(
            IDictionary<string, string> parameters, CancellationToken token)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            if (!parameters.ContainsKey("currency"))
                throw new ArgumentException("パラメータ'currency'が指定されていません。", nameof(parameters));

            return _client.PostAsync<IDictionary<string, WithdrawHistoryResponse>>(
                nameof(WithdrawHistoryAsync).ToApiMethodName(), parameters, token);
        }
    }
}
