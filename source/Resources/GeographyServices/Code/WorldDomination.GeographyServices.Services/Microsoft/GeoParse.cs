using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using CuttingEdge.Conditions;
using Microsoft.SqlServer.Types;
using WorldDomination.GeographyServices.Core;

namespace WorldDomination.GeographyServices.Services.Microsoft
{
    public class GeoParse : IGeoParse
    {
        private static SpatialGeographySet ExtractData(SqlGeography sqlGeography)
        {
            if (sqlGeography == null)
            {
                throw new ArgumentNullException("sqlGeography");
            }

            SqlInt32 geometryCount = sqlGeography.STNumGeometries();
            if (geometryCount <= 0)
            {
                return null;
            }

            var spatialGeographyCollection = new SpatialGeographySet
                                                 {
                                                     OpenGisGeographyType =
                                                         sqlGeography.ToOpenGisGeographyType()
                                                 };

            // Iterate through each 'Geometry' in the geograpahy collection.
            for (int i = 1; i <= geometryCount; i++)
            {
                SqlGeography tempGeography = sqlGeography.STGeometryN(i);
                var spatialGeography = new SpatialGeography
                                           {
                                               NumberOfPoints = tempGeography.STNumPoints().Value,
                                               Centroid = new SpatialPoint
                                                              {
                                                                  Latitude =
                                                                      (decimal) (float)
                                                                                tempGeography.
                                                                                    EnvelopeCenter().Lat,
                                                                  Longitude =
                                                                      (decimal) (float)
                                                                                tempGeography.
                                                                                    EnvelopeCenter().Long
                                                              }
                                           };

                // Now iterate through each point, for each geometry.
                for (int j = 1; j <= spatialGeography.NumberOfPoints; j++)
                {
                    SqlGeography tempPointGeography = tempGeography.STPointN(j);

                    // Add this point to the current collection.
                    if (spatialGeography.SpatialPoints == null)
                    {
                        spatialGeography.SpatialPoints = new List<SpatialPoint>();
                    }

                    spatialGeography.SpatialPoints.Add(new SpatialPoint
                                                           {
                                                               Longitude = (decimal) (float) tempPointGeography.Long,
                                                               Latitude = (decimal) (float) tempPointGeography.Lat,
                                                           });
                }

                // Add this list of points to the current collection of spatial geographies.
                if (spatialGeographyCollection.SpatialGeographys == null)
                {
                    spatialGeographyCollection.SpatialGeographys = new List<SpatialGeography>();
                }

                spatialGeographyCollection.SpatialGeographys.Add(spatialGeography);
            }

            return spatialGeographyCollection.SpatialGeographys == null ||
                   spatialGeographyCollection.SpatialGeographys.Count <= 0
                       ? null
                       : spatialGeographyCollection;
        }

        #region Implementation of IGeoParse

        public SpatialGeographySet Parse(string wellKnownText)
        {
            // Load the binary data.
            if (string.IsNullOrEmpty(wellKnownText))
            {
                throw new ArgumentNullException("wellKnownText");
            }

            // Now, lets load up this wellKnownText.
            // Note: Parse can also do the same thing... http://msdn.microsoft.com/en-us/library/bb933824.aspx
            SqlGeography sqlGeography = SqlGeography.STGeomFromText(new SqlChars(wellKnownText.ToCharArray()),
                                                                    (int) SpatialReferenceSystemIdentifierType.WGS84);

            return sqlGeography == null ? null : ExtractData(sqlGeography);
        }

        public SpatialGeographySet Parse(byte[] wellKnownBinary)
        {
            if (wellKnownBinary == null ||
                wellKnownBinary.Length <= 0)
            {
                throw new ArgumentNullException("wellKnownBinary");
            }

            // Now, lets load up this wellKnownText.
            SqlGeography sqlGeography = SqlGeography.STGeomFromWKB(new SqlBytes(wellKnownBinary),
                                                                   (int) SpatialReferenceSystemIdentifierType.WGS84);

            return sqlGeography == null ? null : ExtractData(sqlGeography);
        }

        public byte[] Parse(decimal latitude, decimal longitude)
        {
            Condition.Requires(latitude).IsInRange(-90, 90);
            Condition.Requires(longitude).IsInRange(-180, 180);

            // Convert the Latitude/Longitude into 'Well Known Text'.
            // NOTE: Format is always LONGITUDE then LATITUDE.
            string wellKnownText = string.Format("POINT ({0} {1})", longitude, latitude);

            // Next, load this into a Microsoft SqlServer Geography object.
            SqlGeography sqlGeography = SqlGeography.STGeomFromText(new SqlChars(wellKnownText.ToCharArray()),
                                                                    (int) SpatialReferenceSystemIdentifierType.WGS84);

            // Now convert this object to a byte array :)
            return sqlGeography.STAsBinary().Buffer;
        }

        #endregion
    }
}