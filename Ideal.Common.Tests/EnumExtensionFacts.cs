using NUnit.Framework;

namespace Ideal.Common.Tests
{
    [TestFixture]
    public class EnumExtensionFacts
    {
        [TestFixture]
        public class TheGetDescriptionMethod
        {
            [Test]
            public void ShouldReturnDescriptionAttributeValue()
            {
                string actual = TestEnum.TestValue.GetDescription();
                Assert.AreEqual("Success",actual);
            }

        }
    }
}