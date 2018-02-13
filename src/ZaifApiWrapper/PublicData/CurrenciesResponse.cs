using Newtonsoft.Json;

namespace ZaifApiWrapper.PublicData
{
    /// <summary>
    /// 現物公開API - currencies
    /// </summary>
    public class CurrenciesResponse
    {
        /// <summary>
        /// 通貨の名前
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// token種別
        /// </summary>
        /// <remarks>tokenの場合、true</remarks>
        [JsonProperty("is_token")]
        public bool IsToken { get; set; }
    }
}
