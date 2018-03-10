using Newtonsoft.Json;

namespace ZaifApiWrapper.LeverageData
{
    /// <summary>
    /// レバレッジ取引 - position_history
    /// </summary>
    public class PositionHistoryResponse
    {
        /// <summary>
        /// グループID
        /// </summary>
        [JsonProperty("group_id")]
        public int GroupId { get; set; }
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
        /// bidまたはask、自己取引の場合はbothとなります
        /// </summary>
        [JsonProperty("your_action")]
        public string YourAction { get; set; }
        /// <summary>
        /// 買いレバレッジID(自分の注文の場合のみ)
        /// </summary>
        [JsonProperty("bid_leverage_id")]
        public int? BidLeverageId { get; set; }
        /// <summary>
        /// 売りレバレッジID(自分の注文の場合のみ)
        /// </summary>
        [JsonProperty("ask_leverage_id")]
        public int? AskLeverageId { get; set; }
    }
}
