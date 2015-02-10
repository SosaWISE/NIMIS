using System.Collections.Generic;
using System.Text;

namespace WorldDomination.GeographyServices.Core
{
    public class Feature
    {
        public SpatialGeographySet SpatialGeographySet { get; set; }
        public IDictionary<string, string> Properties { get; set; }

        public override string ToString()
        {
            return string.Format("{0}; Properties Count: {1}.",
                                 SpatialGeographySet == null
                                     ? "SpatialGeographySet is null"
                                     : SpatialGeographySet.ToString(),
                                 Properties == null ? "-null-" : Properties.Count.ToString());
        }

        public string ToGeoJson()
        {
            if (SpatialGeographySet == null || SpatialGeographySet.SpatialGeographys == null)
            {
                return null;
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("{{\"type\":\"{0}\",\"coordinates\":[{1}]}}",
                                       SpatialGeographySet.OpenGisGeographyType,
                                       GetSpatialGeographyString());

            // Do we have any properties? if so, this becomes a 'Feature'.
            if (Properties != null && Properties.Count > 0)
            {
                return string.Format("{{\"type\":\"Feature\",\"geometry\":{0},\"properties\":{1}}}", stringBuilder,
                                     Properties.ToJson());
            }

            return stringBuilder.ToString();
        }

        public string GetSpatialGeographyString()
        {
            if (SpatialGeographySet == null || SpatialGeographySet.SpatialGeographys == null)
            {
                return null;
            }

            var spatialGeographies = new StringBuilder();

            foreach (SpatialGeography spatialGeography in SpatialGeographySet.SpatialGeographys)
            {
                spatialGeographies.AppendFormat("{0}{1}",
                                                spatialGeographies.Length > 0 ? "," : string.Empty,
                                                spatialGeography.ToGeoJson());
            }

            return spatialGeographies.ToString();
        }
    }
}