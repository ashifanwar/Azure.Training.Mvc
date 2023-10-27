using Azure.Training.Mvc.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Concurrent;
using System.Collections;
using System.IO;
using System.Threading.Tasks;

namespace Azure.Training.Mvc.WebApi.Controllers
{
    public class ContentController : Controller
    {
        private readonly BlobRepository _blobRepository;
        private readonly CosmosClient cosmosClient;
        public ContentController(BlobRepository blobRepository, CosmosClient cosmosClient)
        {
            _blobRepository = blobRepository;
            this.cosmosClient = cosmosClient;
        }
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var pk = "607fa405-a56f-4505-8c75-39766e4af7d1";
            var file = Request.Form.Files["ImageData"];
            using(var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                var bytes = stream.ToArray();
                await _blobRepository.UploadAsync(bytes, file.FileName);

            }

            var contentMetaData = new ContentMetaData
            {
                FileName = file.FileName,
                UploadDateTime = DateTime.UtcNow,
                Id = Guid.NewGuid().ToString(),
                UserGuid = pk
            };

            var db = await cosmosClient.CreateDatabaseIfNotExistsAsync("azuretraining");
            var containerConfig = new ContainerProperties("useruploads", "/userGuid");
            var containerClient = db.Database.CreateContainerIfNotExistsAsync(containerConfig).GetAwaiter().GetResult().Container;
            await containerClient.UpsertItemAsync(contentMetaData, new PartitionKey(pk));
            return RedirectToAction("Index", "Home");
        }
    }
}
