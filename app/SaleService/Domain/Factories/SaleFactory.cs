namespace SaleService.Domain.Factories
{
    public static class SaleFactory
    {
        public static Sale Create(int artworkId, int employeeId, DateTime saleDate, double price)
        {
            if (artworkId <= 0)
                throw new ArgumentException("Invalid artwork ID.");

            if (employeeId <= 0)
                throw new ArgumentException("Invalid employee ID.");

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.");

            if (saleDate > DateTime.Now)
                throw new ArgumentException("Sale date cannot be in the future.");

            return new Sale
            {
                ArtworkId = artworkId,
                EmployeeId = employeeId,
                SaleDate = saleDate,
                Price = price
            };
        }
    }
}