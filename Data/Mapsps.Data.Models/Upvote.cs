using Mapsps.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mapsps.Data.Models
{
    public class Upvote
    {
        public Upvote()
        {
            this.CreatedOn = DateTime.UtcNow;
        }
        public int NicknameId { get; set; }

        public Nickname Nickname { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
