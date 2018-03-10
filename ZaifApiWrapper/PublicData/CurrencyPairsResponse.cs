using Newtonsoft.Json;

namespace ZaifApiWrapper.PublicData
{
    /// <summary>
    /// 現物公開API - currency_pairs
    /// </summary>
    public class CurrencyPairsResponse
    {
        /// <summary>
        /// 通貨ペアの名前
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 通貨ペアのタイトル
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
        /// <summary>
        /// 通貨ペアのシステム文字列
        /// </summary>
        [JsonProperty("currency_pair")]
        public string CurrencyPair { get; set; }
        /// <summary>
        /// 通貨ペアの詳細
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// token種別
        /// </summary>
        /// <remarks>tokenの場合、true</remarks>
        [JsonProperty("is_token")]
        public bool IsToken { get; set; }
        /// <summary>
        /// イベントトークンの場合、0以外
        /// </summary>
        [JsonProperty("event_number")]
        public int EventNumber { get; set; }
        /// <summary>
        /// 通貨シークエンス
        /// </summary>
        [JsonProperty("seq")]
        public int Seq { get; set; }
        /// <summary>
        /// アイテム通貨最小値
        /// </summary>
        [JsonProperty("item_unit_min")]
        public decimal ItemUnitMin { get; set; }
        /// <summary>
        /// アイテム通貨入力単位
        /// </summary>
        [JsonProperty("item_unit_step")]
        public decimal ItemUnitStep { get; set; }
        /// <summary>
        /// アイテム通貨日本語表記
        /// </summary>
        [JsonProperty("item_japanese")]
        public string ItemJapanese { get; set; }
        /// <summary>
        /// 相手通貨最小値
        /// </summary>
        [JsonProperty("aux_unit_min")]
        public decimal AuxUnitMin { get; set; }
        /// <summary>
        /// 相手通貨入力単位
        /// </summary>
        [JsonProperty("aux_unit_step")]
        public decimal AuxUnitStep { get; set; }
        /// <summary>
        /// 相手通貨小数点
        /// </summary>
        [JsonProperty("aux_unit_point")]
        public int AuxUnitPoint { get; set; }
        /// <summary>
        /// 相手通貨日本語表記
        /// </summary>
        [JsonProperty("aux_japanese")]
        public string AuxJapanese { get; set; }
    }
}
