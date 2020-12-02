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
        public void CreateCatAsync(AddCatViewModel input, string userId)
        {
            var test = new Cat()
            {
                ConfirmedPetsCount = 13
            };
            
           
            //var coordinates = this.ExtractGeoData(input.Image.ToString());
            //var longitude = coordinates.longitude;
            //var latitude = coordinates.latitude;
            var cat = new Cat();                      
            var niki = new Image
            {
                Extension = Path.GetExtension(input.Image.FileName),
                UserId = userId,
                Cat = cat,
                CatId = cat.Id,
                //Longitude=longitude,
                //Latitude=latitude
            };
            this.db.Images.Add(niki);
            this.db.SaveChanges();
            
        }
        //public (double longitude, double latitude) ExtractGeoData (string image)
        //{
           
        //}
        
    }              
   
}
