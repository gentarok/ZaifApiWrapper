using System;

namespace ZaifApiWrapper
{
    /// <summary>
    /// 資格情報の形式例外
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class CredentialFormatException : Exception
    {
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="message">例外メッセージ</param>
        public CredentialFormatException(string message) : base(message) { }
    }
}
