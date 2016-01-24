using NUnit.Framework;

namespace Ideal.Common.Tests
{
    [TestFixture]
    public class CryptoHelperTest
    {
        [TestFixture]
        public class TheHashMethod
        {
            [TestCase("SomeValue", ExpectedResult = "4F7AA54B8D9A8F5E7B06BF38217A84DFD7272BD50F5AEBE97AE321F24ECEB291")]
            public string ShouldReturnKnownHashedValues(string input)
            {
                return CryptoHelper.Hash(input);
            }
        }

        [TestFixture]
        public class TheGenerateSaltMethod
        {
            [Test]
            public void ShouldGenerateUniqueValue()
            {
                string salt1 = CryptoHelper.GenerateSalt();
                string salt2 = CryptoHelper.GenerateSalt();

                Assert.AreNotEqual(salt1,salt2);
            }


            [Test]
            public void ShouldReturnSignificantAmountOfData()
            {
                string salt1 = CryptoHelper.GenerateSalt();
                Assert.IsTrue(salt1.Length > 15, salt1);
            }
        }

        [TestFixture]
        public class TheHashPasswordMethod
        {
            [Test]
            public void ShouldGenerateSameResultForSamePassword()
            {
                string password1 = CryptoHelper.HashPassword("test", 1);
                string password2 = CryptoHelper.HashPassword("test", 1);

                Assert.AreNotEqual(password1,password2);
            }
        }

        [TestFixture]
        public class TheVerifyHashedPasswordMethod
        {
            [TestCase("password")]
            [TestCase("123456")]
            [TestCase("ilovemom")]
            [TestCase("!@#$%#$^$%&%^*")]
            [TestCase("ASDF/n")]
            public void ShouldVerifyCorrectly(string input)
            {   
                string hashedPassword = CryptoHelper.HashPassword(input, 10);
                Assert.AreNotEqual(hashedPassword,input);
                Assert.IsTrue(CryptoHelper.VerifyHashedPassword(hashedPassword, input));
            }
        }
    }
}
