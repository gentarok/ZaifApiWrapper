# ZaifApiWrapper

Zaif API (v1.1.1) .NET Wrapper Library

[Zaif](https://zaif.jp/)��API��.NET���痘�p���邽�߂̃��b�p�[���C�u�����ł��B

## Target Framework
.NET Standard 2.0

## NuGet

```
PM> Install-Package ZaifApiWrapper
```

## Usage

[�����h�L�������g](http://techbureau-api-document.readthedocs.io/ja/latest/index.html)�ɋL�ڂ���Ă���eAPI�ɑΉ������N���X�𗘗p���܂��B


|API|�N���X|���l|
|:---|:---|:---|
|�������JAPI|ZaifApiWrapper.PublicApi|�X�g���[�~���OAPI�͊܂݂܂���|
|�������API|ZaifApiWrapper.TradeApi|`active_orders`��`is_token_both`�p�����[�^�̎w��̓T�|�[�g���܂���|
|�敨���JAPI|ZaifApiWrapper.FutureApi||
|���o���b�W���API|ZaifApiWrapper.LeverageApi||

���\�b�h���́A**(API�̃��\�b�h����PascalCase)+Async**�ł��B

(��)```currency_pairs```��```CurrencyPairsAsync()```

## Example

### �������JAPI�A�敨���JAPI

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

### �������API�A���o���b�W���API

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