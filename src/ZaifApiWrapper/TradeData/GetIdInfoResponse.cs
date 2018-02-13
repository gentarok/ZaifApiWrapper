using Newtonsoft.Json;

namespace ZaifApiWrapper.TradeData
{
    /// <summary>
    /// 現物取引API - get_id_info
    /// </summary>
    public class GetIdInfoResponse
    {
        /// <summary>
        /// ユーザーid
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// メールアドレス
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }
        /// <summary>
        /// 名前
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// カナ
        /// </summary>
        [JsonProperty("kana")]
        public string Kana { get; set; }
        /// <summary>
        /// 認証済み
        /// </summary>
        [JsonProperty("certified")]
        public bool Certified { get; set; }
    }
}
