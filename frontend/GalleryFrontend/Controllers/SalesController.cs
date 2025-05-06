using GalleryFrontend.Models;
using GalleryFrontend.Models.Services;
using GalleryFrontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace GalleryFrontend.Controllers
{
    public class SalesController : Controller
    {
        private readonly SalesApiClient _salesApi;
        private readonly ArtworksApiClient _artworksApi;
        private readonly UsersApiClient _usersApi;

        public SalesController(SalesApiClient salesApi, ArtworksApiClient artworksApi, UsersApiClient usersApi)
        {
            _salesApi = salesApi;
            _artworksApi = artworksApi;
            _usersApi = usersApi;
        }

        public async Task<IActionResult> Index()
        {
            var sales = await _salesApi.GetSalesAsync();
            var total = await _salesApi.GetTotalSalesAsync();

            foreach (var sale in sales)
            {
                var artwork = await _artworksApi.GetArtworkAsync(sale.ArtworkId);
                var employee = await _usersApi.GetUserAsync(sale.EmployeeId);

                sale.ArtworkTitle = artwork?.Title ?? "Unknown";
                sale.EmployeeName = employee?.Name ?? "Unknown";
            }

            ViewBag.Total = total;
            return View(sales);
        }

        [HttpPost]
        public async Task<IActionResult> Sell(int id)
        {
            // 1. Obține ID-ul userului logat
            int? employeeId = HttpContext.Session.GetInt32("UserId");
            if (employeeId == null)
            {
                TempData["Error"] = "User not logged in.";
                return RedirectToAction("Login", "Auth");
            }

            // 2. Obține detaliile operei de artă
            var artwork = await _artworksApi.GetArtworkAsync(id);
            if (artwork == null)
            {
                TempData["Error"] = "Artwork not found.";
                return RedirectToAction("EmployeeIndex", "Artworks");
            }

            // 3. Construiește vânzarea
            var sale = new SaleModel
            {
                ArtworkId = artwork.Id,
                EmployeeId = employeeId.Value,
                SaleDate = DateTime.Now,
                Price = artwork.Price
            };

            // 4. Trimite vânzarea către backend
            var success = await _salesApi.SellArtworkAsync(sale);
            if (!success)
                TempData["Error"] = "Sale failed.";

            return RedirectToAction("EmployeeIndex", "Artworks");
        }


    }
}