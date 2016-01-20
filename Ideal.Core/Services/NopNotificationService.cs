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

using Ideal.Core.Interfaces.Notifications;

namespace Ideal.Core.Services
{
    #region

    

    #endregion

    public class NopNotificationService : INotificationService
    {
        public void SendAccountCreate(Model.User user)
        {
            
        }

        public void SendAccountVerified(Model.User user)
        {
            
        }

        public void SendResetPassword(Model.User user)
        {
            
        }

        public void SendPasswordChangeNotice(Model.User user)
        {
            
        }

        public void SendAccountNameReminder(Model.User user)
        {
            
        }

        public void SendAccountDelete(Model.User user)
        {
            
        }

        public void SendChangeEmailRequestNotice(Model.User user, string newEmail)
        {
            
        }

        public void SendEmailChangedNotice(Model.User user, string oldEmail)
        {
            
        }
    }
}
