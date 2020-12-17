using Mapsps.Data;
using Mapsps.Web.ViewModels.CatViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mapsps.Web.ViewComponents
{
    public class TopCatsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public TopCatsViewComponent(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IViewComponentResult Invoke()
        {
            var viewmodel = this.db.Cats.Include("Images").Include("Nicknames").Include("ConfirmedPets")
                .Select(x => new TopCatsViewModel
                {
                    Id = x.Id,
                    ConfirmedPetsCount = x.ConfirmedPets.Count(),
                    MostVotedNickname = x.MostVotedNickname,
                    ImageUrl = x.Images.FirstOrDefault().Id + x.Images.FirstOrDefault().Extension
                })
                .OrderByDescending(x => x.ConfirmedPetsCount)
                .ToList();
            return this.View(viewmodel);
        }
        
    }
}
