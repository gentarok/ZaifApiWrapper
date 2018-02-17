# ZaifApiWrapper

Zaif API (v1.1.1) .NET Wrapper Library

[Zaif](https://zaif.jp/)��API��.NET���痘�p���邽�߂̃��b�p�[���C�u�����ł��B

[![Build status](https://ci.appveyor.com/api/projects/status/lwx98gen2fa09jhh?svg=true)](https://ci.appveyor.com/project/gentarok/zaifapiwrapper)

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

## Appendix

### API�N���X�Ɏw��\�ȃI�v�V����

�eAPI�N���X�̏���������`ApiClientOption`�I�u�W�F�N�g��n�����ƂŁA�ȉ��̃p�����[�^���w��ł��܂��B

|�I�v�V����|����l|����|
|:---||:---|:---|
|MaxRetry|10|API���s�ŃG���[�����������ꍇ�̍Ď��s��|
|HttpErrorRetryInterval|1000|`HttpStatusCodesToRetry`�Ŏw�肳�ꂽ�G���[�����������ꍇ�̍Ď��s�܂ł̃C���^�[�o��(ms)|
|HttpStatusCodesToRetry|`HttpStatusCode.BadGateway`<br/>`HttpStatusCode.ServiceUnavailable`<br>`HttpStatusCode.GatewayTimeout`|�Ď��s�Ώۂ�HTTP�X�e�[�^�X�R�[�h|
|ApiTimeoutRetryInterval|5000|[API�Ăяo���񐔂̋K���ɂ��^�C���A�E�g](http://techbureau-api-document.readthedocs.io/ja/latest/faq/2_error_message.html#time-wait-restriction-please-try-later)���ɍĎ��s���s���ꍇ�̃C���^�[�o��(ms)|


### API���\�b�h�̈���

#### �����̌^

`TradeApi`,`LeverageApi`�̓v���~�e�B�u�f�[�^�^�ȊO��`IDictionary<string, string>`�ł��������Ƃ�܂��B
�g���₷�������g���Ă��������B

�����͊ȒP�ȃ`�F�b�N�͂��Ă��܂����A�����h�L�������g���Q�Ƃ��Đ������l���Z�b�g���Ă��������B

�܂��A`"bid"`, `"ask"`���̕�����Ŏw�肷������̂����A�ύX�̉\�����Ⴂ���̂ɂ��ẮA�����I�ɂ͗񋓌^��ǉ����邱�Ƃ�\�肵�Ă��܂����A���݂͕�����݂̂̎w�肵���Ή����Ă��܂���B

#### �Ăяo���̃L�����Z��

�e���\�b�h��`CancellationToken`���w��ł��܂��B

`CancellationToken`�̎g�����ɂ��Ă�[������](https://docs.microsoft.com/ja-jp/dotnet/csharp/programming-guide/concepts/async/cancel-an-async-task-or-a-list-of-tasks)���Q�Ƃ��Ă��������B

## Licence
[MIT Licence](https://github.com/gentarok/ZaifApiWrapper/blob/master/LICENSE)