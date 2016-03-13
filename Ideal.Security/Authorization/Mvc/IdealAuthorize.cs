using System.Web;
using System.Web.Mvc;

namespace Ideal.Security.Authorization.Mvc
{
	public class IdealAuthorize : AuthorizeAttribute
	{
		public override void OnAuthorization(AuthorizationContext filterContext)
		{


			string state = filterContext.RequestContext.HttpContext.Request.Url.OriginalString;

			base.OnAuthorization(filterContext);
		}

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			return base.AuthorizeCore(httpContext);
		}

		protected override HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
		{
			return base.OnCacheAuthorization(httpContext);
		}
	}
}
