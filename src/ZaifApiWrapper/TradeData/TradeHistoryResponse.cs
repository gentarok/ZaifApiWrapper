using Newtonsoft.Json;

namespace ZaifApiWrapper.TradeData
{
    /// <summary>
    /// 現物取引API - trade_history
    /// </summary>
    public class TradeHistoryResponse
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
        /// 手数料
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// bidまたはask、自己取引の場合はbothとなります
        /// </summary>
        [JsonProperty("your_action")]
        public string YourAction { get; set; }
        /// <summary>
        /// マイナス手数料分
        /// </summary>
        /// <remarks>APIドキュメントからは読み取れないが、実際にはnullの場合があるので注意</remarks>
        [JsonProperty("bonus")]
        public decimal? Bonus { get; set; }
        /// <summary>
        /// 取引日時(UNIX_TIMESTAMP)
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
