using System;
using System.Collections.Generic;
using System.Text;

namespace Mapsps.Web.ViewModels.CatViewModels
{
    public class TopCatsViewModel
    {
        public int Id { get; set; }

        public string MostVotedNickname { get; set; }

        public int ConfirmedPetsCount { get; set; }

        public string ImageUrl { get; set; }
    }
}
