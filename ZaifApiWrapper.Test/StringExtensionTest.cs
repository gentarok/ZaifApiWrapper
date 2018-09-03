using System;
using Xunit;

namespace ZaifApiWrapper.Test
{
    public class StringExtensionTest
    {
        private const string ZENKAKE_SPACE = "　";

        public static object[][] ThrowIfIsNullOrWhiteSpaceData = new object[][]
        {
            new object[] { null, null, null },
            new object[] { null, "", null },
            new object[] { null, "", "" },
            new object[] { null, "test", null },
            new object[] { null, "test", "test" },
            new object[] { "", null, null },
            new object[] { "", "", null },
            new object[] { "", "", "" },
            new object[] { "", "test", null },
            new object[] { "", "test", "test" },
            new object[] { " ", null, null },
            new object[] { " ", "", null },
            new object[] { " ", "", "" },
            new object[] { " ", "test", null },
            new object[] { " ", "test", "test" },
            new object[] { "  ", null, null },
            new object[] { "  ", "", null },
            new object[] { "  ", "", "" },
            new object[] { "  ", "test", null },
            new object[] { "  ", "test", "test" },
            new object[] { ZENKAKE_SPACE, null, null },
            new object[] { ZENKAKE_SPACE, "", null },
            new object[] { ZENKAKE_SPACE, "", "" },
            new object[] { ZENKAKE_SPACE, "test", null },
            new object[] { ZENKAKE_SPACE, "test", "test" },
        };

        public static object[][] ThrowIfValueInvalidData = new object[][]
        {
            new object[] { null, new string[] { }, null, null },
            new object[] { null, new string[] { "" }, null, null },
            new object[] { null, new string[] { "" }, "test", null },
            new object[] { null, new string[] { "" }, "test", "test" },
            new object[] { null, new string[] { "", "" }, null, null },
            new object[] { null, new string[] { "test1", "test2" }, null, null },
            new object[] { null, new string[] { "test1", "test2", "test3"}, null, null },
            new object[] { "test", new string[] { "test1", "test2" }, null, null },
        };

        [Theory]
        [MemberData(nameof(ThrowIfIsNullOrWhiteSpaceData))]
        public void ThrowIfIsNullOrWhiteSpace_throw_ArgumentException_if_value_is_null_or_mhite_space(
            string s, string paramName, string messageParamName)
        {
            //arrange
            //act
            var actual = Record.Exception(() => s.ThrowIfIsNullOrWhiteSpace(paramName, messageParamName));

            //assert
            Assert.IsType<ArgumentException>(actual);
        }

        [Theory]
        [MemberData(nameof(ThrowIfValueInvalidData))]
        public void ThrowIfValueInvalid_throw_ArgumentException_if_value_is_null_or_mhite_space(
            string s, string[] validValues, string paramName, string messageParamName)
        {
            //arrange
            //act
            var actual = Record.Exception(() => s.ThrowIfValueInvalid(validValues, paramName, messageParamName));

            //assert
            Assert.IsType<ArgumentException>(actual);
        }
    }
}
