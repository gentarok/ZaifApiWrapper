using System;
using System.Linq;

namespace ZaifApiWrapper
{
    /// <summary>
    /// String型の拡張メソッド
    /// </summary>
    internal static class StringExtension
    {
        // .NETの一般的な命名規則の名前から、APIで利用される名前に変換するため作成

        /// <summary>
        /// Snake Caseの文字列を取得する
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns>Snake Caseの文字列</returns>
        internal static string ToSnakeCase(this string value) =>
            string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();

        /// <summary>
        /// APIのメソッド名を取得する
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns>APIのメソッド名</returns>
        internal static string ToApiMethodName(this string value) =>
            value.Replace("Async", "").ToSnakeCase();

        // 引数チェック用

        /// <summary>
        /// nullもしくは空文字の場合に<see cref="ArgumentException"/>を発生させます。
        /// </summary>
        /// <param name="value">文字列</param>
        /// <param name="paramName"><see cref="ArgumentException"/>のパラメータ名</param>
        /// <param name="messageParamName">例外メッセージのパラメータ名文字列</param>
        /// <exception cref="ArgumentException"></exception>
        internal static void ThrowArgumentExceptionIfNullOrWhiteSpace(this string value, string paramName, string messageParamName = null) {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"'{messageParamName ?? paramName}'を指定してください。", paramName);
        }

        /// <summary>
        /// <paramref name="allowedValue"/>に含まれる文字列以外の場合に<see cref="ArgumentException"/>を発生させます。
        /// </summary>
        /// <param name="value">文字列</param>
        /// <param name="allowedValue">allowedValue</param>
        /// <param name="paramName"><see cref="ArgumentException"/>のパラメータ名</param>
        /// <param name="messageParamName">例外メッセージのパラメータ名文字列</param>
        /// <exception cref="ArgumentException"></exception>
        internal static void ThrowArgumentExcepitonIfNotContains(this string value, string[] allowedValue, string paramName, string messageParamName = null)
        {
            if (!allowedValue.Contains(value))
                throw new ArgumentException($"'{messageParamName ?? paramName}'は$'{string.Join(", ", allowedValue)}'のいずれかを指定してください。", paramName);
        }
    }
}
