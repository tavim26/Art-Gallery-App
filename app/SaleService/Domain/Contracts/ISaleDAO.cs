namespace SaleService.Domain.Contracts
{
    public interface ISaleDAO
    {
        bool InsertSale(Sale sale);
        double GetTotalSalesAmount();
    }
}