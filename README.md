# ZaifApiWrapper

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

メソッド名は、**(APIのメソッド名のPascalCase)+Async**です。

(例)```currency_pairs```→```CurrencyPairsAsync()```

## Example

### 現物公開API、先物公開API

C# 7.1 or higher

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

C# 7.1 or higher

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

## Licence
[MIT Licence](https://github.com/gentarok/ZaifApiWrapper/blob/master/LICENSE)