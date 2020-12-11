namespace Mapsps.Web.Controllers
{
    using System.Text.Json;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Mapsps.Data.Models;
    using Mapsps.Services;
    using Mapsps.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Text.Encodings.Web;

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

        

        public async Task<IActionResult> Map()
        {                       
            var jsonString = JsonSerializer.Serialize(await this.catService.GetAllCatsAsync());
            return this.View("Map", jsonString);
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
