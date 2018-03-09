namespace ZaifApiWrapper
{
    /// <summary>
    /// API呼び出しで再試行が発生した場合の進捗報告
    /// </summary>
    public class RetryReport
    {
        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="retryCount">再試行回数</param>
        internal RetryReport(int retryCount)
        {
            RetryCount = retryCount;
        }

        /// <summary>
        /// 再試行回数
        /// </summary>
        public int RetryCount { get; private set; }
    }
}
