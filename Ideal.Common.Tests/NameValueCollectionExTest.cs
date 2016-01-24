using System.Collections.Specialized;
using NUnit.Framework;

namespace Ideal.Common.Tests
{
    [TestFixture]
    public class NameValueCollectionExTest
    {
        [TestFixture]
        public class TheGetAndRemoveMethod
        {
            [Test]
            public void ShouldReturnTheCorrectValueAndRemoveIt()
            {
                NameValueCollection config = new NameValueCollection {{"key1", "value1"}};

                Assert.AreEqual(1, config.Count);
                string actual = config.GetAndRemove<string>("key1",true);
                Assert.AreEqual("value1", actual);
                Assert.AreEqual(0, config.Count);
            }
        }
    }
}
