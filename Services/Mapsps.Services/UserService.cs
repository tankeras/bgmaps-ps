using Mapsps.Data;
using Mapsps.Data.Models;
using Mapsps.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapsps.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext db;

        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task IPettedThisPisi(string userId, int catId)
        {
            this.db.ConfirmedPets.Add(new ConfirmedPet
            {
                CatId = catId,
                UserId = userId
            });
            await this.db.SaveChangesAsync();
        }
        public int ConfirmedPetsCount(int catId)
        {
            return this.db.ConfirmedPets.Where(x => x.CatId == catId).Count();
        }
        public (int petted, int total) CatsProgressBar(string userId)
        {
            var region = this.db.Users.Where(x => x.Id == userId).FirstOrDefault().Region;
            int catsPetted = this.db.ConfirmedPets.Include("Cat").Where(x => x.UserId == userId).Where(x => x.Cat.Region == region).Count(); 
            int totalCats = this.db.Cats.Where(x => x.Region == region).Count();
            return (catsPetted, totalCats);
        }
        public async Task<List<AllCatsViewModel>> GetMyCats(string userId)
        {           
            var catsPetted = this.db.ConfirmedPets.Where(x => x.UserId == userId);
            var result = await this.db.Cats.Include("Nicknames").Include("ConfirmedPets").Include("Images").Include("ConfirmedPets")
                .Where(x=>x.ConfirmedPets.Any(x=>x.UserId== userId))
                .Select(x => new AllCatsViewModel
                {
                    ConfirmedPetsCount = x.ConfirmedPets.Count,
                    MostVotedNickname = x.MostVotedNickname,
                    ImagesId = new HashSet<string>(),
                    Id = x.Id,
                    Latitude = x.MostRecentLatitude,
                    Longitude = x.MostRecentLongitude,
                    Region = x.Region
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

        public int HowManyCities()
        {
            var allCities = new List<string>();
            foreach (var cat in db.Cats)
            {
                allCities.Add(cat.Region);
            }
            return allCities.Distinct().Count();
        }
        public int HowManyCats()
        {                    
            return this.db.Cats.Count();
        }

        public async Task SaveUserRegion(string region, string userId)
        {
            this.db.Users.Where(x => x.Id == userId).FirstOrDefault().Region = region;
            await this.db.SaveChangesAsync();
        }
        public string GetUserRegion(string userId)
        {
            if (string.IsNullOrEmpty(userId)) 
            {
                return "";
            }
            return this.db.Users.Where(x => x.Id == userId).FirstOrDefault().Region;
        }
      

    }
}
