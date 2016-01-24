using System.Web.Mvc;
using Ideal.Core.Interfaces.Site;

namespace Ideal.Infrastructure.ViewPages
{
    public abstract class CustomViewPage<T> : WebViewPage
    {
        public ISiteSettings SiteSettings => DependencyResolver.Current.GetService<ISiteSettings>();
    }
}