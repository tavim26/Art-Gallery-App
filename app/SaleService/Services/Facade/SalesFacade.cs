using SaleService.Domain.DTO;
using SaleService.Domain.Mappers;

namespace SaleService.Services
{
    public class SalesFacade
    {
        private readonly SalesService _service;

        public SalesFacade(SalesService service)
        {
            _service = service;
        }

        public List<SaleDTO> GetAll()
            => _service.GetAllSales().Select(SaleMapper.ToDTO).ToList();

        public bool Sell(SaleDTO dto)
        {
            var sale = SaleMapper.FromDTO(dto);

            return _service.SellArtwork(sale);
        }

        public double GetTotalAmount()
            => _service.GetTotalSalesAmount();

        public (byte[] fileBytes, string contentType, string fileName)? ExportPdf(int id)
            => _service.ExportSaleAsPdf(id);
    }
}