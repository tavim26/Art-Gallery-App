using SaleService.Domain;
using SaleService.Domain.Contracts;
using SaleService.Services.Exports;

namespace SaleService.Services
{
    public class SalesService
    {
        private ISaleDAO _saleDAO;

        public SalesService(ISaleDAO saleDAO)
        {
            this._saleDAO = saleDAO;
        }


        public List<Sale> GetAllSales()
        {
            return _saleDAO.GetAllSales();
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


        public (byte[] fileBytes, string contentType, string fileName)? ExportSaleAsPdf(int id)
        {
            var sale = _saleDAO.GetAllSales().FirstOrDefault(s => s.Id == id);
            if (sale == null) return null;

            IExportStrategy strategy = new PdfExportStrategy(); 
            var fileBytes = strategy.Export(sale);
            var fileName = $"sale_{id}.{strategy.GetFileExtension()}";

            return (fileBytes, strategy.GetMimeType(), fileName);
        }

    }
}