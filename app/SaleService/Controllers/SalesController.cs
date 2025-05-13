using Microsoft.AspNetCore.Mvc;
using SaleService.Domain.DTO;
using SaleService.Services;

namespace SaleService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly SalesFacade _facade;

        public SalesController(SalesFacade facade)
        {
            _facade = facade;
        }


        [HttpGet]
        public ActionResult<List<SaleDTO>> GetAll()
        {
            return Ok(_facade.GetAll());
        }

        [HttpPost]
        public ActionResult SellArtwork([FromBody] SaleDTO dto)
        {
            if (dto.ArtworkId <= 0 || dto.EmployeeId <= 0 || dto.Price <= 0)
                return BadRequest("Invalid sale data.");

            return _facade.Sell(dto) ? Ok() : BadRequest("Could not process sale.");
        }

        [HttpGet("totalSalesAmount")]
        public ActionResult<double> GetTotalSalesAmount()
        {
            return Ok(_facade.GetTotalAmount());
        }

        [HttpGet("{id}/pdf")]
        public IActionResult ExportSalePdf(int id)
        {
            var result = _facade.ExportPdf(id);
            if (result == null)
                return NotFound("Sale not found.");

            return File(result.Value.fileBytes, result.Value.contentType, result.Value.fileName);
        }



    }
}