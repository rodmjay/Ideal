﻿#region

using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

#endregion

namespace Ideal
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            DependencyResolution.DependencyBuilder.BuildDependencies();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}