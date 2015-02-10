using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldDomination.GeographyServices.Core
{
    public static class Extensions
    {
        public static string ToJson(this IDictionary<string, string> value)
        {
            if (value == null)
            {
                return null;
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("{");
            foreach (string key in value.Keys)
            {
                bool isNumber = false;
                double temp;
                isNumber = double.TryParse(value[key], out temp);
                stringBuilder.AppendFormat("{0}\"{1}\": {2}{3}{4}", stringBuilder.Length > 1 ? "," : string.Empty, key,
                                           !isNumber ? "\"" : string.Empty, value[key], !isNumber ? "\"" : string.Empty);
            }
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        public static string ToGeoJson(this ICollection<Feature> value)
        {
            if (value == null || value.Count <= 0)
            {
                throw new ArgumentNullException("value");
            }

            var features = new StringBuilder();
            foreach (Feature feature in value.Where(feature => feature.SpatialGeographySet != null))
            {
                features.AppendFormat("{0}{1}", features.Length > 0 ? "," : string.Empty, feature.ToGeoJson());
            }

            var featureCollection = new StringBuilder();
            return
                featureCollection.AppendFormat("{{\"type\":\"FeatureCollection\",\"features\":[{0}]}}", features).
                    ToString();
        }

        public static string ToSpatialGeographyString(this ICollection<Feature> value)
        {
            if (value == null || value.Count <= 0)
            {
                throw new ArgumentNullException("value");
            }

            var features = new StringBuilder();
            foreach (Feature feature in value.Where(feature => feature.SpatialGeographySet != null))
            {
                features.AppendFormat("{0}{1}", features.Length > 0 ? "," : string.Empty,
                                      feature.GetSpatialGeographyString());
            }

            return string.Format("[{0}]", features);
        }
    }
}