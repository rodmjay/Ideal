#region credits
// ***********************************************************************
// Assembly	: Ideal.Security
// Author	: Rod Johnson
// Created	: 03-16-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Security.Claims;

namespace Ideal.Security.Extensions
{
    #region

    

    #endregion

    public static class ClaimsPrincipalExtensions
    {
        public static bool HasClaim(this ClaimsPrincipal user, string type)
        {
            if (user != null)
            {
                return user.HasClaim(x => x.Type == type);
            }
            return false;
        }
    }
}
