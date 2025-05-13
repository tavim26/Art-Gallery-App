using SaleService.Domain;
using SaleService.Infrastructure.Entities;

namespace SaleService.Infrastructure.Factories
{
    public static class SaleEntityFactory
    {
        public static SaleEntity Create(Sale sale)
        {
            if (sale == null)
                throw new ArgumentNullException(nameof(sale));

            return new SaleEntity
            {
                Id = sale.Id,
                ArtworkId = sale.ArtworkId,
                EmployeeId = sale.EmployeeId,
                SaleDate = sale.SaleDate,
                Price = sale.Price
            };
        }
    }
}