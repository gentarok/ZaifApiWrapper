using System;

namespace ZaifApiWrapper
{
    /// <summary>
    /// DateTime型の拡張メソッド
    /// </summary>
    internal static class DateTimeExtention
    {
        /// <summary>
        /// 日時からUNIX_TIMESTAMPを取得します
        /// </summary>
        /// <param name="datetime">datetime</param>
        /// <returns>UNIX_TIMESTAMP</returns>
        internal static long ToUnixTimeStamp(this DateTime datetime) =>
            (long)(datetime.ToUniversalTime() - Definitions.UnixEpoch).TotalSeconds;
    }
}
