﻿#region credits
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

namespace Ideal.Membership.PasswordPolicies
{
    #region

    

    #endregion

    public class NoopPasswordPolicy : IPasswordPolicy
    {
        public string PolicyMessage => "There is no password policy.";

        public bool ValidatePassword(string password)
        {
            return true;
        }
    }
}
