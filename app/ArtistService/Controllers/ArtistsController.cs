using ArtistService.Domain.DTO;
using ArtistService.Facade;
using Microsoft.AspNetCore.Mvc;

namespace ArtistService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly ArtistsFacade _facade;

        public ArtistsController(ArtistsFacade facade)
        {
            _facade = facade;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ArtistDTO>> GetArtists()
        {
            return Ok(_facade.GetAll());
        }

        [HttpGet("{id:int}")]
        public ActionResult<ArtistDTO> GetArtistById(int id)
        {
            var result = _facade.GetById(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public ActionResult CreateArtist(ArtistDTO dto)
        {
            return _facade.Create(dto) ? Ok() : BadRequest();
        }

        [HttpPut]
        public ActionResult UpdateArtist(ArtistDTO dto)
        {
            return _facade.Update(dto) ? Ok() : BadRequest();
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteArtist(int id)
        {
            return _facade.Delete(id) ? Ok() : BadRequest();
        }

        [HttpGet("searchByName")]
        public ActionResult<IEnumerable<ArtistDTO>> SearchArtistsByName([FromQuery] string name)
        {
            return Ok(_facade.SearchByName(name));
        }

        [HttpGet("photo/{id:int}")]
        public ActionResult<string> GetArtistPhoto(int id)
        {
            var photo = _facade.GetPhoto(id);
            return photo != null ? Ok(photo) : NotFound();
        }

    }
}
