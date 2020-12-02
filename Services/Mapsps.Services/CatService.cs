using ExifLib;
using Mapsps.Data;
using Mapsps.Data.Models;
using Mapsps.Web.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapsps.Services
{
    public class CatService
    {
        private readonly ApplicationDbContext db;

        public CatService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public bool CreateCatAsync(AddCatViewModel input, string userId)
        {
            
            var coordinates = this.ExtractGeoData(input.Image.OpenReadStream());
            var longitude = coordinates.longitude;
            var latitude = coordinates.latitude;
            if (latitude == 1)
            {
                return false;
            }
            var cat = new Cat();
            var niki = new Image
            {
                Extension = Path.GetExtension(input.Image.FileName),
                UserId = userId,
                Cat = cat,
                CatId = cat.Id,
                Longitude = longitude,
                Latitude = latitude
            };
            this.db.Images.Add(niki);
            this.db.SaveChanges();
            return true;
        }
        public (double longitude, double latitude) ExtractGeoData(Stream stream)
        {
            using (ExifReader reader = new ExifReader(stream))
            {
                Double[] GpsLongArray;
                Double[] GpsLatArray;
                Double GpsLongDouble;
                Double GpsLatDouble;
                
                if (reader.GetTagValue<Double[]>(ExifTags.GPSLongitude, out GpsLongArray)
                    && reader.GetTagValue<Double[]>(ExifTags.GPSLatitude, out GpsLatArray))
                {
                    GpsLongDouble = GpsLongArray[0] + GpsLongArray[1] / 60 + GpsLongArray[2] / 3600;
                    GpsLatDouble = GpsLatArray[0] + GpsLatArray[1] / 60 + GpsLatArray[2] / 3600;

                    return (GpsLongDouble, GpsLatDouble);
                }
                else
                {
                    return (1, 1);
                }

            }
        }

    }

}
