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
        public ActionResult CreateArtist()
        {
            var form = Request.Form;

            var name = form["Name"];
            var birthDate = DateTime.TryParse(form["BirthDate"], out var bd) ? bd : (DateTime?)null;
            var birthplace = form["Birthplace"];
            var nationality = form["Nationality"];
            var file = Request.Form.Files.GetFile("Photo");

            if (file == null || file.Length == 0)
                return BadRequest("Photo is required.");

            using var ms = new MemoryStream();
            file.CopyTo(ms);
            var photoBytes = ms.ToArray();
            var base64Photo = Convert.ToBase64String(photoBytes);

            var artist = new Artist
            {
                Name = name,
                BirthDate = birthDate,
                Birthplace = birthplace,
                Nationality = nationality,
                Photo = base64Photo
            };

            var result = _artistsService.InsertArtist(artist);
            return result ? Ok() : BadRequest();
        }




        [HttpPut]
        public ActionResult UpdateArtist()
        {
            try
            {
                var form = Request.Form;

                if (!int.TryParse(form["Id"], out var id))
                {
                    Console.WriteLine("Missing or invalid Id.");
                    return BadRequest("Missing or invalid Id.");
                }

                var name = form["Name"];
                var birthDateStr = form["BirthDate"];
                var birthplace = form["Birthplace"];
                var nationality = form["Nationality"];

                Console.WriteLine($"Updating artist: ID={id}, Name={name}");

                DateTime? birthDate = null;
                if (DateTime.TryParse(birthDateStr, out var bd))
                    birthDate = bd;

                var file = form.Files.GetFile("Photo");
                string base64Photo = "";

                if (file != null && file.Length > 0)
                {
                    using var ms = new MemoryStream();
                    file.CopyTo(ms);
                    base64Photo = Convert.ToBase64String(ms.ToArray());
                }

                var existing = _artistsService.GetArtist(id);
                if (existing == null)
                {
                    Console.WriteLine("Artist not found.");
                    return NotFound();
                }

                var artist = new Artist
                {
                    Id = id,
                    Name = name,
                    BirthDate = birthDate,
                    Birthplace = birthplace,
                    Nationality = nationality,
                    Photo = string.IsNullOrEmpty(base64Photo) ? existing.Photo : base64Photo
                };

                var result = _artistsService.UpdateArtist(artist);
                Console.WriteLine(result ? "Update succeeded." : "Update failed.");

                return result ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in UpdateArtist: " + ex.Message);
                return StatusCode(500, "Internal server error.");
            }
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



        [HttpGet("photo/{id:int}")]
        public ActionResult<string> GetArtistPhoto(int id)
        {
            var artist = _artistsService.GetArtist(id);
            if (artist == null)
                return NotFound();

            return Ok(artist.Photo);
        }

    }
}