using Newtonsoft.Json;

namespace ZaifApiWrapper.TradeData
{
    /// <summary>
    /// 現物取引API - active_orders
    /// </summary>
    public class ActiveOrdersResponse
    {
        /// <summary>
        /// 通貨ペア
        /// </summary>
        [JsonProperty("currency_pair")]
        public string CurrencyPair { get; set; }
        /// <summary>
        /// bid(買い) or ask(売り)
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// 価格
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// 発注日時(UNIX_TIMESTAMP)
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
        /// <summary>
        /// 注文のコメント
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}
