using System.Web.Mvc;
using Ideal.Core.Interfaces.Membership;
using Ideal.Core.Interfaces.Site;

namespace Ideal.Infrastructure.ViewPages
{
    public abstract class BaseViewPage<T> : WebViewPage<T>
    {
        public ISiteSettings SiteSettings =>
            DependencyResolver.Current.GetService<ISiteSettings>();

        public IMembershipSettings MembershipSettings =>
            DependencyResolver.Current.GetService<IMembershipSettings>();
    }

    public abstract class BaseViewPage : WebViewPage
    {
        public ISiteSettings SiteSettings =>
            DependencyResolver.Current.GetService<ISiteSettings>();

        public IMembershipSettings MembershipSettings =>
            DependencyResolver.Current.GetService<IMembershipSettings>();
    }
}