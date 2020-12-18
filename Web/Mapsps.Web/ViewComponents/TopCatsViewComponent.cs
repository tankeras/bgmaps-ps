using Mapsps.Data;
using Mapsps.Services;
using Mapsps.Web.ViewModels.CatViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Mapsps.Web.ViewComponents
{
    public class TopCatsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;
        private readonly UserService userService;

        public TopCatsViewComponent(ApplicationDbContext db,
            UserService userService)
        {
            this.db = db;
            this.userService = userService;
        }
        public IViewComponentResult Invoke()
        {
          
            string userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            this.ViewData["Region"] = this.userService.GetUserRegion(userId);
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
