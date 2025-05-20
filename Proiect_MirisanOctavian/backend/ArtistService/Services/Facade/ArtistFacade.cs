using ArtistService.Domain.DTO;
using ArtistService.Domain.Mappers;
using ArtistService.Services;

namespace ArtistService.Facade
{
    public class ArtistsFacade
    {
        private readonly ArtistsService _service;

        public ArtistsFacade(ArtistsService service)
        {
            _service = service;
        }

        public List<ArtistDTO> GetAll()
            => _service.GetArtists().Select(ArtistMapper.ToDTO).ToList();

        public ArtistDTO? GetById(int id)
        {
            var artist = _service.GetArtist(id);
            return artist != null ? ArtistMapper.ToDTO(artist) : null;
        }

        public bool Create(ArtistDTO dto)
            => _service.InsertArtist(ArtistMapper.FromDTO(dto));

        public bool Update(ArtistDTO dto)
            => _service.UpdateArtist(ArtistMapper.FromDTO(dto));

        public bool Delete(int id)
            => _service.DeleteArtist(id);

        public List<ArtistDTO> SearchByName(string name)
            => _service.SearchArtistsByName(name).Select(ArtistMapper.ToDTO).ToList();

        public string? GetPhoto(int id)
        {
            var artist = _service.GetArtist(id);
            return artist?.Photo;
        }
    }
}