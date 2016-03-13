using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Ideal.API
{
	public static class WebApiConfig
	{
		public static HttpConfiguration Register()
		{
			// Web API configuration and services
			HttpConfiguration config = new HttpConfiguration();

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			return config;
		}
	}
}
