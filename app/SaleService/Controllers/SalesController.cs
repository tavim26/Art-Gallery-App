using Microsoft.AspNetCore.Mvc;
using SaleService.Domain;
using SaleService.Infrastructure;
using SaleService.Services;

namespace SaleService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private SalesService _salesService;

        public SalesController(SaleDAO saleDAO)
        {
            _salesService = new SalesService(saleDAO);
        }

        [HttpPost]
        public ActionResult SellArtwork(Sale sale)
        {
            bool result = _salesService.SellArtwork(sale);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpGet("totalSalesAmount")]
        public ActionResult<double> GetTotalSalesAmount()
        {
            double total = _salesService.GetTotalSalesAmount();
            return Ok(total);
        }
    }
}