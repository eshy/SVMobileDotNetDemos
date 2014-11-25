using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;

namespace PicShare.Data.Helpers
{
    public static class FileHelperMethods
    {

        public static string GetPictureFileName(string size, object id)
        {
            return String.Format("picture_{0}_{1}.jpg", size, id);
        }

        #region Azure Storage

        //save a file to azure blob storage
        public static async Task SaveFileToAzureAsync(Stream fileStream, string size, int pictureId, string azureAccount, string azureKey, string containerAddress,
            string contentType = "image\\jpeg")
        {
            var accountCredentials = new StorageCredentials(azureAccount, azureKey);
            var storageAccount = new CloudStorageAccount(accountCredentials, true);
            var blobClient = storageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference(containerAddress);
            container.CreateIfNotExists();

            var serverFileName = GetPictureFileName(size, pictureId); 

            var blob = container.GetBlockBlobReference(serverFileName);
            blob.Properties.ContentType = contentType;
            await blob.UploadFromStreamAsync(fileStream);
        }

        //get the file from azure blob storage and save it locally
        public static async Task GetFileFromAzureAsync(string savePath, string fileName, string azureAccount, string azureKey, string containerAddress)
        {
            var accountCredentials = new StorageCredentials(azureAccount, azureKey);
            var storageAccount = new CloudStorageAccount(accountCredentials, true);
            var blobClient = storageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference(containerAddress);
            container.CreateIfNotExists();

            var blob = container.GetBlockBlobReference(fileName);

            if (blob.Exists())
                await blob.DownloadToFileAsync(Path.Combine(savePath, fileName), FileMode.OpenOrCreate);

        }

        public static async Task<Stream> GetFileFromAzureAsync(string size, int pictureId, string azureAccount, string azureKey, string containerAddress)
        {
            var accountCredentials = new StorageCredentials(azureAccount, azureKey);
            var storageAccount = new CloudStorageAccount(accountCredentials, true);
            var blobClient = storageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference(containerAddress);
            container.CreateIfNotExists();

            var serverFileName = GetPictureFileName(size, pictureId); 
            var blob = container.GetBlockBlobReference(serverFileName);

            if (blob.Exists())
            {
                var stream = new MemoryStream();
                await blob.DownloadToStreamAsync(stream);
                return stream;
            }

            return null;
        }


        public static Stream GetFileFromAzure(string size, string pictureId, string azureAccount, string azureKey, string containerAddress)
        {
            var accountCredentials = new StorageCredentials(azureAccount, azureKey);
            var storageAccount = new CloudStorageAccount(accountCredentials, true);
            var blobClient = storageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference(containerAddress);
            container.CreateIfNotExists();

            var serverFileName = GetPictureFileName(size, pictureId); 
            var blob = container.GetBlockBlobReference(serverFileName);

            if (blob.Exists())
            {
                var stream = new MemoryStream();
                blob.DownloadToStream(stream);
                return stream;
            }

            return null;
        }
        #endregion

    }
}
