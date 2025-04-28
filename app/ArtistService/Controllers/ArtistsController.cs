using ArtistService.Domain;
using ArtistService.Infrastructure;
using ArtistService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArtistService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private ArtistsService _artistsService;

        public ArtistsController(ArtistDAO artistDAO)
        {
            _artistsService = new ArtistsService(artistDAO);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Artist>> GetArtists()
        {
            return _artistsService.GetArtists();
        }

        [HttpGet("{id:int}")]
        public ActionResult<Artist?> GetArtistById(int id)
        {
            var artist = _artistsService.GetArtist(id);
            if (artist == null)
                return NotFound();
            return artist;
        }

        [HttpPost]
        public ActionResult CreateArtist(Artist artist)
        {
            bool result = _artistsService.InsertArtist(artist);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public ActionResult UpdateArtist(Artist artist)
        {
            bool result = _artistsService.UpdateArtist(artist);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteArtist(int id)
        {
            bool result = _artistsService.DeleteArtist(id);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpGet("searchByName")]
        public ActionResult<IEnumerable<Artist>> SearchArtistsByName([FromQuery] string name)
        {
            return _artistsService.SearchArtistsByName(name);
        }
    }
}