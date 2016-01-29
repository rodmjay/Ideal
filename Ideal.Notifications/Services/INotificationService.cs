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

using Ideal.Identity.Model;

namespace Ideal.Notifications.Services
{
    public interface INotificationService
    {
        void SendAccountCreate(User user);
        void SendAccountVerified(User user);
        void SendResetPassword(User user);
        void SendPasswordChangeNotice(User user);
        void SendAccountNameReminder(User user);
        void SendAccountDelete(User user);
        void SendChangeEmailRequestNotice(User user, string newEmail);
        void SendEmailChangedNotice(User user, string oldEmail);
    }
} 