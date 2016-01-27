using System.Configuration;

namespace Ideal.Infrastructure.Configuration
{
    public class AppConfig : ConfigurationSection
    {
        [ConfigurationProperty("site", IsRequired = true)]
        public ConfigSiteSettings Site => (ConfigSiteSettings)base["site"];

        [ConfigurationProperty("membership", IsRequired = false)]
        public ConfigMembershipSettings Membership => (ConfigMembershipSettings)base["membership"];
    }
}
