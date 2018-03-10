using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZaifApiWrapper.TradeData
{
    /// <summary>
    /// 現物取引API - trade
    /// </summary>
    public class TradeResponse
    {
        /// <summary>
        /// 今回の注文で約定した取引量
        /// </summary>
        [JsonProperty("received")]
        public decimal Received { get; set; }
        /// <summary>
        /// 今回の注文で約定せず、板に残った取引量
        /// </summary>
        [JsonProperty("remains")]
        public decimal Remains{ get; set; }
        /// <summary>
        /// 今回の注文がすべて成立した場合は0、一部、もしくはすべて約定しなかった場合は板に残った注文のID。
        /// </summary>
        [JsonProperty("order_id")]
        public int OrderId { get; set; }
        /// <summary>
        /// 残高
        /// </summary>
        [JsonProperty("funds")]
        public IDictionary<string, decimal> Funds { get; set; }
    }
}
