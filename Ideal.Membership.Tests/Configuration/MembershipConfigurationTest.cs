using System;
using System.Configuration;
using Ideal.Membership.Configuration;
using NUnit.Framework;

namespace Ideal.Membership.Tests.Configuration
{
    [TestFixture]
    public class MembershipConfigurationTest
    {
        [Test]
        public void ShouldLoadFromConfig()
        {
            MembershipConfiguration actual =
                (MembershipConfiguration) ConfigurationManager.GetSection("Ideal/Membership");
            Assert.IsNotNull(actual);
        }

        [TestFixture]
        public class AccountLockoutDuration
        {
            [TestCase(1,1,1,1,ExpectedResult = "1.01:01:01")]
            [TestCase(2,1,1,1,ExpectedResult = "2.01:01:01")]
            public string TimeSpan(int days, int hours, int minutes, int seconds)
            {
                var span = new TimeSpan(days,hours,minutes,seconds);
                return span.ToString();
            }

            [TestCase]
            public void ShouldStoreTimespanValue()
            {
                TimeSpan span = new TimeSpan(2,2,2,2);
                var target = new MembershipConfiguration();
                Assert.AreEqual(target.AccountLockoutDuration, default(TimeSpan));
                target.AccountLockoutDuration = span;
                Assert.AreEqual(span,target.AccountLockoutDuration);
            }
        }
    }
}
