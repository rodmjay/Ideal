using System.Security.Claims;
using Ideal.Security.Authorization;
using NUnit.Framework;
using AuthorizationContext = System.Security.Claims.AuthorizationContext;

namespace Ideal.Security.Tests.Authorization
{
    [TestFixture]
    public class AuthorizationManagerTest
    {
        [TestFixture]
        public class TheCheckAccessMethod
        {
            [Test]
            public void ShouldAlwaysReturnTrue()
            {
                var context = new AuthorizationContext(new ClaimsPrincipal(), "asdf", "asdf");

                AuthorizationManagerX manager=  new AuthorizationManagerX();
                var actual = manager.CheckAccess(context);

                Assert.IsTrue(actual);
            }
        }
    }
}
