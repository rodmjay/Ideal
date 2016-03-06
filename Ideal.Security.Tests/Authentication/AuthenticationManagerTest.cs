using System.Security.Claims;
using Ideal.Security.Authentication;
using NUnit.Framework;

namespace Ideal.Security.Tests.Authentication
{
    [TestFixture]
    public class AuthenticationManagerTest
    {
        [TestFixture]
        public class TheAuthenticateMethod
        {
            [Test]
            public void ShouldReturnIncomingPrincipal()
            {
                ClaimsPrincipal principal1 = new ClaimsPrincipal();
                AuthenticationManager manager = new AuthenticationManager();
                ClaimsPrincipal principal2 = manager.Authenticate("resource1", principal1);

                Assert.AreEqual(principal1,principal2);
            }
        }
    }
}
