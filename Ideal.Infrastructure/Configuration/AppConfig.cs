using System.Configuration;

namespace Ideal.Infrastructure.Configuration
{
    public class AppConfig : ConfigurationSection
    {
        private static AppConfig _section;
        public static AppConfig Instance => (_section ?? (_section = (AppConfig)ConfigurationManager.GetSection("Ideal")));

        [ConfigurationProperty("site", IsRequired = true)]
        public ConfigSiteSettings Site => (ConfigSiteSettings)base["site"];
    }
}
