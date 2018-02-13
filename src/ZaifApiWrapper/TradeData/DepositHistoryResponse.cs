using Newtonsoft.Json;

namespace ZaifApiWrapper.TradeData
{
    /// <summary>
    /// 現物取引API - deposit_history
    /// </summary>
    [JsonObject("Result")]
    public class DepositHistoryResponse
    {
        /// <summary>
        /// 出金日時(UNIX_TIMESTAMP)
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
        /// <summary>
        /// 出金先アドレス
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }
        /// <summary>
        /// 取引量
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// トランザクションid
        /// </summary>
        [JsonProperty("txid")]
        public string Txid { get; set; }
    }
}
