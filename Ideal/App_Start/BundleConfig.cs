﻿#region

using System.Web.Optimization;

#endregion

namespace Ideal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/angular-core").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-resource.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/angular-app").IncludeDirectory("~/app","*.js"));

            bundles.Add(new ScriptBundle("~/bundles/less-js").Include("~/Scripts/less-1.5.1.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            var lessBundle = new StyleBundle("~/Content/css");
            lessBundle.Include("~/less/bootstrap/bootstrap.less");
            lessBundle.Include("~/less/font-awesome/font-awesome.less");
            lessBundle.Include("~/content/site.less");

            bundles.Add(lessBundle);
        }
    }
}