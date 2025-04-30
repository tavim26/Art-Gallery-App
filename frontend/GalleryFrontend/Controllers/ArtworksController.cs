using Microsoft.AspNetCore.Mvc;
using GalleryFrontend.Models;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index(string searchTitle = null, string type = null, int? artistId = null, double? maxPrice = null)
        {
            // Construim URL-ul pentru opere
            string artworksUrl = "artworks/";
            if (!string.IsNullOrEmpty(searchTitle))
            {
                artworksUrl = $"artworks/searchByTitle?title={Uri.EscapeDataString(searchTitle)}";
            }
            else if (type != null && type != "All")
            {
                artworksUrl = $"artworks/filterByType?type={Uri.EscapeDataString(type)}";
            }
            else if (artistId.HasValue)
            {
                artworksUrl = $"artworks/filterByArtistId?artistId={artistId}";
            }
            else if (maxPrice.HasValue)
            {
                artworksUrl = $"artworks/filterByMaxPrice?maxPrice={maxPrice}";
            }

            // Obținem operele
            var artworksResponse = await _httpClient.GetAsync(artworksUrl);
            var artworks = new List<ArtworkModel>();
            if (artworksResponse.IsSuccessStatusCode)
            {
                var artworksJson = await artworksResponse.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(artworksJson))
                {
                    artworks = JsonSerializer.Deserialize<List<ArtworkModel>>(artworksJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }

            // Obținem artiștii pentru combobox
            var artistsResponse = await _httpClient.GetAsync("artists/");
            var artists = new List<ArtistModel>();
            if (artistsResponse.IsSuccessStatusCode)
            {
                var artistsJson = await artistsResponse.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(artistsJson))
                {
                    artists = JsonSerializer.Deserialize<List<ArtistModel>>(artistsJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }

            // Populăm ArtistName pentru fiecare operă
            foreach (var artwork in artworks)
            {
                var artist = artists.Find(a => a.Id == artwork.ArtistId);
                artwork.ArtistName = artist?.Name ?? "Unknown";
            }

            // Creăm modelul pentru view
            var model = new ArtworkFilterModel
            {
                SearchTitle = searchTitle,
                SelectedType = type ?? "All",
                SelectedArtistId = artistId,
                MaxPrice = maxPrice,
                Artists = artists,
                Artworks = artworks
            };

            return View(model);
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