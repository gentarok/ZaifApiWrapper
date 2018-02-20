using System;

namespace ZaifApiWrapper
{
    /// <summary>
    /// リトライ上限例外
    /// </summary>
    public class RetryCountOverException : Exception
    {
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="message">例外メッセージ</param>
        public RetryCountOverException(string message) : base(message) { }
    }
}
