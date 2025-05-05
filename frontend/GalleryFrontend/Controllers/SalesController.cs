using GalleryFrontend.Models;
using GalleryFrontend.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace GalleryFrontend.Controllers
{
    public class SalesController : Controller
    {
        private readonly SalesApiClient _salesApi;

        public SalesController(SalesApiClient salesApi)
        {
            _salesApi = salesApi;
        }

        [HttpPost]
        public async Task<IActionResult> Sell(int id)
        {
            // Exemplu: presupunem EmployeeId = 1 pentru test
            var sale = new SaleModel
            {
                ArtworkId = id,
                EmployeeId = 1, // TO DO: înlocuiește cu user autenticat când implementezi autentificarea
                SaleDate = DateTime.Now,
                Price = 25000  // TO DO: poate fi preluat din ArtworkService mai târziu
            };

            var success = await _salesApi.SellArtworkAsync(sale);
            if (!success)
                TempData["Error"] = "Sale failed.";

            return RedirectToAction("EmployeeIndex", "Artworks");
        }
    }
}
