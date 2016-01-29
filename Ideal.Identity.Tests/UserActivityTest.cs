using Ideal.Identity.Events;
using Ideal.Identity.Model;
using NUnit.Framework;

namespace Ideal.Identity.Tests
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