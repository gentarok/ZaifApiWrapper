using Newtonsoft.Json;

namespace ZaifApiWrapper.LeverageData
{
    /// <summary>
    /// レバレッジ取引 - get_positions
    /// </summary>
    public class GetPositionsResponse
    {
        /// <summary>
        /// グループID
        /// </summary>
        [JsonProperty("group_id")]
        public int GroupId { get; set; }
        /// <summary>
        /// 通貨ペア
        /// </summary>
        [JsonProperty("currency_pair")]
        public string CurrencyPair { get; set; }
        /// <summary>
        /// bid(買い) or ask(売り)
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// 価格
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// リミット価格
        /// </summary>
        [JsonProperty("limit")]
        public decimal Limit { get; set; }
        /// <summary>
        /// ストップ価格
        /// </summary>
        [JsonProperty("stop")]
        public decimal Stop { get; set; }
        /// <summary>
        /// 発注日時(UNIX_TIMESTAMP)
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
        /// <summary>
        /// 注文の有効期限(UNIX_TIMESTAMP)
        /// </summary>
        [JsonProperty("term_end")]
        public long TermEnd { get; set; }
        /// <summary>
        /// レバレッジ
        /// </summary>
        [JsonProperty("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// 支払い手数料
        /// </summary>
        [JsonProperty("fee_spent")]
        public decimal FeeSpent { get; set; }
        /// <summary>
        /// クローズ日時(UNIX_TIMESTAMP)
        /// </summary>
        [JsonProperty("timestamp_closed")]
        public long timestamp_closed { get; set; }
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
    }
}
