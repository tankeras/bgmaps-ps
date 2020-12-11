using Mapsps.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mapsps.Data.Models
{
    public class ConfirmedPet
    {
        public ConfirmedPet()
        {
            this.CreatedOn = DateTime.UtcNow;
        }
        public int CatId { get; set; }

        public Cat Cat { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime CreatedOn { get; set; }
    }

}

