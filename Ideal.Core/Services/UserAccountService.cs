#region credits
// ***********************************************************************
// Assembly	: Ideal.Core
// Author	: Rod Johnson
// Created	: 03-23-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Transactions;
using Ideal.Core.Interfaces.Data;
using Ideal.Core.Interfaces.Membership;
using Ideal.Core.Interfaces.Notifications;
using Ideal.Core.Interfaces.Service;
using Ideal.Core.Interfaces.Validation;
using Ideal.Core.Model;

namespace Ideal.Core.Services
{

    public class UserAccountService : IDisposable, IUserAccountService
    {
        IUserRepository _userRepository;
        readonly INotificationService _notificationService;
        readonly IPasswordPolicy _passwordPolicy;
        private readonly IMembershipSettings _settings;
        private readonly IUnitOfWork _unitOfWork;

        public UserAccountService(
            IUserRepository userAccountRepository,
            INotificationService notificationService,
            IPasswordPolicy passwordPolicy,
            IMembershipSettings settings,
            IUnitOfWork unitOfWork)
        {
            _settings = settings;
            _unitOfWork = unitOfWork;
            if (userAccountRepository == null) throw new ArgumentNullException("userAccountRepository");

            this._userRepository = userAccountRepository;
            this._notificationService = notificationService;
            this._passwordPolicy = passwordPolicy;
        }

        public void Dispose()
        {
            if (_userRepository != null)
            {
                // this.userRepository.Dispose();
                _userRepository = null;
            }
        }

        public virtual void SaveChanges()
        {
            //this.userRepository.SaveOrUpdate();
        }

        public virtual IQueryable<User> GetAll()
        {
            return GetAll(null);
        }

        public virtual IQueryable<User> GetAll(string tenant)
        {
            if (!_settings.MultiTenant)
            {
                tenant = _settings.DefaultTenant;
            }

            if (String.IsNullOrWhiteSpace(tenant)) return Enumerable.Empty<User>().AsQueryable();

            return this._userRepository.GetAll().Where(x => x.Tenant == tenant && x.IsAccountClosed == false);
        }

        public virtual User GetByUsername(string username)
        {
            return GetByUsername(null, username);
        }

        public virtual User GetByUsername(string tenant, string username)
        {
            if (!_settings.MultiTenant)
            {
                tenant = _settings.DefaultTenant;
            }

            if (String.IsNullOrWhiteSpace(tenant)) return null;
            if (String.IsNullOrWhiteSpace(username)) return null;

            var account = _userRepository.GetAll().SingleOrDefault(x => x.Tenant == tenant && x.Username == username);
            if (account == null)
            {
                Tracing.Verbose($"[UserAccountService.GetByUsername] failed to locate account: {tenant}, {username}");
            }
            return account;
        }

        public virtual User GetByEmail(string email)
        {
            return GetByEmail(null, email);
        }

        public virtual User GetByEmail(string tenant, string email)
        {
            if (!_settings.MultiTenant)
            {
                tenant = _settings.DefaultTenant;
            }

            if (String.IsNullOrWhiteSpace(tenant)) return null;
            if (String.IsNullOrWhiteSpace(email)) return null;

            var account = _userRepository.GetAll().SingleOrDefault(x => x.Tenant == tenant && x.Email == email);
            if (account == null)
            {
                //Tracing.Verbose(String.Format("[UserAccountService.GetByEmail] failed to locate account: {0}, {1}", tenant, email));
            }
            return account;
        }

        public virtual User GetByID(int id)
        {
            var account = this._userRepository.GetById(id);
            if (account == null)
            {
                //Tracing.Verbose(String.Format("[UserAccountService.GetByID] failed to locate account: {0}", id));
            }
            return account;
        }

        public virtual User GetByVerificationKey(string key)
        {
            if (String.IsNullOrWhiteSpace(key)) return null;

            var account = _userRepository.GetAll().Where(x => x.VerificationKey == key).SingleOrDefault();
            if (account == null)
            {
                //Tracing.Verbose(String.Format("[UserAccountService.GetByVerificationKey] failed to locate account: {0}", key));
            }
            return account;
        }

        public virtual bool UsernameExists(string username)
        {
            return UsernameExists(null, username);
        }

