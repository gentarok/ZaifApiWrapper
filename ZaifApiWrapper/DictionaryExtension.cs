using System;
using System.Collections.Generic;

namespace ZaifApiWrapper
{
    /// <summary>
    /// Dictionary型の拡張メソッド
    /// </summary>
    internal static class DictionaryExtension
    {
        // 引数チェック用

        /// <summary>
        /// <paramref name="key"/>のキーが含まれない場合に<see cref="ArgumentException"/>を発生させます。
        /// </summary>
        /// <param name="dictionary">dictionary</param>
        /// <param name="key">key</param>
        /// <param name="paramName">パラメータ名</param>
        /// <exception cref="ArgumentException"></exception>
        internal static void ThrowIfNotContainsKey(this IDictionary<string, string> dictionary, string key, string paramName)
        {
            if (!dictionary.ContainsKey(key))
                throw new ArgumentException($"パラメータ '{key}' が指定されていません。", paramName);
        }
    }
}
