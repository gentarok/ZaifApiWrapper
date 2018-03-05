using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZaifApiWrapper.TradeData
{
    /// <summary>
    /// 現物取引API - withdraw
    /// </summary>
    public class WithdrawResponse
    {
        /// <summary>
        /// 出金id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// 振替えID
        /// </summary>
        [JsonProperty("txid")]
        public string Txid { get; set; }
        /// <summary>
        /// 今回の引き出しにかかった手数料
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// 残高
        /// </summary>
        [JsonProperty("funds")]
        public IDictionary<string, decimal> Funds { get; set; }
    }
}