        public virtual bool UsernameExists(string tenant, string username)
        {
            if (String.IsNullOrWhiteSpace(username)) return false;

            if (_settings.UsernamesUniqueAcrossTenants)
            {
                return this._userRepository.GetAll().Where(x => x.Username == username).Any();
            }
            else
            {
                if (!_settings.MultiTenant)
                {
                    tenant = _settings.DefaultTenant;
                }

                if (String.IsNullOrWhiteSpace(tenant)) return false;

                return this._userRepository.GetAll().Where(x => x.Tenant == tenant && x.Username == username).Any();
            }
        }

        public virtual bool EmailExists(string email)
        {
            return EmailExists(null, email);
        }

        public virtual bool EmailExists(string tenant, string email)
        {
            if (!_settings.MultiTenant)
            {
                tenant = _settings.DefaultTenant;
            }

            if (String.IsNullOrWhiteSpace(tenant)) return false;
            if (String.IsNullOrWhiteSpace(email)) return false;

            return this._userRepository.GetAll().Any(x => x.Tenant == tenant && x.Email == email);
        }

        public virtual IValidationContainer<User> CreateAccount(string username, string password, string email, string firstName, string lastName, string phone, string address)
        {
            return CreateAccount(null, username, password, email, firstName, lastName, phone, address);
        }

