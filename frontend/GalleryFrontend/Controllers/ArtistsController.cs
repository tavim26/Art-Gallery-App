using Microsoft.AspNetCore.Mvc;
using GalleryFrontend.Models;
using System.Net.Http;
using System.Text.Json;

namespace GalleryFrontend.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ArtistsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7000/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("artists/");
            var json = await response.Content.ReadAsStringAsync();
            var artists = JsonSerializer.Deserialize<List<ArtistModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View("ArtistsIndex");
        }


        public async Task<IActionResult> ViewPhoto(int id)
        {
            var response = await _httpClient.GetAsync($"artists/photo/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var photoUrl = await response.Content.ReadAsStringAsync();
            photoUrl = photoUrl.Trim('"'); // eliminăm ghilimelele JSON

            return View(model: photoUrl);
        }

    }
}