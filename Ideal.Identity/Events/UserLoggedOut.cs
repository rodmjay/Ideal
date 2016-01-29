#region credits
// ***********************************************************************
// Assembly	: Ideal.Core
// Author	: Rod Johnson
// Created	: 02-24-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using User = Ideal.Identity.Model.User;

namespace Ideal.Identity.Events
{
    #region

    

    #endregion

    public class UserLoggedOut : UserActivity
    {
        public UserLoggedOut(User user) : base(user, "User Logged Out")
        {

        }
    }
}