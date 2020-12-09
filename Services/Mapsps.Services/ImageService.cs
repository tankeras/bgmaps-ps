using ExifLib;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mapsps.Services
{
    public class ImageService
    {
        private readonly IConfiguration config;

        public ImageService(IConfiguration config)
        {
            this.config = config;
        }
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
        public async Task<ImageAnalysis> IsThereCatInImage (Stream stream)
        {
            var key = config.GetValue<string>("ComputerVisionKey");
            var endpoint = config.GetValue<string>("ComputerVisionEndpoint");

            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
            { Endpoint = endpoint };
            List<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>()
                { VisualFeatureTypes.Tags };     
 
            return await client.AnalyzeImageInStreamAsync(stream, features);
        }
    }
}
