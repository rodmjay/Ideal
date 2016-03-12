using System.Configuration;

namespace Ideal
{
	public static class IdealConstants
	{
		public static string ClientOrigin
		{
			get { return ConfigurationManager.AppSettings["client:originUrl"]; }
		}
		public static string ClientCallbackUrl
		{
			get { return IdealConstants.ClientOrigin + "/stscallback"; }
		}

		public static string ClientId
		{
			get { return ConfigurationManager.AppSettings["sts:clientId"]; }
		}

		public static string ClientSecret
		{
			get { return ConfigurationManager.AppSettings["sts:clientSecret"]; }
		}

		public static string STSOrigin
		{
			get { return ConfigurationManager.AppSettings["sts:originUrl"]; }
		}

		public static string STSEndpoint
		{
			get { return STSOrigin + "/identity"; }
		}

		public static string STSTokenEndpoint
		{
			get { return STSEndpoint + "/connect/token"; }
		}

		public static string STSAuthorizationEndpoint
		{
			get { return STSEndpoint + "/connect/authorize"; }
		}

		public static string STSUserInfoEndpoint
		{
			get { return STSEndpoint + "/connect/userinfo"; }
		}

		public static string STSEndSessionEndpoint
		{
			get { return STSEndpoint + "/connect/endsession"; }
		}

		public static string STSRevokeTokenEndpoint
		{
			get { return STSEndpoint + "/connect/revokation"; }
		}

		public static string ApiOriginUrl
		{
			get { return ConfigurationManager.AppSettings["api:originUrl"]; }
		}
	}
}
