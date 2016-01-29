#region credits
// ***********************************************************************
// Assembly	: Ideal.Core
// Author	: Rod Johnson
// Created	: 03-20-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Ideal.Core.Interfaces.Membership;

namespace Ideal.Core.Model.Membership
{
    [DisplayColumn("Username")]
    public sealed partial class User : DomainObject
    {
        static User()
        {
            Settings = DependencyResolver.Current.GetService<IMembershipSettings>();
        }

        internal const string ChangeEmailVerificationPrefix = "changeEmail";
        internal const int VerificationKeyStaleDurationDays = 1;

        private static readonly IMembershipSettings Settings;

        public User()
        {
            Claims = new List<UserClaim>();
        }

        internal User(string tenant, string username, string password, string email)
        {
            if (String.IsNullOrWhiteSpace(tenant)) throw new ArgumentException("tenant");
            if (String.IsNullOrWhiteSpace(username)) throw new ArgumentException("username");
            if (String.IsNullOrWhiteSpace(password)) throw new ArgumentException("password");
            if (String.IsNullOrWhiteSpace(email)) throw new ArgumentException("email");

            Tenant = tenant;
            Username = username;
            Email = email;
            Created = UtcNow;
            SetPassword(password);
            IsAccountVerified = !Settings.RequireAccountVerification;
            IsLoginAllowed = Settings.AllowLoginAfterAccountCreation;
            Claims = new List<UserClaim>();

            if (Settings.RequireAccountVerification)
            {
                VerificationKey = StripUglyBase64(GenerateSalt());
                VerificationKeySent = UtcNow;
            }
        }

        [StringLength(50)]
        [Required]
        public string Tenant { get; set; }

        [StringLength(100)]
        [Required]
        public string Username { get; set; }

        [EmailAddress]
        [StringLength(100)]
        [Required]
        public string Email { get; set; }

        public DateTime PasswordChanged { get; set; }

        public bool IsAccountVerified { get; set; }
        public bool IsLoginAllowed { get; set; }
        public bool IsAccountClosed { get; set; }
        public DateTime? AccountClosed { get; set; }

        public DateTime? LastLogin { get; set; }
        public DateTime? LastFailedLogin { get; set; }
        public int FailedLoginCount { get; set; }

        [StringLength(100)]
        public string VerificationKey { get; set; }
        public DateTime? VerificationKeySent { get; set; }

        [Required]
        [StringLength(200)]
        public string HashedPassword { get; set; }

        public ICollection<UserClaim> Claims { get; set; }

        internal bool VerifyAccount(string key)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                Tracing.Verbose("[UserAccount.VerifyAccount] failed -- no key");
                return false;
            }
            if (IsAccountVerified)
            {
                Tracing.Verbose("[UserAccount.VerifyAccount] failed -- account already verified");
                return false;
            }
            if (!VerificationKey.Equals(key, StringComparison.InvariantCultureIgnoreCase))
            {
                Tracing.Verbose("[UserAccount.VerifyAccount] failed -- verification key doesn't match");
                return false;
            }

            IsAccountVerified = true;
            VerificationKey = null;
            VerificationKeySent = null;

