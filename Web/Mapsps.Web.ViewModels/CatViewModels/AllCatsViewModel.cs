using AutoMapper;
using Mapsps.Data.Models;
using Mapsps.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mapsps.Web.ViewModels
{
    public class AllCatsViewModel 
    {
        public AllCatsViewModel()
        {
            this.ImagesId = new HashSet<string>();
        }
        public int Id { get; set; }

        public string MostVotedNickname { get; set; }

        public int ConfirmedPetsCount { get; set; }

        public ICollection<string> ImagesId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}
