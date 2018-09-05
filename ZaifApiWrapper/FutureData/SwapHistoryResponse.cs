using Newtonsoft.Json;

namespace ZaifApiWrapper.FutureData
{
    /// <summary>
    /// 先物公開 - trades
    /// </summary>
    public class SwapHistoryResponse
    {
        /// <summary>
        /// スワップポイント確定日時(UNIX_TIMESTAMP)
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
        /// <summary>
        /// 買いスワップレート
        /// </summary>
        [JsonProperty("swap_rate_bid")]
        public decimal SwapRateBid { get; set; }
        /// <summary>
        /// 売りスワップレート
        /// </summary>
        [JsonProperty("swap_rate_ask")]
        public decimal SwapRateAsk { get; set; }
    }
}
