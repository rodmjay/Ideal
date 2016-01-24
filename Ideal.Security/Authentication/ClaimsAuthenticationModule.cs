#region credits
// ***********************************************************************
// Assembly	: Ideal.Security
// Author	: Rod Johnson
// Created	: 03-21-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Web;
using System.Web.Security;

namespace Ideal.Security.Authentication
{
    #region

    

    #endregion

    /// <summary>
    /// Custom implementation of the SessionAuthenticationModule HTTPModule to allow for more robust security configuration
    /// </summary>
    public class ClaimsAuthenticationModule : SessionAuthenticationModule
    {
        protected bool IsSlidingExpiration { get; set; }
        protected TimeSpan Timeout { get; set; }
        protected string LoginUrl { get; set; }
        protected string CookieName { get; set; }
        protected string CookieDomain { get; set; }
        protected bool RequireSsl { get; set; }

        protected override void InitializeModule(HttpApplication context)
        {            
            base.InitializeModule(context);

            context.EndRequest += context_EndRequest;
        }

        /// <summary>
        /// Correct behavior in case the cookie expires.  We want to redirect to the login page, not show 404 error.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void context_EndRequest(object sender, EventArgs e)
        {
            var context = (sender as HttpApplication).Context;

            if (context.Response.StatusCode == 401)
            {
                var noRedirect = context.Items["NoRedirect"];

                if (noRedirect == null)
                {
                    var loginUrl = LoginUrl + "?returnUrl=" + HttpUtility.UrlEncode(context.Request.RawUrl, context.Request.ContentEncoding);
                    context.Response.Redirect(loginUrl);
                }
            }
        }

        /// <summary>
        /// Piggyback the settings of forms authentication
        /// </summary>
        protected override void InitializePropertiesFromConfiguration()
        {
            base.InitializePropertiesFromConfiguration();

            // read formsauth configuration
            IsSlidingExpiration = FormsAuthentication.SlidingExpiration;
            Timeout = FormsAuthentication.Timeout;
            LoginUrl = FormsAuthentication.LoginUrl;
            CookieName = FormsAuthentication.FormsCookieName;
            CookieDomain = FormsAuthentication.CookieDomain;
            RequireSsl = FormsAuthentication.RequireSSL;

            // configure cookie handler
            CookieHandler.Name = CookieName;
            CookieHandler.Domain = CookieDomain;
            CookieHandler.RequireSsl = RequireSsl;            
        }

        /// <summary>
        /// Enables sliding expiration behavior of the user's session.
        /// </summary>
        /// <param name="e">The <see cref="SessionSecurityTokenReceivedEventArgs"/> instance containing the event data.</param>
        protected override void OnSessionSecurityTokenReceived(SessionSecurityTokenReceivedEventArgs e)
        {
            base.OnSessionSecurityTokenReceived(e);

            if (IsSlidingExpiration)
            {
                if (NeedsRenew(e.SessionToken))
                {
                    var timeout = e.SessionToken.IsPersistent
                        ? Timeout
                        : TimeSpan.FromMinutes(SessionHelpers.GetSessionTimeoutInMinutes);

                    e.SessionToken = CreateSessionSecurityToken(
                        e.SessionToken.ClaimsPrincipal,
                        e.SessionToken.Context,
                        DateTime.UtcNow,
                        DateTime.UtcNow.Add(timeout),
                        e.SessionToken.IsPersistent);

                    e.ReissueCookie = true;
                }
            }

            if (e.SessionToken.ValidTo < DateTime.UtcNow)
            {
                DeleteSessionTokenCookie();
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Decides if the cookie needs to be renewed
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        protected virtual bool NeedsRenew(SessionSecurityToken token)
        {
            DateTime utcNow = DateTime.UtcNow;

            TimeSpan span = (TimeSpan)(utcNow - token.ValidFrom);
            TimeSpan span2 = (TimeSpan)(token.ValidTo - utcNow);

            if (span2 > span)
            {
                return false;
            }

            return true;
        }
    }
}