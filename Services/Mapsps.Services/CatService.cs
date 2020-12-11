using ExifLib;
using Mapsps.Data;
using Mapsps.Data.Models;
using Mapsps.Web.ViewModels;
using Mapsps.Services.Mapping;
using Mapsps.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Mapsps.Services
{
    public class CatService
    {
        private readonly ApplicationDbContext db;
        private readonly BlobService blobService;
        private readonly ImageService imageService;

        public CatService(ApplicationDbContext db,
            BlobService blobService,
            ImageService imageService
          )
        {
            this.db = db;            
            this.blobService = blobService;
            this.imageService = imageService;
            
        }
        public async Task<bool> CreateCatAsync(AddCatViewModel input, string userId)
        {          
            await this.imageService.IsThereCatInImage(input.Image.OpenReadStream());                     
            var coordinates = this.imageService.ExtractGeoData(input.Image.OpenReadStream());                                   
            var longitude = coordinates.longitude;
            var latitude = coordinates.latitude;
            if (latitude == 1)
            {
                return false;
            }
            var cat = new Cat() 
            { 
                City = await this.GetCityFromGeoData(latitude, longitude) 
            };             
                
            cat.Nicknames.Add(new Nickname() { Name = input.Nickname });
            var image = new Image
            {
                Extension = Path.GetExtension(input.Image.FileName),
                UserId = userId,
                Cat = cat,
                CatId = cat.Id,
                Longitude = longitude,
                Latitude = latitude,
                
            };
            this.db.Images.Add(image);
            this.db.Cats.Add(cat);
            this.db.SaveChanges();
            await this.blobService.UploadBlob(input.Image.OpenReadStream(), image.Id, image.Extension);
            return true;
        }
        public async Task<ICollection<AllCatsViewModel>> GetAllCatsAsync()
        {            
            var result = await this.db.Cats.Include("Nicknames").Include("ConfirmedPets").Include("Images")
                .Select(x => new AllCatsViewModel
                {
                    ConfirmedPetsCount = x.ConfirmedPets.Count,
                    MostVotedNickname = x.MostVotedNickname,
                    ImagesId = new HashSet<string>(),
                    Id = x.Id,
                    Latitude = x.MostRecentLatitude,
                    Longitude = x.MostRecentLongitude
                })
                .ToListAsync();
            foreach (var cat in result)
            {
                foreach (var image in this.db.Cats.Include("Images").Include("Nicknames").Where(x => x.Id == cat.Id).FirstOrDefault().Images)
                {
                    cat.ImagesId.Add(image.Id + image.Extension);
                }
            }
            return result;
        }

        public async Task<DetailsViewModel> GetDetailsAsync(int id)
        {
            var cat = await this.db.Cats.Include("Nicknames").Include("Images").Include("ConfirmedPets").Where(x => x.Id == id)
                .Select(x => new DetailsViewModel
                {
                    ConfirmedPetsCount = x.ConfirmedPets.Count(),
                    MostVotedNickname = x.MostVotedNickname,
                    Images = new HashSet<ImageViewModel>(),
                    Nicknames = new HashSet<NicknameViewModel>(),
                    Id = x.Id,
                    City=x.City
                })
                .FirstOrDefaultAsync();

            foreach (var nickname in this.db.Cats.Include("Nicknames").Where(x => x.Id == cat.Id).FirstOrDefault().Nicknames)
            {
                cat.Nicknames.Add(new NicknameViewModel()
                {
                    Id=nickname.Id,
                    Name=nickname.Name,
                    Votes=nickname.Votes,
                });
            }

            foreach (var image in this.db.Cats.Include("Images").Where(x => x.Id == cat.Id).FirstOrDefault().Images)
            {
                cat.Images.Add(new ImageViewModel
                {
                    Id = image.Id,
                    Extension = image.Extension,
                    Latitude = image.Latitude,
                    Longitude = image.Longitude,
                });
            }
            return cat;
        }
        public async Task<string> GetCityFromGeoData(double latitude, double longitude)
        {
            var client = new RestClient($"https://geocodeapi.p.rapidapi.com/GetNearestCities?latitude={latitude}&longitude={longitude}&range=10000");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-key", "339665a255msh1b5ea06ce7ce333p1608b2jsn3091abb7c5a3");
            request.AddHeader("x-rapidapi-host", "geocodeapi.p.rapidapi.com");
            IRestResponse response = await client.ExecuteAsync(request);
            var responseCleaned = response.Content.Substring(5, response.Content.Length - 8);
            GeoDataViewModel geoData = JsonConvert.DeserializeObject<GeoDataViewModel>(responseCleaned);
            return geoData.City;
        }      
    }
}



