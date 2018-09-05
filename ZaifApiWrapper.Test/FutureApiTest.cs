using System.Collections.Generic;
using Xunit;
using ZaifApiWrapper.FutureData;

namespace ZaifApiWrapper.Test
{
    public class FutureApiTest
    {
        #region Test data

        public static object[][] ApiClientOptionData = new object[][]
        {
            new object[] { new ApiClientOption() }
        };

        public static object[][] GroupsAsyncSuccessData_1 = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン（引数は最小限）
            new object[]
            {
                //戻り値
                @"[
                    {
                        ""id"": 1,
                        ""currency_pair"": ""btc_jpy"",
                        ""start_timestamp"": 1480518000,
                        ""end_timestamp"": 4102412399,
                        ""use_swap"": false
                    },
                    {
                        ""id"": 2,
                        ""currency_pair"": ""btc_jpy"",
                        ""start_timestamp"": 1488294000,
                        ""end_timestamp"": 1498834800,
                        ""use_swap"": false
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
                        ""id"": 0,
                        ""currency_pair"": """",
                        ""start_timestamp"": 0,
                        ""end_timestamp"": 0,
                        ""use_swap"": false
                    },
                ]",
                //引数
                "all",
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"[
                    {
                        ""id"": 2147483647,
                        ""currency_pair"": ""XXXXXXXXXX"",
                        ""start_timestamp"": 9223372036854775807,
                        ""end_timestamp"": 9223372036854775807,
                        ""use_swap"": true
                    },
                    {
                        ""id"": 0,
                        ""currency_pair"": ""YYYYYYYYYY"",
                        ""start_timestamp"": 9223372036854775807,
                        ""end_timestamp"": 9223372036854775807,
                        ""use_swap"": false
                    }
                ]",
                //引数
                "active",
            },
        };

        public static object[][] GroupsAsyncSuccessData_2 = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン（引数は最小限）
            new object[]
            {
                //戻り値
                @"[
                    {
                        ""id"": 1,
                        ""currency_pair"": ""btc_jpy"",
                        ""start_timestamp"": 1480518000,
                        ""end_timestamp"": 4102412399,
                        ""use_swap"": false
                    },
                    {
                        ""id"": 2,
                        ""currency_pair"": ""btc_jpy"",
                        ""start_timestamp"": 1488294000,
                        ""end_timestamp"": 1498834800,
                        ""use_swap"": false
                    }
                ]",
                //引数
                0,
            },
            //引数最小値全指定、戻り値最小値（ゼロ）
            new object[]
            {
                //戻り値
                @"[]",
                //引数
                0,
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"[
                    {
                        ""id"": 2147483647,
                        ""currency_pair"": ""XXXXXXXXXX"",
                        ""start_timestamp"": 9223372036854775807,
                        ""end_timestamp"": 9223372036854775807,
                        ""use_swap"": true
                    },
                    {
                        ""id"": 0,
                        ""currency_pair"": ""YYYYYYYYYY"",
                        ""start_timestamp"": 9223372036854775807,
                        ""end_timestamp"": 9223372036854775807,
                        ""use_swap"": false
                    }
                ]",
                //引数
                int.MaxValue,
            },
        };

        public static object[][] LastPriceAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン（引数は最小限）
            new object[]
            {
                //戻り値
                @"{""last_price"": 112155.0}",
                //引数
                0,
                "_",
            },
            //引数最小値全指定、戻り値最小値（１つ）
            new object[]
            {
                //戻り値
                @"{""last_price"": 0}",
                //引数
                0,
                "_",
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"{""last_price"": 9999999999.99999999}",
                //引数
                int.MaxValue,
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
                   ""last"": 112155.0,
                   ""high"": 117000.0,
                   ""low"": 112155.0,
                   ""vwap"": 115847.1429,
                   ""volume"": 150.0007,
                   ""bid"": 116995.0,
                   ""ask"": 117000.0
                }",
                //引数
                0,
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
                0,
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
                int.MaxValue,
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
                0,
                "_",
            },
            //引数最小値全指定、戻り値最小値（ゼロ）
            new object[]
            {
                //戻り値
                @"[]",
                //引数
                0,
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
                int.MaxValue,
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
                0,
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
                0,
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
                int.MaxValue,
                "XXXXXXXXXX",
            },
        };

        public static object[][] SwapHistoryAsyncSuccessData = new object[][]
        {
        //
            //APIドキュメントに記載されているデータを返すパターン（引数は最小限）
            new object[]
            {
                //戻り値
                @"[
                    {
                        ""timestamp"": 1504008000,
                        ""swap_rate_bid"": 0.1,
                        ""swap_rate_ask"": -0.1
                    },
                    {
                        ""timestamp"": 1504008000,
                        ""swap_rate_bid"": 0.375,
                        ""swap_rate_ask"": -0.375
                    }
                ]",
                //引数
                0,
                "_",
                1,
            },
            //引数最小値全指定、戻り値最小値（ゼロ）
            new object[]
            {
                //戻り値
                @"[]",
                //引数
                0,
                "_",
                null,
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"[
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": -9223372036854775808,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": -9223372036854775808,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                    {
                        ""timestamp"": 9223372036854775807,
                        ""swap_rate_bid"": 9223372036854775807,
                        ""swap_rate_ask"": 9223372036854775807,
                    },
                ]",
                //引数
                int.MaxValue,
                "XXXXXXXXXX",
                100,
            },
        };

        #endregion

        [Fact]
        public void Construtcor_should_return_instance_1()
        {
            //arrange

            //act
            var obj = new FutureApi();

            //assert
            Assert.NotNull(obj);
            Assert.IsType<FutureApi>(obj);
        }

        [Theory]
        [MemberData(nameof(ApiClientOptionData))]
        public void Construtcor_should_return_instance_2(ApiClientOption option)
        {
            //arrange

            //act
            var obj = new FutureApi(option);

            //assert
            Assert.NotNull(obj);
            Assert.IsType<FutureApi>(obj);
        }

        [Theory]
        [MemberData(nameof(GroupsAsyncSuccessData_1))]
        public async void GroupsAsync_should_success_1(string jsonString, string group)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new FutureApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.GroupsAsync(group);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<IEnumerable<GroupsResponse>>(actual);
        }

        [Theory]
        [MemberData(nameof(GroupsAsyncSuccessData_2))]
        public async void GroupsAsync_should_success_2(string jsonString, int groupId)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new FutureApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.GroupsAsync(groupId);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<IEnumerable<GroupsResponse>>(actual);
        }

        [Theory]
        [MemberData(nameof(LastPriceAsyncSuccessData))]
        public async void LastPriceAsync_should_success(string jsonString, int groupId, string currencyPair)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new FutureApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.LastPriceAsync(groupId, currencyPair);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<LastPriceResponse>(actual);
        }

        [Theory]
        [MemberData(nameof(TickerAsyncSuccessData))]
        public async void TickerAsync_should_success(string jsonString, int groupId, string currencyPair)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new FutureApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.TickerAsync(groupId, currencyPair);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<TickerResponse>(actual);
        }

        [Theory]
        [MemberData(nameof(TradesAsyncSuccessData))]
        public async void TradesAsync_should_success(string jsonString, int groupId, string currencyPair)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new FutureApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.TradesAsync(groupId, currencyPair);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<IEnumerable<TradesResponse>>(actual);
        }

        [Theory]
        [MemberData(nameof(DepthAsyncSuccessData))]
        public async void DepthAsync_should_success(string jsonString, int groupId, string currencyPair)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new FutureApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.DepthAsync(groupId, currencyPair);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<DepthResponse>(actual);
        }

        [Theory]
        [MemberData(nameof(SwapHistoryAsyncSuccessData))]
        public async void SwapHistoryAsync_should_success(string jsonString, int groupId, string currencyPair, int? page)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new FutureApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.SwapHistoryAsync(groupId, currencyPair, page);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<IEnumerable<SwapHistoryResponse>>(actual);
        }
    }
}
