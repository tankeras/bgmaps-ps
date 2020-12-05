using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ExifLib;
using Mapsps.Data;
using Mapsps.Data.Models;
using Mapsps.Services.Mapping;
using Mapsps.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapsps.Services
{
    public class CatService
    {
        private readonly ApplicationDbContext db;
        private readonly IConfiguration config;
        private readonly BlobService blobService;
        private readonly ImageService imageService;

        public CatService(ApplicationDbContext db,
            IConfiguration config,
            BlobService blobService,
            ImageService imageService)
        {
            this.db = db;
            this.config = config;
            this.blobService = blobService;
            this.imageService = imageService;
        }
        public async Task<bool> CreateCatAsync(AddCatViewModel input, string userId)
        {
            var coordinates = this.imageService.ExtractGeoData(input.Image.OpenReadStream());
            var longitude = coordinates.longitude;
            var latitude = coordinates.latitude;
            if (latitude == 1)
            {
                return false;
            }
            var cat = new Cat();
            cat.Nicknames.Add(new Nickname() { Name = input.Nickname });
            var image = new Image
            {
                Extension = Path.GetExtension(input.Image.FileName),
                UserId = userId,
                Cat = cat,
                CatId = cat.Id,
                Longitude = longitude,
                Latitude = latitude
            };
            this.db.Images.Add(image);
            this.db.Cats.Add(cat);
            this.db.SaveChanges();
            await this.blobService.UploadBlob(input.Image.OpenReadStream(), image.Id, image.Extension);
            return true;
        }
        public ICollection<AllCatsViewModel> GetAllCatsAsync() 
        {
            var viewModels = new List<AllCatsViewModel>();
            var result = this.db.Cats.Include("Nicknames")
                .Select(x => new AllCatsViewModel
                {
                    ConfirmedPetsCount = x.ConfirmedPetsCount,
                    MostVotedNickname = x.MostVotedNickname,
                    ImagesId = new HashSet<string>(),
                    Id=x.Id
                })
                .ToList();
            foreach (var cat in result)
            {
                foreach (var image in this.db.Cats.Include("Images").Include("Nicknames").Where(x => x.Id == cat.Id).FirstOrDefault().Images)
                {
                    cat.ImagesId.Add(image.Id + image.Extension);
                }
            }
            return result;

            //var count = this.db.Cats.Count();
            //var projection = this.db.Cats.Include("Images").Include("Nicknames").To<AllCatsViewModel>();             

           

        }
        

    }
}



