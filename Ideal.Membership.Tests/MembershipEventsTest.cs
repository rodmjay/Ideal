using System.Web.Management;
using Ideal.Core.Common.Membership;
using Ideal.Core.Model;
using NUnit.Framework;

namespace Ideal.Membership.Tests
{
    [TestFixture]
    public class MembershipEventsTest
    {
        [TestFixture]
        public class TheConstructor
        {
            [TestCase("test")]
            [TestCase("franz")]
            [TestCase("jerry springer")]
            public void ShouldSetUsernameProperty(string username)
            {
                User user = new User {Username = username};
                MembershipEvent actual = new MembershipEvent(MembershipEventCode.UserCreated, user);

                Assert.IsInstanceOf<WebRequestEvent>(actual);
                Assert.AreEqual(actual.Username,user.Username);
            }

            [TestCase("test")]
            [TestCase("franz")]
            [TestCase("jerry springer")]
            public void ShouldSetTenantProperty(string tenant)
            {
                User user = new User { Tenant = tenant };
                MembershipEvent actual = new MembershipEvent(MembershipEventCode.UserCreated, user);
                Assert.AreEqual(actual.Tenant, user.Tenant);
            }

            [TestCase(MembershipEventCode.UserCreated)]
            [TestCase(MembershipEventCode.UserLockedOut)]
            [TestCase(MembershipEventCode.UserLogout)]
            [TestCase(MembershipEventCode.UserLogin)]
            public void ShouldSetEventDetailCodeProperly(MembershipEventCode eventCode)
            {
                MembershipEvent actual = new MembershipEvent(eventCode,new User());

                Assert.AreEqual((int) eventCode, actual.EventDetailCode);
                Assert.AreEqual(eventCode.GetDescription(),actual.Message);
            }
        }
    }
}