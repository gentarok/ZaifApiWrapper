using Newtonsoft.Json;

namespace ZaifApiWrapper.PublicData
{
    /// <summary>
    /// 現物公開API - last_price
    /// </summary>
    public class LastPriceResponse
    {
        /// <summary>
        /// 現在の終値
        /// </summary>
        [JsonProperty("last_price")]
        public decimal LastPrice { get; set; }
    }
}
