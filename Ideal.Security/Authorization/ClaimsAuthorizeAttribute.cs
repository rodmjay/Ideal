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

using System.Web.Mvc;

namespace Ideal.Security.Authorization
{
    #region

    

    #endregion

    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        public string _action;
        public string[] _resources;

        private static readonly string _label = typeof(ClaimsAuthorizeAttribute).FullName;

        public ClaimsAuthorizeAttribute()
        { }

        public ClaimsAuthorizeAttribute(string action, params string[] resources)
        {
            _action = action;
            _resources = resources;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Items[_label] = filterContext;
            base.OnAuthorization(filterContext); 
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            if (!string.IsNullOrWhiteSpace(_action))
            {
                return ClaimsAuthorization.CheckAccess(_action, _resources);
            }
            var filterContext = httpContext.Items[_label] as AuthorizationContext;
            return CheckAccess(filterContext);
        }

        protected virtual bool CheckAccess(AuthorizationContext filterContext)
        {
            var action = filterContext.RouteData.Values["action"] as string;
            var controller = filterContext.RouteData.Values["controller"] as string;

            return ClaimsAuthorization.CheckAccess(action, controller);
        }
    }
}
