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

namespace Ideal.Identity.Passwords
{
    #region

    

    #endregion

    public class NoopPasswordService : IPasswordService
    {
        public string PolicyMessage => "There is no password policy.";

        public bool ValidatePassword(string password)
        {
            return true;
        }
    }
}
