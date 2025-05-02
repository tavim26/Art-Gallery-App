using ArtworkService.Domain;
using ArtworkService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArtworkService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtworkImagesController : ControllerBase
    {
        private readonly ArtworkImagesService _imageService;

        public ArtworkImagesController(ArtworkImagesService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        public ActionResult AddImage([FromBody] ArtworkImage image)
        {
            if (image == null || image.ArtworkId <= 0 || string.IsNullOrWhiteSpace(image.ImageUrl))
                return BadRequest("ArtworkId and ImageUrl are required.");

            var result = _imageService.InsertArtworkImage(image);
            return result ? Ok() : BadRequest("Failed to insert image.");
        }



        [HttpGet("{artworkId:int}")]
        public ActionResult<List<string>> GetArtworkImages(int artworkId)
        {
            var images = _imageService.GetArtworkImages(artworkId);
            if (images == null || images.Count == 0)
                return NotFound();

            var urls = images.Select(img => img.ImageUrl).ToList();
            return Ok(urls);
        }

    }
}