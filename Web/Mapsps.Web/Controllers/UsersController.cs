using Mapsps.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mapsps.Web.Controllers
{
  
    public class UsersController : Controller
    {
        private readonly UserService userService;

        public UsersController(UserService userService)
        {
            this.userService = userService;
        }
             
        [IgnoreAntiforgeryToken]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ConfirmPet([FromBody]int catId)
        {

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value; 
            await this.userService.IPettedThisPisi(userId, catId);
            return this.Json(this.userService.ConfirmedPetsCount(catId));
        }

    }
}
