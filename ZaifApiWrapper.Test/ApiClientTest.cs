using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using Xunit;

namespace ZaifApiWrapper.Test
{
    public class ApiClientTest
    {
        #region Test data

        public static object[][] GetAsyncSuccessDana = new object[][]
        {
            new object[] { @"{ ""key"": ""value"" }", },
            new object[] { @"[ { ""key"": 1 }, { ""key"": 2 }, ]", },
        };

        // GetAsync, PostAsyncがリトライになるケースのデータ
        public static object[][] RetryData = new object[][]
        {
            new object[]
            {
                HttpStatusCode.BadGateway, string.Empty,
            },
            new object[]
            {
                HttpStatusCode.ServiceUnavailable, string.Empty,
            },
            new object[]
            {
                HttpStatusCode.GatewayTimeout, string.Empty,
            },
            new object[]
            {
                HttpStatusCode.OK, @"{ ""success"": 0, ""error"": ""time wait restriction, please try later."" }",
            },
            new object[]
            {
                HttpStatusCode.OK, @"{ ""success"": 0, ""error"": ""trade temporarily unavailable."" }",
            },
        };

        // GetAsync, PostAsyncがリトライになるケースのデータ
        public static object[][] InvalidCredentialData = new object[][]
        {
           new object[]
           {
               "00000000-0000-0000-0000-0000000000000", //桁が多い
               TestHelper.VALID_CREDENTIAL,
           },
           new object[]
           {
               TestHelper.VALID_CREDENTIAL,
               "AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA", //大文字
           },
           new object[]
           {
               "00000000-0000-0000-0000-00000000000g", //範囲外
               TestHelper.VALID_CREDENTIAL,
           },
           new object[]
           {
               TestHelper.VALID_CREDENTIAL,
               "000000000-000-0000-0000-000000000000", //形式異常
           },
        };

        #endregion

        [Fact]
        public void Costructor_should_return_instance()
        {
            //arrange

            //act
            var obj = TestHelper.CreateApiClientWithMockHttpAccessor();

            //assert
            Assert.NotNull(obj);
            Assert.IsType<ApiClient>(obj);
        }

        [Theory]
        [MemberData(nameof(GetAsyncSuccessDana))]
        public async void GetAsync_should_success(string jsonString)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = TestHelper.CreateApiClientWithMockHttpAccessor(response);

            //act
            var actual = await obj.GetAsync<object>("_", new[] { "_" }, CancellationToken.None, null);

            //assert
            Assert.NotNull(actual);
        }

        [Theory]
        [MemberData(nameof(RetryData))]
        public void GetAsync_should_throw_RetryCountOverException(HttpStatusCode statusCode, string jsonString)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(statusCode, jsonString);
            var accessor = TestHelper.CreateHttpClientAccessor(response);
            var option = TestHelper.CreateApiClientOption(accessor);
            option.MaxRetry = 5;
            //インターバルを入れてないとprogress?.Report()の終了前にテストが完了してしまうため
            option.HttpErrorRetryInterval = 10;
            option.ApiErrorRetryInterval = 10;
            var obj = TestHelper.CreateApiClientWithMockHttpAccessor(option);

            var counts = new List<int>();
            var types = new List<ErrorType>();
            var retryReport = new Progress<RetryReport>(
                x =>
                {
                    counts.Add(x.RetryCount);
                    types.Add(x.ErrorType);
                });

            //act
            var actual = Record.ExceptionAsync(async () => await obj.GetAsync<object>("_", new[] { "_" }, CancellationToken.None, retryReport));

