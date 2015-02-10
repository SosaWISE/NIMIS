using System.IO;
using NUnit.Framework;
using WorldDomination.GeographyServices.Core;
using WorldDomination.GeographyServices.Services.Microsoft;

namespace WorldDomination.GeographyServices.Tests
{
    public class GeographyTests
    {
        [Test]
        // ReSharper disable InconsistentNaming
        public void GivenSomeBinaryData_GeoParse_ReturnsAValidSpatialGeographySet()
        // ReSharper restore InconsistentNaming
        {
            // Arrange.
            byte[] boundary = File.ReadAllBytes("LA Boundary - Reduced to a Medium Zoom Level.bin");
            IGeoParse geoParse = new GeoParse();

            // Act.
            SpatialGeographySet result = geoParse.Parse(boundary);

            // Assert.
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.SpatialGeographys);
            
            // Broken :( Currently, returns 2 objects, not 1.
            Assert.AreEqual(1, result.SpatialGeographys);
        }

        [Test]
        // ReSharper disable InconsistentNaming
        public void GivenSomeWellKnownTextData_GeoParse_ReturnsAValidSpatialGeographySet()
        // ReSharper restore InconsistentNaming
        {
            // Arrange.
            string boundary = File.ReadAllText("LA Boundary - Reduced to a Medium Zoom Level.txt");
            IGeoParse geoParse = new GeoParse();

            // Act.
            SpatialGeographySet result = geoParse.Parse(boundary);

            // Assert.
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.SpatialGeographys);

            // Broken :( Currently, returns 2 objects, not 1.
            Assert.AreEqual(1, result.SpatialGeographys);
        }
    }
}