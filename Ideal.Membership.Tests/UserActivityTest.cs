using Ideal.Core.Common.Membership.Events;
using Ideal.Core.Model;
using NUnit.Framework;

namespace Ideal.Core.Tests.Membership
{
    [TestFixture]
    public class UserActivityTest
    {
        [TestFixture]
        public class TheConstructor
        {
            [Test]
            public void ShouldSetUserAndFriendlyName()
            {
                User user = new User();
                UserActivity activity = new UserCreated(user,"~/login");
                Assert.AreEqual(activity.User,user);
                Assert.IsTrue(activity.FriendlyName.Length>0);
            }
        }
    }
}