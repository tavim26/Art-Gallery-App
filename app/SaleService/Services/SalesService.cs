using SaleService.Domain;
using SaleService.Domain.Contracts;

namespace SaleService.Services
{
    public class SalesService
    {
        private ISaleDAO _saleDAO;

        public SalesService(ISaleDAO saleDAO)
        {
            this._saleDAO = saleDAO;
        }

        public bool SellArtwork(Sale sale)
        {
            if (sale == null)
                return false;
            return _saleDAO.InsertSale(sale);
        }

        public double GetTotalSalesAmount()
        {
            return _saleDAO.GetTotalSalesAmount();
        }
    }
}