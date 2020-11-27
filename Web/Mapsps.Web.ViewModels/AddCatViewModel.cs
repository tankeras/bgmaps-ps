using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Mapsps.Web.ViewModels
{
    public class AddCatViewModel 
    {
        [IsImageValid]
        public IFormFile Image { get; set; }

        
    }
}
