using Mapsps.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mapsps.Data.Models
{
    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }      
        public int CatId { get; set; }

        public Cat Cat { get; set; }
     
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string Extension { get; set; }

        public int Upvotes { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}
