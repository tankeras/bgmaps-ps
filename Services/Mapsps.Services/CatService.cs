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
using Newtonsoft.Json.Linq;

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
                Region = await this.imageService.GetCityFromGeoData(latitude, longitude)
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
        public async Task<ICollection<AllCatsViewModel>> GetAllCatsAsync(string sortOrder)
        {
            var result = await this.db.Cats.Include("Nicknames").Include("ConfirmedPets").Include("Images")
                .Select(x => new AllCatsViewModel
                {
                    ConfirmedPetsCount = x.ConfirmedPets.Count,
                    MostVotedNickname = x.MostVotedNickname,
                    ImagesId = new HashSet<string>(),
                    Id = x.Id,
                    Latitude = x.MostRecentLatitude,
                    Longitude = x.MostRecentLongitude,
                    City = x.Region
                })
                .ToListAsync();
            foreach (var cat in result)
            {
                foreach (var image in this.db.Cats.Include("Images").Include("Nicknames").Where(x => x.Id == cat.Id).FirstOrDefault().Images)
                {
                    cat.ImagesId.Add(image.Id + image.Extension);
                }
            }
            switch (sortOrder)
            {
                case "Top":
                    return result.OrderBy(x => x.ConfirmedPetsCount).ToList();
                    break;
                case "Nearby":
                    return result.OrderByDescending(x => x.ConfirmedPetsCount).ToList();
                    break;
                default:
                    return result.OrderBy(x => x.ConfirmedPetsCount).ToList();
            }


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
                    City = x.Region
                })
                .FirstOrDefaultAsync();

            foreach (var nickname in this.db.Cats.Include("Nicknames").Where(x => x.Id == cat.Id).FirstOrDefault().Nicknames)
            {
                cat.Nicknames.Add(new NicknameViewModel()
                {
                    Id = nickname.Id,
                    Name = nickname.Name,
                    Votes = nickname.Votes,
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

        public async Task<List<AllCatsViewModel>> GetNearbyCats(double latitude, double longitude)
        {
            var nearbyCatIdsLatitude = new List<int>();
            var nearbyCatIdsLongitude = new List<int>();
            await this.db.Images.Where(x => Math.Abs(x.Latitude - latitude) <= 0.003).ForEachAsync(x => nearbyCatIdsLatitude.Add(x.CatId));
            nearbyCatIdsLatitude.Count();
            await this.db.Images.Where(x => Math.Abs(x.Longitude - longitude) <= 0.003).ForEachAsync(x => nearbyCatIdsLongitude.Add(x.CatId));
            var intersectedList = nearbyCatIdsLatitude.Intersect(nearbyCatIdsLongitude).ToList();
            var cats = await this.db.Cats
                .Where(x => intersectedList
                .Contains(x.Id))
                .Select(x => new AllCatsViewModel()
                {
                    Id = x.Id,
                    Latitude = x.MostRecentLatitude,
                    Longitude = x.MostRecentLongitude,
                    ImagesId = new HashSet<string>(),
                })
                .ToListAsync();
            foreach (var cat in cats)
            {
                foreach (var image in this.db.Cats.Include("Images").Include("Nicknames").Where(x => x.Id == cat.Id).FirstOrDefault().Images)
                {
                    cat.ImagesId.Add(image.Id + image.Extension);
                }
            }
            return cats;
        }
    }
}




