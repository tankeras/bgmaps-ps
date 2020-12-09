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
        public CatsController(CatService catService, UserManager<ApplicationUser> userManager)
        {
            this.catService = catService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddCatViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }            
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            try
            {               
                if (await this.catService.CreateCatAsync(input, userId))
                {                   
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
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Image does not contain location data");
                return this.View("Add");
            }
            return this.RedirectToAction("All");
        }

        public IActionResult All()
        {
            return this.View(this.catService.GetAllCatsAsync());
        }

        public IActionResult Details(int id)
        {
            return this.View(this.catService.GetDetailsAsync(id));
        }
    }
}
