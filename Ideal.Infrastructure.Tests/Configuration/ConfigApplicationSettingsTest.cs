using System.Configuration;
using Ideal.Infrastructure.Configuration;
using NUnit.Framework;

namespace Ideal.Infrastructure.Tests.Configuration
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
