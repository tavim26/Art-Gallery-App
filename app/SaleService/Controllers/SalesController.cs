using Microsoft.AspNetCore.Mvc;
using SaleService.Domain.Contracts;
using SaleService.Domain.DTO;
using SaleService.Domain.Mappers;
using SaleService.Services;
using SaleService.Services.Exports;

namespace SaleService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly SalesService _salesService;

        public SalesController(SalesService salesService)
        {
            _salesService = salesService;
        }


        [HttpGet]
        public ActionResult<List<SaleDTO>> GetAll()
        {
            var sales = _salesService.GetAllSales();
            var dtos = sales.Select(s => SaleMapper.ToDTO(s)).ToList();
            return Ok(dtos);
        }


        [HttpPost]
        public ActionResult SellArtwork([FromBody] SaleDTO dto)
        {
            if (dto.ArtworkId <= 0 || dto.EmployeeId <= 0 || dto.Price <= 0)
                return BadRequest("Invalid sale data.");

            var sale = SaleService.Domain.Factories.SaleFactory.Create(
                dto.ArtworkId,
                dto.EmployeeId,
                dto.SaleDate,
                dto.Price
            );

            var result = _salesService.SellArtwork(sale);
            return result ? Ok() : BadRequest("Could not process sale.");
        }



        [HttpGet("totalSalesAmount")]
        public ActionResult<double> GetTotalSalesAmount()
        {
            var total = _salesService.GetTotalSalesAmount();
            return Ok(total);
        }



        [HttpGet("{id}/pdf")]
        public IActionResult ExportSalePdf(int id)
        {
            var sale = _salesService.GetAllSales().FirstOrDefault(s => s.Id == id);
            if (sale == null)
                return NotFound("Sale not found.");

            IExportStrategy strategy = new PdfExportStrategy(); 

            var fileBytes = strategy.Export(sale);
            return File(fileBytes, strategy.GetMimeType(), $"sale_{id}.{strategy.GetFileExtension()}");

        }

    }
}