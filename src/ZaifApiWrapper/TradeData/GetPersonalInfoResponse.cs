using Newtonsoft.Json;

namespace ZaifApiWrapper.TradeData
{
    /// <summary>
    /// 現物取引API - get_personal_info
    /// </summary>
    public class GetPersonalInfoResponse
    {
        /// <summary>
        /// ニックネーム
        /// </summary>
        [JsonProperty("ranking_nickname")]
        public string RankingNickname { get; set; }
        /// <summary>
        /// 画像のパス
        /// </summary>
        [JsonProperty("icon_path")]
        public string IconPath { get; set; }
    }
}
