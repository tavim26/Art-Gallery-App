using SaleService.Domain.DTO;

namespace SaleService.Domain.Mappers
{
    public static class SaleMapper
    {
        public static SaleDTO ToDTO(Sale s) => new SaleDTO
        {
            Id = s.Id,
            ArtworkId = s.ArtworkId,
            EmployeeId = s.EmployeeId,
            SaleDate = s.SaleDate,
            Price = s.Price
        };

        public static Sale FromDTO(SaleDTO dto) => new Sale
        {
            Id = dto.Id,
            ArtworkId = dto.ArtworkId,
            EmployeeId = dto.EmployeeId,
            SaleDate = dto.SaleDate,
            Price = dto.Price
        };
    }
}