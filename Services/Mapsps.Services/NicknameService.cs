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
    public class NicknameService
    {
        private readonly ApplicationDbContext db;

        public NicknameService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<bool> SuggestNickname(string Name, int catId, string userId) 
        {
            if (this.db.Nicknames.Any(x => x.Name == Name)) {
                return false;
            }
            Nickname nickname = new Nickname()
            {
                Name = Name,
                CatId = catId,              
            };
            nickname.Upvotes.Add(new Upvote() { UserId = userId, NicknameId = nickname.Id });
            db.Nicknames.Add(nickname);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task Upvote (string userId, int nicknameId)
        {
            if (this.db.Upvotes.Any(x => x.NicknameId + x.UserId == nicknameId + userId))
            {
                var upvote = this.db.Upvotes.Where(x => x.NicknameId + x.UserId == nicknameId + userId).FirstOrDefault();
                this.db.Nicknames.Where(x => x.Id == nicknameId).FirstOrDefault().Votes --;
                this.db.Remove(upvote);
                await this.db.SaveChangesAsync();
            }
            else
            {
                Upvote upvote = new Upvote() { NicknameId = nicknameId, UserId = userId };
                var count = this.db.Nicknames.Include(x=>x.Upvotes).Where(x => x.Id == nicknameId).FirstOrDefault().Upvotes.Count();
                this.db.Nicknames.Where(x => x.Id == nicknameId).FirstOrDefault().Votes++;
                await this.db.Upvotes.AddAsync(new Upvote() { NicknameId = nicknameId, UserId = userId });
                await this.db.SaveChangesAsync();
            }
            
        }
    }
}
