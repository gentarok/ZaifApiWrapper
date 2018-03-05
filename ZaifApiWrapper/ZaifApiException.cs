using System;

namespace ZaifApiWrapper
{
    /// <summary>
    /// API呼び出し例外
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ZaifApiException : Exception
    {
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="message">例外メッセージ</param>
        public ZaifApiException(string message) : base(message) { }
    }
}
