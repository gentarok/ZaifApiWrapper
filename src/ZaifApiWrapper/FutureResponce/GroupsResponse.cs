using Newtonsoft.Json;

namespace ZaifApiWrapper.FutureData
{
    /// <summary>
    /// 先物公開 - groups
    /// </summary>
    public class GroupsResponse
    {
        /// <summary>
        /// グループID
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// 通貨ペア
        /// </summary>
        [JsonProperty("currency_pair")]
        public string CurrencyPair { get; set; }
        /// <summary>
        /// 開始日時
        /// </summary>
        [JsonProperty("start_timestamp")]
        public long StartTimestamp { get; set; }
        /// <summary>
        /// 終了日時
        /// </summary>
        [JsonProperty("end_timestamp")]
        public long EndTimestamp { get; set; }
        /// <summary>
        /// スワップの有無
        /// </summary>
        [JsonProperty("use_swap")]
        public bool UseSwap { get; set; }
    }
}
