using Microsoft.AspNetCore.Mvc;
using GalleryFrontend.Models;
using System.Net.Http;
using System.Text.Json;

namespace GalleryFrontend.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ArtistsController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7000/");
        }

        public async Task<IActionResult> VisitorIndex(string name = null)
        {
            string url = string.IsNullOrWhiteSpace(name)
                ? "artists/"
                : $"artists/searchByName?name={Uri.EscapeDataString(name)}";

            var res = await _httpClient.GetAsync(url);
            if (!res.IsSuccessStatusCode) return View(new List<ArtistModel>());

            var json = await res.Content.ReadAsStringAsync();
            var artists = JsonSerializer.Deserialize<List<ArtistModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<ArtistModel>();

            return View("IndexVisitor", artists);
        }

        public async Task<IActionResult> ViewPhoto(int id)
        {
            var res = await _httpClient.GetAsync($"artists/photo/{id}");
            if (!res.IsSuccessStatusCode) return NotFound();

            var photoUrl = (await res.Content.ReadAsStringAsync()).Trim('"');
            return View(model: photoUrl);
        }
    }
}