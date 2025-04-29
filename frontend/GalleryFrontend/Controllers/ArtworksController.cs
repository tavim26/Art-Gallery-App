using Microsoft.AspNetCore.Mvc;
using GalleryFrontend.Models;
using System.Net.Http;
using System.Text.Json;

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

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("artworks/");
            var json = await response.Content.ReadAsStringAsync();
            var artworks = JsonSerializer.Deserialize<List<ArtworkModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Pentru fiecare artwork, aducem numele artistului
            foreach (var artwork in artworks)
            {
                var artistResponse = await _httpClient.GetAsync($"artists/{artwork.ArtistId}");
                if (artistResponse.IsSuccessStatusCode)
                {
                    var artistJson = await artistResponse.Content.ReadAsStringAsync();
                    var artist = JsonSerializer.Deserialize<ArtistModel>(artistJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (artist != null)
                        artwork.ArtistName = artist.Name;
                    else
                        artwork.ArtistName = "Unknown";
                }
                else
                {
                    artwork.ArtistName = "Unknown";
                }
            }

            return View(artworks);
        }


        public async Task<IActionResult> ViewImages(int artworkId)
        {
            var response = await _httpClient.GetAsync($"artworks/images/{artworkId}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var images = JsonSerializer.Deserialize<List<string>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(images);
        }

    }
}