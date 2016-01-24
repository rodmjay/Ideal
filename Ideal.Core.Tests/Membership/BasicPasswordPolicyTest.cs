using Ideal.Core.Common.Membership.PasswordPolicies;
using NUnit.Framework;

namespace Ideal.Core.Tests.Membership
{
    [TestFixture]
    public class BasicPasswordPolicyTest
    {
        [TestFixture]
        public class ThePolicyMessageProperty
        {
            
        }

        [TestFixture]
        public class TheValidatePasswordMethod
        {
            [TestCase((uint)6,"1234567",ExpectedResult = true)]
            [TestCase((uint)6,"123456",ExpectedResult = true)]
            [TestCase((uint)6,"12345",ExpectedResult = false)]
            public bool ValidatesMinLength(uint minLength, string password)
            {
                var policy = new BasicPasswordPolicy
                {
                    MinLength = minLength
                };
                return policy.ValidatePassword(password);
            }

            [TestCase((uint)6,"ABCDEFG",ExpectedResult = true)]
            [TestCase((uint)6,"ABCDEF", ExpectedResult = true)]
            [TestCase((uint)6,"ABCDE", ExpectedResult = false)]
            public bool ValidatesUpperAlphas(uint minUpperAlphas, string password)
            {
                var policy = new BasicPasswordPolicy
                {
                    UpperAlphas = minUpperAlphas
                };
                return policy.ValidatePassword(password);
            }

            [TestCase((uint)6, "abcdefg", ExpectedResult = true)]
            [TestCase((uint)6, "abcdef", ExpectedResult = true)]
            [TestCase((uint)6, "abcde", ExpectedResult = false)]
            public bool ValidatesLowerAlphas(uint minLowerAlphas, string password)
            {
                var policy = new BasicPasswordPolicy
                {
                    LowerAlphas = minLowerAlphas
                };
                return policy.ValidatePassword(password);
            }

            [TestCase((uint)6, "1234567", ExpectedResult = true)]
            [TestCase((uint)6, "123456", ExpectedResult = true)]
            [TestCase((uint)6, "12345", ExpectedResult = false)]
            public bool ValidatesNumerics(uint minNumerics, string password)
            {
                var policy = new BasicPasswordPolicy
                {
                    Numerics = minNumerics
                };
                return policy.ValidatePassword(password);
            }

            [TestCase((uint)6, "!@#$%^&", ExpectedResult = true)]
            [TestCase((uint)6, "!@#$%^", ExpectedResult = true)]
            [TestCase((uint)6, "!@#$%", ExpectedResult = false)]
            public bool ValidatesNonAlphaNumerics(uint minNonAlphas, string password)
            {
                var policy = new BasicPasswordPolicy
                {
                    NonAlphaNumerics = minNonAlphas
                };
                return policy.ValidatePassword(password);
            }
        }

    }
}