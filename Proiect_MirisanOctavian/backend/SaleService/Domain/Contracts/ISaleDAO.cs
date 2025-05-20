namespace SaleService.Domain.Contracts
{
    public interface ISaleDAO
    {
        List<Sale> GetAllSales();
        bool InsertSale(Sale sale);
        double GetTotalSalesAmount();
    }
}