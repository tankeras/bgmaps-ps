﻿using Mapsps.Data.Common.Models;
using System.Collections.Generic;

namespace Mapsps.Data.Models
{
    public class Nickname : BaseModel<int>
    {
        public Nickname()
        {
            this.Upvotes = new HashSet<Upvote>();
        }

        public string Name { get; set; }

        public Cat Cat { get; set; }

        public int Votes { get; set; }

        public ICollection<Upvote> Upvotes { get; set; }
    }
}