using Newtonsoft.Json;

namespace ZaifApiWrapper.FutureData
{
    /// <summary>
    /// 先物公開 - last_price
    /// </summary>
    public class LastPriceResponse
    {
        //TODO: last_priceのドキュメントには'all'または'active'を指定するとgroup_id付きのデータのコレクションが取得できると書いてあるが、実際にやってみるとエラーになったため保留。
        ///// <summary>
        ///// グループID
        ///// </summary>
        //[JsonProperty("group_id")]
        //public int? GroupId { get; set; }
        /// <summary>
        /// 現在の終値
        /// </summary>
        [JsonProperty("last_price")]
        public decimal LastPrice { get; set; }
    }
}
