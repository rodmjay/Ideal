using System.Web.Mvc;
using Ideal.Core.Interfaces.Site;

namespace Ideal.Infrastructure.ViewPages
{
    public class LayoutPage : ViewPage
    {
        public ISiteSettings SiteSettings { get; set; }
    }
}