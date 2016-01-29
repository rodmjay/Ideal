using System.Web.Mvc;
using Ideal.Core.Settings;
using Ideal.Identity.Settings;
using Ideal.Membership.Settings;

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