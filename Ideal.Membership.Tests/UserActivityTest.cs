using Ideal.Core.Model.Membership;
using Ideal.Core.Model.Membership.Events;
using NUnit.Framework;

namespace Ideal.Membership.Tests
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