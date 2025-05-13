using ArtistService.Domain;
using ArtistService.Infrastructure.Entities;

namespace ArtistService.Infrastructure.Factories
{
    public static class ArtistEntityFactory
    {
        public static ArtistEntity Create(Artist artist)
        {
            return new ArtistEntity
            {
                Id = artist.Id,
                Name = artist.Name,
                BirthDate = artist.BirthDate,
                Birthplace = artist.Birthplace,
                Nationality = artist.Nationality,
                Photo = artist.Photo
            };
        }
    }
}