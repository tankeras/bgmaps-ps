using Mapsps.Data.Models;
using Mapsps.Services;
using Mapsps.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mapsps.Web.Controllers
{
    public class CatsController : BaseController
    {
        private readonly CatService catService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;
        private readonly ImageService imageService;

        public CatsController(CatService catService,
            UserManager<ApplicationUser> userManager,
            ImageService imageService)
        {
            this.catService = catService;
            this.userManager = userManager;
            this.imageService = imageService;
        }

        [Authorize]
        public async Task<IActionResult> Add()
        {
            var ilina = await this.catService.GetNearbyCats(42.404555555555554, 25.57955);
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddCatViewModel input)
        {
            //var coordinates = this.imageService.ExtractGeoData(input.Image.OpenReadStream());
            //var nearbyCats = await this.catService.GetNearbyCats(coordinates.latitude, coordinates.longitude);
            if (!ModelState.IsValid)
            {
                return this.View();
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                if (await this.catService.CreateCatAsync(input, userId))
                {
                    //if (nearbyCats.Count > 0)
                    //{
                    //    return this.View("Nearby", nearbyCats);
                    //}
                    return this.RedirectToAction("All");
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Image does not contain location data");
                    return this.View("Add");
                }
            }           
            catch (InvalidDataException ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View("Add");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, "Image does not contain location data");
                return this.View("Add");
            }

        }

        public async Task<IActionResult> All(string sortOrder)
        {
            return this.View(await this.catService.GetAllCatsAsync(sortOrder));
        }

        public async Task<IActionResult> Details(int id)
        {           
            return this.View(await this.catService.GetDetailsAsync(id));
        }
     
        
    }
}
