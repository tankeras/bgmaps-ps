using ExifLib;
using Mapsps.Data;
using Mapsps.Data.Models;
using Mapsps.Web.ViewModels;
using Mapsps.Web.ViewModels.ImageViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mapsps.Services
{
    public class ImageService
    {
        private readonly IConfiguration config;
        private readonly ApplicationDbContext db;
        

        public ImageService(IConfiguration config,
            ApplicationDbContext db
            )
        {
            this.config = config;
            this.db = db;
        }

        public async Task AddImage(AddImageViewModel input, string userId)
        {
            var imageStream = input.Image.OpenReadStream();
            await this.IsThereCatInImage(imageStream);
            var coordinates = this.ExtractGeoData(imageStream);
            Image newImage = new Image()
            {
                CatId = input.CatId,
                Latitude = coordinates.latitude,
                Longitude = coordinates.longitude,
                UserId = userId,
                Extension = Path.GetExtension(input.Image.FileName),
            };
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
        public async Task IsThereCatInImage (Stream stream)
        {
            var key = config.GetValue<string>("ComputerVisionKey");
            var endpoint = config.GetValue<string>("ComputerVisionEndpoint");
            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
            { Endpoint = endpoint };
            List<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>()
                { VisualFeatureTypes.Tags };
            var imageAnalysis = await client.AnalyzeImageInStreamAsync(stream, features);
            var tags = imageAnalysis.Tags.ToList();
            tags.Remove(tags.Where(x => x.Name == "outdoor").FirstOrDefault());
            tags.Remove(tags.Where(x => x.Name == "indoor").FirstOrDefault());
            tags.Remove(tags.Where(x => x.Name == "text").FirstOrDefault());

            if (tags.Any(x => x.Name == "human face"))
            {
                throw new InvalidDataException("No cat can be that ugly");
            }
            if (!tags.Any(x => x.Name == "cat"))
            {
                throw new InvalidDataException($"You can upload this image to {tags.FirstOrDefault().Name}Maps");
            }
        }
        public async Task<string> GetCityFromGeoData(double latitude, double longitude)
        {
            var acceskey= this.config.GetValue<string>("PositionstackAccessKey");
            var client = new RestClient($"http://api.positionstack.com/v1/reverse?access_key={acceskey}&query={latitude},{longitude}&limit=1");
            var request = new RestRequest(Method.GET);
            request.AddHeader("limit", "1");
            request.AddHeader("fields", "results.locality,results.region");
            IRestResponse response = await client.ExecuteAsync(request);
            string responseCleaned = response.Content.Substring(9, response.Content.Length - 11);
            dynamic config = JsonConvert.DeserializeObject<JObject>(responseCleaned);
            if (config.locality == null)
            {
                if (config.region == "Sofiya")
                {
                    return "Sofia";
                }
                return (string)config.region;
            }
            return (string)config.locality;
        }
    }
}
