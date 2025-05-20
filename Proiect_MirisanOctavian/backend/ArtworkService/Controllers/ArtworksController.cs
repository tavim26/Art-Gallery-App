using ArtworkService.Domain.DTO;
using ArtworkService.Domain.Mappers;
using ArtworkService.Services;

using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ArtworkService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtworksController : ControllerBase
    {
        private readonly ArtworksService _artworksService;

        public ArtworksController(ArtworksService artworksService)
        {
            _artworksService = artworksService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ArtworkDTO>> GetArtworks()
        {
            var artworks = _artworksService.GetArtworks().Select(ArtworkMapper.ToDTO);
            return Ok(artworks);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ArtworkDTO> GetArtworkById(int id)
        {
            var artwork = _artworksService.GetArtwork(id);
            if (artwork == null)
                return NotFound();

            return Ok(ArtworkMapper.ToDTO(artwork));
        }

        [HttpPost]
        public ActionResult CreateArtwork(ArtworkDTO dto)
        {

            Console.WriteLine($"Received DTO: {JsonSerializer.Serialize(dto)}");

            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Type))
                return BadRequest("Title and Type are required.");

            var artwork = ArtworkMapper.FromDTO(dto);
            bool result = _artworksService.InsertArtwork(artwork);
            return result ? Ok() : BadRequest();
        }

        [HttpPut]
        public ActionResult UpdateArtwork(ArtworkDTO dto)
        {
            if (dto.Id <= 0)
                return BadRequest("Invalid artwork ID.");

            var artwork = ArtworkMapper.FromDTO(dto);
            bool result = _artworksService.UpdateArtwork(artwork);
            return result ? Ok() : BadRequest();
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteArtwork(int id)
        {
            bool result = _artworksService.DeleteArtwork(id);
            return result ? Ok() : BadRequest();
        }

        [HttpGet("searchByTitle")]
        public ActionResult<IEnumerable<ArtworkDTO>> SearchByTitle([FromQuery] string title)
        {
            var artworks = _artworksService.SearchByTitle(title).Select(ArtworkMapper.ToDTO);
            return Ok(artworks);
        }

        [HttpGet("filterByType")]
        public ActionResult<IEnumerable<ArtworkDTO>> FilterByType([FromQuery] string type)
        {
            var artworks = _artworksService.FilterByType(type).Select(ArtworkMapper.ToDTO);
            return Ok(artworks);
        }

        [HttpGet("filterByArtistId")]
        public ActionResult<IEnumerable<ArtworkDTO>> FilterByArtistId([FromQuery] int artistId)
        {
            var artworks = _artworksService.FilterByArtistId(artistId).Select(ArtworkMapper.ToDTO);
            return Ok(artworks);
        }

        [HttpGet("filterByMaxPrice")]
        public ActionResult<IEnumerable<ArtworkDTO>> FilterByMaxPrice([FromQuery] double maxPrice)
        {
            var artworks = _artworksService.FilterByMaxPrice(maxPrice).Select(ArtworkMapper.ToDTO);
            return Ok(artworks);
        }

        [HttpGet("export/{format}")]
        public IActionResult Export(string format)
        {
            var exportResult = _artworksService.ExportArtworks(format);
            if (exportResult == null)
                return BadRequest("Invalid export format.");

            return File(exportResult.Value.content, exportResult.Value.contentType, exportResult.Value.fileName);
        }



        [HttpGet("stats/byType")]
        public ActionResult<List<ArtworkStatsDTO>> GetByTypeStats()
        {
            return Ok(_artworksService.GetStatsByType());
        }

        [HttpGet("stats/byArtist")]
        public ActionResult<List<ArtworkStatsDTO>> GetByArtistStats()
        {
            return Ok(_artworksService.GetStatsByArtist());
        }



    }
}