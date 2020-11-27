using Mapsps.Data.Common.Models;
using System.Collections.Generic;

namespace Mapsps.Data.Models
{
    public class Cat : BaseModel<int>
    {
        public ICollection<Image> Images { get; set; }

        public ICollection<Nickname> Nicknames { get; set; }

        public int ConfirmedPetsCount { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }




    }
}