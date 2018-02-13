using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZaifApiWrapper.PublicData
{
    /// <summary>
    /// 現物公開API - depth
    /// </summary>
    public class DepthResponse
    {
        /// <summary>
        /// 売り板情報
        /// </summary>
        /// <remarks>
        /// 配列の最初が価格、最後が量
        /// </remarks>
        [JsonProperty("asks")]
        public IEnumerable<decimal[]> Asks { get; set; }
        /// <summary>
        /// 買い板情報
        /// </summary>
        /// <remarks>
        /// 配列の最初が価格、最後が量
        /// </remarks>
        [JsonProperty("bids")]
        public IEnumerable<decimal[]> Bids { get; set; }
    }
}
