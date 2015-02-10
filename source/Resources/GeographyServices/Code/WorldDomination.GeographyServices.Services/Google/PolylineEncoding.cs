using System.Collections.Generic;
using System.Text;
using WorldDomination.GeographyServices.Core;

namespace WorldDomination.GeographyServices.Services.Google
{
    public static class PolylineEncoding
    {
        private const int BinaryChunkSize = 5;
        private const int MinAscii = 63;

        public static string Encode(this ICollection<SpatialPoint> spatialPoints)
        {
            int plat = 0;
            int plng = 0;

            var encodedPoints = new StringBuilder();

            foreach (SpatialPoint spatialPoint in spatialPoints)
            {
                // Round to 5 decimal places and drop the decimal.
                var late5 = (int) (((double) spatialPoint.Latitude)*1e5);
                var lnge5 = (int) (((double) spatialPoint.Longitude)*1e5);

                // Encode the differences between the points.
                encodedPoints.Append(EncodeSignedNumber(late5 - plat));
                encodedPoints.Append(EncodeSignedNumber(lnge5 - plng));

                // Store the current point.
                plat = late5;
                plng = lnge5;
            }

            return encodedPoints.ToString();
        }

        private static string EncodeSignedNumber(int num)
        {
            int sgnNum = num << 1; //shift the binary value.

            //if negative invert.
            if (num < 0)
            {
                sgnNum = ~(sgnNum);
            }

            return (EncodeNumber(sgnNum));
        }

        private static string EncodeNumber(int number)
        {
            var encodeString = new StringBuilder();

            while (number >= 0x20)
            {
                // While another chunk follows.
                encodeString.Append((char) ((0x20 | (number & 0x1f)) + MinAscii));

                // OR value with 0x20, convert to decimal and add 63.
                number >>= BinaryChunkSize; //shift to next chunk
            }

            encodeString.Append((char) (number + MinAscii));

            return encodeString.ToString();
        }
    }
}