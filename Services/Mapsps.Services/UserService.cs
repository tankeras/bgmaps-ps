using Mapsps.Data;
using Mapsps.Data.Models;
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
        public int HowManyCities()
        {
            var allCities = new List<string>();
            foreach (var cat in db.Cats)
            {
                allCities.Add(cat.City);
            }
            return allCities.Distinct().Count();
        }
        public int HowManyCats()
        {                    
            return this.db.Cats.Count();
        }

    }
}
