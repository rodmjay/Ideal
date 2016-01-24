using Ideal.Security.Encryption;
using NUnit.Framework;

namespace Ideal.Security.Tests.Encryption
{
    [TestFixture]
    public class MachineKeyCookieTransformTest
    {
        [TestFixture]
        public class TheDecodeMethod
        {
            [TestCase("a")]
            [TestCase("aa")]
            [TestCase("aaa")]
            public void ShouldDecode(string input)
            {
                byte[] unencryptedBytes = input.GetBytes();
                byte[] encryptedBytes = new MachineKeyCookieTransform().Encode(unencryptedBytes);

                Assert.AreNotEqual(unencryptedBytes,encryptedBytes);

                byte[] decodedBytes = new MachineKeyCookieTransform().Decode(encryptedBytes);
                Assert.AreEqual(unencryptedBytes,decodedBytes);
            }
        }

        [TestFixture]
        public class TheEncodeMethod
        {
            [TestCase("a")]
            [TestCase("aa")]
            [TestCase("aaa")]
            public void ShouldEncode(string input)
            {
                byte[] unencryptedBytes = input.GetBytes();
                byte[] encryptedBytes = new MachineKeyCookieTransform().Encode(unencryptedBytes);

                Assert.AreNotEqual(unencryptedBytes,encryptedBytes);
            }
        }
    }
}
