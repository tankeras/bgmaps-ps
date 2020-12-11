using System;
using System.Collections.Generic;
using System.Text;

namespace Mapsps.Web.ViewModels
{
    public class GeoDataViewModel 
    {
        public string Country { get; set; }

        public string CountryId { get; set; }

        public string City { get; set; }

        public int Population { get; set; }

        public double Distance { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
