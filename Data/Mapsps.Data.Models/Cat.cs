using Mapsps.Data.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mapsps.Data.Models
{
    public class Cat : BaseModel<int>
    {
        public Cat()
        {
            this.Images = new HashSet<Image>();
            this.Nicknames = new HashSet<Nickname>();
        }

        public ICollection<Image> Images { get; set; }

        public ICollection<Nickname> Nicknames { get; set; }

        public int ConfirmedPetsCount { get; set; }

        public string MostVotedNickname => this.Nicknames.Count > 0 ? this.Nicknames.OrderBy(x => x.Votes).LastOrDefault().Name : string.Empty;

        public double MostRecentLatitude => this.Images.LastOrDefault().Latitude;

        public double MostRecentLongitude => this.Images.LastOrDefault().Longitude;
      
    }

}