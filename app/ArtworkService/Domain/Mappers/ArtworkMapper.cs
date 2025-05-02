using ArtworkService.Domain.DTO;

namespace ArtworkService.Domain.Mappers
{
    public static class ArtworkMapper
    {
        public static ArtworkDTO ToDTO(Artwork artwork)
        {
            return new ArtworkDTO
            {
                Id = artwork.Id,
                Title = artwork.Title,
                YearCreated = artwork.YearCreated,
                Type = artwork.Type,
                ArtistId = artwork.ArtistId,
                Price = artwork.Price
            };
        }

        public static Artwork FromDTO(ArtworkDTO dto)
        {
            return new Artwork(dto.Id, dto.Title, dto.YearCreated, dto.Type, dto.ArtistId, dto.Price);
        }
    }
}