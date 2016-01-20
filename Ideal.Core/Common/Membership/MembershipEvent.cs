using System.Globalization;
using System.Web.Management;
using Ideal.Core.Model;

namespace Ideal.Core.Common.Membership
{
    public class MembershipEvent : WebRequestEvent, IUserInfoEvent
    {
        private readonly User _user;
        private readonly string _culture;

        public MembershipEvent(MembershipEventCode code, User user)
            : base(code.GetDescription(), user, WebEventCodes.WebExtendedBase, (int)code)
        {
            _user = user;
            _culture = CultureInfo.CurrentCulture.Name;
        }

        public override void FormatCustomEventDetails(WebEventFormatter formatter)
        {
            base.FormatCustomEventDetails(formatter);

            formatter.AppendLine("User Name: " + _user.Username);
            formatter.AppendLine("Culture: " + _culture);
        }

        public string Tenant
        {
            get
            {
                if (_user == null)
                    return string.Empty;
                return _user.Tenant;
            }
        }

        public string Username
        {
            get
            {
                if (_user == null)
                    return string.Empty;
                return _user.Username;
            }
        }
    }
}
