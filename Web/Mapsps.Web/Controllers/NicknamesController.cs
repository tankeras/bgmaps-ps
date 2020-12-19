using Mapsps.Services;
using Mapsps.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mapsps.Web.Controllers
{
    public class NicknamesController : Controller
    {
        private readonly NicknameService nicknameService;

        public NicknamesController(NicknameService nicknameService)
        {
            this.nicknameService = nicknameService;
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        [Authorize]
        public async Task Add([FromBody] AddNicknameViewModel model)

        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!await this.nicknameService.SuggestNickname(model.Name, model.catId, userId))
            {
                this.ModelState.AddModelError(String.Empty, "Username already exists");
                throw new Exception();
            }           
        }
        [Authorize]
        public async Task<ActionResult> Upvote(int Id, int catId)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.nicknameService.Upvote(userId, Id);
            return this.Redirect($"/Cats/Details/{catId}");
        }
    }
}
