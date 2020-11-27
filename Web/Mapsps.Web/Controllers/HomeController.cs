namespace Mapsps.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Mapsps.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddCatViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            return this.RedirectToAction("Map", new { filename = input.Image.FileName.ToString() });
        }

        public IActionResult Map(string filename)
        {
            this.ViewData["Filename"] = filename;
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
