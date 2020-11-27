using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace Mapsps.Web.ViewModels
{

    public class IsImageValid : ValidationAttribute
    {
        private readonly string[] extensions;

        public IsImageValid()
        {
            extensions = new string[] { ".jpg", ".png", ".bmp" };
            this.ErrorMessage = "File must be a jpeg, png or bmp";
        }

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!extensions.Contains(extension.ToLower()))
                {
                    return false;
                }
            }

            return true;
        }
        
    }
}

