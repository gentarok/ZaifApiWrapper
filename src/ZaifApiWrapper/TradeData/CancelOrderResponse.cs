using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZaifApiWrapper.TradeData
{
    /// <summary>
    /// 現物取引API - cancel_order
    /// </summary>
    public class CancelOrderResponse
    {
        /// <summary>
        /// 注文id
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
