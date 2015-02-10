namespace WorldDomination.GeographyServices.Core
{
    public class SpatialPoint
    {
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }

        public override string ToString()
        {
            return string.Format("Lat: {0}  Long: {1}", Latitude, Longitude);
        }

        public string ToGeoJson()
        {
            return string.Format("[{0},{1}]", Longitude, Latitude);
        }
    }
}