            //assert
            Assert.IsType<RetryCountOverException>(actual.Result);
            Assert.Equal(5, counts.Count);
            Assert.True(types.All(
                x =>
                {
                    return
                        (x == ErrorType.HttpError && statusCode != HttpStatusCode.OK) ||
                        (x == ErrorType.ApiError && statusCode == HttpStatusCode.OK);
                }));
        }

        [Fact]
        public void GetAsync_should_throw_ZaifApiException()
        {
            //arrange
            var jsonString = @"{ ""error"": ""api errer raised."" }";

            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = TestHelper.CreateApiClientWithMockHttpAccessor(response);

            //act
            var actual = Record.ExceptionAsync(async () => await obj.GetAsync<object>("_", new[] { "_" }, CancellationToken.None, null));

            //assert
            Assert.IsType<ZaifApiException>(actual.Result);
        }

        [Fact]
        public async void PostAsync_should_success()
        {
            //arrange
            var jsonString = @"{ ""success"": 1, ""return"": { ""key"": ""value"" } }";

            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = TestHelper.CreateApiClientWithMockHttpAccessor(response);

            //act
            var actual = await obj.PostAsync<object>("_", null, CancellationToken.None, null);

            //assert
            Assert.NotNull(actual);
        }

        [Theory]
        [MemberData(nameof(InvalidCredentialData))]
        public void PostAsync_should_throw_CredentialFormatException(string apiKey, string apiSecret)
        {
            // arrange
            var response = TestHelper.CreateJsonResponse(HttpStatusCode.OK, string.Empty);
            var accessor = TestHelper.CreateHttpClientAccessor(response);
            var option = TestHelper.CreateApiClientOption(apiKey, apiSecret, accessor);
            var obj = TestHelper.CreateApiClientWithMockHttpAccessor(option);

            //act
            var actual = Record.ExceptionAsync(async () => await obj.PostAsync<object>("_", null, CancellationToken.None, null));

            //assert
            Assert.IsType<CredentialFormatException>(actual.Result);
        }

        [Theory]
        [MemberData(nameof(RetryData))]
        public void PostAsync_should_throw_RetryCountOverException(HttpStatusCode statusCode, string jsonString)
        {
            //arrange
            var response = TestHelper.CreateJsonResponse(statusCode, jsonString);
            var accessor = TestHelper.CreateHttpClientAccessor(response);
            var option = TestHelper.CreateApiClientOption(accessor);
            //インターバルを入れてないとprogress?.Report()の終了前にテストが完了してしまうため
            option.HttpErrorRetryInterval = 10;
            option.ApiErrorRetryInterval = 10;
            var obj = TestHelper.CreateApiClientWithMockHttpAccessor(option);

            var counts = new List<int>();
            var types = new List<ErrorType>();
            var retryReport = new Progress<RetryReport>(
                x =>
                {
                    counts.Add(x.RetryCount);
                    types.Add(x.ErrorType);
                });

            //act
            var actual = Record.ExceptionAsync(async () => await obj.PostAsync<object>("_", null, CancellationToken.None, retryReport));

            //assert
            Assert.IsType<RetryCountOverException>(actual.Result);
            Assert.Equal(10, counts.Count);
            Assert.True(types.All(
                x =>
                {
                    return
                        (x == ErrorType.HttpError && statusCode != HttpStatusCode.OK) ||
                        (x == ErrorType.ApiError && statusCode == HttpStatusCode.OK);
                }));
        }

        [Fact]
        public void PostAsync_should_throw_ZaifApiException()
        {
            //arrange
            var jsonString = @"{ ""success"": 0 , ""error"": ""api errer raised."" }";

            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = TestHelper.CreateApiClientWithMockHttpAccessor(response);

            //act
            var actual = Record.ExceptionAsync(async () => await obj.PostAsync<object>("_", null, CancellationToken.None, null));

            //assert
            Assert.IsType<ZaifApiException>(actual.Result);
        }

        [Fact]
        public void PostAsync_should_throw_HttpRequestException()
        {
            // arrange
            var response = TestHelper.CreateJsonResponse(HttpStatusCode.InternalServerError, string.Empty);

            var obj = TestHelper.CreateApiClientWithMockHttpAccessor(response);

            //act
            var actual = Record.ExceptionAsync(async () => await obj.PostAsync<object>("_", null, CancellationToken.None, null));

            //assert
            Assert.IsType<HttpRequestException>(actual.Result);
        }

        [Fact]
        public void PostAsync_should_increment_nonce_after_HttpException()
        {
            // arrange
            var response = TestHelper.CreateJsonResponse(HttpStatusCode.InternalServerError, string.Empty);

            var obj = TestHelper.CreateApiClientWithMockHttpAccessor(response);

            var ex = Record.ExceptionAsync(async () => await obj.PostAsync<object>("_", null, CancellationToken.None, null));
            var nonce = (long)typeof(ApiClient).GetField("_nonce", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);

            //act
            Record.ExceptionAsync(async () => await obj.PostAsync<object>("_", null, CancellationToken.None, null));
            var actual = (long)typeof(ApiClient).GetField("_nonce", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);

            //assert
            Assert.IsType<HttpRequestException>(ex.Result);
            Assert.True(nonce < actual);
        }

        [Fact]
        public void PostAsync_should_increment_nonce_after_ZaifApiException()
        {
            //arrange
            var jsonString = @"{ ""success"": 0 , ""error"": ""api errer raised."" }";

            var response = TestHelper.CreateJsonResponse(jsonString);

            var obj = TestHelper.CreateApiClientWithMockHttpAccessor(response);

            var ex = Record.ExceptionAsync(async () => await obj.PostAsync<object>("_", null, CancellationToken.None, null));
            var nonce = (long)typeof(ApiClient).GetField("_nonce", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);

            //act
            Record.ExceptionAsync(async () => await obj.PostAsync<object>("_", null, CancellationToken.None, null));
            var actual = (long)typeof(ApiClient).GetField("_nonce", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);

            //assert
            Assert.IsType<ZaifApiException>(ex.Result);
            Assert.True(nonce < actual);
        }
    }
}
