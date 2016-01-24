using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Ideal.Security.Authorization;
using NUnit.Framework;

namespace Ideal.Security.Tests.Authorization
{
    [TestFixture]
    public class ClaimsAuthorizationTest
    {
        [TestFixture]
        public class TheCheckAccessMethod
        {
            [TestCase("read","resource1",ExpectedResult = true)]
            [TestCase("delete","resource1",ExpectedResult = false)]
            public bool ReturnsTrueIfUserHasAccessToResource(string action, string resource)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim("resource1", "read")
                };

                ClaimsIdentity identy = new ClaimsIdentity(claims);

                ClaimsPrincipal principal = new ClaimsPrincipal(identy);
               
                AuthorizationContext context = ClaimsAuthorization.CreateAuthorizationContext(principal, action, new[] {resource});

                return ClaimsAuthorization.CheckAccess(context);
            }
        }

        [TestFixture]
        public class TheCreateAuthorizationContextMethod
        {
            [TestCase("read",new[] { "resource1"})]
            public void ShouldCreateAuthorizationContextCorrectly(string action, string[] resources)
            {
                ClaimsIdentity identy = new ClaimsIdentity();
                identy.AddClaim(new Claim("resource1","read"));
                identy.AddClaim(new Claim("resource1","delete"));

                ClaimsPrincipal principal = new ClaimsPrincipal(identy);
                var actual = ClaimsAuthorization.CreateAuthorizationContext(principal, action, resources);

                Assert.IsTrue(actual.Principal.Identities.Count()==1);
                Assert.IsTrue(actual.Principal.Claims.Count()==2);
            }
        }
    }
}
