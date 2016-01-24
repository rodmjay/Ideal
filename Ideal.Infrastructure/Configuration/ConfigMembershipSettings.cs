using System;
using System.Configuration;
using Ideal.Core.Interfaces.Membership;

namespace Ideal.Infrastructure.Configuration
{
    public class ConfigMembershipSettings : ConfigurationElement, IMembershipSettings
    {
        [ConfigurationProperty("multiTenant")]
        public bool MultiTenant
        {
            get { return (bool)base["multiTenant"]; }
            set { base["multiTenant"] = value; }
        }

        [ConfigurationProperty("defaultTenant")]
        public string DefaultTenant
        {
            get { return (string)base["defaultTenant"]; }
            set { base["defaultTenant"] = value; }
        }

        [ConfigurationProperty("emailIsUsername")]
        public bool EmailIsUsername
        {
            get { return (bool)base["emailIsUsername"]; }
            set { base["emailIsUsername"] = value; }
        }

        [ConfigurationProperty("usernamesUniqueAcrossTenants")]
        public bool UsernamesUniqueAcrossTenants
        {
            get { return (bool)base["usernamesUniqueAcrossTenants"]; }
            set { base["usernamesUniqueAcrossTenants"] = value; }
        }

        [ConfigurationProperty("requireAccountVerification")]
        public bool RequireAccountVerification
        {
            get { return (bool)base["requireAccountVerification"]; }
            set { base["requireAccountVerification"] = value; }
        }

        [ConfigurationProperty("allowLoginAfterAccountCreation")]
        public bool AllowLoginAfterAccountCreation
        {
            get { return (bool)base["allowLoginAfterAccountCreation"]; }
            set { base["allowLoginAfterAccountCreation"] = value; }
        }

        [ConfigurationProperty("accountLockoutFailedLoginAttempts")]
        public int AccountLockoutFailedLoginAttempts
        {
            get { return (int)base["accountLockoutFailedLoginAttempts"]; }
            set { base["accountLockoutFailedLoginAttempts"] = value; }
        }

        [ConfigurationProperty("accountLockoutDuration")]
        public TimeSpan AccountLockoutDuration { get; set; }

        [ConfigurationProperty("accountLockoutMinutes")]
        public int AccountLockoutMinutes
        {
            get { return (int)base["accountLockoutMinutes"]; }
            set { base["accountLockoutMinutes"] = value; }
        }

        [ConfigurationProperty("allowAccountDeletion")]
        public bool AllowAccountDeletion
        {
            get { return (bool)base["allowAccountDeletion"]; }
            set { base["allowAccountDeletion"] = value; }
        }

        [ConfigurationProperty("minimumPasswordLength")]
        public int MinimumPasswordLength
        {
            get { return (int)base["minimumPasswordLength"]; }
            set { base["minimumPasswordLength"] = value; }
        }

        [ConfigurationProperty("passwordResetFrequency")]
        public int PasswordResetFrequency
        {
            get { return (int)base["passwordResetFrequency"]; }
            set { base["passwordResetFrequency"] = value; }
        }

        [ConfigurationProperty("passwordHashingIterationCount")]
        public int PasswordHashingIterationCount
        {
            get { return (int)base["passwordHashingIterationCount"]; }
            set { base["passwordHashingIterationCount"] = value; }
        }

        [ConfigurationProperty("allowEmailchangeWhenEmailIsUsername")]
        public bool AllowEmailChangeWhenEmailIsUsername
        {
            get { return (bool)base["allowEmailchangeWhenEmailIsUsername"]; }
            set { base["allowEmailchangeWhenEmailIsUsername"] = value; }
        }
    }
}
