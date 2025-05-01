using Microsoft.AspNetCore.Mvc;
using GalleryFrontend.Models;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace GalleryFrontend.Controllers
{

   

    namespace GalleryFrontend.Controllers
    {
        public class ArtworksController : Controller
        {
            private readonly HttpClient _httpClient;

            public ArtworksController(IHttpClientFactory httpClientFactory)
            {
                _httpClient = httpClientFactory.CreateClient();
                _httpClient.BaseAddress = new Uri("http://localhost:7000/");
            }

            public async Task<IActionResult> VisitorIndex(string title = null, string type = null, int? artistId = null)
            {
                string url = "artworks/";

                if (!string.IsNullOrWhiteSpace(title))
                    url = $"artworks/searchByTitle?title={Uri.EscapeDataString(title)}";
                else if (!string.IsNullOrWhiteSpace(type))
                    url = $"artworks/filterByType?type={Uri.EscapeDataString(type)}";
                else if (artistId.HasValue)
                    url = $"artworks/filterByArtistId?artistId={artistId}";

                var artworks = await GetArtworksFromUrl(url);
                var artists = await GetArtists();

                foreach (var artwork in artworks)
                    artwork.ArtistName = artists.FirstOrDefault(a => a.Id == artwork.ArtistId)?.Name ?? "Unknown";

                var model = new ArtworkFilterModel
                {
                    Artworks = artworks,
                    Artists = artists
                };

                return View("IndexVisitor", model);
            }

            private async Task<List<ArtworkModel>> GetArtworksFromUrl(string url)
            {
                var res = await _httpClient.GetAsync(url);
                if (!res.IsSuccessStatusCode) return new();

                var json = await res.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ArtworkModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            }


            public async Task<IActionResult> EmployeeIndex(string title = null, string type = null, int? artistId = null)
            {
                string url = "artworks/";

                if (!string.IsNullOrWhiteSpace(title))
                    url = $"artworks/searchByTitle?title={Uri.EscapeDataString(title)}";
                else if (!string.IsNullOrWhiteSpace(type))
                    url = $"artworks/filterByType?type={Uri.EscapeDataString(type)}";
                else if (artistId.HasValue)
                    url = $"artworks/filterByArtistId?artistId={artistId}";

                var artworks = await GetArtworksFromUrl(url);
                var artists = await GetArtists();

                foreach (var artwork in artworks)
                    artwork.ArtistName = artists.FirstOrDefault(a => a.Id == artwork.ArtistId)?.Name ?? "Unknown";

                var model = new ArtworkFilterModel
                {
                    Artworks = artworks,
                    Artists = artists
                };

                return View("IndexEmployee", model);
            }



            public async Task<IActionResult> ViewImages(int artworkId)
            {
                var res = await _httpClient.GetAsync($"artworks/images/{artworkId}");
                if (!res.IsSuccessStatusCode) return NotFound();

                var json = await res.Content.ReadAsStringAsync();
                var images = JsonSerializer.Deserialize<List<string>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return View(images);
            }

            public async Task<IActionResult> ExportCsv()
            {
                var res = await _httpClient.GetAsync("artworks/export/csv");
                if (!res.IsSuccessStatusCode) return BadRequest();

                var bytes = await res.Content.ReadAsByteArrayAsync();
                return File(bytes, "text/csv", "artworks.csv");
            }

            public async Task<IActionResult> ExportJson()
            {
                var res = await _httpClient.GetAsync("artworks/export/json");
                if (!res.IsSuccessStatusCode) return BadRequest();

                var json = await res.Content.ReadAsStringAsync();
                return File(System.Text.Encoding.UTF8.GetBytes(json), "application/json", "artworks.json");
            }

            private async Task<List<ArtworkModel>> GetArtworks()
            {
                var res = await _httpClient.GetAsync("artworks/");
                if (!res.IsSuccessStatusCode) return new();

                var json = await res.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ArtworkModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            }

            private async Task<List<ArtistModel>> GetArtists()
            {
                var res = await _httpClient.GetAsync("artists/");
                if (!res.IsSuccessStatusCode) return new();

                var json = await res.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ArtistModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            }




            [HttpGet]
            public async Task<IActionResult> Add()
            {
                var artists = await GetArtists();
                var model = new ArtworkFormModel { Artists = artists };
                return View(model);
            }

            [HttpPost]
            public async Task<IActionResult> Add(ArtworkModel artwork)
            {
                var json = JsonSerializer.Serialize(artwork);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("artworks", content);
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "Artwork creation failed.";
                    var model = new ArtworkFormModel { Artwork = artwork, Artists = await GetArtists() };
                    return View(model);
                }

                return RedirectToAction("EmployeeIndex");
            }



            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                var response = await _httpClient.GetAsync($"artworks/{id}");
                if (!response.IsSuccessStatusCode) return NotFound();

                var json = await response.Content.ReadAsStringAsync();
                var artwork = JsonSerializer.Deserialize<ArtworkModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var model = new ArtworkFormModel
                {
                    Artwork = artwork,
                    Artists = await GetArtists()
                };

                return View(model);
            }

            [HttpPost]
            public async Task<IActionResult> Edit(ArtworkModel artwork)
            {
                var json = JsonSerializer.Serialize(artwork);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync("artworks", content);
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "Update failed.";
                    var model = new ArtworkFormModel { Artwork = artwork, Artists = await GetArtists() };
                    return View(model);
                }

                return RedirectToAction("EmployeeIndex");
            }



            [HttpPost]
            public async Task<IActionResult> Delete(int id)
            {
                var response = await _httpClient.DeleteAsync($"artworks/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Deletion failed.";
                }

                return RedirectToAction("EmployeeIndex");
            }



        }
    }


}