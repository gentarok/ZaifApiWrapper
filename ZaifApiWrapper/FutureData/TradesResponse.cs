using Newtonsoft.Json;

namespace ZaifApiWrapper.FutureData
{
    /// <summary>
    /// 先物公開 - trades
    /// </summary>
    public class TradesResponse
    {
        /// <summary>
        /// 取引日時(UNIX_TIMESTAMP)
        /// </summary>
        [JsonProperty("date")]
        public long Date { get; set; }
        /// <summary>
        /// 取引価格
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// 取引量
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// 取引ID
        /// </summary>
        [JsonProperty("tid")]
        public int Tid { get; set; }
        /// <summary>
        /// 通貨ペア
        /// </summary>
        [JsonProperty("currency_pair")]
        public string CurrencyPair { get; set; }
        /// <summary>
        /// 取引種別
        /// </summary>
        [JsonProperty("trade_type")]
        public string TradeType { get; set; }
    }
}
