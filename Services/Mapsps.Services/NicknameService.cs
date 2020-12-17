using Mapsps.Data;
using Mapsps.Data.Models;
using Mapsps.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapsps.Services
{
    public class NicknameService
    {
        private readonly ApplicationDbContext db;

        public NicknameService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<bool> SuggestNickname(string Name, int catId) 
        {
            if (this.db.Nicknames.Any(x => x.Name == Name)) {
                return false;
            }
            Nickname nickname = new Nickname()
            {
                Name = Name,
                CatId = catId
            };
            db.Nicknames.Add(nickname);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
