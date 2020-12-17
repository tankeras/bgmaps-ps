using Mapsps.Services;
using Mapsps.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task Add([FromBody] AddNicknameViewModel model)

        {
            if (!await this.nicknameService.SuggestNickname(model.Name, model.catId))
            {
                this.ModelState.AddModelError(String.Empty, "Username already exists");
                throw new Exception();
            }           
        }
    }
}
