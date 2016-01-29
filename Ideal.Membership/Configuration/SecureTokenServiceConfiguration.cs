using System.Configuration;
using Ideal.Core.Interfaces.Settings;

namespace Ideal.Membership.Configuration
{
    public class SecureTokenServiceConfiguration : ConfigurationSection, ISecureTokenServiceSettings
    {
        [ConfigurationProperty("issuerUri", IsRequired = true)]
        public string IssuerUri
        {
            get { return (string) base["issuerUri"]; }
            set { base["issuerUri"] = value; }
        }

        [ConfigurationProperty("rootUri", IsRequired = true)]
        public string RootUri
        {
            get {
                return (string)base["rootUri"];
            }
            set {
                base["rootUri"] = value;
            }
        }

        [ConfigurationProperty("identityUri", DefaultValue = "/identity")]
        public string IdenityUri
        {
            get {
                return RootUri + (string)base["identityUri"];
            }
            set {
                base["identityUri"] = value;
            }
        }

        [ConfigurationProperty("tokenUri", DefaultValue = "/connect/token")]
        public string TokenUri
        {
            get
            {
                return RootUri + (string)base["tokenUri"];
            }
            set
            {
                base["tokenUri"] = value;
            }
        }

        [ConfigurationProperty("authUri", DefaultValue = "/connect/authorize")]
        public string AuthUri
        {
            get
            {
                return RootUri + (string)base["authUri"];
            }
            set
            {
                base["authUri"] = value;
            }
        }

        [ConfigurationProperty("userInfoUri", DefaultValue = "/connect/userinfo")]
        public string UserInfoUri
        {
            get
            {
                return RootUri + (string)base["userInfoUri"];
            }
            set
            {
                base["userInfoUri"] = value;
            }
        }

        [ConfigurationProperty("endSessionUri", DefaultValue = "/connect/endsession")]
        public string EndSessionUri
        {
            get
            {
                return RootUri + (string)base["endSessionUri"];
            }
            set
            {
                base["endSessionUri"] = value;
            }
        }

        [ConfigurationProperty("revokeUri", DefaultValue = "/connect/revocation")]
        public string RevokeUri
        {
            get
            {
                return RootUri + (string)base["revokeUri"];
            }
            set
            {
                base["revokeUri"] = value;
            }
        }
    }
}