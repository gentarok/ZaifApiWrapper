using Newtonsoft.Json;

namespace ZaifApiWrapper.FutureData
{
    /// <summary>
    /// 先物公開 - last_price
    /// </summary>
    public class LastPriceResponse
    {
        /// <summary>
        /// グループID
        /// </summary>
        [JsonProperty("group_id")]
        public int? GroupId { get; set; }
        /// <summary>
        /// 現在の終値
        /// </summary>
        [JsonProperty("last_price")]
        public decimal LastPrice { get; set; }
    }
}
