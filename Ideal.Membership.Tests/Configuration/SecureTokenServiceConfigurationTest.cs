using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Ideal.Membership.Configuration;
using NUnit.Framework;

namespace Ideal.Membership.Tests.Configuration
{
    [TestFixture]
    public class SecureTokenServiceConfigurationTest
    {
        [TestFixture]
        public class TheIssuerUriProperty
        {
            [TestCase("test",ExpectedResult = "test")]
            public string ShouldStoreValue(string input)
            {
                var actual = new SecureTokenServiceConfiguration{
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

            [TestCase("/identity",ExpectedResult = "http://test/identity")]
            public string ShouldReturnValueAppendedWithRoot(string input)
            {
                var actual = new SecureTokenServiceConfiguration(){
                    RootUri = "http://test",
                    IdenityUri = input
                };
                return actual.IdenityUri;
            }

            [TestCase("http://test", ExpectedResult = "http://test/identity")]
            public string ShouldReturnDefaultValueIfNoValue(string input)
            {
                var actual = new SecureTokenServiceConfiguration()
                {
                    RootUri = "http://test",
                };
                return actual.IdenityUri;
            }
        }

        [TestFixture]
        public class TheTokenUriProperty
        {
            
        }

        [TestFixture]
        public class TheAuthUriProperty
        {
            
        }

        [TestFixture]
        public class TheUserInfoUriProperty
        {
            
        }

        [TestFixture]
        public class TheEndSessionUriProperty
        {
            
        }

        [TestFixture]
        public class TheRevokeUriProperty
        {
            
        }
    }
}
