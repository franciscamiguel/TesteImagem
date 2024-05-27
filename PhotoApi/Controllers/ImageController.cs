using Microsoft.AspNetCore.Mvc;
using PhotoApi.Models;
using PhotoApi.Util;

namespace PhotoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;

        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Image image)
        {
            string imageWatermark = ImageHelper.ApplyWatermark(image.Photo);

            var response = new Image
            {
                Id =  Guid.NewGuid(),
                Title = image.Title,
                Photo = imageWatermark,
            };

            return Ok(response);
        }
    }
}
