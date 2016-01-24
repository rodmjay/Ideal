#region credits
// ***********************************************************************
// Assembly	: DemoApplication.Security
// Author	: Rod Johnson
// Created	: 03-21-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using System.Web.Routing;

namespace Ideal.Security.Authorization
{
    #region

    

    #endregion

    public class ClaimsAuthorizeModule : IAclModule
    {
        public bool IsAccessibleToUser(IControllerTypeResolver controllerTypeResolver, DefaultSiteMapProvider provider,
            HttpContext context, SiteMapNode node)
        {
            // is security trimming enabled?
            if (!provider.SecurityTrimmingEnabled)
            {
                return true;
            }

            // external nodes
            var nodeUrl = node.Url;
            if (nodeUrl.StartsWith("http") || nodeUrl.StartsWith("ftp"))
            {
                return true;
            }

            // Is it a regular node?
            var mvcNode = node as MvcSiteMapNode;
            if (mvcNode == null)
            {
                return true;
            }

            var controllerType = controllerTypeResolver.ResolveControllerType(mvcNode.Area, mvcNode.Controller);
            if (controllerType == null)
            {
                return false;
            }

            // Find routes for the sitemap node's url
            HttpContextBase httpContext = new HttpContextMethodOverrider(context, null);
            string originalPath = httpContext.Request.Path;
            var originalRoutes = RouteTable.Routes.GetRouteData(httpContext);
            httpContext.RewritePath(nodeUrl, true);

            HttpContextBase httpContext2 = new HttpContext2(context);
            RouteData routes = mvcNode.GetRouteData(httpContext2);
            if (routes == null)
            {
                return true; // Static URL's will have no route data, therefore return true.
            }
            foreach (var routeValue in mvcNode.RouteValues)
            {
                routes.Values[routeValue.Key] = routeValue.Value;
            }
            if (originalRoutes != null && (!routes.Route.Equals(originalRoutes.Route) || originalPath != nodeUrl || mvcNode.Area == String.Empty))
            {
                routes.DataTokens.Remove("area");
                //routes.DataTokens.Remove("Namespaces");
                //routes.Values.Remove("area");
            }
            var requestContext = new RequestContext(httpContext, routes);

            // Create controller context
            var controllerContext = new ControllerContext();
            controllerContext.RequestContext = requestContext;

            // Whether controller is built by the ControllerFactory (or otherwise by Activator)
            bool factoryBuiltController = false;
            try
            {
                string controllerName = requestContext.RouteData.GetRequiredString("controller");
                controllerContext.Controller = ControllerBuilder.Current.GetControllerFactory().CreateController(requestContext, controllerName) as ControllerBase;
                factoryBuiltController = true;
            }
            catch
            {
                try
                {
                    controllerContext.Controller = Activator.CreateInstance(controllerType) as ControllerBase;
                }
                catch
                {
                }
            }

            ControllerDescriptor controllerDescriptor = null;
            if (typeof(IController).IsAssignableFrom(controllerType))
            {
                controllerDescriptor = new ReflectedControllerDescriptor(controllerType);
            }
            else if (typeof(IAsyncController).IsAssignableFrom(controllerType))
            {
                controllerDescriptor = new ReflectedAsyncControllerDescriptor(controllerType);
            }

            ActionDescriptor actionDescriptor = null;
            try
            {
                actionDescriptor = controllerDescriptor.FindAction(controllerContext, mvcNode.Action);
            }
            catch
            {
            }
            if (actionDescriptor == null)
            {
                actionDescriptor = controllerDescriptor.GetCanonicalActions().FirstOrDefault(a => a.ActionName == mvcNode.Action);
            }

            // Verify security
            try
            {
                if (actionDescriptor != null)
                {
                    // fixes #130 - Check whether we have an AllowAnonymous Attribute
                    var ignoreAuthorization = actionDescriptor.HasAttribute<AllowAnonymousAttribute>();
                    if (ignoreAuthorization)
                        return true;

                    if (actionDescriptor.HasAttribute<OnlyAnonymousAttribute>())
                        return !requestContext.HttpContext.Request.IsAuthenticated;

                    IFilterProvider filterProvider = ResolveFilterProvider();
                    IEnumerable<Filter> filters;

                    // If depencency resolver has an IFilterProvider registered, use it
                    if (filterProvider != null)
                    {
                        filters = filterProvider.GetFilters(controllerContext, actionDescriptor);
                    }
                    // Otherwise use FilterProviders.Providers
                    else
                    {
                        filters = FilterProviders.Providers.GetFilters(controllerContext, actionDescriptor);
                    }

                    IEnumerable<AuthorizeAttribute> authorizeAttributesToCheck =
                        filters
                            .Where(f => f.Instance is AuthorizeAttribute)
                            .Select(f => f.Instance as AuthorizeAttribute);

                    // Verify all attributes
                    foreach (var authorizeAttribute in authorizeAttributesToCheck)
                    {
                        try
                        {
                            var currentAuthorizationAttributeType = authorizeAttribute.GetType();

                            var builder = new AuthorizeAttributeBuilder();
                            var subclassedAttribute =
                                currentAuthorizationAttributeType == typeof(AuthorizeAttribute) ?
                                   new InternalAuthorize(authorizeAttribute) : // No need to use Reflection.Emit when ASP.NET MVC built-in attribute is used
                                   (IAuthorizeAttribute)builder.Build(currentAuthorizationAttributeType).Invoke(null);

                            // Copy all properties
                            ObjectCopier.Copy(authorizeAttribute, subclassedAttribute);

                            if (!subclassedAttribute.IsAuthorized(controllerContext.HttpContext))
                            {
                                return false;
                            }
                        }
                        catch
                        {
                            // do not allow on exception
                            return false;
                        }
                    }
                }

                // No objection.
                return true;
            }
            finally
            {
                // Restore HttpContext
                httpContext.RewritePath(originalPath, true);

                // Release controller
                if (factoryBuiltController)
                    ControllerBuilder.Current.GetControllerFactory().ReleaseController(controllerContext.Controller);
            }
        }

        protected string filterProviderCacheKey = "__MVCSITEMAP_F255D59E-D3E4-4BA9-8A5F-2AF0CAB282F4";
        protected IFilterProvider ResolveFilterProvider()
        {
            if (HttpContext.Current != null)
            {
                if (!HttpContext.Current.Items.Contains(filterProviderCacheKey))
                {
                    HttpContext.Current.Items[filterProviderCacheKey] =
                        DependencyResolver.Current.GetService<IFilterProvider>();
                }
                return (IFilterProvider)HttpContext.Current.Items[filterProviderCacheKey];
            }
            return DependencyResolver.Current.GetService<IFilterProvider>();
        }
    }
}