            return true;
        }

        internal bool ChangePassword(string oldPassword, string newPassword, int failedLoginCount, TimeSpan lockoutDuration)
        {
            if (Authenticate(oldPassword, failedLoginCount, lockoutDuration))
            {
                if (oldPassword == newPassword)
                {
                    Tracing.Verbose(
                        $"[UserAccount.ChangePassword] failed for tenant:user {Tenant}:{Username} -- new password same as old password");

                    throw new ValidationException("The new password must be different than the old password.");
                }

                SetPassword(newPassword);
                return true;
            }

            Tracing.Verbose(
                $"[UserAccount.ChangePassword] failed for tentant:username {Tenant}:{Username} -- auth failed");

            return false;
        }

        internal void SetPassword(string password)
        {
            if (String.IsNullOrWhiteSpace(password))
            {
                Tracing.Verbose("[UserAccount.SetPassword] failed -- no password provided");

                throw new ValidationException("Invalid password");
            }

            Tracing.Verbose("[UserAccount.SetPassword] setting new password hash");

            HashedPassword = HashPassword(password);
            PasswordChanged = UtcNow;
        }

        internal bool IsVerificationKeyStale
        {
            get
            {
                if (VerificationKeySent == null)
                {
                    return true;
                }

                if (VerificationKeySent < UtcNow.AddDays(-VerificationKeyStaleDurationDays))
                {
                    return true;
                }

                return false;
            }
        }

        internal bool ResetPassword()
        {
            // if they've not yet verified then don't allow changes
            if (!IsAccountVerified)
            {
                Tracing.Verbose("[UserAccount.ResetPassword] failed -- account not verified");

                return false;
            }

            // if there's no current key, or if there is a key but 
            // it's older than one day, create a new reset key
            if (IsVerificationKeyStale)
            {
                Tracing.Verbose("[UserAccount.ResetPassword] creating new verification keys");

                VerificationKey = StripUglyBase64(GenerateSalt());
                VerificationKeySent = UtcNow;
            }
            else
            {
                Tracing.Verbose("[UserAccount.ResetPassword] not creating new verification keys");
            }

            return true;
        }

        internal bool ChangePasswordFromResetKey(string key, string newPassword)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                Tracing.Verbose("[UserAccount.ChangePasswordFromResetKey] failed -- no key");
                return false;
            }

            if (!IsAccountVerified)
            {
                Tracing.Verbose("[UserAccount.ChangePasswordFromResetKey] failed -- account not verified");
                return false;
            }

            // if the key is too old don't honor it
            if (IsVerificationKeyStale)
            {
                Tracing.Verbose("[UserAccount.ChangePasswordFromResetKey] failed -- verification key too old");
                return false;
            }

            // check if key matches
            if (!VerificationKey.Equals(key, StringComparison.InvariantCultureIgnoreCase))
            {
                Tracing.Verbose("[UserAccount.ChangePasswordFromResetKey] failed -- verification keys don't match");
                return false;
            }

            VerificationKey = null;
            VerificationKeySent = null;
            SetPassword(newPassword);

            return true;
        }

        internal bool Authenticate(string password, int failedLoginCount, TimeSpan lockoutDuration)
        {
            if (failedLoginCount <= 0) throw new ArgumentException("failedLoginCount");

            if (String.IsNullOrWhiteSpace(password))
            {
                Tracing.Verbose("[UserAccount.Authenticate] failed -- no password");
                return false;
            }

            if (!IsAccountVerified)
            {
                Tracing.Verbose("[UserAccount.Authenticate] failed -- account not verified");
                return false;
            }
            if (!IsLoginAllowed)
            {
                Tracing.Verbose("[UserAccount.Authenticate] failed -- account not allowed to login");
                return false;
            }

            if (HasTooManyRecentPasswordFailures(failedLoginCount, lockoutDuration))
            {
                Tracing.Verbose("[UserAccount.Authenticate] failed -- account in lockout due to failed login attempts");

                FailedLoginCount++;
                return false;
            }

            var valid = VerifyHashedPassword(password);
            if (valid)
            {
                Tracing.Verbose("[UserAccount.Authenticate] authentication success");

                LastLogin = UtcNow;
                FailedLoginCount = 0;
            }
            else
            {
                Tracing.Verbose("[UserAccount.Authenticate] failed -- invalid password");

                LastFailedLogin = UtcNow;
                if (FailedLoginCount > 0) FailedLoginCount++;
                else FailedLoginCount = 1;
            }

            return valid;
        }

        internal bool HasTooManyRecentPasswordFailures(int failedLoginCount, TimeSpan lockoutDuration)
        {
            if (failedLoginCount <= 0) throw new ArgumentException("failedLoginCount");

            if (failedLoginCount <= FailedLoginCount)
            {
                return LastFailedLogin >= UtcNow.Subtract(lockoutDuration);
            }

            return false;
        }

        internal bool ChangeEmailRequest(string newEmail)
        {
            if (String.IsNullOrWhiteSpace(newEmail)) throw new ValidationException("Invalid email.");

            // if they've not yet verified then fail
            if (!IsAccountVerified)
            {
                Tracing.Verbose("[UserAccount.ChangeEmailRequest] failed -- account not verified");
                return false;
            }

            var lowerEmail = newEmail.ToLower(new System.Globalization.CultureInfo("tr-TR", false));
            var emailHash = StripUglyBase64(Hash(ChangeEmailVerificationPrefix + lowerEmail));

            // if there's no current key, or it's not a change email key
            // or if there is a key but it's older than one day, then create 
            // a new reset key
            if (IsVerificationKeyStale ||
                VerificationKey == null ||
                !VerificationKey.StartsWith(emailHash))
            {
                Tracing.Verbose("[UserAccount.ChangeEmailRequest] creating a new reset key");

                var random = StripUglyBase64(GenerateSalt());
                VerificationKey = emailHash + random;
                VerificationKeySent = UtcNow;
            }
            else
            {
                Tracing.Verbose("[UserAccount.ChangeEmailRequest] not creating a new reset key");
            }

            return true;
        }

        internal bool ChangeEmailFromKey(string key, string newEmail)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                Tracing.Verbose("[UserAccount.ChangeEmailFromKey] failed -- no key");
                return false;
            }
            if (String.IsNullOrWhiteSpace(newEmail)) throw new ValidationException("Invalid email.");

            // only honor resets within the past day
            if (!IsVerificationKeyStale)
            {
                if (key == VerificationKey)
                {
                    var lowerEmail = newEmail.ToLower(new System.Globalization.CultureInfo("tr-TR", false));
                    var emailHash = StripUglyBase64(Hash(ChangeEmailVerificationPrefix + lowerEmail));
                    if (VerificationKey.StartsWith(emailHash))
                    {
                        Email = newEmail;
                        VerificationKey = null;
                        VerificationKeySent = null;

                        return true;
                    }
                    else
                    {
                        Tracing.Verbose("[UserAccount.ChangeEmailFromKey] failed -- verification key is not marked as a email change verificaiton key");
                    }
                }
                else
                {
                    Tracing.Verbose("[UserAccount.ChangeEmailFromKey] failed -- verification keys don't match");
                }
            }
            else
            {
                Tracing.Verbose("[UserAccount.ChangeEmailFromKey] failed -- verification key is stale");
            }

            return false;
        }

        internal void CloseAccount()
        {
            Tracing.Verbose(String.Format("[UserAccount.CloseAccount] called on: {0}, {0}", Tenant, Username));

            IsLoginAllowed = false;
            IsAccountClosed = true;
            AccountClosed = UtcNow;
        }

        internal bool IsPasswordExpired
        {
            get
            {
                var frequency = Settings.PasswordResetFrequency;
                if (frequency <= 0) return false;

                var now = UtcNow;
                var last = PasswordChanged;
                return last.AddDays(frequency) <= now;
            }
        }

        public bool HasClaim(string type)
        {
            if (String.IsNullOrWhiteSpace(type)) throw new ArgumentException("type");

            return Claims.Any(x => x.Type == type);
        }

        public bool HasClaim(string type, string value)
        {
            if (String.IsNullOrWhiteSpace(type)) throw new ArgumentException("type");
            if (String.IsNullOrWhiteSpace(value)) throw new ArgumentException("value");

            return Claims.Any(x => x.Type == type && x.Value == value);
        }

        public IEnumerable<string> GetClaimValues(string type)
        {
            if (String.IsNullOrWhiteSpace(type)) throw new ArgumentException("type");

            var query =
                from claim in Claims
                where claim.Type == type
                select claim.Value;
            return query.ToArray();
        }

        public string GetClaimValue(string type)
        {
            if (String.IsNullOrWhiteSpace(type)) throw new ArgumentException("type");

            var query =
                from claim in Claims
                where claim.Type == type
                select claim.Value;
            return query.SingleOrDefault();
        }

        public void AddClaim(string type, string value)
        {
            if (String.IsNullOrWhiteSpace(type)) throw new ArgumentException("type");
            if (String.IsNullOrWhiteSpace(value)) throw new ArgumentException("value");

            if (!HasClaim(type, value))
            {
                Tracing.Verbose($"[UserAccount.AddClaim] {Tenant}, {Username}, {type}, {value}");

                Claims.Add(
                    new UserClaim
                    {
                        Type = type,
                        Value = value
                    });
            }
        }

        public void RemoveClaim(string type)
        {
            if (String.IsNullOrWhiteSpace(type)) throw new ArgumentException("type");

            var claimsToRemove =
                from claim in Claims
                where claim.Type == type
                select claim;
            foreach (var claim in claimsToRemove.ToArray())
            {
                Tracing.Verbose($"[UserAccount.RemoveClaim] {Tenant}, {Username}, {type}, {claim.Value}");
                Claims.Remove(claim);
            }
        }

        public void RemoveClaim(string type, string value)
        {
            if (String.IsNullOrWhiteSpace(type)) throw new ArgumentException("type");
            if (String.IsNullOrWhiteSpace(value)) throw new ArgumentException("value");

            var claimsToRemove =
                from claim in Claims
                where claim.Type == type && claim.Value == value
                select claim;
            foreach (var claim in claimsToRemove.ToArray())
            {
                Tracing.Verbose($"[UserAccount.RemoveClaim] {Tenant}, {Username}, {type}, {value}");
                Claims.Remove(claim);
            }
        }

        static readonly string[] UglyBase64 = { "+", "/", "=" };
        static string StripUglyBase64(string s)
        {
            if (s == null) return null;
            return UglyBase64.Aggregate(s, (current, ugly) => current.Replace(ugly, ""));
        }

        internal string Hash(string value)
        {
            return CryptoHelper.Hash(value);
        }

        internal string GenerateSalt()
        {
            return CryptoHelper.GenerateSalt();
        }

        internal string HashPassword(string password)
        {
            return CryptoHelper.HashPassword(password, Settings.PasswordHashingIterationCount);
        }

        internal bool VerifyHashedPassword(string password)
        {
            return CryptoHelper.VerifyHashedPassword(HashedPassword, password);
        }

        internal DateTime UtcNow => DateTime.UtcNow;
    }
}