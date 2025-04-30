using Microsoft.AspNetCore.Mvc;
using GalleryFrontend.Models;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GalleryFrontend.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient _httpClient;

        public EmployeeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7000/"); // Proxy către microservicii
        }

        // GET: /Employee/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Employee/Artworks
        public async Task<IActionResult> Artworks()
        {
            var response = await _httpClient.GetAsync("artworks");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to load artworks.";
                return View(new List<ArtworkModel>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var artworks = JsonSerializer.Deserialize<List<ArtworkModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(artworks);
        }

        // GET: /Employee/AddArtwork
        public IActionResult AddArtwork()
        {
            return View(new ArtworkModel());
        }

        // POST: /Employee/AddArtwork
        [HttpPost]
        public async Task<IActionResult> AddArtwork(ArtworkModel artwork)
        {
            if (!ModelState.IsValid)
            {
                return View(artwork);
            }

            var response = await _httpClient.PostAsync("artworks", new StringContent(JsonSerializer.Serialize(artwork), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to add artwork.";
                return View(artwork);
            }

            return RedirectToAction("Artworks");
        }

        // GET: /Employee/EditArtwork/{id}
        public async Task<IActionResult> EditArtwork(int id)
        {
            var response = await _httpClient.GetAsync($"artworks/{id}");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to load artwork.";
                return RedirectToAction("Artworks");
            }

            var json = await response.Content.ReadAsStringAsync();
            var artwork = JsonSerializer.Deserialize<ArtworkModel>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(artwork);
        }

        // POST: /Employee/EditArtwork
        [HttpPost]
        public async Task<IActionResult> EditArtwork(ArtworkModel artwork)
        {
            if (!ModelState.IsValid)
            {
                return View(artwork);
            }

            var response = await _httpClient.PutAsync("artworks", new StringContent(JsonSerializer.Serialize(artwork), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to update artwork.";
                return View(artwork);
            }

            return RedirectToAction("Artworks");
        }

        // POST: /Employee/DeleteArtwork/{id}
        [HttpPost]
        public async Task<IActionResult> DeleteArtwork(int id)
        {
            var response = await _httpClient.DeleteAsync($"artworks/{id}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Failed to delete artwork.";
            }

            return RedirectToAction("Artworks");
        }

        // GET: /Employee/Artists
        public async Task<IActionResult> Artists()
        {
            var response = await _httpClient.GetAsync("artists");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to load artists.";
                return View(new List<ArtistModel>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var artists = JsonSerializer.Deserialize<List<ArtistModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(artists);
        }

        // GET: /Employee/AddArtist
        public IActionResult AddArtist()
        {
            return View(new ArtistModel());
        }

        // POST: /Employee/AddArtist
        [HttpPost]
        public async Task<IActionResult> AddArtist(ArtistModel artist)
        {
            if (!ModelState.IsValid)
            {
                return View(artist);
            }

            var response = await _httpClient.PostAsync("artists", new StringContent(JsonSerializer.Serialize(artist), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to add artist.";
                return View(artist);
            }

            return RedirectToAction("Artists");
        }

        // GET: /Employee/EditArtist/{id}
        public async Task<IActionResult> EditArtist(int id)
        {
            var response = await _httpClient.GetAsync($"artists/{id}");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to load artist.";
                return RedirectToAction("Artists");
            }

            var json = await response.Content.ReadAsStringAsync();
            var artist = JsonSerializer.Deserialize<ArtistModel>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(artist);
        }

        // POST: /Employee/EditArtist
        [HttpPost]
        public async Task<IActionResult> EditArtist(ArtistModel artist)
        {
            if (!ModelState.IsValid)
            {
                return View(artist);
            }

            var response = await _httpClient.PutAsync("artists", new StringContent(JsonSerializer.Serialize(artist), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to update artist.";
                return View(artist);
            }

            return RedirectToAction("Artists");
        }

        // POST: /Employee/DeleteArtist/{id}
        [HttpPost]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var response = await _httpClient.DeleteAsync($"artists/{id}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Failed to delete artist.";
            }

            return RedirectToAction("Artists");
        }

        // GET: /Employee/ExportArtworksCsv
        public async Task<IActionResult> ExportArtworksCsv()
        {
            var response = await _httpClient.GetAsync("artworks/export/csv");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Failed to export artworks to CSV.";
                return RedirectToAction("Artworks");
            }

            var content = await response.Content.ReadAsByteArrayAsync();
            return File(content, "text/csv", "artworks.csv");
        }

        // GET: /Employee/ExportArtworksJson
        public async Task<IActionResult> ExportArtworksJson()
        {
            var response = await _httpClient.GetAsync("artworks/export/json");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Failed to export artworks to JSON.";
                return RedirectToAction("Artworks");
            }

            var content = await response.Content.ReadAsByteArrayAsync();
            return File(content, "application/json", "artworks.json");
        }

        // POST: /Employee/SellArtwork/{id}
        [HttpPost]
        public async Task<IActionResult> SellArtwork(int id)
        {
            var response = await _httpClient.PostAsync($"artworks/sell/{id}", null);
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Failed to sell artwork.";
            }

            return RedirectToAction("Artworks");
        }
    }
}