using Mapsps.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Mapsps.Data.Models;
using System.Security.Claims;
using Mapsps.Web.ViewModels.ImageViewModels;
using System.IO;

namespace Mapsps.Web.Controllers
{
    public class ImagesController : Controller
    {
        private readonly ImageService imageService;
        public ImagesController(ImageService imageService)
        {
            this.imageService = imageService;
        }
        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task Add([FromBody]IFormFile files)
        {
            try
            {
                await this.imageService.AddImage(new AddImageViewModel(), User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            catch (InvalidDataException ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message /*"Image does not contain location data"*/);
                
            }

            
        }
    }
}
