using System;
using System.Collections.Generic;
using Xunit;
using ZaifApiWrapper.TradeData;

namespace ZaifApiWrapper.Test
{
    public class TradeApiTest
    {
        #region Test data

        public static object[][] ApiClientOptionData = new object[][]
        {
            new object[] { new ApiClientOption() }
        };

        public static object[][] GetInfoAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン（引数は最小限）
            new object[]
            {
                //戻り値
                @"{
                    ""success"":1,
                    ""return"":{
                        ""funds"":{
                            ""jpy"":15320,
                            ""btc"":1.389,
                            ""xem"":100.2,
                            ""mona"":2600,
                            ""pepecash"":0.1
                        },
                        ""deposit"":{
                            ""jpy"":20440,
                            ""btc"":1.479,
                            ""xem"":100.2,
                            ""mona"":3200,
                            ""pepecash"":0.1
                        },
                        ""rights"":{
                            ""info"":1,
                            ""trade"":1,
                            ""withdraw"":0,
                            ""personal_info"":0,
                            ""id_info"":0,
                        },
                        ""trade_count"":18,
                        ""open_orders"":3,
                        ""server_time"":1401950833
                    }
                }",
                //引数
            },
            //引数最小値全指定、戻り値最小値（１つ）
            new object[]
            {
                //戻り値
                @"{
                    ""success"":1,
                    ""return"":{
                        ""funds"":{
                        },
                        ""deposit"":{
                        },
                        ""rights"":{
                        },
                        ""trade_count"":0,
                        ""open_orders"":0,
                        ""server_time"":0
                    }
                }",
                //引数
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"{
                    ""success"":1,
                    ""return"":{
                        ""funds"":{
                            ""jpy"":9999999999.99999999,
                            ""btc"":9999999999.99999999,
                            ""xem"":9999999999.99999999,
                            ""mona"":9999999999.99999999,
                            ""pepecash"":9999999999.99999999
                        },
                        ""deposit"":{
                            ""jpy"":9999999999.99999999,
                            ""btc"":9999999999.99999999,
                            ""xem"":9999999999.99999999,
                            ""mona"":9999999999.99999999,
                            ""pepecash"":9999999999.99999999
                        },
                        ""rights"":{
                            ""info"":1,
                            ""trade"":1,
                            ""withdraw"":1,
                            ""personal_info"":1,
                            ""id_info"":1,
                        },
                        ""trade_count"":2147483647,
                        ""open_orders"":2147483647,
                        ""server_time"":9223372036854775807
                    }
                }",
                //引数
            },
        };

        public static object[][] GetInfo2AsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン（引数は最小限）
            new object[]
            {
                //戻り値
                @"{
                    ""success"":1,
                    ""return"":{
                        ""funds"":{
                            ""jpy"":15320,
                            ""btc"":1.389,
                            ""xem"":100.2,
                            ""mona"":2600,
                            ""pepecash"":0.1
                        },
                        ""deposit"":{
                            ""jpy"":20440,
                            ""btc"":1.479,
                            ""xem"":100.2,
                            ""mona"":3200,
                            ""pepecash"":0.1
                        },
                        ""rights"":{
                            ""info"":1,
                            ""trade"":1,
                            ""withdraw"":0,
                            ""personal_info"":0,
                            ""id_info"":0,
                        },
                        ""open_orders"":3,
                        ""server_time"":1401950833
                    }
                }",
                //引数
            },
            //引数最小値全指定、戻り値最小値（１つ）
            new object[]
            {
                //戻り値
                @"{
                    ""success"":1,
                    ""return"":{
                        ""funds"":{
                        },
                        ""deposit"":{
                        },
                        ""rights"":{
                        },
                        ""open_orders"":0,
                        ""server_time"":0
                    }
                }",
                //引数
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"{
                    ""success"":1,
                    ""return"":{
                        ""funds"":{
                            ""jpy"":9999999999.99999999,
                            ""btc"":9999999999.99999999,
                            ""xem"":9999999999.99999999,
                            ""mona"":9999999999.99999999,
                            ""pepecash"":9999999999.99999999
                        },
                        ""deposit"":{
                            ""jpy"":9999999999.99999999,
                            ""btc"":9999999999.99999999,
                            ""xem"":9999999999.99999999,
                            ""mona"":9999999999.99999999,
                            ""pepecash"":9999999999.99999999
                        },
                        ""rights"":{
                            ""info"":1,
                            ""trade"":1,
                            ""withdraw"":1,
                            ""personal_info"":1,
                            ""id_info"":1,
                        },
                        ""open_orders"":2147483647,
                        ""server_time"":9223372036854775807
                    }
                }",
                //引数
            },
        };

        public static object[][] GetPersonalInfoAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン（引数は最小限）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""ranking_nickname"": ""ニックネーム"",
                        ""icon_path"": ""https://abs.twimg.com/sticky/default_profile_images/default_profile_0_normal.png""
                    }
                }",
                //引数
            },
            //引数最小値全指定、戻り値最小値（１つ）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""ranking_nickname"": """",
                        ""icon_path"": """"
                    }
                }",
                //引数
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""ranking_nickname"": ""○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○"",
                        ""icon_path"": ""https://localhost/xxxxxxxxxx/xxxxxxxxxx/xxxxxxxxxx/xxxxxxxxxx/xxxxxxxxxx/xxxxxxxxxx/xxxxxxxxxx/xxxxxxxxxx/xxxxxxxxxx.png""
                    }
                }",
                //引数
            },
        };

        public static object[][] GetIdInfoAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン（にしたいけど記載がない）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""id"": ""xxx"",
                        ""email"": ""xxx@example.com"",
                        ""name"": ""なまえ"",
                        ""kana"": ""カナ"",
                        ""certified"": true,
                    }
                }",
                //引数
            },
            //引数最小値全指定、戻り値最小値（１つ）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""id"": """",
                        ""email"": """",
                        ""name"": """",
                        ""kana"": """",
                        ""certified"": false,
                    }
                }",
                //引数
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""id"": ""xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"",
                        ""email"": ""xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx@xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx.com"",
                        ""name"": ""なまえ０１２３４５６７８９００１２３４５６７８９００１２３４５６７８９００１２３４５６７８９００１２３４５６７"",
                        ""kana"": ""カナ０１２３４５６７８９００１２３４５６７８９００１２３４５６７８９００１２３４５６７８９００１２３４５６７８"",
                        ""certified"": true,
                    }
                }",
                //引数
            },
        };

        public static object[][] TradeHistoryAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン（にしたいけど記載がない）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""182"": {
                            ""currency_pair"": ""btc_jpy"",
                            ""action"": ""bid"",
                            ""amount"": 0.03,
                            ""price"": 56000,
                            ""fee"": 0,
                            ""your_action"": ""ask"",
                            ""bonus"": 1.6,
                            ""timestamp"": 1402018713,
                            ""comment"" : ""demo""
                        }
                    }
                }",
                //引数
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
                    ""success"": 1,
                    ""return"": {
                        ""0"": {
                            ""currency_pair"": """",
                            ""action"": """",
                            ""amount"": 0,
                            ""price"": 0,
                            ""fee"": 0,
                            ""your_action"": """",
                            ""bonus"": null,
                            ""timestamp"": 0,
                            ""comment"" : """"
                        }
                    }
                }",
                //引数
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                false,
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""2147483647"": {
                            ""currency_pair"": ""XXXXXXXXXXX"",
                            ""action"": ""bid"",
                            ""amount"": 9999999999.99999999,
                            ""price"": 9999999999.99999999,
                            ""fee"": 9999999999.99999999,
                            ""your_action"": ""ask"",
                            ""bonus"": 9999999999.99999999,
                            ""timestamp"": 9223372036854775807,
                            ""comment"" : ""XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX""
                        },
                        ""0"": {
                            ""currency_pair"": ""XXXXXXXXXXX"",
                            ""action"": ""ask"",
                            ""amount"": 9999999999.99999999,
                            ""price"": 9999999999.99999999,
                            ""fee"": 9999999999.99999999,
                            ""your_action"": ""bid"",
                            ""bonus"": 9999999999.99999999,
                            ""timestamp"": 9223372036854775807,
                            ""comment"" : ""XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX""
                        },
                    }
                }",
                //引数
                int.MaxValue,
                int.MaxValue,
                int.MaxValue,
                int.MaxValue,
                "ASC",
                long.MaxValue,
                long.MaxValue,
                "_",
                true,
            },
        };

        public static object[][] ActiveOrdersAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""184"": {
                            ""currency_pair"": ""btc_jpy"",
                            ""action"": ""ask"",
                            ""amount"": 0.03,
                            ""price"": 56000,
                            ""timestamp"": 1402021125,
                            ""comment"" : ""demo""
                        }
                    }
                }",
                //引数
                "_",
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
                            ""currency_pair"": """",
                            ""action"": """",
                            ""amount"": 0,
                            ""price"": 0,
                            ""timestamp"": 0,
                            ""comment"" : """"
                        }
                    }
                }",
                //引数
                "_",
                false,
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""2147483647"": {
                            ""currency_pair"": ""XXXXXXXXXXX"",
                            ""action"": ""bid"",
                            ""amount"": 9999999999.99999999,
                            ""price"": 9999999999.99999999,
                            ""timestamp"": 9223372036854775807,
                            ""comment"" : ""XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX""
                        },
                        ""0"": {
                            ""currency_pair"": ""XXXXXXXXXXX"",
                            ""action"": ""ask"",
                            ""amount"": 9999999999.99999999,
                            ""price"": 9999999999.99999999,
                            ""timestamp"": 9223372036854775807,
                            ""comment"" : ""XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX""
                        },
                    }
                }",
                //引数
                "_",
                true,
            },
        };

        public static object[][] TradeAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""received"": 0.1,
                        ""remains"": 0,
                        ""order_id"": 0,
                        ""funds"": {
                            ""jpy"": 325,
                            ""btc"": 1.392,
                            ""mona"": 2600
                        }
                    }
                }",
                //引数
                "_",
                "ask",
                0,
                0,
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
                        ""received"": 0,
                        ""remains"": 0,
                        ""order_id"": 0,
                        ""funds"": {
                        }
                    }
                }",
                //引数
                "_",
                "bid",
                0,
                0,
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
                        ""received"": 9999999999.99999999,
                        ""remains"": 9999999999.99999999,
                        ""order_id"": 2147483647,
                        ""funds"": {
                            ""jpy"": 9999999999.99999999,
                            ""btc"": 9999999999.99999999,
                            ""mona"": 9999999999.99999999
                        }
                    }
                }",
                //引数
                "_",
                "ask",
                decimal.MaxValue,
                decimal.MaxValue,
                decimal.MaxValue,
                "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
            },
        };

        public static object[][] CancelOrderAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""order_id"": 184,
                        ""funds"": {
                            ""jpy"": 15320,
                            ""btc"": 1.392,
                            ""mona"": 2600,
                            ""kaori"": 0.1
                        }
                    }
                }",
                //引数
                0,
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
                        ""order_id"": 0,
                        ""funds"": {
                        }
                    }
                }",
                //引数
                0,
                null,
                false,
            },
            //引数なるべく最大値で全指定、戻り値なるべく最大値で全指定（複数）
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""order_id"": 2147483647,
                        ""funds"": {
                            ""jpy"": 9999999999.99999999,
                            ""btc"": 9999999999.99999999,
                            ""mona"": 9999999999.99999999,
                            ""kaori"": 9999999999.99999999
                        }
                    }
                }",
                //引数
                int.MaxValue,
                "_",
                true,
            },
        };

        public static object[][] WithdrawAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン
            new object[]
            {
                //戻り値
                @"{
                    ""success"": 1,
                    ""return"": {
                        ""id"": 23634,
                        ""fee"": 0.001,
                        ""txid"":,
                        ""funds"": {
                            ""jpy"": 15320,
                            ""btc"": 1.392,
                            ""xem"": 100.2,
                            ""mona"": 2600
                        }
                    }
                }",
                //引数
                "_",
                "_",
                0,
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
                        ""id"": 0,
                        ""fee"": 0,
                        ""txid"":,
                        ""funds"": {
                        }
                    }
                }",
                //引数
                "_",
                "_",
                0,
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
                         ""id"": 2147483647,
                         ""fee"": 9999999999.99999999,
                         ""txid"": ""ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff"",
                         ""funds"": {
                             ""jpy"": 9999999999.99999999,
                             ""btc"": 9999999999.99999999,
                             ""xem"": 9999999999.99999999,
                             ""mona"": 9999999999.99999999
                         }
                    }
                }",
                //引数
                "mona",
                "00000000000000000000000000000000000000000000000000000000000000000",
                decimal.MaxValue,
                "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                decimal.MaxValue,
            },
        };

        public static object[][] DepositHistoryAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン
            new object[]
            {
                //戻り値
                @"{
                    ""success"":1,
                    ""return"":{
                        ""3816"":{
                            ""timestamp"":1435745065,
                            ""address"":""12qwQ3sPJJAosodSUhSpMds4WfUPBeFEM2"",
                            ""amount"":0.001,
                            ""txid"":""64dcf59523379ba282ae8cd61d2e9382c7849afe3a3802c0abb08a60067a159f"",
                        },
                        ""3814"":{
                            ""timestamp"":1435548083,
                            ""address"":""12qwQ3sPJJAosodSUhSpMds4WfUPBeFEM2"",
                            ""amount"":0.001,
                            ""txid"":""7d012cfff6e67a8938f93215367eef4177604459631ea62c85550980dca71819""
                        },
                    }
                }",
                //引数
                "_",
                null,
                null,
                null,
                null,
                null,
                null,
                null,
            },
            //引数最小値全指定、戻り値最小値（ゼロ）
            new object[]
            {
                //戻り値
                @"{
                    ""success"":1,
                    ""return"":{
                        ""0"":{
                            ""timestamp"":0,
                            ""address"":"""",
                            ""amount"":0,
                            ""txid"":"""",
                        },
                    }
                }",
                //引数
                "_",
                null,
                null,
                null,
                null,
                null,
                null,
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
                            ""timestamp"":9223372036854775807,
                            ""address"":""ffffffffffffffffffffffffffffffffff"",
                            ""amount"":9999999999.99999999,
                            ""txid"":""ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff"",
                        },
                        ""0"":{
                            ""timestamp"":9223372036854775807,
                            ""address"":""0000000000000000000000000000000000"",
                            ""amount"":9999999999.99999999,
                            ""txid"":""0000000000000000000000000000000000000000000000000000000000000000""
                        },
                    }
                }",
                //引数
                "_",
                int.MaxValue,
                int.MaxValue,
                int.MaxValue,
                int.MaxValue,
                "ASC",
                long.MaxValue,
                long.MaxValue,
            },
        };

        public static object[][] WithdrawHistoryAsyncSuccessData = new object[][]
        {
            //APIドキュメントに記載されているデータを返すパターン
            new object[]
            {
                //戻り値
                @"{
                    ""success"":1,
                    ""return"":{
                        ""3816"":{
                            ""timestamp"":1435745065,
                            ""address"":""12qwQ3sPJJAosodSUhSpMds4WfUPBeFEM2"",
                            ""amount"":0.001,
                            ""txid"":""64dcf59523379ba282ae8cd61d2e9382c7849afe3a3802c0abb08a60067a159f"",
                        },
                        ""3814"":{
                            ""timestamp"":1435548083,
                            ""address"":""12qwQ3sPJJAosodSUhSpMds4WfUPBeFEM2"",
                            ""amount"":0.001,
                            ""txid"":""7d012cfff6e67a8938f93215367eef4177604459631ea62c85550980dca71819""
                        },
                    }
                }",
                //引数
                "_",
                null,
                null,
                null,
                null,
                null,
                null,
                null,
            },
            //引数最小値全指定、戻り値最小値（ゼロ）
            new object[]
            {
                //戻り値
                @"{
                    ""success"":1,
                    ""return"":{
                        ""0"":{
                            ""timestamp"":0,
                            ""address"":"""",
                            ""amount"":0,
                            ""txid"":"""",
                        },
                    }
                }",
                //引数
                "_",
                null,
                null,
                null,
                null,
                null,
                null,
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
                            ""timestamp"":9223372036854775807,
                            ""address"":""ffffffffffffffffffffffffffffffffff"",
                            ""amount"":9999999999.99999999,
                            ""txid"":""ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff"",
                        },
                        ""0"":{
                            ""timestamp"":9223372036854775807,
                            ""address"":""0000000000000000000000000000000000"",
                            ""amount"":9999999999.99999999,
                            ""txid"":""0000000000000000000000000000000000000000000000000000000000000000""
                        },
                    }
                }",
                //引数
                "_",
                int.MaxValue,
                int.MaxValue,
                int.MaxValue,
                int.MaxValue,
                "ASC",
                long.MaxValue,
                long.MaxValue,
            },
        };

        #endregion

        [Theory]
        [InlineData(TestHelper.VALID_CREDENTIAL, TestHelper.VALID_CREDENTIAL)]
        public void Construtcor_should_return_instance_1(string apiKey, string apiSecret)
        {
            //arrange

            //act
            var obj = new TradeApi(apiKey, apiSecret);

            //assert
            Assert.NotNull(obj);
            Assert.IsType<TradeApi>(obj);
        }

        [Theory]
        [MemberData(nameof(ApiClientOptionData))]
        public void Construtcor_should_return_instance_2(ApiClientOption option)
        {
            //arrange

            //act
            var obj = new TradeApi(option);

            //assert
            Assert.NotNull(obj);
            Assert.IsType<TradeApi>(obj);
        }

        [Theory]
        [MemberData(nameof(GetInfoAsyncSuccessData))]
        public async void GetInfoAsync_should_success(string jsonString)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new TradeApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.GetInfoAsync();

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<GetInfoResponse> (actual);
        }

        [Theory]
        [MemberData(nameof(GetInfo2AsyncSuccessData))]
        public async void GetInfo2Async_should_success(string jsonString)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new TradeApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.GetInfo2Async();

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<GetInfo2Response>(actual);
        }

        [Theory]
        [MemberData(nameof(GetPersonalInfoAsyncSuccessData))]
        public async void GetPersonalInfoAsync_should_success(string jsonString)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new TradeApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.GetPersonalInfoAsync();

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<GetPersonalInfoResponse>(actual);
        }

        [Theory]
        [MemberData(nameof(GetIdInfoAsyncSuccessData))]
        public async void GetIdInfoAsync_should_success(string jsonString)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new TradeApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.GetIdInfoAsync();

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<GetIdInfoResponse>(actual);
        }

        [Theory]
        [MemberData(nameof(TradeHistoryAsyncSuccessData))]
        public async void TradeHistoryAsync_should_success(string jsonString, int? from, int? count, 
            int? fromId, int? endId, string order, long? since, long? end, string currencyPair, bool? isToken)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new TradeApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.TradeHistoryAsync(from, count, fromId, endId, order, since, end, currencyPair, isToken);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<IDictionary<int, TradeHistoryResponse>>(actual);
        }

        [Theory]
        [MemberData(nameof(ActiveOrdersAsyncSuccessData))]
        public async void ActiveOrdersAsync_should_success(string jsonString, string currencyPair, bool? isToken)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new TradeApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.ActiveOrdersAsync(currencyPair, isToken);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<IDictionary<int, ActiveOrdersResponse>>(actual);
        }

        [Theory]
        [MemberData(nameof(TradeAsyncSuccessData))]
        public async void TradeAsync_should_success(string jsonString, string currencyPair, string action,
            decimal price, decimal amount, decimal? limit, string comment)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new TradeApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.TradeAsync(currencyPair, action, price, amount, limit, comment);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<TradeResponse>(actual);
        }

        [Theory]
        [MemberData(nameof(CancelOrderAsyncSuccessData))]
        public async void CancelOrderAsync_should_success(string jsonString, int orderId, string currencyPair, bool? isToken)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new TradeApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.CancelOrderAsync(orderId, currencyPair, isToken);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<CancelOrderResponse>(actual);
        }

        [Theory]
        [MemberData(nameof(WithdrawAsyncSuccessData))]
        public async void WithdrawAsync_should_success(string jsonString, string currency, string address, 
            decimal amount, string message, decimal? optFee)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new TradeApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.WithdrawAsync(currency, address, amount, message, optFee);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<WithdrawResponse>(actual);
        }

        [Theory]
        [MemberData(nameof(DepositHistoryAsyncSuccessData))]
        public async void DepositHistoryAsync_should_success(string jsonString, string currency, 
            int? from, int? count, int? fromId, int? endId, string order, long? since, long? end)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new TradeApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.DepositHistoryAsync(currency, from, count, fromId, endId, order, since, end);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<IDictionary<int, DepositHistoryResponse>>(actual);
        }

        [Theory]
        [MemberData(nameof(WithdrawHistoryAsyncSuccessData))]
        public async void WithdrawHistoryAsync_should_success(string jsonString, string currency,
            int? from, int? count, int? fromId, int? endId, string order, long? since, long? end)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = new TradeApi(TestHelper.CreateApiClientWithMockHttpAccessor(response));

            //act
            var actual = await obj.WithdrawHistoryAsync(currency, from, count, fromId, endId, order, since, end);

            //assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<IDictionary<int, WithdrawHistoryResponse>>(actual);
        }

        [Fact]
        public void ActiveOrdersAsync_should_throw_NotSupportedException_if_parameters_include_is_token_both()
        {
            //arrange
            var obj = new TradeApi(TestHelper.CreateApiClientWithMockHttpAccessor());
            var parameters = new Dictionary<string, string>
            {
                { "is_token_both", "true" }
            };

            //act
            var actual = Record.ExceptionAsync(async () => await obj.ActiveOrdersAsync(parameters));

            //assert
            Assert.IsType<NotSupportedException>(actual.Result);
        }

        [Fact]
        public void WithdrawAsync_should_throw_ArgumentException_if_parameters_include_opt_fee_when_currency_is_not_btc_or_mona_1()
        {
            //arrange
            var obj = new TradeApi(TestHelper.CreateApiClientWithMockHttpAccessor());

            //act
            var actual = Record.ExceptionAsync(async () => await obj.WithdrawAsync("xem", "test", 0, null, optFee: 1));

            //assert
            Assert.IsType<ArgumentException>(actual.Result);
        }

        [Fact]
        public void WithdrawAsync_should_throw_ArgumentException_if_parameters_include_opt_fee_when_currency_is_not_btc_or_mona_2()
        {
            //arrange
            var obj = new TradeApi(TestHelper.CreateApiClientWithMockHttpAccessor());
            var parameters = new Dictionary<string, string>
            {
                { "currency", "xem" },
                { "address", "test" },
                { "amount", "0" },
                { "opt_fee", "1" }
            };

            //act
            var actual = Record.ExceptionAsync(async () => await obj.WithdrawAsync(parameters));

            //assert
            Assert.IsType<ArgumentException>(actual.Result);
        }
    }
}
