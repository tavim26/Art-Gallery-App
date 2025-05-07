using ArtistService.Domain.DTO;
using ArtistService.Domain.Mappers;
using ArtistService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArtistService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly ArtistsService _artistsService;

        public ArtistsController(ArtistsService artistsService)
        {
            _artistsService = artistsService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ArtistDTO>> GetArtists()
        {
            var artists = _artistsService.GetArtists()
                                         .Select(ArtistMapper.ToDTO)
                                         .ToList();

            return Ok(artists);
        }



        [HttpGet("{id:int}")]
        public ActionResult<ArtistDTO> GetArtistById(int id)
        {
            var artist = _artistsService.GetArtist(id);
            if (artist == null)
                return NotFound();

            return Ok(ArtistMapper.ToDTO(artist));
        }





        [HttpPost]
        public ActionResult CreateArtist(ArtistDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Name is required.");

            if (dto.Photo == null)
                dto.Photo = "";

            var artist = ArtistMapper.FromDTO(dto);
            var result = _artistsService.InsertArtist(artist);

            return result ? Ok() : BadRequest();
        }


        [HttpPut]
        public ActionResult UpdateArtist(ArtistDTO dto)
        {
            if (dto.Id <= 0)
                return BadRequest("Invalid artist ID.");

            var existing = _artistsService.GetArtist(dto.Id);
            if (existing == null)
                return NotFound();

            if (string.IsNullOrEmpty(dto.Photo))
                dto.Photo = existing.Photo;

            var artist = ArtistMapper.FromDTO(dto);
            var result = _artistsService.UpdateArtist(artist);

            return result ? Ok() : BadRequest();
        }






        [HttpDelete("{id:int}")]
        public ActionResult DeleteArtist(int id)
        {
            var result = _artistsService.DeleteArtist(id);
            return result ? Ok() : BadRequest();
        }



        [HttpGet("searchByName")]
        public ActionResult<IEnumerable<ArtistDTO>> SearchArtistsByName([FromQuery] string name)
        {
            var artists = _artistsService.SearchArtistsByName(name)
                                         .Select(ArtistMapper.ToDTO)
                                         .ToList();

            return Ok(artists);
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
