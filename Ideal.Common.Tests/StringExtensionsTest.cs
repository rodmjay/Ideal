using NUnit.Framework;

namespace Ideal.Common.Tests
{
    [TestFixture]
    public class StringExtensionsTest
    {
        [TestFixture]
        public class TheGetBytesMethod
        {
            [TestCase("a", ExpectedResult = new byte[] {97,0})]
            [TestCase("aa", ExpectedResult = new byte[] {97,0,97,0})]
            public byte[] ReturnsByteRepresentationOfString(string input)
            {
                return input.GetBytes();
             }
        }

        [TestFixture]
        public class TheGetStringMethod
        {
            [TestCase(new byte[] { 97, 0 },ExpectedResult = "a")]
            [TestCase(new byte[] { 97, 0, 97, 0 },ExpectedResult = "aa")]
            public string ReturnsStringRepresentationOfBytes(byte[] input)
            {
                return input.GetString();
            }
        }
    }
}
