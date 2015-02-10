using System;
using Microsoft.SqlServer.Types;

namespace WorldDomination.GeographyServices.Services.Microsoft
{
    public static class SqlGeographyExtensions
    {
        public static OpenGisGeographyType ToOpenGisGeographyType(this SqlGeography value)
        {
            OpenGisGeographyType openGisGeographyType;

            if (value == null)
            {
                throw new NullReferenceException("value");
            }

            switch (value.STGeometryType().ToString())
            {
                case "Point":
                    openGisGeographyType = OpenGisGeographyType.Point;
                    break;
                case "MultiPoint":
                    openGisGeographyType = OpenGisGeographyType.MultiPoint;
                    break;
                case "LineString":
                    openGisGeographyType = OpenGisGeographyType.LineString;
                    break;
                case "MultiLineString":
                    openGisGeographyType = OpenGisGeographyType.MultiLineString;
                    break;
                case "Polygon":
                    openGisGeographyType = OpenGisGeographyType.Polygon;
                    break;
                case "MultiPolygon":
                    openGisGeographyType = OpenGisGeographyType.MultiPolygon;
                    break;
                case "GeometryCollection":
                    openGisGeographyType = OpenGisGeographyType.GeometryCollection;
                    break;
                default:
                    throw new InvalidOperationException("Unhandled STGeometryType found: " +
                                                        value.STGeometryType().ToString());
            }

            return openGisGeographyType;
        }
    }
}