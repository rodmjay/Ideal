using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ideal
{
	public static class IdealConstants
	{
		public const string EdgeMVC = "http://localhost:49839";
		public const string EdgeMVCSTSCallback = EdgeMVC + "/stscallback";

		public const string EdgeClientSecret = "secret";
		public const string EdgeClientId = "edgeauthcode";

		public const string GPNSTSOrigin = "https://gpn-identity.azurewebsites.net";
		public const string GPNSTS = GPNSTSOrigin + "/identity";
		public const string GPNSTSTokenEndpoint = GPNSTS + "/connect/token";
		public const string GPNSTSAuthorizationEndpoint = GPNSTS + "/connect/authorize";
		public const string GPNSTSUserInfoEndpoint = GPNSTS + "/connect/userinfo";
		public const string GPNSTSEndSessionEndpoint = GPNSTS + "/connect/endsession";
		public const string GPNSTSRevokeTokenEndpoint = GPNSTS + "/connect/revokation";

		public const string GPNAPIOrigin = "https://gpn-api.azurewebsites.net";
	}
}
