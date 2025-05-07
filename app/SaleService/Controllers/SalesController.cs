using Microsoft.AspNetCore.Mvc;
using SaleService.Domain.DTO;
using SaleService.Domain.Mappers;
using SaleService.Services;

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

            var sale = SaleMapper.FromDTO(dto);
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

            var pdfBytes = PdfGenerator.GenerateSalePdf(sale);
            return File(pdfBytes, "application/pdf", $"sale_{id}.pdf");
        }

    }
}