using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.Types;

namespace WorldDomination.GeographyServices.Core
{
    public class SpatialGeographySet
    {
        public ICollection<SpatialGeography> SpatialGeographys { get; set; }
        public OpenGisGeographyType OpenGisGeographyType { get; set; }

        public decimal? Latitude
        {
            get
            {
                return OpenGisGeographyType == OpenGisGeographyType.Point && SpatialGeographys != null &&
                       SpatialGeographys.Count == 1
                           ? SpatialGeographys.First().SpatialPoints.First().Latitude
                           : 0;
            }
        }

        public decimal? Longitude
        {
            get
            {
                return OpenGisGeographyType == OpenGisGeographyType.Point && SpatialGeographys != null &&
                       SpatialGeographys.Count == 1
                           ? SpatialGeographys.First().SpatialPoints.First().Longitude
                           : 0;
            }
        }

        public int TotalNumberOfPoints
        {
            get
            {
                return SpatialGeographys == null || SpatialGeographys.Count <= 0
                           ? 0
                           : (from q in SpatialGeographys select q.NumberOfPoints).Sum();
            }
        }

        public override string ToString()
        {
            return string.Format("{0} - SpatialGeography Count: {1}; Total Point Count: {2}.",
                                 OpenGisGeographyType,
                                 SpatialGeographys == null || SpatialGeographys.Count <= 0 ? 0 : SpatialGeographys.Count,
                                 TotalNumberOfPoints);
        }
    }
}