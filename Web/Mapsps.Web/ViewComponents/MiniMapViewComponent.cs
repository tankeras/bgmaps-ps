using Mapsps.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mapsps.Web.ViewComponents
{
    public class MiniMapViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {

            return this.View();
        }
    }
}
