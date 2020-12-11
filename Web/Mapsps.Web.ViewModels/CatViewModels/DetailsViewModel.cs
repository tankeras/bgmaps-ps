using Mapsps.Data.Models;
using Mapsps.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mapsps.Web.ViewModels
{
    public class DetailsViewModel : IMapFrom<Cat>
    {
        public int Id { get; set; }

        public ICollection<ImageViewModel> Images { get; set; }       

        public ICollection<NicknameViewModel> Nicknames { get; set; } 

        public string MostVotedNickname { get; set; }

        public int ConfirmedPetsCount { get; set; }

        public string City { get; set; }



    }
}
