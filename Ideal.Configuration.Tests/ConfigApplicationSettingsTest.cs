using System.Configuration;
using NUnit.Framework;

namespace Ideal.Configuration.Tests
{
    [TestFixture]
    public class ConfigApplicationSettingsTest
    {
        [Test]
        public void ShouldLoadFromAppConfig()
        {
            ConfigApplicationSettings actual = (ConfigApplicationSettings) ConfigurationManager.GetSection("Ideal");
            Assert.IsNotNull(actual);
        }
    }
}
