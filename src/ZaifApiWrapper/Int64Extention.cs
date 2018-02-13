using System;

namespace ZaifApiWrapper
{
    /// <summary>
    /// Int64型の拡張メソッド
    /// </summary>
    public static class Int64Extention
    {
        /// <summary>
        /// UNIX_TIMESTAMPからDateTimeを取得します
        /// </summary>
        /// <param name="unixTimeStamp">UNIX_TIMESTAMP</param>
        /// <returns><see cref="DateTime"/>オブジェクト。</returns>
        public static DateTime ToDateTime(this Int64 unixTimeStamp) =>
            Definitions.UnixEpoch.AddSeconds(unixTimeStamp);
    }
}
