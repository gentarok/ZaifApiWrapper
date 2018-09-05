using System;
using System.Collections.Generic;
using Xunit;
using ZaifApiWrapper.LeverageData;

namespace ZaifApiWrapper.Test
{
    public class LeverageApiTest
    {
        #region Test data

        public static object[][] ApiClientOptionData = new object[][]
        {
            new object[] { new ApiClientOption() }
        };

        public static object[][] GetPositionsAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン（引数は最小限）
            new object[]
            {
                //戻り値
                @"{
                    ""success"":1,
                    ""return"":{
                        ""182"":{
                            ""group_id"":1,
                            ""currency_pair"":""btc_jpy"",
                            ""action"":""bid"",
                            ""leverage"":2.5,
                            ""price"":110005,
                            ""limit"":130000,
                            ""stop"":90000,
                            ""amount"":0.03,
                            ""fee_spent"":0,
                            ""timestamp"":1402018713,
                            ""term_end"":1404610713,
                            ""timestamp_closed"":1402019000,
                            ""deposit"":35.76,
                            ""deposit_jpy"":35.76,
                            ""refunded"":35.76,
                            ""refunded_jpy"":35.76,
                            ""swap"":0,
                        }
                    }
                }",
                //引数
                "margin",
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
            },
            //引数最小値全指定、戻り値最小値（１つ）
            new object[]
            {
                //戻り値
                @"{
                    ""success"":1,
                    ""return"":{
                        ""0"":{
                            ""group_id"":0,
                            ""currency_pair"":"""",
                            ""action"":"""",
                            ""leverage"":0,
                            ""price"":0,
                            ""limit"":0,
                            ""stop"":0,
                            ""amount"":0,
                            ""fee_spent"":0,
                            ""timestamp"":0,
                            ""term_end"":0,
                            ""timestamp_closed"":0,
                            ""deposit"":0,
                            ""refunded"":0,
                            ""swap"":0,
                        }
                    }
                }",
                //引数
                "margin",
                0,
                0,
                0,
                0,
                0,
                "ASC",
                0,
                0,
                "",
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"{
                    ""success"":1,
                    ""return"":{
                        ""2147483647"":{
                            ""group_id"":2147483647,
                            ""currency_pair"":""ZZZZZZZZZZ"",
                            ""action"":""ask"",
                            ""leverage"":999.9999,
                            ""price"":9999999999.99999999,
                            ""limit"":9999999999.99999999,
                            ""stop"":9999999999.99999999,
                            ""amount"":9999999999.99999999,
                            ""fee_spent"":9999999999.99999999,
                            ""timestamp"":9223372036854775807,
                            ""term_end"":9223372036854775807,
                            ""timestamp_closed"":9223372036854775807,
                            ""price_avg"":9999999999.99999999,
                            ""amount_done"":9999999999.99999999,
                            ""close_avg"":9999999999.99999999,
                            ""close_done"":9999999999.99999999,
                            ""deposit"":9999999999.99999999,
                            ""deposit_jpy"":9999999999.99999999,
                            ""deposit_btc"":9999999999.99999999,
                            ""deposit_xem"":9999999999.99999999,
                            ""deposit_mona"":9999999999.99999999,
                            ""deposit_price_jpy"":9999999999.99999999,
                            ""deposit_price_btc"":9999999999.99999999,
                            ""deposit_price_xem"":9999999999.99999999,
                            ""deposit_price_mona"":9999999999.99999999,
                            ""refunded"":9999999999.99999999,
                            ""refunded_jpy"":9999999999.99999999,
                            ""refunded_btc"":9999999999.99999999,
                            ""refunded_xem"":9999999999.99999999,
                            ""refunded_mona"":9999999999.99999999,
                            ""refunded_price_jpy"":9999999999.99999999,
                            ""refunded_price_btc"":9999999999.99999999,
                            ""refunded_price_xem"":9999999999.99999999,
                            ""refunded_price_mona"":9999999999.99999999,
                            ""swap"":9999999999.99999999,
                            ""guard_fee"":9999999999.99999999,
                        },
                        ""0"":{
                            ""group_id"":2147483647,
                            ""currency_pair"":""ZZZZZZZZZZ"",
                            ""action"":""bid"",
                            ""leverage"":999.9999,
                            ""price"":9999999999.99999999,
                            ""limit"":9999999999.99999999,
                            ""stop"":9999999999.99999999,
                            ""amount"":9999999999.99999999,
                            ""fee_spent"":9999999999.99999999,
                            ""timestamp"":9223372036854775807,
                            ""term_end"":9223372036854775807,
                            ""timestamp_closed"":9223372036854775807,
                            ""price_avg"":9999999999.99999999,
                            ""amount_done"":9999999999.99999999,
                            ""close_avg"":9999999999.99999999,
                            ""close_done"":9999999999.99999999,     
                            ""deposit"":9999999999.99999999,
                            ""deposit_jpy"":9999999999.99999999,
                            ""deposit_btc"":9999999999.99999999,
                            ""deposit_xem"":9999999999.99999999,
                            ""deposit_mona"":9999999999.99999999,
                            ""deposit_price_jpy"":9999999999.99999999,
                            ""deposit_price_btc"":9999999999.99999999,
                            ""deposit_price_xem"":9999999999.99999999,
                            ""deposit_price_mona"":9999999999.99999999,
                            ""refunded"":9999999999.99999999,
                            ""refunded_jpy"":9999999999.99999999,
                            ""refunded_btc"":9999999999.99999999,
                            ""refunded_xem"":9999999999.99999999,
                            ""refunded_mona"":9999999999.99999999,
                            ""refunded_price_jpy"":9999999999.99999999,
                            ""refunded_price_btc"":9999999999.99999999,
                            ""refunded_price_xem"":9999999999.99999999,
                            ""refunded_price_mona"":9999999999.99999999,
                            ""swap"":9999999999.99999999,
                            ""guard_fee"":9999999999.99999999,
                        },
                    }
                }",
                //引数
                "futures",
                int.MaxValue,
                int.MaxValue,
                int.MaxValue,
                int.MaxValue,
                int.MaxValue,
                "DESC",
                long.MaxValue,
                long.MaxValue,
                "XXXXXXXXXX", //長さは適当
            },
        };

        public static object[][] PositionHistoryAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン（引数は最小限）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""182"": {
                            ""group_id"": 1,
                            ""currency_pair"": ""btc_jpy"",
                            ""action"": ""bid"",
                            ""amount"": 0.0001,
                            ""price"": 499000,
                            ""timestamp"": 1504251232,
                            ""your_action"": ""bid"",
                            ""bid_leverage_id"": 182,
                        },
                        ""183"": {
                            ""group_id"": 1,
                            ""currency_pair"": ""btc_jpy"",
                            ""action"": ""ask"",
                            ""amount"": 0.0001,
                            ""price"": 450000,
                            ""timestamp"": 1504251267,
                            ""your_action"": ""ask"",
                            ""ask_leverage_id"": 182,
                        },
                    }
                }",
                //引数
                "margin",
                0,
                null,
            },
            //引数最小値全指定、戻り値最小値（１つ）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""0"": {
                            ""group_id"": 0,
                            ""currency_pair"": """",
                            ""action"": """",
                            ""amount"": 0,
                            ""price"": 0,
                            ""timestamp"": 0,
                            ""your_action"": """",
                        },
                    }
                }",
                //引数
                "margin",
                0,
                null,
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""2147483647"": {
                            ""group_id"": 2147483647,
                            ""currency_pair"": ""XXXXXXXXXX"",
                            ""action"": ""ask"",
                            ""amount"": 9999999999.99999999,
                            ""price"": 9999999999.99999999,
                            ""timestamp"": 9223372036854775807,
                            ""your_action"": ""ask"",
                            ""bid_leverage_id"": 2147483647,
                            ""ask_leverage_id"": 2147483647,
                        },
                        ""0"": {
                            ""group_id"": 2147483647,
                            ""currency_pair"": ""XXXXXXXXXX"",
                            ""action"": ""bid"",
                            ""amount"": 9999999999.99999999,
                            ""price"": 9999999999.99999999,
                            ""timestamp"": 9223372036854775807,
                            ""your_action"": ""bid"",
                            ""bid_leverage_id"": 2147483647,
                            ""ask_leverage_id"": 2147483647,
                        },
                    }
                }",
                //引数
                "futures",
                int.MaxValue,
                int.MaxValue,
            },
        };

        public static object[][] ActivePositionsAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン（引数は最小限）
            new object[]
            {
                //戻り値
                @"{
                    ""success"":1,
                    ""return"":{
                        ""184"":{
                            ""group_id"":""1"",
                            ""currency_pair"":""btc_jpy"",
                            ""action"":""ask"",
                            ""amount"":0.0001,
                            ""price"":450000,
                            ""timestamp"":1402021125,
                            ""term_end"":1404613125,
                            ""leverage"":1,
                            ""fee_spent"":0.0015,
                            ""price_avg"":450000,
                            ""amount_done"":0.0001,
                            ""deposit_jpy"":48.72
                        }
                    }
                }",
                //引数
                "margin",
                0,
                null,
            },
            //引数最小値全指定、戻り値最小値（１つ）
            new object[]
            {
                //戻り値
                @"{
                    ""success"":1,
                    ""return"":{
                        ""0"":{
                            ""group_id"":""0"",
                            ""currency_pair"":"""",
                            ""action"":"""",
                            ""amount"":0,
                            ""price"":0,
                            ""timestamp"":0,
                            ""term_end"":0,
                            ""leverage"":0,
                            ""fee_spent"":0,
                            ""price_avg"":0,
                            ""amount_done"":0,
                        }
                    }
                }",
                //引数
                "margin",
                0,
                null,
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"{
                    ""success"":1,
                    ""return"":{
                        ""2147483647"":{
                            ""group_id"":""2147483647"",
                            ""currency_pair"":""XXXXXXXXXX"",
                            ""action"":""ask"",
                            ""amount"":9999999999.99999999,
                            ""price"":9999999999.99999999,
                            ""limit"":9999999999.99999999,
                            ""stop"":9999999999.99999999,
                            ""timestamp"":9223372036854775807,
                            ""term_end"":9223372036854775807,
                            ""leverage"":9999.999,
                            ""fee_spent"":9999999999.99999999,
                            ""price_avg"":9999999999.99999999,
                            ""amount_done"":9999999999.99999999,
                            ""close_avg"":9999999999.99999999,
                            ""close_done"":9999999999.99999999,
                            ""deposit_jpy"":9999999999.99999999,
                            ""deposit_btc"":9999999999.99999999,
                            ""deposit_xem"":9999999999.99999999,
                            ""deposit_mona"":9999999999.99999999,
                            ""deposit_price_jpy"":9999999999.99999999,
                            ""deposit_price_btc"":9999999999.99999999,
                            ""deposit_price_xem"":9999999999.99999999,
                            ""deposit_price_mona"":9999999999.99999999,
                            ""swap"":9999999999.99999999,
                        },
                        ""0"":{
                            ""group_id"":""2147483647"",
                            ""currency_pair"":""XXXXXXXXXX"",
                            ""action"":""bid"",
                            ""amount"":9999999999.99999999,
                            ""price"":9999999999.99999999,
                            ""limit"":9999999999.99999999,
                            ""stop"":9999999999.99999999,
                            ""timestamp"":9223372036854775807,
                            ""term_end"":9223372036854775807,
                            ""leverage"":9999.999,
                            ""fee_spent"":9999999999.99999999,
                            ""price_avg"":9999999999.99999999,
                            ""amount_done"":9999999999.99999999,
                            ""close_avg"":9999999999.99999999,
                            ""close_done"":9999999999.99999999,
                            ""deposit_jpy"":9999999999.99999999,
                            ""deposit_btc"":9999999999.99999999,
                            ""deposit_xem"":9999999999.99999999,
                            ""deposit_mona"":9999999999.99999999,
                            ""deposit_price_jpy"":9999999999.99999999,
                            ""deposit_price_btc"":9999999999.99999999,
                            ""deposit_price_xem"":9999999999.99999999,
                            ""deposit_price_mona"":9999999999.99999999,
                            ""swap"":9999999999.99999999,
                        },
                    }
                }",
                //引数
                "futures",
                int.MaxValue,
                int.MaxValue,
            },
        };

        public static object[][] CreatePositionAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン（引数は最小限）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""leverage_id"": 22258,
                        ""timestamp"": 1504253833,
                        ""term_end"": 1506845833,
                        ""price_avg"": 118000,
                        ""amount_done"": 0.0001,
                        ""deposit_jpy"": 11.92,
                        ""funds"": {
                            ""jpy"": 325,
                            ""btc"": 1.392,
                            ""mona"": 2600
                        }
                    }
                }",
                //引数
                "margin",
                "_",
                "ask",
                0,
                0,
                0,
                null,
                null,
                null,
            },
            //引数最小値全指定、戻り値最小値（１つ）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""leverage_id"": 0,
                        ""timestamp"": 0,
                        ""term_end"": 0,
                        ""price_avg"": 0,
                        ""amount_done"": 0,
                        ""funds"": {
                        }
                    }
                }",
                //引数
                "margin",
                "_",
                "ask",
                0,
                0,
                0,
                null,
                null,
                null,
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""leverage_id"": 2147483647,
                        ""timestamp"": 9223372036854775807,
                        ""term_end"": 9223372036854775807,
                        ""price_avg"": 9999999999.99999999,
                        ""amount_done"": 9999999999.99999999,
                        ""deposit_jpy"":9999999999.99999999,
                        ""deposit_btc"":9999999999.99999999,
                        ""deposit_xem"":9999999999.99999999,
                        ""deposit_mona"":9999999999.99999999,
                        ""deposit_price_jpy"":9999999999.99999999,
                        ""deposit_price_btc"":9999999999.99999999,
                        ""deposit_price_xem"":9999999999.99999999,
                        ""deposit_price_mona"":9999999999.99999999,
                        ""funds"": {
                            ""jpy"": 9999999999.99999999,
                            ""btc"": 9999999999.99999999,
                            ""xem"": 9999999999.99999999,
                            ""mona"": 9999999999.99999999,
                        }
                    }
                }",
                //引数
                "futures",
                "XXXXXXXXXX",
                "bid",
                decimal.MaxValue,
                decimal.MaxValue,
                decimal.MaxValue,
                int.MaxValue,
                decimal.MaxValue,
                decimal.MaxValue,
            },
        };

        public static object[][] ChangePositionAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン（引数は最小限）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""leverage_id"": 22258,
                        ""price_avg"": 118000,
                        ""amount_done"": 0.0001,
                    }
                }",
                //引数
                "margin",
                0,
                0,
                null,
                null,
                null,
            },
            //引数最小値全指定、戻り値最小値（１つ）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""leverage_id"": 0,
                        ""price_avg"": 0,
                        ""amount_done"": 0,
                    }
                }",
                //引数
                "margin",
                0,
                0,
                null,
                null,
                null,
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""leverage_id"": 2147483647,
                        ""timestamp_closed"": 9223372036854775807,
                        ""price_avg"": 9999999999.99999999,
                        ""amount_done"": 9999999999.99999999,
                        ""close_avg"": 9999999999.99999999,
                        ""close_done"": 9999999999.99999999,
                        ""refunded_jpy"": 9999999999.99999999,
                        ""refunded_btc"": 9999999999.99999999,
                        ""refunded_xem"": 9999999999.99999999,
                        ""refunded_mona"": 9999999999.99999999,
                        ""refunded_price_jpy"": 9999999999.99999999,
                        ""refunded_price_btc"": 9999999999.99999999,
                        ""refunded_price_xem"": 9999999999.99999999,
                        ""refunded_price_mona"": 9999999999.99999999,
                        ""swap"": 9999999999.99999999,
                        ""guard_fee"": 9999999999.99999999,
                    }
                }",
                //引数
                "futures",
                int.MaxValue,
                decimal.MaxValue,
                int.MaxValue,
                decimal.MaxValue,
                decimal.MaxValue,
            },
        };

        public static object[][] CancelPositionAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン（引数は最小限）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""leverage_id"": 2072,
                        ""refunded_jpy"": 645.96,
                        ""funds"": {
                            ""btc"": 0.496,
                            ""jpy"": 1564.96,
                            ""xem"": 0.0,
                            ""mona"": 10.0
                        },
                        ""fee_spent"": 0.0,
                        ""timestamp_closed"": 1508384951,
                        ""swap"": 0.0
                    }
                }",
                //引数
                "margin",
                0,
                null,
            },
            //引数最小値全指定、戻り値最小値（１つ）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""leverage_id"": 0,
                        ""funds"": {
                        },
                        ""fee_spent"": 0,
                        ""timestamp_closed"": 0,
                    }
                }",
                //引数
                "margin",
                0,
                null,

            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""leverage_id"": 2147483647,
                        ""refunded_jpy"": 9999999999.99999999,
                        ""refunded_btc"": 9999999999.99999999,
                        ""refunded_xem"": 9999999999.99999999,
                        ""refunded_mona"": 9999999999.99999999,
                        ""refunded_price_jpy"": 9999999999.99999999,
                        ""refunded_price_btc"": 9999999999.99999999,
                        ""refunded_price_xem"": 9999999999.99999999,
                        ""refunded_price_mona"": 9999999999.99999999,
                        ""funds"": {
                            ""jpy"": 9999999999.99999999,
                            ""btc"": 9999999999.99999999,
                            ""xem"": 9999999999.99999999,
                            ""mona"": 9999999999.99999999,
                        },
                        ""fee_spent"": 9999999999.99999999,
                        ""timestamp_closed"": 9223372036854775807,
                        ""price_avg"": 9999999999.99999999,
                        ""amount_done"": 9999999999.99999999,
                        ""close_avg"": 9999999999.99999999,
                        ""close_done"": 9999999999.99999999,
                        ""swap"": 9999999999.99999999,
                        ""guard_fee"": 9999999999.99999999,
                    }
                }",
                //引数
                "futures",
                int.MaxValue,
                int.MaxValue,
            },
        };

        #endregion

        [Theory]
        [InlineData(TestHelper.VALID_CREDENTIAL, TestHelper.VALID_CREDENTIAL)]
        public void Construtcor_should_return_instance_1(string apiKey, string apiSecret)
        {
            //arrange

            //act
            var obj = new LeverageApi(apiKey, apiSecret);

            //assert
            Assert.NotNull(obj);
            Assert.IsType<LeverageApi>(obj);
        }

        [Theory]
        [MemberData(nameof(ApiClientOptionData))]
        public void Construtcor_should_return_instance_2(ApiClientOption option)
        {
            //arrange

            //act
            var obj = new LeverageApi(option);

            //assert
            Assert.NotNull(obj);
            Assert.IsType<LeverageApi>(obj);
        }

        [Theory]
        [MemberData(nameof(GetPositionsAsyncSuccessData))]
        public async void GetPositionsAsync_should_success(string jsonString, string type, int? groupId,
            int? from, int? count, int? fromId, int? endId, string order, long? since, long? end, string currencyPair)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new LeverageApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.GetPositionsAsync(
                    type, groupId, from, count, fromId, endId, order, since, end, currencyPair);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<IDictionary<int, GetPositionsResponse>>(actual);
        }

        [Theory]
        [MemberData(nameof(PositionHistoryAsyncSuccessData))]
        public async void PositionHistoryAsync_should_success(string jsonString, string type, int leverageId, int? groupId)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new LeverageApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.PositionHistoryAsync(type, leverageId, groupId);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<IDictionary<int, PositionHistoryResponse>>(actual);
        }

        [Theory]
        [MemberData(nameof(ActivePositionsAsyncSuccessData))]
        public async void ActivePositionsAsync_should_success(string jsonString, string type, int? groupId, string currencyPair)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new LeverageApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.ActivePositionsAsync(type, groupId, currencyPair);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<IDictionary<int, ActivePositionsResponse>>(actual);
        }

        [Theory]
        [MemberData(nameof(CreatePositionAsyncSuccessData))]
        public async void CreatePositionAsync_should_success(string jsonString, string type, string currencyPair,
            string action, decimal amount, decimal price, decimal leverage, int? groupId, decimal? limit, decimal? stop)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new LeverageApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.CreatePositionAsync(type, currencyPair, action, amount, price, leverage, groupId, limit, stop);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<CreatePositionResponse>(actual);
        }

        [Theory]
        [MemberData(nameof(ChangePositionAsyncSuccessData))]
        public async void ChangePositionAsync_should_success(string jsonString, string type, int leverageId,
            decimal price, int? groupId, decimal? limit, decimal? stop)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new LeverageApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.ChangePositionAsync(type, leverageId, price, groupId, limit, stop);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<ChangePositionResponse>(actual);
        }

        [Theory]
        [MemberData(nameof(CancelPositionAsyncSuccessData))]
        public async void CancelPositionAsync_1_should_success(string jsonString, string type, int leverageId, int? groupId)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new LeverageApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.CancelPositionAsync(type, leverageId, groupId);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<CancelPositionResponse>(actual);
        }

        [Fact]
        public void GetPositionsAsync_should_throw_ArgumentException_if_futures_group_id_is_null_1()
        {
            //arrange
            var obj = new LeverageApi(TestHelper.CreateApiClientWithMockHttpAccessor());

            //act
            var actual = Record.ExceptionAsync(async () => await obj.GetPositionsAsync("futures", groupId: null));

            //assert
            Assert.IsType<ArgumentException>(actual.Result);
        }

        [Fact]
        public void GetPositionsAsync_should_throw_ArgumentException_if_futures_group_id_is_null_2()
        {
            //arrange
            var obj = new LeverageApi(TestHelper.CreateApiClientWithMockHttpAccessor());
            var parameters = new Dictionary<string, string>
            {
                { "type", "futures" }
            };

            //act
            var actual = Record.ExceptionAsync(async () => await obj.GetPositionsAsync(parameters));

            //assert
            Assert.IsType<ArgumentException>(actual.Result);
        }

        [Fact]
        public void PositionHistoryAsync_should_throw_ArgumentException_if_futures_group_id_is_null_1()
        {
            //arrange
            var obj = new LeverageApi(TestHelper.CreateApiClientWithMockHttpAccessor());

            //act
            var actual = Record.ExceptionAsync(async () => await obj.PositionHistoryAsync("futures", 1, groupId: null));

            //assert
            Assert.IsType<ArgumentException>(actual.Result);
        }

        [Fact]
        public void PositionHistoryAsync_should_throw_ArgumentException_if_futures_group_id_is_null_2()
        {
            //arrange
            var obj = new LeverageApi(TestHelper.CreateApiClientWithMockHttpAccessor());
            var parameters = new Dictionary<string, string>
            {
                { "type", "futures" },
                { "leverage_id", "1" }
            };

            //act
            var actual = Record.ExceptionAsync(async () => await obj.PositionHistoryAsync(parameters));

            //assert
            Assert.IsType<ArgumentException>(actual.Result);
        }

        [Fact]
        public void ActivePositionsAsync_should_throw_ArgumentException_if_futures_group_id_is_null_1()
        {
            //arrange
            var obj = new LeverageApi(TestHelper.CreateApiClientWithMockHttpAccessor());

            //act
            var actual = Record.ExceptionAsync(async () => await obj.ActivePositionsAsync("futures", groupId: null));

            //assert
            Assert.IsType<ArgumentException>(actual.Result);
        }

        [Fact]
        public void ActivePositionsAsync_should_throw_ArgumentException_if_futures_group_id_is_null_2()
        {
            //arrange
            var obj = new LeverageApi(TestHelper.CreateApiClientWithMockHttpAccessor());
            var parameters = new Dictionary<string, string>
            {
                { "type", "futures" }
            };

            //act
            var actual = Record.ExceptionAsync(async () => await obj.ActivePositionsAsync(parameters));

            //assert
            Assert.IsType<ArgumentException>(actual.Result);
        }

        [Fact]
        public void CreatePositionAsync_should_throw_ArgumentException_if_futures_group_id_is_null_1()
        {
            //arrange
            var obj = new LeverageApi(TestHelper.CreateApiClientWithMockHttpAccessor());

            //act
            var actual = Record.ExceptionAsync(async () => await obj.CreatePositionAsync("futures", "btc_jpy", "bid", 0, 0, 0, groupId: null));

            //assert
            Assert.IsType<ArgumentException>(actual.Result);
        }

        [Fact]
        public void CreatePositionAsync_should_throw_ArgumentException_if_futures_group_id_is_null_2()
        {
            //arrange
            var obj = new LeverageApi(TestHelper.CreateApiClientWithMockHttpAccessor());
            var parameters = new Dictionary<string, string>
            {
                { "type", "futures" },
                { "currency_pair", "btc_jpy" },
                { "action", "bid" },
                { "price", "0" },
                { "amount", "0" },
                { "leverage", "0" }
            };

            //act
            var actual = Record.ExceptionAsync(async () => await obj.CreatePositionAsync(parameters));

            //assert
            Assert.IsType<ArgumentException>(actual.Result);
        }

        [Fact]
        public void ChangePositionAsync_should_throw_ArgumentException_if_futures_group_id_is_null_1()
        {
            //arrange
            var obj = new LeverageApi(TestHelper.CreateApiClientWithMockHttpAccessor());

            //act
            var actual = Record.ExceptionAsync(async () => await obj.ChangePositionAsync("futures", 0, 0, groupId: null));

            //assert
            Assert.IsType<ArgumentException>(actual.Result);
        }

        [Fact]
        public void ChangePositionAsync_should_throw_ArgumentException_if_futures_group_id_is_null_2()
        {
            //arrange
            var obj = new LeverageApi(TestHelper.CreateApiClientWithMockHttpAccessor());
            var parameters = new Dictionary<string, string>
            {
                { "type", "futures" },
                { "price", "0" },
                { "amount", "0" },
                { "leverage_id", "0" }
            };

            //act
            var actual = Record.ExceptionAsync(async () => await obj.ChangePositionAsync(parameters));

            //assert
            Assert.IsType<ArgumentException>(actual.Result);
        }

        [Fact]
        public void CancelPositionAsync_should_throw_ArgumentException_if_futures_group_id_is_null_1()
        {
            //arrange
            var obj = new LeverageApi(TestHelper.CreateApiClientWithMockHttpAccessor());

            //act
            var actual = Record.ExceptionAsync(async () => await obj.CancelPositionAsync("futures", 1, groupId: null));

            //assert
            Assert.IsType<ArgumentException>(actual.Result);
        }

        [Fact]
        public void CancelPositionAsync_should_throw_ArgumentException_if_futures_group_id_is_null_2()
        {
            //arrange
            var obj = new LeverageApi(TestHelper.CreateApiClientWithMockHttpAccessor());
            var parameters = new Dictionary<string, string>
            {
                { "type", "futures" },
                { "leverage_id", "0" }
            };

            //act
            var actual = Record.ExceptionAsync(async () => await obj.CancelPositionAsync(parameters));

            //assert
            Assert.IsType<ArgumentException>(actual.Result);
        }

    }
}
