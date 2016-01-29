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

using Ideal.Core.Interfaces.Services;
using Ideal.Core.Model.Membership;

namespace Ideal.Core.Services
{
    #region

    

    #endregion

    public class NoopNotificationService : INotificationService
    {
        public void SendAccountCreate(User user)
        {
            
        }

        public void SendAccountVerified(User user)
        {
            
        }

        public void SendResetPassword(User user)
        {
            
        }

        public void SendPasswordChangeNotice(User user)
        {
            
        }

        public void SendAccountNameReminder(User user)
        {
            
        }

        public void SendAccountDelete(User user)
        {
            
        }

        public void SendChangeEmailRequestNotice(User user, string newEmail)
        {
            
        }

        public void SendEmailChangedNotice(User user, string oldEmail)
        {
            
        }
    }
}
