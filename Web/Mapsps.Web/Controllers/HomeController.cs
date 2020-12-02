namespace Mapsps.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Claims;
    using Mapsps.Data.Models;
    using Mapsps.Services;
    using Mapsps.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;


    public class HomeController : BaseController
    {
        private readonly CatService catService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;

        public HomeController(CatService catService, UserManager<ApplicationUser> userManager )
        {
            this.catService = catService;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddCatViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }            
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            try
            {               
                if (this.catService.CreateCatAsync(input, userId))
                {
                    return this.RedirectToAction("All");
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Image does not contain location data");
                    return this.View("Add");
                }
            }
            catch (System.Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Image does not contain location data");
                return this.View("Add");
            }

            
            return this.RedirectToAction("All");
        }

        public IActionResult All()
        {
            return this.View();
        }

        public IActionResult Map(string filename)
        {
            this.ViewData["Filename"] = filename;
            return this.View();
        }
        public IActionResult Privacy()
        {
            return this.View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
