using ExifLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mapsps.Services
{
    public class ImageService
    {
        public (double longitude, double latitude) ExtractGeoData(Stream stream)
        {
            using (ExifReader reader = new ExifReader(stream))
            {
                Double[] GpsLongArray;
                Double[] GpsLatArray;
                Double GpsLongDouble;
                Double GpsLatDouble;

                if (reader.GetTagValue<Double[]>(ExifTags.GPSLongitude, out GpsLongArray)
                    && reader.GetTagValue<Double[]>(ExifTags.GPSLatitude, out GpsLatArray))
                {
                    GpsLongDouble = GpsLongArray[0] + GpsLongArray[1] / 60 + GpsLongArray[2] / 3600;
                    GpsLatDouble = GpsLatArray[0] + GpsLatArray[1] / 60 + GpsLatArray[2] / 3600;
                    return (GpsLongDouble, GpsLatDouble);
                }
                else
                {
                    return (1, 1);
                }
            }
        }
    }
}
