using System.Collections.Generic;
using System.Text;

namespace WorldDomination.GeographyServices.Core
{
    public class SpatialGeography
    {
        public ICollection<SpatialPoint> SpatialPoints { get; set; }
        public SpatialPoint Centroid { get; set; }
        public int NumberOfPoints { get; set; }

        public override string ToString()
        {
            return string.Format("Centroid=> Lat: {0}  Long: {1} | Count: {2}",
                                 Centroid != null ? Centroid.Latitude.ToString() : "-",
                                 Centroid != null ? Centroid.Longitude.ToString() : "-",
                                 SpatialPoints != null ? SpatialPoints.Count.ToString() : "-");
        }

        public string ToGeoJson()
        {
            var stringBuilder = new StringBuilder();
            var spatialPoints = new StringBuilder();

            foreach (SpatialPoint spatialpoint in SpatialPoints)
            {
                spatialPoints.AppendFormat("{0}{1}",
                                           spatialPoints.Length > 0 ? "," : string.Empty,
                                           spatialpoint.ToGeoJson());
            }

            return stringBuilder.AppendFormat("[{0}]", spatialPoints).ToString();
        }
    }
}