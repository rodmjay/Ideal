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

using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Web.Security;
using Ideal.Identity.Model;
using Ideal.Security.Extensions;

namespace Ideal.Security.Authentication
{
    #region

    

    #endregion

    public class ClaimsAuthenticationService : IAuthenticationService
    {
        public virtual void SignIn(User user, bool isPersistant = false)
        {
            Tracing.Information($"[ClaimsBasedAuthenticationService.Signin] called: {user.Username}");

            if (String.IsNullOrWhiteSpace(user.Username)) throw new ArgumentException("username");

            // gather claims
            var claims = new List<Claim>();
            foreach (UserClaim uc in user.Claims)
                claims.Add(new Claim(uc.Type, uc.Value));

            if (!String.IsNullOrWhiteSpace(user.Email))
            {
                claims.Insert(0, new Claim(ClaimTypes.Email, user.Email));
            }
            claims.Insert(0, new Claim(ClaimTypes.AuthenticationMethod, AuthenticationMethods.Password));
            claims.Insert(0, new Claim(ClaimTypes.AuthenticationInstant, DateTime.UtcNow.ToString("s")));
            claims.Insert(0, new Claim(ClaimTypes.Name, user.Username));
            claims.Insert(0, new Claim(ClaimTypes.NameIdentifier, user.Username));

            // create principal/identity
            var id = new ClaimsIdentity(claims, "Forms");
            var cp = new ClaimsPrincipal(id);

            // claims transform
            cp = FederatedAuthentication.FederationConfiguration.IdentityConfiguration.ClaimsAuthenticationManager.Authenticate(String.Empty, cp);

            // issue cookie
            var sam = FederatedAuthentication.SessionAuthenticationModule;
            if (sam == null)
                throw new Exception("SessionAuthenticationModule is not configured and it needs to be.");

            var token = new SessionSecurityToken(cp, isPersistant ?  FormsAuthentication.Timeout : TimeSpan.FromMinutes(SessionHelpers.GetSessionTimeoutInMinutes))
                {
                    IsPersistent = isPersistant
                };
            sam.WriteSessionTokenToCookie(token);

            Tracing.Verbose(
                $"[ClaimsBasedAuthenticationService.Signin] cookie issued: {claims.GetValue(ClaimTypes.NameIdentifier)}");
        }

        public virtual void SignOut()
        {
            Tracing.Information(
                $"[ClaimsBasedAuthenticationService.SignOut] called: {ClaimsPrincipal.Current.Claims.GetValue(ClaimTypes.NameIdentifier)}");

            // clear cookie
            var sam = FederatedAuthentication.SessionAuthenticationModule;
            if (sam == null)
            {
                Tracing.Verbose("[ClaimsBasedAuthenticationService.Signout] SessionAuthenticationModule is not configured");
                throw new Exception("SessionAuthenticationModule is not configured and it needs to be.");
            }

            sam.SignOut();            
        }
    }
}