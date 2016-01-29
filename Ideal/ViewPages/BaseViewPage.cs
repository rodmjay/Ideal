using System.Web.Mvc;
using Ideal.Core.Settings;
using Ideal.Identity.Services;
using Ideal.Identity.Settings;

namespace Ideal.ViewPages
{
    public abstract class BaseViewPage<T> : WebViewPage<T>
    {
        public ISiteSettings SiteSettings =>
            DependencyResolver.Current.GetService<ISiteSettings>();

        public IAccountSettings MembershipSettings =>
            DependencyResolver.Current.GetService<IAccountSettings>();
    }

    public abstract class BaseViewPage : WebViewPage
    {
        public ISiteSettings SiteSettings =>
            DependencyResolver.Current.GetService<ISiteSettings>();

        public IAccountSettings MembershipSettings =>
            DependencyResolver.Current.GetService<IAccountSettings>();
    }
}