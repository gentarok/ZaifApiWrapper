# ZaifApiWrapper

[![NuGet version](https://badge.fury.io/nu/ZaifApiWrapper.svg)](https://badge.fury.io/nu/ZaifApiWrapper)
[![Build status](https://ci.appveyor.com/api/projects/status/lwx98gen2fa09jhh?svg=true)](https://ci.appveyor.com/project/gentarok/zaifapiwrapper)
[![codecov](https://codecov.io/gh/gentarok/ZaifApiWrapper/branch/master/graph/badge.svg)](https://codecov.io/gh/gentarok/ZaifApiWrapper)

Zaif API (v1.1.1) .NET Wrapper Library

[Zaif](https://zaif.jp/)のAPIを.NETから利用するためのラッパーライブラリです。

## Target Framework
.NET Standard 2.0

## NuGet

```
PM> Install-Package ZaifApiWrapper
```

## Usage

[公式ドキュメント](http://techbureau-api-document.readthedocs.io/ja/latest/index.html)に記載されている各APIに対応したクラスを利用します。


|API|クラス|備考|
|:---|:---|:---|
|現物公開API|ZaifApiWrapper.PublicApi|ストリーミングAPIは含みません|
|現物取引API|ZaifApiWrapper.TradeApi|`active_orders`の`is_token_both`パラメータの指定はサポートしません|
|先物公開API|ZaifApiWrapper.FutureApi||
|レバレッジ取引API|ZaifApiWrapper.LeverageApi||

メソッド名は **(APIのメソッド名のPascalCase)+Async** です。(例:```currency_pairs```→```CurrencyPairsAsync()```)

## Example

### 現物公開API、先物公開API

C# 7.1 or greater

```CSharp
using System;
using System.Threading.Tasks;
using ZaifApiWrapper;

namespace ConsoleApp1
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var api = new PublicApi(); // or new FutureApi();
            try
            {
                var currencies = await api.CurrenciesAsync();
                foreach (var currency in currencies)
                {
                    // Do Something...
                    Console.WriteLine(currency.Name);
                }
            }
            catch (Exception ex)
            {
                // Handle errors...
                Console.Error.WriteLine(ex);
            }
        }
    }
}
```

### 現物取引API、レバレッジ取引API

C# 7.1 or greater

```CSharp
using System;
using System.Threading.Tasks;
using ZaifApiWrapper;

namespace ConsoleApp1
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            string apiKey = args[0];    // API Key
            string apiSecret = args[1]; // API Secret
            var api = new TradeApi(apiKey, apiSecret); // or new LeverageApi(apiKey, apiSecret);
            try
            {
                var info = await api.GetInfoAsync();
                // Do Something...
                Console.WriteLine(info.OpenOrders);
            }
            catch (Exception ex)
            {
                // Handle errors...
                Console.Error.WriteLine(ex);
            }
        }
    }
}
```

## Appendix

### APIクラスに指定可能なオプション

各APIクラスの初期化時に`ApiClientOption`オブジェクトを渡すことで、以下のパラメータが指定できます。

|オプション|既定値|説明|
|:---|:---|:---|
|MaxRetry|10|API実行でエラーが発生した場合の再試行回数|
|HttpErrorRetryInterval|1000|`HttpStatusCodesToRetry`で指定されたエラーが発生した場合の再試行までのインターバル(ms)|
|HttpStatusCodesToRetry|`HttpStatusCode.BadGateway`<br/>`HttpStatusCode.ServiceUnavailable`<br>`HttpStatusCode.GatewayTimeout`|再試行対象のHTTPステータスコード|
|ApiErrorRetryInterval|5000|APIから`ApiErrorMessagePatternToRetry`にマッチするメッセージを受け取った場合の再試行までのインターバル(ms)|
|ApiErrorMessagePatternToRetry|"please try later\|temporarily unavailable\|not incremented"<br/>（[こちら](http://techbureau-api-document.readthedocs.io/ja/latest/faq/2_error_message.html)を参照してください。）|再試行対象のメッセージに含まれる文字列（正規表現で指定可）|


### APIメソッドの引数

#### 引数の型

`TradeApi`, `LeverageApi`はプリミティブデータ型以外に`IDictionary<string, string>`でも引数をとれます。
使いやすい方を使ってください。

引数は簡単なチェックはしていますが、公式ドキュメントを参照して正しい値をセットしてください。

また、`"bid"`, `"ask"`等の文字列で指定する引数のうち、変更の可能性が低いものについては、将来的には列挙型を追加することを検討しますが、現在は文字列のみの指定しか対応していません。

#### 呼び出しのキャンセル

各メソッドは`CancellationToken`を指定できます。（使い方については[こちら](https://docs.microsoft.com/ja-jp/dotnet/csharp/programming-guide/concepts/async/cancel-an-async-task-or-a-list-of-tasks)を参照してください）

ただし、サーバー側の処理完了後、レスポンスを受け取る前にキャンセルした場合などの対応は、必要に応じてプログラム側で行ってください。

#### 再試行時のコールバック

各メソッドは`IProgress<RetryReport>`を渡すことで、再試行を行った際のコールバックを指定する事ができます。

C# 7.1 or greater

```CSharp
using System;
using System.Threading;
using System.Threading.Tasks;
using ZaifApiWrapper;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var api = new PublicApi();
            var onRetry = new Progress<RetryReport>(OnRetry);
            try
            {
                var currencies = await api.CurrenciesAsync("all", CancellationToken.None, onRetry);
                // Do Something...
            }
            catch (Exception ex)
            {
                // Handle errors...
            }
        }

        static void OnRetry(RetryReport report)
        {
            Console.WriteLine($"Count:{report.RetryCount}");
            switch (report.ErrorType)
            {
                case ErrorType.HttpError:
                    Console.WriteLine($"StatusCode:{report.StatusCode}");
                    break;
                case ErrorType.ApiError:
                    Console.WriteLine($"ErrorMessage:{report.ApiErrorMessage}");
                    break;
            }
        }
    }
}
```

## Licence
[MIT Licence](https://github.com/gentarok/ZaifApiWrapper/blob/master/LICENSE)