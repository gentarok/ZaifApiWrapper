using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZaifApiWrapper.LeverageData
{
    /// <summary>
    /// レバレッジ取引 - create_position
    /// </summary>
    public class CreatePositionResponse
    {
        /// <summary>
        /// レバレッジID。
        /// </summary>
        [JsonProperty("leverage_id")]
        public int LeverageId { get; set; }
        /// <summary>
        /// 注文日時(UNIX_TIMESTAMP)
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
        /// <summary>
        /// 注文の有効期限(UNIX_TIMESTAMP)
        /// </summary>
        [JsonProperty("term_end")]
        public long TermEnd { get; set; }
        /// <summary>
        /// 平均建玉価格
        /// </summary>
        [JsonProperty("price_avg")]
        public decimal PriceAvg { get; set; }
        /// <summary>
        /// 建玉数
        /// </summary>
        [JsonProperty("amount_done")]
        public decimal AmountDone { get; set; }
        /// <summary>
        /// 実際にデポジットした額(JPY）
        /// </summary>
        [JsonProperty("deposit_jpy")]
        public decimal? DepositJpy { get; set; }
        /// <summary>
        /// デポジット時計算レート(JPY）
        /// </summary>
        [JsonProperty("deposit_price_jpy")]
        public decimal? DepositPriceJpy { get; set; }
        /// <summary>
        /// 実際にデポジットした額(BTC）
        /// </summary>
        [JsonProperty("deposit_btc")]
        public decimal? DepositBtc { get; set; }
        /// <summary>
        /// デポジット時計算レート(BTC）
        /// </summary>
        [JsonProperty("deposit_price_btc")]
        public decimal? DepositPriceBtc { get; set; }
        /// <summary>
        /// 実際にデポジットした額(XEM）
        /// </summary>
        [JsonProperty("deposit_xem")]
        public decimal? DepositXem { get; set; }
        /// <summary>
        /// デポジット時計算レート(XEM）
        /// </summary>
        [JsonProperty("deposit_price_xem")]
        public decimal? DepositPriceXem { get; set; }
        /// <summary>
        /// 実際にデポジットした額(MONA）
        /// </summary>
        [JsonProperty("deposit_mona")]
        public decimal? DepositMona { get; set; }
        /// <summary>
        /// デポジット時計算レート(MONA）
        /// </summary>
        [JsonProperty("deposit_price_mona")]
        public decimal? DepositPriceMona { get; set; }
        /// <summary>
        /// 残高
        /// </summary>
        [JsonProperty("funds")]
        public IDictionary<string, decimal> Funds { get; set; }
    }
}
