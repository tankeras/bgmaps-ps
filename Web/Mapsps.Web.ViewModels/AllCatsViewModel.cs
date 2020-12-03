using Mapsps.Data.Models;
using Mapsps.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mapsps.Web.ViewModels
{
    public class AllCatsViewModel : IMapFrom<Cat>
    {
        public AllCatsViewModel()
        {
            ImageId = new HashSet<string>();
        }
        public ICollection<string> ImageId { get; set; }
     
        public string MostVotedNickname { get; set; }

        public int ConfrimedPetsCount { get; set; }
    }
}
