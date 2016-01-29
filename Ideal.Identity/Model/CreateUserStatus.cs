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

using System.ComponentModel;

namespace Ideal.Identity.Model
{
    public enum CreateUserStatus
    {
        [Description("An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.")]
        Unknown,

        [Description("A user name for that e-mail address already exists. Please enter a different e-mail address.")]
        Success,

        [Description("User name already exists. Please enter a different user name.")]
        DuplicateUserName,

        [Description("Email already in use.")]
        DuplicateEmail,
    }
}