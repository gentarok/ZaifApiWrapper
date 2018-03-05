using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZaifApiWrapper.TradeData
{
    /// <summary>
    /// 現物取引API - get_info
    /// </summary>
    public class GetInfoResponse
    {
        /// <summary>
        /// 資産の残高
        /// </summary>
        [JsonProperty("funds")]
        public IDictionary<string, decimal> Funds { get; set; }
        /// <summary>
        /// 資産の残高に注文情報を加味したもの
        /// </summary>
        /// <remarks>
        /// depositは現在の資産の残高に注文情報を加味したものになります。
        /// 買い注文が存在する場合、その注文の値段と量をかけ合わせたもので、
        /// 売り注文が存在する場合は、その注文の量のみが加味されます。
        /// </remarks>
        [JsonProperty("deposit")]
        public IDictionary<string, decimal> Deposit { get; set; }
        /// <summary>
        /// キーが保持している権限
        /// </summary>
        [JsonProperty("rights")]
        public IDictionary<string, int> Rights { get; set; }
        /// <summary>
        /// 実行したトレード数
        /// </summary>
        [JsonProperty("trade_count")]
        public int TradeCount { get; set; }
        /// <summary>
        /// アクティブな注文数
        /// </summary>
        [JsonProperty("open_orders")]
        public int OpenOrders { get; set; }
        /// <summary>
        /// UNIX時間で換算された日本時間(UNIX_TIMESTAMP)
        /// </summary>
        [JsonProperty("server_time")]
        public long ServerTime { get; set; }
    }
}
