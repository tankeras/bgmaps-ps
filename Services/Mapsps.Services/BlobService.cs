using Mapsps.Web.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mapsps.Services
{
    public class BlobService
    {
        private readonly IConfiguration config;

        public BlobService(IConfiguration config)
        {
            this.config = config;
        }
        public async Task UploadBlob(Stream stream, string imageId, string imageExtension)
        {
            string accountname = this.config.GetValue<string>("AzureAccountName");
            string accesskey = this.config.GetValue<string>("AzureAccountKey");
            try
            {
                var credentials = new StorageCredentials(accountname, accesskey);
                var account = new CloudStorageAccount(credentials, useHttps: true);
                CloudBlobClient client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference("catimages");
                await container.CreateIfNotExistsAsync();                
                var blob = container.GetBlockBlobReference($"{imageId}{imageExtension}");
                await blob.UploadFromStreamAsync(stream);                              
            }
            catch (Exception ex)
            {
                
            }
        }
        
    }
}
