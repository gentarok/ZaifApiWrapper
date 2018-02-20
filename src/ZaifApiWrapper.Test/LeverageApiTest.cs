using System;
using System.Linq;
using Moq;
using Xunit;

namespace ZaifApiWrapper.Test
{
    public class LeverageApiTest
    {
        [Fact]
        public void GetPositionsAsync_1_should_success()
        {
            //arrange
            var apiClient = new Mock<IApiClient>();
            var api = new LeverageApi(apiClient.Object);

            //act
            var ex = Record.ExceptionAsync(async () => await api.GetPositionsAsync("margin"));

            //assert
            Assert.Null(ex.Result);
        }

        [Fact]
        public void PositionHistoryAsync_1_should_success()
        {
            //arrange
            var apiClient = new Mock<IApiClient>();
            var api = new LeverageApi(apiClient.Object);

            //act
            var ex = Record.ExceptionAsync(async () => await api.PositionHistoryAsync("margin", 0));

            //assert
            Assert.Null(ex.Result);
        }

        [Fact]
        public void CreatePositionAsync_1_should_success()
        {
            //arrange
            var apiClient = new Mock<IApiClient>();
            var api = new LeverageApi(apiClient.Object);

            //act
            var ex = Record.ExceptionAsync(async() => await api.CreatePositionAsync("futures", "btc_jpy", "bid", 0.000_000_01m, 1_000_000, 25, 1));

            //assert
            Assert.Null(ex.Result);
        }

        [Fact]
        public void ActivePositionsAsync_1_should_success()
        {
            //arrange
            var apiClient = new Mock<IApiClient>();
            var api = new LeverageApi(apiClient.Object);

            //act
            var ex = Record.ExceptionAsync(async () => await api.ActivePositionsAsync("futures", 1));

            //assert
            Assert.Null(ex.Result);
        }

        [Fact]
        public void ChangePositionAsync_1_should_success()
        {
            //arrange
            var apiClient = new Mock<IApiClient>();
            var api = new LeverageApi(apiClient.Object);

            //act
            var ex = Record.ExceptionAsync(async () => await api.ChangePositionAsync("margin", 1, 100_000_000m));

            //assert
            Assert.Null(ex.Result);
        }

        [Fact]
        public void CancelPositionAsync_1_should_success()
        {
            //arrange
            var apiClient = new Mock<IApiClient>();
            var api = new LeverageApi(apiClient.Object);

            //act
            var ex = Record.ExceptionAsync(async () => await api.CancelPositionAsync("margin", 1));

            //assert
            Assert.Null(ex.Result);
        }
    }
}
