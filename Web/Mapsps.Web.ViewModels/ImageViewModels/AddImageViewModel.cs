using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mapsps.Web.ViewModels.ImageViewModels
{
    public class AddImageViewModel

    {
        public IFormFile Image { get; set; }

        public int CatId { get; set; }


    }
}
