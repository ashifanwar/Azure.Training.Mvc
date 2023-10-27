using Azure.Storage.Blobs;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Azure.Training.Mvc.WebApi
{
    public class BlobRepository
    {
        private readonly BlobServiceClient blobServiceClient;
        public BlobRepository(string blobConnectionString)
        {
            var blobContainerClient = new BlobContainerClient(blobConnectionString, "azuretraining");
            blobContainerClient.CreateIfNotExists();
            blobServiceClient = new BlobServiceClient(blobConnectionString);
        }
        public async Task UploadAsync(byte[] blobContent, string blobName)
        {
            var blobContainer = blobServiceClient.GetBlobContainerClient("azuretraining");
            await blobContainer.CreateIfNotExistsAsync().ConfigureAwait(false);
            var blobClient = blobContainer.GetBlobClient(blobName);

            var memoryStream = new MemoryStream(blobContent);

            await blobClient.UploadAsync(memoryStream, true).ConfigureAwait(false);
        }
    }
}
