using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZaifApiWrapper.LeverageData
{
    /// <summary>
    /// レバレッジ取引 - cancel_position
    /// </summary>
    public class CancelPositionResponse
    {
        /// <summary>
        /// レバレッジID
        /// </summary>
        [JsonProperty("leverage_id")]
        public int LeverageId { get; set; }
        /// <summary>
        /// 支払い手数料
        /// </summary>
        [JsonProperty("fee_spent")]
        public decimal FeeSpent { get; set; }
        /// <summary>
        /// クローズ日時(UNIX_TIMESTAMP)
        /// </summary>
        [JsonProperty("timestamp_closed")]
        public long TimestampClosed { get; set; }
        /// <summary>
        /// 建玉平均価格
        /// </summary>
        [JsonProperty("price_avg")]
        public decimal PriceAvg { get; set; }
        /// <summary>
        /// 建玉数
        /// </summary>
        [JsonProperty("amount_done")]
        public decimal AmountDone { get; set; }
        /// <summary>
        /// 決済平均価格
        /// </summary>
        [JsonProperty("close_avg")]
        public decimal CloseAvg { get; set; }
        /// <summary>
        /// 決済数
        /// </summary>
        [JsonProperty("close_done")]
        public decimal CloseDone { get; set; }
        /// <summary>
        /// 実際に返却した額(JPY）
        /// </summary>
        [JsonProperty("refunded_jpy")]
        public decimal? RefundedJpy { get; set; }
        /// <summary>
        /// 実際に返却した額(JPY）
        /// </summary>
        [JsonProperty("refunded_price_jpy")]
        public decimal? RefundedPriceJpy { get; set; }
        /// <summary>
        /// 実際に返却した額(BTC）
        /// </summary>
        [JsonProperty("refunded_btc")]
        public decimal? RefundedBtc { get; set; }
        /// <summary>
        /// 実際に返却した額(BTC）
        /// </summary>
        [JsonProperty("refunded_price_btc")]
        public decimal? RefundedPriceBtc { get; set; }
        /// <summary>
        /// 実際に返却した額(XEM）
        /// </summary>
        [JsonProperty("refunded_xem")]
        public decimal? RefundedXem { get; set; }
        /// <summary>
        /// 実際に返却した額(XEM）
        /// </summary>
        [JsonProperty("refunded_price_xem")]
        public decimal? RefundedPriceXem { get; set; }
        /// <summary>
        /// 実際に返却した額(MONA）
        /// </summary>
        [JsonProperty("refunded_mona")]
        public decimal? RefundedMona { get; set; }
        /// <summary>
        /// 実際に返却した額(MONA）
        /// </summary>
        [JsonProperty("refunded_price_mona")]
        public decimal? RefundedPriceMona { get; set; }
        /// <summary>
        /// 受け取ったスワップの額(AirFXのみ）
        /// </summary>
        [JsonProperty("swap")]
        public decimal? Swap { get; set; }
        /// <summary>
        /// 追証ガード手数料(信用取引のみ)
        /// </summary>
        [JsonProperty("guard_fee")]
        public decimal? GuardFee { get; set; }
        /// <summary>
        /// 残高
        /// </summary>
        [JsonProperty("funds")]
        public IDictionary<string, decimal> Funds { get; set; }
    }
}
