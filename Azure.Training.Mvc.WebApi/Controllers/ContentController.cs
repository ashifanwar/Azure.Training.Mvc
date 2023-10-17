using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace Azure.Training.Mvc.WebApi.Controllers
{
    public class ContentController : Controller
    {
        private readonly BlobRepository _blobRepository;
        public ContentController(BlobRepository blobRepository)
        {
            _blobRepository = blobRepository;       
        }
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var file = Request.Form.Files["ImageData"];
            using(var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                var bytes = stream.ToArray();
                await _blobRepository.UploadAsync(bytes);

            }
            return RedirectToAction("Index", "Home");
        }
    }
}
