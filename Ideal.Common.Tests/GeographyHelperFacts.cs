using NUnit.Framework;

namespace Ideal.Common.Tests
{
    [TestFixture]
    public class GeographyHelperFacts
    {
        [TestFixture]
        public class TheCreatePointMethod
        {
            [TestCase(0.0d,0.0d)]
            public void CreatesAKnownPoint(double latitude, double longitude)
            {
                //DbGeography point = GeographyHelpers.CreatePoint(latitude, longitude);

                //Assert.IsNotNull(point);
                //Assert.IsTrue(point.Latitude == 0);
                //Assert.IsTrue(point.Longitude.Value == 0);
            }
        }
    }
}
