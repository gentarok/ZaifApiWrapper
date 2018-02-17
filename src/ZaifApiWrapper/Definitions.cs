using System;

namespace ZaifApiWrapper
{
    /// <summary>
    /// 定義済みの値
    /// </summary>
    internal static class Definitions
    {
        /// <summary>
        /// UNIX Epoch
        /// </summary>
        internal static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // 引数チェック用

        /// <summary>
        /// 注文の種類
        /// </summary>
        internal static readonly string[] Actions = { "bid", "ask" };
        /// <summary>
        /// 並び順
        /// </summary>
        internal static readonly string[] Orders = { "ASC", "DESC" };
        /// <summary>
        /// レバレッジ取引の種類
        /// </summary>
        internal static readonly string[] LeverageTypes = { "margin", "futures" };
        /// <summary>
        /// レバレッジ取引のグループ
        /// </summary>
        internal static readonly string[] LeverageGroups = { "all", "active" };
    }
}
