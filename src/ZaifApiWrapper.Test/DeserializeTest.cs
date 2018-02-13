using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ZaifApiWrapper.LeverageData;
using ZaifApiWrapper.TradeData;

namespace ZaifApiWrapper.Test
{
    public class DeserializeTest
    {
        private JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            FloatParseHandling = FloatParseHandling.Decimal,
        };

        [Fact]
        public void Deserialize_WithdrawHistoryResponse_should_to_be_success()
        {
            // arrange
            var value = @"{
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
        ""0"":{
          ""timestamp"":0,
          ""address"":"""",
          ""amount"":0.00000001,
          ""txid"":""""
        },
        ""999999"":{
          ""timestamp"":9999999999,
          ""address"":""XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"",
          ""amount"":9999999999.99999999,
          ""txid"":""ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff""
        },
    }
}";

            // act
            var jObject = JsonConvert.DeserializeObject<JObject>(value, Settings);
            var obj = jObject["return"].ToObject<Dictionary<string, WithdrawHistoryResponse>>();

            // assert
            Assert.NotNull(obj);
            Assert.IsType<Dictionary<string, WithdrawHistoryResponse>>(obj);
            Assert.Equal(4, obj.Count());

            var data1 = obj["0"];
            Assert.NotNull(data1);
            Assert.Equal(0, data1.Timestamp);
            Assert.Equal("", data1.Address);
            Assert.Equal(0.00000001m, data1.Amount);
            Assert.Equal("", data1.Txid);

            var data2 = obj["999999"];
            Assert.NotNull(data2);
            Assert.Equal(9999999999L, data2.Timestamp);
            Assert.Equal("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", data2.Address);
            Assert.Equal(9999999999.99999999m, data2.Amount);
            Assert.Equal("ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff", data2.Txid);
        }

        [Fact]
        public void Deserialize_DepositHistoryResponse_should_to_be_success()
        {
            // arrange
            var value = @"{
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
}";
            // act
            var jObject = JsonConvert.DeserializeObject<JObject>(value, Settings);
            var obj = jObject["return"].ToObject<Dictionary<string, DepositHistoryResponse>>();

            // assert
            Assert.NotNull(obj);
            Assert.IsType<Dictionary<string, DepositHistoryResponse>>(obj);
            Assert.Equal(2, obj.Count());
        }

        [Fact]
        public void Deserialize_GetPositionsResponse_should_to_be_success()
        {
            // arrange
            var value = @"{
    ""success"": 1,
    ""return"": {
        ""182"": {
            ""group_id"": 1,
            ""currency_pair"": ""btc_jpy"",
            ""action"": ""bid"",
            ""leverage"": 2.5,
            ""price"": 110005,
            ""limit"": 130000,
            ""stop"": 90000,
            ""amount"": 0.03,
            ""fee_spent"": 0,
            ""timestamp"": 1402018713,
            ""term_end"": 1404610713,
            ""timestamp_closed"": 1402019000,
            ""deposit"": 35.76 ,
            ""deposit_jpy"": 35.76,
            ""refunded"": 35.76 ,
            ""refunded_jpy"": 35.76,
            ""swap"": 0,
        }
    }
}";
            // act
            var jObject = JsonConvert.DeserializeObject<JObject>(value, Settings);
            var obj = jObject["return"].ToObject<Dictionary<string, GetPositionsResponse>>();

            // assert
            Assert.NotNull(obj);
            Assert.IsType<Dictionary<string, GetPositionsResponse>>(obj);
            Assert.Single(obj);

            Assert.Equal(35.76m, obj["182"].DepositJpy.Value);
            Assert.Null(obj["182"].DepositBtc);
            Assert.Null(obj["182"].DepositXem);
            Assert.Null(obj["182"].DepositMona);
            Assert.Equal(35.76m, obj["182"].RefundedJpy.Value);
            Assert.Null(obj["182"].RefundedBtc);
            Assert.Null(obj["182"].RefundedXem);
            Assert.Null(obj["182"].RefundedMona);
        }
    }
}
