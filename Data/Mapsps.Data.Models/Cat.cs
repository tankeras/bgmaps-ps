using Mapsps.Data.Common.Models;
using System.Collections.Generic;

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
    }
}