        public virtual IValidationContainer<User> CreateAccount(string tenant, string username, string password, string email, string firstName, string lastName, string phone, string address)
        {
            //Tracing.Information(String.Format("[UserAccountService.CreateAccount] called: {0}, {1}, {2}", tenant, username, email));

            if (_settings.EmailIsUsername)
            {
                username = email;
            }

            if (!_settings.MultiTenant)
            {
                tenant = _settings.DefaultTenant;
            }

            if (String.IsNullOrWhiteSpace(tenant)) throw new ArgumentException("tenant");
            if (String.IsNullOrWhiteSpace(username)) throw new ArgumentException("username");
            if (String.IsNullOrWhiteSpace(password)) throw new ArgumentException("password");
            if (String.IsNullOrWhiteSpace(email)) throw new ArgumentException("email");
            if (String.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("firstName");
            if (String.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("lastName");
            if (String.IsNullOrWhiteSpace(phone)) throw new ArgumentException("phone");
            //if (String.IsNullOrWhiteSpace(address)) throw new ArgumentException("address");

            ValidatePassword(tenant, username, password);

            var validator = new EmailAddressAttribute();
            if (!validator.IsValid(email))
            {
                //Tracing.Verbose(String.Format("[UserAccountService.CreateAccount] Email validation failed: {0}, {1}, {2}", tenant, username, email));

                throw new ValidationException("Email is invalid.");
            }

            if (UsernameExists(tenant, username))
            {
                //Tracing.Verbose(String.Format("[UserAccountService.CreateAccount] Username already exists: {0}, {1}", tenant, username));

                var msg = _settings.EmailIsUsername ? "Email" : "Username";
                throw new ValidationException(msg + " already in use.");
            }

            if (EmailExists(tenant, email))
            {
                //Tracing.Verbose(String.Format("[UserAccountService.CreateAccount] Email already exists: {0}, {1}, {2}", tenant, username, email));

                throw new ValidationException("Email already in use.");
            }

            var account = new User(tenant, username, password, email)
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Address = address,
                    PhoneNumber = phone
                };

            var validation = account.GetValidationContainer();
            if (!validation.IsValid)
                return validation;

            this._userRepository.SaveOrUpdate(account);

            if (this._notificationService != null)
            {
                if (_settings.RequireAccountVerification)
                {
                    this._notificationService.SendAccountCreate(account);
                }
                else
                {
                    this._notificationService.SendAccountVerified(account);
                }
            }

            _unitOfWork.Commit();
            return validation;
        }

        protected internal virtual void ValidatePassword(string tenant, string username, string password)
        {
            if (_passwordPolicy != null)
            {


                if (!_passwordPolicy.ValidatePassword(password))
                {
                    //Tracing.Verbose(String.Format("[ValidatePassword] Failed: {0}, {1}, {2}", tenant, username, passwordPolicy.PolicyMessage));

                    throw new ValidationException("Invalid password: " + _passwordPolicy.PolicyMessage);
                }
            }
        }

        public virtual IValidationContainer<User> VerifyAccount(string key)
        {
            //Tracing.Information(String.Format("[UserAccountService.VerifyAccount] called: {0}", key));

            var account = this.GetByVerificationKey(key);
            if (account == null) throw new UserNotFoundException();

            var container = account.GetValidationContainer();

            //Tracing.Verbose(String.Format("[UserAccountService.VerifyAccount] account located: {0}, {1}", account.Tenant, account.Username));

            var result = account.VerifyAccount(key);
            if (result == false)
                container.ValidationErrors.Add("", new List<string>() { "Unable to verify account" });

            this._userRepository.SaveOrUpdate(account);

            if (result && this._notificationService != null)
            {
                this._notificationService.SendAccountVerified(account);
            }
            _unitOfWork.Commit();
            return container;
        }

        public virtual bool CancelNewAccount(string key)
        {
            //Tracing.Information(String.Format("[UserAccountService.CancelNewAccount] called: {0}", key));

            var account = this.GetByVerificationKey(key);
            if (account == null) return false;

            //Tracing.Verbose(String.Format("[UserAccountService.CancelNewAccount] account located: {0}, {1}", account.Tenant, account.Username));

            if (account.IsAccountVerified) return false;
            if (account.VerificationKey != key) return false;

            //Tracing.Verbose(String.Format("[UserAccountService.CancelNewAccount] deleting account: {0}, {1}", account.Tenant, account.Username));

            DeleteAccount(account);

            return true;
        }

        public virtual bool DeleteAccount(string username)
        {
            return DeleteAccount(null, username);
        }

        public virtual bool DeleteAccount(string tenant, string username)
        {
            //Tracing.Information(String.Format("[UserAccountService.DeleteAccount] called: {0}, {1}", tenant, username));

            if (!_settings.MultiTenant)
            {
                tenant = _settings.DefaultTenant;
            }

            if (String.IsNullOrWhiteSpace(tenant)) return false;
            if (String.IsNullOrWhiteSpace(username)) return false;

            var account = this.GetByUsername(tenant, username);
            if (account == null) return false;

            DeleteAccount(account);

            return true;
        }

        protected internal virtual void DeleteAccount(User account)
        {
            if (_settings.AllowAccountDeletion || !account.IsAccountVerified)
            {
                //Tracing.Verbose(String.Format("[UserAccountService.DeleteAccount] removing account record: {0}, {1}", account.Tenant, account.Username));
                this._userRepository.Delete(account);
            }
            else
            {
                //Tracing.Verbose(String.Format("[UserAccountService.DeleteAccount] marking account closed: {0}, {1}", account.Tenant, account.Username));
                account.CloseAccount();
            }

            using (var tx = new TransactionScope())
            {
                this._userRepository.SaveOrUpdate(account);

                if (this._notificationService != null)
                {
                    this._notificationService.SendAccountDelete(account);
                }

                tx.Complete();
            }
        }

        public virtual IValidationContainer<User> Authenticate(string username, string password)
        {
            return Authenticate(null, username, password);
        }

        public virtual IValidationContainer<User> Authenticate(string tenant, string username, string password)
        {
            return Authenticate(
                tenant, username, password,
                _settings.AccountLockoutFailedLoginAttempts,
                _settings.AccountLockoutDuration);
        }

        public virtual IValidationContainer<User> Authenticate(
            string username, string password,
            int failedLoginCount, TimeSpan lockoutDuration)
        {
            return Authenticate(null, username, password, failedLoginCount, lockoutDuration);
        }

        public virtual IValidationContainer<User> Authenticate(
            string tenant, string username, string password,
            int failedLoginCount, TimeSpan lockoutDuration)
        {
            Tracing.Information(String.Format("[UserAccountService.Authenticate] called: {0}, {1}", tenant, username));

            if (!_settings.MultiTenant)
            {
                tenant = _settings.DefaultTenant;
            }

            if (String.IsNullOrWhiteSpace(tenant)) throw new ArgumentException("tenant");
            if (String.IsNullOrWhiteSpace(username)) throw new ArgumentException("username");
            if (String.IsNullOrWhiteSpace(password)) throw new ArgumentException("password");

            var account = this.GetByUsername(tenant, username);
            if (account == null)
                throw new UserNotFoundException();

            return Authenticate(account, password, failedLoginCount, lockoutDuration);
        }

        protected internal virtual IValidationContainer<User> Authenticate(User account, string password, int failedLoginCount, TimeSpan lockoutDuration)
        {
            var container = account.GetValidationContainer();
            if (!container.IsValid)
                return container;

            var result = account.Authenticate(password, failedLoginCount, lockoutDuration);

            if (!result)
                container.ValidationErrors.Add("", new List<string>() { "Unable to authenticate user" });

            this._userRepository.SaveOrUpdate(account);
            _unitOfWork.Commit();

            Tracing.Verbose(String.Format("[UserAccountService.Authenticate] authentication outcome: {0}, {1}, {2}", account.Tenant, account.Username, result ? "Successful Login" : "Failed Login"));

            return container;
        }

        public virtual void SetPassword(string username, string newPassword)
        {
            SetPassword(null, username, newPassword);
        }

        public virtual void SetPassword(string tenant, string username, string newPassword)
        {
            Tracing.Information(String.Format("[UserAccountService.SetPassword] called: {0}, {1}", tenant, username));

            if (!_settings.MultiTenant)
            {
                tenant = _settings.DefaultTenant;
            }

            if (String.IsNullOrWhiteSpace(tenant)) throw new ValidationException("Invalid tenant.");
            if (String.IsNullOrWhiteSpace(username)) throw new ValidationException("Invalid username.");
            if (String.IsNullOrWhiteSpace(newPassword)) throw new ValidationException("Invalid newPassword.");

            ValidatePassword(tenant, username, newPassword);

            var account = this.GetByUsername(tenant, username);
            if (account == null) throw new ValidationException("Invalid tenant and/or username.");

            Tracing.Information(String.Format("[UserAccountService.SetPassword] setting new password for: {0}, {1}", tenant, username));

            account.SetPassword(newPassword);
            this._userRepository.SaveOrUpdate(account);

            if (this._notificationService != null)
            {
                this._notificationService.SendPasswordChangeNotice(account);
            }

            _unitOfWork.Commit();
        }

        public virtual bool ChangePassword(
            string username, string oldPassword, string newPassword)
        {
            return ChangePassword(null, username, oldPassword, newPassword);
        }

        public virtual bool ChangePassword(
            string tenant, string username,
            string oldPassword, string newPassword)
        {
            return ChangePassword(
                tenant, username,
                oldPassword, newPassword,
                _settings.AccountLockoutFailedLoginAttempts,
                _settings.AccountLockoutDuration);
        }

        public virtual bool ChangePassword(
            string username,
            string oldPassword, string newPassword,
            int failedLoginCount, TimeSpan lockoutDuration)
        {
            return ChangePassword(null, username, oldPassword, newPassword, failedLoginCount, lockoutDuration);
        }

        public virtual bool ChangePassword(
            string tenant, string username,
            string oldPassword, string newPassword,
            int failedLoginCount, TimeSpan lockoutDuration)
        {
            Tracing.Information(String.Format("[UserAccountService.ChangePassword] called: {0}, {1}", tenant, username));

            if (!_settings.MultiTenant)
            {
                tenant = _settings.DefaultTenant;
            }

            if (String.IsNullOrWhiteSpace(tenant)) return false;
            if (String.IsNullOrWhiteSpace(username)) return false;
            if (String.IsNullOrWhiteSpace(oldPassword)) return false;
            if (String.IsNullOrWhiteSpace(newPassword)) return false;

            ValidatePassword(tenant, username, newPassword);

            var account = this.GetByUsername(tenant, username);
            if (account == null) return false;

            bool result = false;
            try
            {
                result = account.ChangePassword(oldPassword, newPassword, failedLoginCount, lockoutDuration);
                Tracing.Verbose(String.Format("[UserAccountService.ChangePassword] change password outcome: {0}, {1}, {2}", account.Tenant, account.Username, result ? "Successful" : "Failed"));
            }
            finally
            {

                this._userRepository.SaveOrUpdate(account);

                if (result && this._notificationService != null)
                {
                    this._notificationService.SendPasswordChangeNotice(account);
                }

                _unitOfWork.Commit();
            }
            return result;
        }

        public virtual IValidationContainer<User> ResetPassword(string email)
        {
            return ResetPassword(null, email);
        }

        public virtual IValidationContainer<User> ResetPassword(string tenant, string email)
        {
            Tracing.Information(String.Format("[UserAccountService.ResetPassword] called: {0}, {1}", tenant, email));

            if (!_settings.MultiTenant)
            {
                tenant = _settings.DefaultTenant;
            }

            if (String.IsNullOrWhiteSpace(tenant)) throw new ArgumentNullException("tenant");
            if (String.IsNullOrWhiteSpace(email)) throw new ArgumentNullException("email");

            var account = this.GetByEmail(tenant, email);
            if (account == null) throw new UserNotFoundException();

            var container = account.GetValidationContainer();
            if (!container.IsValid)
                return container;

            if (!account.IsAccountVerified)
            {
                // if they're not verified, resend the new account email
                if (_settings.RequireAccountVerification &&
                    this._notificationService != null)
                {
                    Tracing.Verbose(String.Format("[UserAccountService.ResetPassword] account not verified, re-sending account create notification: {0}, {1}", account.Tenant, account.Username));

                    this._notificationService.SendAccountCreate(account);
                    return container;
                }

                // if we don't have a notification system then not much we can do
                Tracing.Warning(String.Format("[UserAccountService.ResetPassword] account not verified, no notification to re-send invite: {0}, {1}", account.Tenant, account.Username));

                container.ValidationErrors.Add("", new List<string>() { "Account not yet verified" });

                return container;
            }

            var result = account.ResetPassword();

            Tracing.Verbose(String.Format("[UserAccountService.ResetPassword] reset password outcome: {0}, {1}, {2}", account.Tenant, account.Username, result ? "Successful" : "Failed"));

            if (result)
            {

                this._userRepository.SaveOrUpdate(account);

                if (this._notificationService != null)
                {
                    this._notificationService.SendResetPassword(account);
                }

                _unitOfWork.Commit();
            }
            return container;
        }

        public virtual bool ChangePasswordFromResetKey(string key, string newPassword)
        {
            Tracing.Information(String.Format("[UserAccountService.ChangePasswordFromResetKey] called: {0}", key));

            if (String.IsNullOrWhiteSpace(key))
                return false;

            var account = this.GetByVerificationKey(key);
            if (account == null) return false;

            Tracing.Verbose(String.Format("[UserAccountService.ChangePasswordFromResetKey] account located: {0}, {1}", account.Tenant, account.Username));

            ValidatePassword(account.Tenant, account.Username, newPassword);

            var result = account.ChangePasswordFromResetKey(key, newPassword);

            Tracing.Verbose(String.Format("[UserAccountService.ChangePasswordFromResetKey] change password outcome: {0}, {1}, {2}", account.Tenant, account.Username, result ? "Successful" : "Failed"));

            if (result)
            {
                this._userRepository.SaveOrUpdate(account);

                if (this._notificationService != null)
                {
                    this._notificationService.SendPasswordChangeNotice(account);
                }
                _unitOfWork.Commit();
            }
            return result;
        }

        public virtual void SendUsernameReminder(string email)
        {
            SendUsernameReminder(null, email);
        }

        public virtual void SendUsernameReminder(string tenant, string email)
        {
            if (this._notificationService == null)
            {
                throw new InvalidOperationException("NotificationService not configured.");
            }

            Tracing.Information(String.Format("[UserAccountService.SendUsernameReminder] called: {0}, {1}", tenant, email));

            if (!_settings.MultiTenant)
            {
                tenant = _settings.DefaultTenant;
            }

            if (String.IsNullOrWhiteSpace(tenant)) return;
            if (String.IsNullOrWhiteSpace(email)) return;

            var account = this.GetByEmail(tenant, email);
            if (account != null)
            {
                Tracing.Verbose(String.Format("[UserAccountService.SendUsernameReminder] account located: {0}, {1}", account.Tenant, account.Username));

                this._notificationService.SendAccountNameReminder(account);
            }
        }

        public virtual bool ChangeEmailRequest(string username, string newEmail)
        {
            return ChangeEmailRequest(null, username, newEmail);
        }

        public virtual bool ChangeEmailRequest(string tenant, string username, string newEmail)
        {
            Tracing.Information(String.Format("[UserAccountService.ChangeEmailRequest] called: {0}, {1}, {2}", tenant, username, newEmail));

            if (_settings.EmailIsUsername &&
                !_settings.AllowEmailChangeWhenEmailIsUsername)
            {
                Tracing.Warning(String.Format("[UserAccountService.ChangeEmailRequest] security setting EmailIsUsername is true and AllowEmailChangeWhenEmailIsUsername is false, so change request failed: {0}, {1}, {2}", tenant, username, newEmail));

                return false;
            }

            if (!_settings.MultiTenant)
            {
                tenant = _settings.DefaultTenant;
            }

            if (String.IsNullOrWhiteSpace(tenant)) return false;
            if (String.IsNullOrWhiteSpace(username)) return false;
            if (String.IsNullOrWhiteSpace(newEmail)) return false;

            EmailAddressAttribute validator = new EmailAddressAttribute();
            if (!validator.IsValid(newEmail))
            {
                Tracing.Verbose(
                    $"[UserAccountService.ChangeEmailRequest] email validation failed: {tenant}, {username}, {newEmail}");

                throw new ValidationException("Email is invalid.");
            }

            var account = this.GetByUsername(tenant, username);
            if (account == null) return false;

            Tracing.Verbose(
                $"[UserAccountService.ChangeEmailRequest] account located: {account.Tenant}, {account.Username}");

            var result = account.ChangeEmailRequest(newEmail);

            Tracing.Verbose(
                $"[UserAccountService.ChangeEmailRequest] change request outcome: {account.Tenant}, {account.Username}, {(result ? "Successful" : "Failed")}");

            if (result)
            {
                using (var tx = new TransactionScope())
                {
                    this._userRepository.SaveOrUpdate(account);

                    if (this._notificationService != null)
                    {
                        this._notificationService.SendChangeEmailRequestNotice(account, newEmail);
                    }

                    tx.Complete();
                }
            }

            return result;
        }

        public virtual bool ChangeEmailFromKey(string password, string key, string newEmail)
        {
            return ChangeEmailFromKey(
                password, key, newEmail,
                _settings.AccountLockoutFailedLoginAttempts,
                _settings.AccountLockoutDuration);
        }

        public virtual bool ChangeEmailFromKey(
            string password, string key, string newEmail,
            int failedLoginCount, TimeSpan lockoutDuration)
        {
            Tracing.Information(String.Format("[UserAccountService.ChangeEmailFromKey] called: {0}, {1}", key, newEmail));

            if (_settings.EmailIsUsername &&
                !_settings.AllowEmailChangeWhenEmailIsUsername)
            {
                Tracing.Warning(String.Format("[UserAccountService.ChangeEmailFromKey] security setting EmailIsUsername is true and AllowEmailChangeWhenEmailIsUsername is false, so change request failed: key: {0}, new email: {1}", key, newEmail));

                return false;
            }

            if (String.IsNullOrWhiteSpace(password)) return false;
            if (String.IsNullOrWhiteSpace(key)) return false;
            if (String.IsNullOrWhiteSpace(newEmail)) return false;

            var account = this.GetByVerificationKey(key);
            if (account == null) return false;

            Tracing.Verbose(String.Format("[UserAccountService.ChangeEmailFromKey] account located: {0}, {1}", account.Tenant, account.Username));

            if (!Authenticate(account, password, failedLoginCount, lockoutDuration).IsValid)
            {
                return false;
            }

            var oldEmail = account.Email;
            var result = account.ChangeEmailFromKey(key, newEmail);

            if (result &&
                _settings.EmailIsUsername &&
                _settings.AllowEmailChangeWhenEmailIsUsername)
            {
                Tracing.Warning(String.Format("[UserAccountService.ChangeEmailFromKey] security setting EmailIsUsername is true and AllowEmailChangeWhenEmailIsUsername is true, so changing username: {0}, to: {1}", account.Username, newEmail));
                account.Username = newEmail;
            }

            Tracing.Verbose(String.Format("[UserAccountService.ChangeEmailFromKey] change email outcome: {0}, {1}, {2}", account.Tenant, account.Username, result ? "Successful" : "Failed"));

            if (result)
            {
                using (var tx = new TransactionScope())
                {
                    this._userRepository.SaveOrUpdate(account);

                    if (this._notificationService != null)
                    {
                        this._notificationService.SendEmailChangedNotice(account, oldEmail);
                    }

                    tx.Complete();
                }
            }
            return result;
        }

        public virtual bool IsPasswordExpired(string username)
        {
            return IsPasswordExpired(null, username);
        }

        public virtual bool IsPasswordExpired(string tenant, string username)
        {
            if (!_settings.MultiTenant)
            {
                tenant = _settings.DefaultTenant;
            }

            if (String.IsNullOrWhiteSpace(tenant)) return false;
            if (String.IsNullOrWhiteSpace(username)) return false;

            var account = this.GetByUsername(tenant, username);
            if (account == null) return false;

            return account.IsPasswordExpired;
        }

        public bool SetProfilePicture(string tenant, string username, string pictureId)
        {
            throw new NotImplementedException();
        }

        public IValidationContainer<User> SaveOrUpdate(User user)
        {
            var container = user.GetValidationContainer();
            if (!container.IsValid)
                return container;

            _userRepository.SaveOrUpdate(user);
            _unitOfWork.Commit();

            return container;
        }
    }
}
