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

            await EnrichSalesAsync(sales);

            ViewBag.Total = total;
            return View(sales);
        }

        private async Task EnrichSalesAsync(List<SaleModel> sales)
        {
            foreach (var sale in sales)
            {
                var artworkTask = _artworksApi.GetArtworkAsync(sale.ArtworkId);
                var employeeTask = _usersApi.GetUserAsync(sale.EmployeeId);

                await Task.WhenAll(artworkTask, employeeTask);

                sale.ArtworkTitle = artworkTask.Result?.Title ?? "Unknown";
                sale.EmployeeName = employeeTask.Result?.Name ?? "Unknown";
            }
        }

        [HttpPost]
        public async Task<IActionResult> Sell(int id)
        {
            int? employeeId = HttpContext.Session.GetInt32("UserId");
            if (employeeId == null)
            {
                TempData["Error"] = "User not logged in.";
                return RedirectToAction("Login", "Auth");
            }

            var sale = await BuildSaleAsync(id, employeeId.Value);
            if (sale == null)
            {
                TempData["Error"] = "Artwork not found.";
                return RedirectToAction("EmployeeIndex", "Artworks");
            }

            var success = await _salesApi.SellArtworkAsync(sale);
            if (!success)
                TempData["Error"] = "Sale failed.";

            return RedirectToAction("EmployeeIndex", "Artworks");
        }

        private async Task<SaleModel?> BuildSaleAsync(int artworkId, int employeeId)
        {
            var artwork = await _artworksApi.GetArtworkAsync(artworkId);
            if (artwork == null)
                return null;

            return new SaleModel
            {
                ArtworkId = artwork.Id,
                EmployeeId = employeeId,
                SaleDate = DateTime.Now,
                Price = artwork.Price
            };
        }


        public async Task<IActionResult> Download(int id)
        {
            var pdfBytes = await _salesApi.DownloadSalePdfAsync(id);
            return File(pdfBytes, "application/pdf", $"sale_{id}.pdf");
        }

    }

}