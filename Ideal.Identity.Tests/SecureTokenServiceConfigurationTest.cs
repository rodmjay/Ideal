using Ideal.Identity.Configuration;
using NUnit.Framework;

namespace Ideal.Identity.Tests
{
    [TestFixture]
    public class SecureTokenServiceConfigurationTest
    {
        [TestFixture]
        public class TheIssuerUriProperty
        {
            [TestCase("test", ExpectedResult = "test")]
            public string ShouldStoreValue(string input)
            {
                var actual = new SecureTokenServiceConfiguration
                {
                    IssuerUri = input
                };
                return actual.IssuerUri;
            }
        }

        [TestFixture]
        public class TheRootUriProperty
        {
            [TestCase("test", ExpectedResult = "test")]
            public string ShouldStoreValue(string input)
            {
                var actual = new SecureTokenServiceConfiguration
                {
                    RootUri = input
                };
                return actual.RootUri;
            }
        }

        [TestFixture]
        public class TheIdenityUriProperty
        {
            [TestCase("test", ExpectedResult = "test")]
            public string ShouldStoreValue(string input)
            {
                var actual = new SecureTokenServiceConfiguration
                {
                    IdenityUri = input
                };
                return actual.IdenityUri;
            }

            [TestCase("http://test", "/asdf", ExpectedResult = "http://test/asdf")]
            [TestCase("asdf", "/identity", ExpectedResult = "asdf/identity")]
            public string ShouldReturnValueAppendedWithRoot(string rootUri, string identityUri)
            {
                var actual = new SecureTokenServiceConfiguration()
                {
                    RootUri = rootUri,
                    IdenityUri = identityUri
                };
                return actual.IdenityUri;
            }

            [TestCase("http://test", ExpectedResult = "http://test/identity")]
            public string ShouldReturnDefaultValueIfNoValue(string rootUri)
            {
                var actual = new SecureTokenServiceConfiguration()
                {
                    RootUri = rootUri,
                };
                return actual.IdenityUri;
            }
        }

        [TestFixture]
        public class TheTokenUriProperty
        {
            [TestCase("http://test", ExpectedResult = "http://test/connect/token")]
            [TestCase("", ExpectedResult = "/connect/token")]
            public string ShouldReturnDefaultValueIfNoValue(string input)
            {
                var actual = new SecureTokenServiceConfiguration()
                {
                    RootUri = input,
                };
                return actual.TokenUri;
            }
        }

        [TestFixture]
        public class TheAuthUriProperty
        {
            [TestCase("http://test", ExpectedResult = "http://test/connect/authorize")]
            public string ShouldReturnDefaultValueIfNoValue(string rootUri)
            {
                var actual = new SecureTokenServiceConfiguration()
                {
                    RootUri = rootUri,
                };
                return actual.AuthUri;
            }
        }

        [TestFixture]
        public class TheUserInfoUriProperty
        {
            [TestCase("http://test", ExpectedResult = "http://test/connect/userinfo")]
            public string ShouldReturnDefaultValueIfNoValue(string rootUri)
            {
                var actual = new SecureTokenServiceConfiguration()
                {
                    RootUri = rootUri,
                };
                return actual.UserInfoUri;
            }
        }

        [TestFixture]
        public class TheEndSessionUriProperty
        {
            [TestCase("http://test", ExpectedResult = "http://test/connect/endsession")]
            public string ShouldReturnDefaultValueIfNoValue(string rootUri)
            {
                var actual = new SecureTokenServiceConfiguration()
                {
                    RootUri = rootUri,
                };
                return actual.EndSessionUri;
            }
        }

        [TestFixture]
        public class TheRevokeUriProperty
        {
            [TestCase("http://test", ExpectedResult = "http://test/connect/revocation")]
            public string ShouldReturnDefaultValueIfNoValue(string rootUri)
            {
                var actual = new SecureTokenServiceConfiguration()
                {
                    RootUri = rootUri,
                };
                return actual.RevokeUri;
            }
        }
    }
}
