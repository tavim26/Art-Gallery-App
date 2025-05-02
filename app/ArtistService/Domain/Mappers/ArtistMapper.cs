using ArtistService.Domain.DTO;

namespace ArtistService.Domain.Mappers
{
    public static class ArtistMapper
    {
        public static ArtistDTO ToDTO(Artist artist)
        {
            return new ArtistDTO
            {
                Id = artist.Id,
                Name = artist.Name,
                BirthDate = artist.BirthDate?.ToString("yyyy-MM-dd"),
                Birthplace = artist.Birthplace,
                Nationality = artist.Nationality,
                Photo = artist.Photo
            };
        }

        public static Artist FromDTO(ArtistDTO dto)
        {
            return new Artist
            {
                Id = dto.Id,
                Name = dto.Name,
                BirthDate = string.IsNullOrEmpty(dto.BirthDate) ? null : DateTime.Parse(dto.BirthDate),
                Birthplace = dto.Birthplace,
                Nationality = dto.Nationality,
                Photo = dto.Photo
            };
        }
    }
}