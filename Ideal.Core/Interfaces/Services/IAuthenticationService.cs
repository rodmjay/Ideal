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

using Ideal.Core.Model.Membership;

namespace Ideal.Core.Interfaces.Services
{
    #region

    

    #endregion

    public interface IAuthenticationService
    {
        void SignIn(User user, bool isPersistant);
        void SignOut();
    }
}
