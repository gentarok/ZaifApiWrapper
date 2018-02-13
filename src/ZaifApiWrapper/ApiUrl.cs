namespace ZaifApiWrapper
{
    /// <summary>
    /// APIのURL
    /// </summary>
    internal static class ApiUrl
    {
        /// <summary>
        /// ホストのURL
        /// </summary>
        internal static readonly string Base = "https://api.zaif.jp/";
        /// <summary>
        /// 現物公開APIのエンドポイント
        /// </summary>
        internal static readonly string Public = $"{Base}api/1";
        /// <summary>
        /// 現物取引APIのエンドポイント
        /// </summary>
        internal static readonly string Trade = $"{Base}tapi";
        /// <summary>
        /// 先物取引APIのエンドポイント
        /// </summary>
        internal static readonly string Future = $"{Base}fapi/1";
        /// <summary>
        /// レバレッジ取引APIのエンドポイント
        /// </summary>
        internal static readonly string Leverage = $"{Base}tlapi";
        /// <summary>
        /// ストリーミングAPIのURI(SSL WebSocket)
        /// </summary>
        internal static readonly string Stream = "wss://ws.zaif.jp/stream";
    }
}
