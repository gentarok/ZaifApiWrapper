using Newtonsoft.Json;

namespace ZaifApiWrapper.PublicData
{
    /// <summary>
    /// 現物公開API - ticker
    /// </summary>
    public class TickerResponse
    {
        /// <summary>
        /// 終値
        /// </summary>
        [JsonProperty("last")]
        public decimal Last { get; set; }
        /// <summary>
        /// 過去24時間の高値
        /// </summary>
        [JsonProperty("high")]
        public decimal High { get; set; }
        /// <summary>
        /// 過去24時間の安値
        /// </summary>
        [JsonProperty("low")]
        public decimal Low { get; set; }
        /// <summary>
        /// 過去24時間の加重平均
        /// </summary>
        /// <remarks>
        /// <para>
        /// vwap算出方法
        /// 個々の取引価格 * 個々の取引量　→　A
        /// Aの過去24時間分を合算　→　B
        /// 過去24時間分の個々の取引量を合算　→　C
        /// B/C →　vwap
        /// </para>
        /// </remarks>
        [JsonProperty("vwap")]
        public decimal Vwap { get; set; }
        /// <summary>
        /// 過去24時間の出来高
        /// </summary>
        [JsonProperty("volume")]
        public decimal Volume { get; set; }
        /// <summary>
        /// 買気配値
        /// </summary>
        [JsonProperty("bid")]
        public decimal Bid { get; set; }
        /// <summary>
        /// 売気配値
        /// </summary>
        [JsonProperty("ask")]
        public decimal Ask { get; set; }
    }
}
