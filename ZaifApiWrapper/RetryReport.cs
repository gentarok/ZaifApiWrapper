using System.Net;

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
        /// <param name="errorType">エラーの種類</param>
        /// <param name="statusCode">エラーのHttpStatusCode</param>
        /// <param name="errorMessage">エラーメッセージ</param>
        internal RetryReport(int retryCount, ErrorType errorType, HttpStatusCode? statusCode, string errorMessage)
        {
            RetryCount = retryCount;
            ErrorType = errorType;
            StatusCode = statusCode;
            ApiErrorMessage = errorMessage;
        }

        /// <summary>
        /// 再試行回数
        /// </summary>
        public int RetryCount { get; private set; }

        /// <summary>
        /// エラーの種類
        /// </summary>
        public ErrorType ErrorType { get; private set; }

        /// <summary>
        /// エラーのHttpStatusCode（APIエラーの場合はnull）
        /// </summary>
        public HttpStatusCode? StatusCode { get; private set; }

        /// <summary>
        /// エラーメッセージ（HTTPエラーの場合はnull）
        /// </summary>
        public string ApiErrorMessage { get; private set; }
    }
}
