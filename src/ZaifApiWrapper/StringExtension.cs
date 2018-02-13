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
    }
}
