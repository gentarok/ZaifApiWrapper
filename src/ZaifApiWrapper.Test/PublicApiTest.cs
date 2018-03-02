using Moq;
using System.Collections.Generic;
using Xunit;
using ZaifApiWrapper.PublicData;

namespace ZaifApiWrapper.Test
{
    public class PublicApiTest
    {
        #region Test data

        public static object[][] ApiClientOptionData = new object[][]
        {
            new object[] { new ApiClientOption(new Mock<IHttpClientAccessor>().Object) }
        };

        public static object[][] CurrenciesAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン（引数は最小限）
            new object[]
            {
                //戻り値
                @"[
    {
        ""name"": ""btc"",
        ""is_token"": false
    },
    {
        ""name"": ""XCP"",
        ""is_token"": true
    },
]",
                //引数
                "_",
            },
            //引数最小値全指定、戻り値最小値（ゼロ）
            new object[]
            {
                //戻り値
                @"[]",
                //引数
                "_",
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                @"[
    {
        ""name"": ""btc"",
        ""is_token"": false
    },
    {
        ""name"": ""xem"",
        ""is_token"": false
    },
    {
        ""name"": ""XCP"",
        ""is_token"": true
    },
]",
                //引数
                "all",
            },
        };

        public static object[][] CurrencyPairsAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン（引数は最小限）
            new object[]
            {
                //戻り値
                @"[
    {
        ""name"": ""BTC/JPY"",
        ""title"": ""BTC/JPY"",
        ""currency_pair"": ""btc_jpy"",
        ""description"": ""\u30d3\u30c3\u30c8\u30b3\u30a4\u30f3\u30fb\u65e5\u672c\u5186\u306e\u53d6\u5f15\u3092\u884c\u3046\u3053\u3068\u304c\u3067\u304d\u307e\u3059"",
        ""is_token"": false,
        ""event_number"": 0,
        ""item_unit_min"": 0.0001,
        ""item_unit_step"": 0.0001,
        ""aux_unit_min"": 5.0,
        ""aux_unit_step"": 5.0,
        ""seq"": 0,
        ""aux_japanese"": ""\u65e5\u672c\u5186"",
        ""item_japanese"": ""\u30d3\u30c3\u30c8\u30b3\u30a4\u30f3"",
        ""aux_unit_point"": 0,
    },
    {
        ""name"": ""KINOKOUSAKA/JPY"",
        ""title"": ""KINOKOUSAKA/JPY \u53d6\u5f15\u6240 - ZAIF Exchange"",
        ""currency_pair"": ""kinokousaka_jpy"",
        ""description"": ""KINOKOUSAKA/JPY \u53d6\u5f15\u6240\u3002KINOKOUSAKA\u3068\u65e5\u672c\u5186\u306e\u53d6\u5f15\u304c\u884c\u3048\u307e\u3059\u3002"",
        ""is_token"": true,
        ""event_number"": 1,
        ""item_unit_min"": 0.01,
        ""item_unit_step"": 0.01,
        ""aux_unit_min"": 0.01,
        ""aux_unit_step"": 0.01,
        ""seq"": 134,
        ""aux_japanese"": ""\u65e5\u672c\u5186"",
        ""item_japanese"": ""KINOKOUSAKA"",
        ""aux_unit_point"": 2,
    }
]",
                //引数
                "all",
            },
            //引数最小値全指定、戻り値最小値（１つ）
            new object[]
            {
                //戻り値
                @"[
    {
        ""name"": ""BTC/JPY"",
        ""title"": ""BTC/JPY"",
        ""currency_pair"": ""btc_jpy"",
        ""description"": ""\u30d3\u30c3\u30c8\u30b3\u30a4\u30f3\u30fb\u65e5\u672c\u5186\u306e\u53d6\u5f15\u3092\u884c\u3046\u3053\u3068\u304c\u3067\u304d\u307e\u3059"",
        ""is_token"": false,
        ""event_number"": 0,
        ""item_unit_min"": 0.0001,
        ""item_unit_step"": 0.0001,
        ""aux_unit_min"": 5.0,
        ""aux_unit_step"": 5.0,
        ""seq"": 0,
        ""aux_japanese"": ""\u65e5\u672c\u5186"",
        ""item_japanese"": ""\u30d3\u30c3\u30c8\u30b3\u30a4\u30f3"",
        ""aux_unit_point"": 0,
    }
]",
                //引数
                "_",
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                @"[
    {
        ""name"": ""XXXXXXXXXX"",
        ""title"": ""XXXXXXXXXX"",
        ""currency_pair"": ""XXXXXXXXXX"",
        ""description"": ""\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff"",
        ""is_token"": false,
        ""event_number"": 2147483647,
        ""item_unit_min"": 9999999999.99999999,
        ""item_unit_step"": 9999999999.99999999,
        ""aux_unit_min"": 9999999999.99999999,
        ""aux_unit_step"": 9999999999.99999999,
        ""seq"": 2147483647,
        ""aux_japanese"": ""\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff"",
        ""item_japanese"": ""\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff"",
        ""aux_unit_point"": 2147483647,
    },
    {
        ""name"": ""XXXXXXXXXX"",
        ""title"": ""XXXXXXXXXX"",
        ""currency_pair"": ""XXXXXXXXXX"",
        ""description"": ""\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff"",
        ""is_token"": false,
        ""event_number"": 2147483647,
        ""item_unit_min"": 9999999999.99999999,
        ""item_unit_step"": 9999999999.99999999,
        ""aux_unit_min"": 9999999999.99999999,
        ""aux_unit_step"": 9999999999.99999999,
        ""seq"": 2147483647,
        ""aux_japanese"": ""\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff"",
        ""item_japanese"": ""\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff\uffff"",
        ""aux_unit_point"": 2147483647,
    }
]",
                //引数
                "all",
            },
        };

        public static object[][] LastPriceAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン（引数は最小限）
            new object[]
            {
                //戻り値
                @"{""last_price"": 134820.0}",
                //引数
                "_",
            },
            //引数最小値全指定、戻り値最小値（１つ）
            new object[]
            {
                //戻り値
                @"{""last_price"": 0}",
                //引数
                "_",
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"{""last_price"": 9999999999.99999999}",
                //引数
                "XXXXXXXXXX",
            },
        };

        public static object[][] TickerAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン（引数は最小限）
            new object[]
            {
                //戻り値
                @"{
    ""last"": 135875.0,
    ""high"": 136000.0,
    ""low"": 131570.0,
    ""vwap"": 133301.7489,
    ""volume"": 6889.215,
    ""bid"": 135875.0,
    ""ask"": 135920.0
}",
                //引数
                "_",
            },
            //引数最小値全指定、戻り値最小値（１つ）
            new object[]
            {
                //戻り値
                @"{
   ""last"": 0,
   ""high"": 0,
   ""low"": 0,
   ""vwap"": 0,
   ""volume"": 0,
   ""bid"": 0,
   ""ask"": 0
}",
                //引数
                "_",
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"{
   ""last"": 9999999999.99999999,
   ""high"": 9999999999.99999999,
   ""low"": 9999999999.99999999,
   ""vwap"": 9999999999.99999999,
   ""volume"": 9999999999.99999999,
   ""bid"": 9999999999.99999999,
   ""ask"": 9999999999.99999999
}",
                //引数
                "XXXXXXXXXX",
            },
        };

        public static object[][] TradesAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン（引数は最小限）
            new object[]
            {
                //戻り値
                @"[
    {
        ""date"": 1491756592,
        ""price"": 135340.0,
        ""amount"": 0.02,
        ""tid"": 102659,
        ""currency_pair"": ""btc_jpy"",
        ""trade_type"": ""ask""
    },
    {
        ""date"": 1491756591,
        ""price"": 135345.0,
        ""amount"": 0.01,
        ""tid"": 102658,
        ""currency_pair"": ""btc_jpy"",
        ""trade_type"": ""bid""
    },
]",
                //引数
                "_",
            },
            //引数最小値全指定、戻り値最小値（ゼロ）
            new object[]
            {
                //戻り値
                @"[]",
                //引数
                "_",
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"[
    {
        ""date"": 9223372036854775807,
        ""price"": 9999999999.99999999,
        ""amount"":9999999999.99999999,
        ""tid"": 2147483647,
        ""currency_pair"": ""XXXXXXXXXX"",
        ""trade_type"": ""ask""
    },
    {
        ""date"": 9223372036854775807,
        ""price"": 9999999999.99999999,
        ""amount"":9999999999.99999999,
        ""tid"": 0,
        ""currency_pair"": ""ZZZZZZZZZZ"",
        ""trade_type"": ""bid""
    },
]",
                //引数
                "XXXXXXXXXX",
            },
        };

        public static object[][] DepthAsyncSuccessData = new object[][]
{
            //APIドキュメントに記載されているデータを返すパターン（引数は最小限）
            new object[]
            {
                //戻り値
                @"{
    ""asks"": [
        [
            134875.0,
            0.0063
        ],
        [
            134885.0,
            0.1639
        ],
    ],
    ""bids"": [
        [
            134870.0,
            0.01
        ],
        [
            134865.0,
            0.3066
        ],
    ]
}",
                //引数
                "_",
            },
            //引数最小値全指定、戻り値最小値（ゼロ）
            new object[]
            {
                //戻り値
                @"{
    ""asks"": [ ],
    ""bids"": [ ]
}",
                //引数
                "_",
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"{
    ""asks"": [
        [
            9999999999.99999999,
            9999999999.99999999,
        ],
    ],
    ""bids"": [
        [
            9999999999.99999999,
            9999999999.99999999,
        ],
        [
            9999999999.99999999,
            9999999999.99999999,
        ],
    ]
}",
                //引数
                "XXXXXXXXXX",
            },
};

        #endregion

        [Theory]
        [MemberData(nameof(ApiClientOptionData))]
        public void Construtcor_should_return_instance(ApiClientOption option)
        {
            //arrange

            //act
            var obj = new PublicApi(option);

            //assert
            Assert.NotNull(obj);
            Assert.IsType<PublicApi>(obj);
        }

        [Theory]
        [MemberData(nameof(CurrenciesAsyncSuccessData))]
        public async void CurrenciesAsync_should_success(string jsonString, string currency)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new PublicApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.CurrenciesAsync(currency);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<IEnumerable<CurrenciesResponse>>(actual);
        }

        [Theory]
        [MemberData(nameof(CurrencyPairsAsyncSuccessData))]
        public async void CurrencyPairsAsync_should_success(string jsonString, string currencyPair)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new PublicApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.CurrencyPairsAsync(currencyPair);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<IEnumerable<CurrencyPairsResponse>>(actual);
        }


        [Theory]
        [MemberData(nameof(LastPriceAsyncSuccessData))]
        public async void LastPriceAsync_should_success(string jsonString, string currencyPair)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new PublicApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.LastPriceAsync(currencyPair);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<LastPriceResponse>(actual);
        }

        [Theory]
        [MemberData(nameof(TickerAsyncSuccessData))]
        public async void TickerAsync_should_success(string jsonString, string currencyPair)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new PublicApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.TickerAsync(currencyPair);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<TickerResponse>(actual);
        }

        [Theory]
        [MemberData(nameof(TradesAsyncSuccessData))]
        public async void TradesAsync_should_success(string jsonString, string currencyPair)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new PublicApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.TradesAsync(currencyPair);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<IEnumerable<TradesResponse>>(actual);
        }

        [Theory]
        [MemberData(nameof(DepthAsyncSuccessData))]
        public async void DepthAsync_should_success(string jsonString, string currencyPair)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new PublicApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.DepthAsync(currencyPair);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<DepthResponse>(actual);
        }
    }
}
