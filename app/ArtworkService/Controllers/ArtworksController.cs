using ArtworkService.Domain;
using ArtworkService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace ArtworkService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtworksController : ControllerBase
    {
        private ArtworksService _artworksService;

        public ArtworksController(ArtworksService artworksService)
        {
            this._artworksService = artworksService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Artwork>> GetArtworks()
        {
            return _artworksService.GetArtworks();
        }

        [HttpGet("{id:int}")]
        public ActionResult<Artwork?> GetArtworkById(int id)
        {
            var artwork = _artworksService.GetArtwork(id);
            if (artwork == null)
                return NotFound();
            return artwork;
        }

        [HttpGet("Images/{artworkId:int}")]
        public ActionResult<IEnumerable<ArtworkImage>> GetArtworkImages(int artworkId)
        {
            var images = _artworksService.GetArtworkImages(artworkId);
            return images;
        }

        [HttpPost]
        public ActionResult CreateArtwork(Artwork artwork)
        {
            bool result = _artworksService.InsertArtwork(artwork);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public ActionResult UpdateArtwork(Artwork artwork)
        {
            bool result = _artworksService.UpdateArtwork(artwork);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteArtwork(int id)
        {
            bool result = _artworksService.DeleteArtwork(id);
            if (result)
                return Ok();
            return BadRequest();
        }


        [HttpGet("searchByTitle")]
        public ActionResult<IEnumerable<Artwork>> SearchByTitle([FromQuery] string title)
        {
            return _artworksService.SearchByTitle(title);
        }

        [HttpGet("filterByType")]
        public ActionResult<IEnumerable<Artwork>> FilterByType([FromQuery] string type)
        {
            return _artworksService.FilterByType(type);
        }

        [HttpGet("filterByMaxPrice")]
        public ActionResult<IEnumerable<Artwork>> FilterByMaxPrice([FromQuery] double maxPrice)
        {
            return _artworksService.FilterByMaxPrice(maxPrice);
        }

        //export


        [HttpGet("export/csv")]
        public IActionResult ExportArtworksCsv()
        {
            var artworks = _artworksService.GetArtworks();
            var builder = new StringBuilder();
            builder.AppendLine("Id,Title,YearCreated,Type,ArtistId,Price");

            foreach (var artwork in artworks)
            {
                builder.AppendLine($"{artwork.Id.ArtworkId},{artwork.Title},{artwork.YearCreated},{artwork.Type},{artwork.ArtistId},{artwork.Price}");
            }

            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "artworks.csv");
        }

        [HttpGet("export/json")]
        public IActionResult ExportArtworksJson()
        {
            var artworks = _artworksService.GetArtworks();
            var json = JsonSerializer.Serialize(artworks);

            return File(Encoding.UTF8.GetBytes(json), "application/json", "artworks.json");
        }
    }
}