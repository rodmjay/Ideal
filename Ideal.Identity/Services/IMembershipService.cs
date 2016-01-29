#region credits
// ***********************************************************************
// Assembly	: Ideal.Core
// Author	: Rod Johnson
// Created	: 03-25-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System;
using System.Linq;
using Ideal.Core.Validation;
using Ideal.Identity.Model;

namespace Ideal.Identity.Services
{
    public interface IMembershipService
    {
        void Dispose();
        void SaveChanges();
        IQueryable<User> GetAll();
        IQueryable<User> GetAll(string tenant);
        User GetByUsername(string username);
        User GetByUsername(string tenant, string username);
        User GetByEmail(string email);
        User GetByEmail(string tenant, string email);
        User GetByID(int id);
        User GetByVerificationKey(string key);
        bool UsernameExists(string username);
        bool UsernameExists(string tenant, string username);
        bool EmailExists(string email);
        bool EmailExists(string tenant, string email);
        IValidationContainer<User> CreateAccount(string username, string password, string email, string firstName, string lastName, string phone, string address);
        IValidationContainer<User> CreateAccount(string tenant, string username, string password, string email, string firstName, string lastName, string phone, string address);
        IValidationContainer<User> VerifyAccount(string key);
        bool CancelNewAccount(string key);
        bool DeleteAccount(string username);
        bool DeleteAccount(string tenant, string username);
        IValidationContainer<User> Authenticate(string username, string password);
        IValidationContainer<User> Authenticate(string tenant, string username, string password);

        IValidationContainer<User> Authenticate(
            string username, string password,
            int failedLoginCount, TimeSpan lockoutDuration);

        IValidationContainer<User> Authenticate(
            string tenant, string username, string password,
            int failedLoginCount, TimeSpan lockoutDuration);

        bool ChangePassword(
            string username, string oldPassword, string newPassword);

        bool ChangePassword(
            string tenant, string username,
            string oldPassword, string newPassword);

        bool ChangePassword(
            string username,
            string oldPassword, string newPassword,
            int failedLoginCount, TimeSpan lockoutDuration);

        bool ChangePassword(
            string tenant, string username,
            string oldPassword, string newPassword,
            int failedLoginCount, TimeSpan lockoutDuration);

        IValidationContainer<User> ResetPassword(string email);
        IValidationContainer<User> ResetPassword(string tenant, string email);
        bool ChangePasswordFromResetKey(string key, string newPassword);
        void SendUsernameReminder(string email);
        void SendUsernameReminder(string tenant, string email);
        bool ChangeEmailRequest(string username, string newEmail);
        bool ChangeEmailRequest(string tenant, string username, string newEmail);
        bool ChangeEmailFromKey(string password, string key, string newEmail);

        bool ChangeEmailFromKey(
            string password, string key, string newEmail,
            int failedLoginCount, TimeSpan lockoutDuration);

        bool IsPasswordExpired(string username);
        bool IsPasswordExpired(string tenant, string username);

        bool SetProfilePicture(string tenant, string username, string pictureId);
        IValidationContainer<User> SaveOrUpdate(User user);
    }
}