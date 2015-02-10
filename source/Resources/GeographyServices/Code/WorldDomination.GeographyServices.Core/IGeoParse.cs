namespace WorldDomination.GeographyServices.Core
{
    public interface IGeoParse
    {
        SpatialGeographySet Parse(string wellKnownText);
        SpatialGeographySet Parse(byte[] wellKnownBinary);
        byte[] Parse(decimal latitude, decimal longitude);
    }
}