﻿using System;
using System.Configuration;
using Ideal.Infrastructure.Configuration;
using NUnit.Framework;

namespace Ideal.Infrastructure.Tests.Configuration
{
    [TestFixture]
    public class ConfigMembershipSettingsTest
    {
        [Test]
        public void ShouldLoadFromConfig()
        {
            ConfigMembershipSettings actual =
                ((ConfigApplicationSettings) ConfigurationManager.GetSection("Ideal")).Membership;
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
                var target = new ConfigMembershipSettings();
                Assert.AreEqual(target.AccountLockoutDuration, default(TimeSpan));
                target.AccountLockoutDuration = span;
                Assert.AreEqual(span,target.AccountLockoutDuration);
            }
        }
    }
}
